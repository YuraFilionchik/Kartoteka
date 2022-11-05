
using DBFprog1.Authorize;
using Ionic.Zip;
using logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace DBFprog1
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        #region ------------Переменные-------------
        private string DB_name = string.Empty;
        public const string DB_NEW = "newDB.dbf"; //новое имя базы данных после конвертации
        public const string DB_NEW2 = "newDB.fpt";
        public const string DB_name_temp = "saving.dbf";
        public const string DB_name_temp2 = "saving.fpt";
        public string CurrentDir = Directory.GetCurrentDirectory();
        public string CurrentUser = string.Empty;     //текущий польщователь
        public DataTable CurrentDT = null;
        private string BackUpDir = null; //папка для записи бекапов
        private string HistoryFile = "history.txt"; //файл записи изменений в БД
        private static string patternFullTrakt = @"[A-Z,А-Я]{1,4}\d{2,4}\w{0,2}-(1|4|16|64|128)[s,S]\d{1,2}";//-\d{1,2}-\d{1,2}";
        private static string patternLine = @"[A-Z,А-Я]{1,4}\d{2,4}\w{0,2}-(1|4|16|64|128)[s,S]\d{1,2}";
        private static string patternNode = @"\(\d{5}\w{0,2}\)";
        private TextBox tb;
        private string LastFile = string.Empty;    //последний открытый файл БД
        private int StartRowIndex = 0;
        public string SearchWord = string.Empty;
        private WorkDBF Wdbf = null;
        private bool isAdmin = false;
        private bool NeedReSave = false;
        private bool flagResave = false;//для определения идет конвертация файла или пересохранение
        private delegate void AdminDelegate();
        private event AdminDelegate AdminSet;
        public bool VFPOLEDB_installed = false;
        private List<LinesStat> Lines = new List<LinesStat>();
        private ResultsForm ResultForm;
        public bool IsAdmin
        {

            get
            {

                return isAdmin;
            }
            set
            {

                isAdmin = value;
                if (AdminSet != null) AdminSet();
            }
        }
        public IniFile Cfg;
        public string ConfigFile = Directory.GetCurrentDirectory() + "\\config.ini";
        #region Columns Info
        public List<string> HiddenColumns = new List<string>()
        {
            "id",
            "pg",
            "bg",
            "tg",
			//"zam",
			"sys",
            "trassa",
            "kto"
        };

        public struct ColumnFormat
        {
            public string SourceName;
            public string DisplayName;
            public bool IsVisible;

        };
        public List<ColumnFormat> ColsFormat = new List<ColumnFormat>()
        {
            new ColumnFormat{SourceName="nomer",DisplayName="Система", IsVisible=true },
            new ColumnFormat{SourceName="modul",DisplayName="ТГ(ТП)", IsVisible=true },
            new ColumnFormat{SourceName="port",DisplayName="ВГ(ВП)", IsVisible=true },
            new ColumnFormat{SourceName="kabel",DisplayName="ПГ(ПП)", IsVisible=true },
            new ColumnFormat{SourceName="psl_gp",DisplayName="Направление", IsVisible=true },
            new ColumnFormat{SourceName="zam",DisplayName="Распоряжение", IsVisible=true },
        };
        DataGridViewCellStyle HeaderStyle = new DataGridViewCellStyle();
        DataGridViewCellStyle CellStyle = new DataGridViewCellStyle();

        #endregion
        #region old cell value
        public struct cellvalues
        {
            public int columnIndex;
            public int rowIndex;
            public string oldCellValue;
            public string newValue;

        }
        #endregion
        public struct deletedRowsStruct
        {
            public int id;
            public int Index;
            public String Row;
        }
        public List<int> editedRows = new List<int>();
        public List<deletedRowsStruct> DeletedRows = new List<deletedRowsStruct>();
        public List<cellvalues> editedCells = new List<cellvalues>();
        public string OldCellValue;


        #endregion
        public MainForm()
        {
            InitializeComponent();
            #region cellstyle
            CellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            CellStyle.BackColor = System.Drawing.Color.Black;
            CellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            CellStyle.ForeColor = System.Drawing.Color.Yellow;
            CellStyle.SelectionBackColor = System.Drawing.Color.Indigo;
            CellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            CellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            #endregion

            #region headerstyle
            HeaderStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            HeaderStyle.BackColor = Color.Black;
            HeaderStyle.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(204)));
            HeaderStyle.ForeColor = Color.Yellow;
            HeaderStyle.SelectionBackColor = SystemColors.Highlight;
            HeaderStyle.SelectionForeColor = SystemColors.HighlightText;
            HeaderStyle.WrapMode = DataGridViewTriState.True;
            #endregion

            LoadConfig(ConfigFile);
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            управлениеПаролямиToolStripMenuItem.Enabled = false;

            #region Events
            AdminSet += AdminChanged;
            dataGridView1.DataSourceChanged += DGV1dataSourceChanged;
            //dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellValueChanged += dataGridView1_CellEndEdit;
            dataGridView1.SelectionChanged += dataGridView1_selection;
            //cbFontSize.SelectedIndexChanged+=Fontchanged;
            cbFontSize.SelectedIndexChanged += Fontchanged;
            cbFontSize.TextChanged += Fontchanged;
            FormClosing += formclosing;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            KeyDown += MainForm_KeyDown;
            dataGridView1.UserDeletingRow += RemovingRows;
            Shown += MainForm_Shown;
            cbTrakts.SelectedIndexChanged += new EventHandler(cbTrakts_SelectedIndexChanged);
            cbTrakts.KeyDown += new KeyEventHandler(cbTrakts_KeyDown);
            #endregion

            HideColumns(HiddenColumns);


        }

        #region ------------Обработчик нажатий клавиш -----------

        void MainForm_Shown(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(LastFile) && File.Exists(LastFile)) OpenFile(LastFile);
        }

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.Shift && e.KeyCode == Keys.Z) IsAdmin = !IsAdmin;
            if (dataGridView1.SelectedRows.Count < 1) return;
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            switch (e.KeyCode)
            {
                //                case Keys.Enter:
                //            		if(tb==null||!tb.Focus())btF6.PerformClick();
                //            		
                //                    break;
                case Keys.F1:
                    if (listBox1.Focused)
                    {
                        dataGridView1.FirstDisplayedCell.Selected = true;
                        dataGridView1.Focus();
                    }
                    else
                        listBox1.Focus();
                    break;
                case Keys.F2:
                    btF2.PerformClick();
                    break;
                case Keys.F3:
                    bF3.PerformClick();
                    break;
                case Keys.F4:
                    //                                        (new Thread(delegate()
                    //            {
                    //       
                    //                                	Invoke(new Action<bool>(l => toolStripProgressBar1.Visible = l), true);
                    //                                	
                    //               foreach (DataGridViewRow row in dataGridView1.Rows)
                    //               {
                    //               	Invoke(new Action<int>(l => toolStripProgressBar1.Value = l), (int)(100 * (row.Index + 1) /dataGridView1.Rows.Count));
                    //				Invoke(new Action<string>(l => toolStripStatusLabel2.Text = l + "%"), (100 * (row.Index + 1) / dataGridView1.Rows.Count).ToString());	
                    //					Invoke(new Action<int>(l => dataGridView1.FirstDisplayedScrollingRowIndex = l), row.Index);
                    //                    	foreach(DataGridViewCell cell in row.Cells)
                    //                    	{
                    //                    		if(cell.Visible)
                    //                    		{
                    //                    			if(String.IsNullOrWhiteSpace(cell.Value.ToString()))
                    //                    			{
                    //                    				cell.Value="";
                    //                    				               			}
                    //                    		}
                    //                    	}
                    //                    }
                    //            }
                    //            )).Start();

                    break;

                case Keys.F5:
                    bF5.PerformClick();
                    break;

                case Keys.F6:
                    btF6.PerformClick();
                    break;
                case Keys.F7:
                    btF7.PerformClick();
                    break;

            }

        }
        #endregion

        #region ----Обработчики событий---------
        //*********************select system***********************

        //вывод результата поиска по трактам
        void cbTrakts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LinesStat line = Lines.Find(x => x.lineName == cbTrakts.SelectedItem.ToString());
            if (line == null) return;
            ResultForm = new ResultsForm(line);
            ResultForm.Show();
        }
        //custom text search
        void cbTrakts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            LinesStat line = FindLine(cbTrakts.Text);
            if (line == null) return;
            ResultForm = new ResultsForm(line);
            ResultForm.Show();
        }

        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if ((listBox1.SelectedItem != null) && listBox1.SelectedItem.ToString() == dataGridView1["nomer", i].Value.ToString().Trim())
                    dataGridView1.FirstDisplayedScrollingRowIndex = i;
            }
        }

        void dataGridView1_selection(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0 && dataGridView1.Columns.Contains("id"))
                toolStripStatusLabel1.Text = dataGridView1.SelectedRows[0].Cells["id"].Value.ToString();
        }

        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            OldCellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e == null || OldCellValue == null) return;
            //проверка изменилась ли содержимое ячейки
            if (OldCellValue != dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            {
                if (!editedRows.Contains(e.RowIndex)) editedRows.Add(e.RowIndex);
                if (!editedCells.Any(x => x.rowIndex == e.RowIndex && x.columnIndex == e.ColumnIndex))//если такой ячейки нет в списке измененных
                    editedCells.Add(new cellvalues
                    {
                        columnIndex = e.ColumnIndex,
                        rowIndex = e.RowIndex,
                        oldCellValue = OldCellValue,
                        newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()
                    });
                else //если эта ячейка уже редактировалась
                {
                    editedCells.Remove(editedCells.First(x => x.columnIndex == e.ColumnIndex && x.rowIndex == e.RowIndex));
                    editedCells.Add(new cellvalues
                    {
                        columnIndex = e.ColumnIndex,
                        rowIndex = e.RowIndex,
                        oldCellValue = OldCellValue,
                        newValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()
                    });
                }
                setStatusStripChanged(isDataChanged());
            }
        }

        void RemovingRows(object sender, DataGridViewRowCancelEventArgs e)
        {
            try

            {
                int Id = int.Parse(e.Row.Cells["id"].Value.ToString());

                if (!DeletedRows.Exists(x => x.id == Id))
                {

                    DeletedRows.Add(new deletedRowsStruct
                    {
                        id = Id,
                        Index = e.Row.Index,
                        Row = "Система: " + e.Row.Cells["nomer"].Value +
                         "; ТГ(ТП): " + e.Row.Cells["modul"].Value +
                         "; ВГ(ВП): " + e.Row.Cells["port"].Value +
                         "; ПГ(ПП): " + e.Row.Cells["kabel"].Value +
                         "; Направление: " + e.Row.Cells["psl_gp"].Value +
                                        "Распоряжение: " + e.Row.Cells["zam"].Value
                    });
                }
            }
            catch (Exception exc)
            {
                Writelog.WriteLog(exc.Message);
                MessageBox.Show(exc.Message, "removing rows");
            }
        }
        // Конец конвертации файла
        void Wdbf_MyEvent()
        {
            if (flagResave)
            {

                flagResave = false;
                File.Delete(DB_name);
                File.Delete(DB_name.Substring(0, DB_name.Length - 3) + "fpt");
                File.Move(Path.Combine(CurrentDir, "\\", DB_name_temp), DB_name);
                File.Move(Path.Combine(CurrentDir, "\\", DB_name_temp2), DB_name.Substring(0, DB_name.Length - 3) + "fpt");
            }
            else DB_name = CurrentDir + "\\" + DB_NEW;
            if (!File.Exists(DB_name)) return;
            Invoke(new Action<DataTable>(l => CurrentDT = l), Wdbf.GetAll(DB_name));
            if (dataGridView1.InvokeRequired) Invoke(new Action<DataTable>(l => dataGridView1.DataSource = l), CurrentDT);
            if (InvokeRequired) Invoke(new Action<string>(l => Text = l), DB_name);
            Invoke(new Action<string>(l => toolStripStatusLabel1.Text = l), "Готово.");
            Invoke(new Action<bool>(l => toolStripProgressBar1.Visible = l), false);
            Invoke(new Action<bool>(l => файлToolStripMenuItem.Enabled = l), true);
            Invoke(new Action<bool>(l => коToolStripMenuItem.Enabled = l), false);
            Cfg.Write("settings", "lastfile", DB_name);
            if (InvokeRequired) Invoke(new Action<string>(l => toolStripStatusLabel2.Text = l), "");
            if (InvokeRequired) Invoke(new Action<string>(l => toolStripStatusLabel1.Text = l), "Сохранено.");
            editedCells.Clear();
            editedRows.Clear();
            DeletedRows.Clear();

        }

        void formclosing(object sender, FormClosingEventArgs e)
        {

            if (!VFPOLEDB_installed) return;
            Cfg.DeleteSection("MENU");
            foreach (var item in listBox1.Items)
            {
                Cfg.Write("MENU", item.ToString(), "");
            }

            Cfg.Write("settings", "Column1Size", dataGridView1.Columns["nomer"].Width.ToString());
            Cfg.Write("settings", "Column2Size", dataGridView1.Columns["modul"].Width.ToString());
            Cfg.Write("settings", "Column3Size", dataGridView1.Columns["port"].Width.ToString());
            Cfg.Write("settings", "Column4Size", dataGridView1.Columns["kabel"].Width.ToString());
            Cfg.Write("settings", "Column5Size", dataGridView1.Columns["psl_gp"].Width.ToString());
            Cfg.Write("settings", "Column6Size", dataGridView1.Columns["zam"].Width.ToString());

            DialogResult mb = DialogResult.None;
            if (isDataChanged()) mb = MessageBox.Show("Сохранить изменения перед выходом", "Внимание.",
                                            MessageBoxButtons.YesNoCancel,
                                            MessageBoxIcon.Question,
                                            MessageBoxDefaultButton.Button1
                                           );
            if (mb == DialogResult.Yes && IsAdmin) Save();
            e.Cancel |= mb == DialogResult.Cancel;


        }

        void DGV1dataSourceChanged(object sender, EventArgs e)
        {
            HideColumns(HiddenColumns);
        }

        void Fontchanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cbFontSize.Text) || cbFontSize.Text.Length < 2) return;

            try
            {
                int size = int.Parse(cbFontSize.Text.ToString());
                SetFontSize(size);
                Cfg.Write("settings", "fontsize", size.ToString());
                изменитьШрифтToolStripMenuItem.HideDropDown();
                правкаToolStripMenuItem.HideDropDown();
            }
            catch (Exception ex)
            {
                Writelog.WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "FontChanged");
            }

        }

        void AdminChanged()
        {
            if (IsAdmin)
            {
                BackUpDB(true);
                добавитьСтрокуToolStripMenuItem.Enabled = true;
                удалитьСтрокуToolStripMenuItem.Enabled = true;
                toolStripStatusLabel3.Text = "РЕЖИМ РЕДАКТИРОВАНИЯ";
                dataGridView1.ReadOnly = false;
                коToolStripMenuItem.Enabled = true;
                загрузитьОглавлениеИзФайлаToolStripMenuItem.Enabled = true;
                переименоватьToolStripMenuItem.Enabled = true;
                // backupToolStripMenuItem.Enabled=true;
                выбратьПапкуДляBackupToolStripMenuItem.Enabled = true;
                toolStripMenuItem1.Enabled = true;
                Writelog.WriteLog(CurrentUser + ":::" + "переход в режим редактирования");
            }
            else
            {
                добавитьСтрокуToolStripMenuItem.Enabled = false;
                удалитьСтрокуToolStripMenuItem.Enabled = false;
                коToolStripMenuItem.Enabled = true;
                dataGridView1.ReadOnly = true;
                toolStripStatusLabel3.Text = "РЕЖИМ ПРОСМОТРА";
                управлениеПаролямиToolStripMenuItem.Enabled = false;
                загрузитьОглавлениеИзФайлаToolStripMenuItem.Enabled = false;
                переименоватьToolStripMenuItem.Enabled = false;
                toolStripMenuItem1.Enabled = false;
                // backupToolStripMenuItem.Enabled=false;
                выбратьПапкуДляBackupToolStripMenuItem.Enabled = false;
                Writelog.WriteLog(CurrentUser + ":::" + "переход в режим просмотра");
                CurrentUser = "null";
                if (isDataChanged()) Save();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string key in Cfg.GetAllKeys("MENU"))
            {
                listBox1.Items.Add(key);
            }

        }

        private void bF3_Click(object sender, EventArgs e)
        {
            InputForm findform = new InputForm();
            findform.textBox1.Text = SearchWord;
            findform.ShowDialog();
        }

        private void bF5_Click(object sender, EventArgs e)
        {

            TrassaForm tf = new TrassaForm();
            if (dataGridView1.SelectedRows[0].Cells["psl_gp"].Value != null)
                tf.Text = "Трасса " + dataGridView1.SelectedRows[0].Cells["psl_gp"].Value.ToString();
            else tf.Text = "Трасса";
            // int id = int.Parse(dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
            tf.tbTrassa.Text = dataGridView1.SelectedRows[0].Cells["trassa"].Value.ToString();

            tf.ShowDialog();
        }

        private void bF6_Click(object sender, EventArgs e)
        {
            TrassaForm tf = new TrassaForm();
            if (dataGridView1.SelectedRows[0].Cells["psl_gp"].Value != null)
                tf.Text = "Карточка " + dataGridView1.SelectedRows[0].Cells["psl_gp"].Value.ToString();
            else tf.Text = "Карточка";
            // int id = int.Parse(dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
            tf.tbTrassa.Text = dataGridView1.SelectedRows[0].Cells["kto"].Value.ToString();
            tf.ShowDialog();
        }

        void BtF2Click(object sender, EventArgs e)
        {
            Search(SearchWord, ++StartRowIndex);
        }

        void BtF7Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(SearchWord)) return;
            List<string> cols = new List<string>();
            string[] words = SearchWord.Split(' ');
            //определяем список столбцов с найденным словом
            foreach (DataGridViewCell cell in dataGridView1.Rows[StartRowIndex].Cells)
            {
                if (words.Count() == 1 && cell.Value.ToString().ToLower().Contains(SearchWord.ToLower()))
                    cols.Add(cell.OwningColumn.Name.ToString());
                if (words.Count() == 2
                   && cell.Value.ToString().ToLower().Contains(words[0].ToLower())
                   && cell.Value.ToString().ToLower().Contains(words[1].ToLower()))
                    cols.Add(cell.OwningColumn.Name.ToString());
                if (words.Count() == 3
                   && cell.Value.ToString().ToLower().Contains(words[0].ToLower())
                   && cell.Value.ToString().ToLower().Contains(words[1].ToLower())
                    && cell.Value.ToString().ToLower().Contains(words[2].ToLower()))
                    cols.Add(cell.OwningColumn.Name.ToString());
            }

            Result resultForm = new Result();

            #region trassa
            if (cols.Contains("trassa") || cols.Contains("kto"))
            {
                string kart = dataGridView1.Rows[StartRowIndex].Cells["kto"].Value.ToString();
                string trassa = dataGridView1.Rows[StartRowIndex].Cells["trassa"].Value.ToString();
                string kartStr = string.Empty;
                var strokiKart = kart.Split('\n');
                var strokiTras = trassa.Split('\n');
                string[] chanals = GetChanals(strokiTras);

                foreach (string s in chanals)
                {
                    if (words.Count() == 1 && s != null && s.ToLower().Contains(SearchWord.ToLower()))
                    {
                        resultForm.tbTrass.Text += s.TrimStart();

                    }
                    if (words.Count() == 2 && s != null
                       && s.ToLower().Contains(words[0].ToLower())
                       && s.ToLower().Contains(words[1].ToLower()))
                    {
                        resultForm.tbTrass.Text += s.TrimStart();

                    }
                }
                #region поиск строки в карточке с разыскиваемыми словами
                for (int i = 0; i < strokiKart.Length; i++)
                {
                    if (strokiKart.Length > 0 && words.Count() == 1 && strokiKart[i].ToLower().Contains(SearchWord.ToLower()))
                    {
                        if (i == 0 && strokiKart.Length > 2) { kartStr = strokiKart[i] + strokiKart[i + 1] + strokiKart[i + 2]; break; }
                        if (i == 0 && strokiKart.Length < 2) { kartStr = strokiKart[i]; break; }
                        if (i > 0 && i <= strokiKart.Length - 3) { kartStr = strokiKart[i] + strokiKart[i + 1] + strokiKart[i + 2]; break; }
                        if (i > 0 && i > strokiKart.Length - 3) { kartStr = strokiKart[i]; break; }
                    }
                    else if (strokiKart.Length > 0 && words.Count() == 2
                            && strokiKart[i].ToLower().Contains(words[0].ToLower())
                            && strokiKart[i].ToLower().Contains(words[1].ToLower()))
                    {
                        if (i == 0 && strokiKart.Length > 2) { kartStr = strokiKart[i] + strokiKart[i + 1] + strokiKart[i + 2]; break; }
                        if (i == 0 && strokiKart.Length < 2) { kartStr = strokiKart[i]; break; }
                        if (i > 0 && i <= strokiKart.Length - 3) { kartStr = strokiKart[i] + strokiKart[i + 1] + strokiKart[i + 2]; break; }
                        if (i > 0 && i > strokiKart.Length - 3) { kartStr = strokiKart[i]; break; }

                    }
                    else if (strokiKart.Length > 0 && words.Count() == 3
                            && strokiKart[i].ToLower().Contains(words[0].ToLower())
                            && strokiKart[i].ToLower().Contains(words[1].ToLower())
                            && strokiKart[i].ToLower().Contains(words[2].ToLower()))
                    {
                        if (i == 0 && strokiKart.Length > 2) { kartStr = strokiKart[i] + strokiKart[i + 1] + strokiKart[i + 2]; break; }
                        if (i == 0 && strokiKart.Length < 2) { kartStr = strokiKart[i]; break; }
                        if (i > 0 && i <= strokiKart.Length - 3) { kartStr = strokiKart[i] + strokiKart[i + 1] + strokiKart[i + 2]; break; }
                        if (i > 0 && i > strokiKart.Length - 3) { kartStr = strokiKart[i]; break; }
                    }

                }
                #endregion
                if (String.IsNullOrWhiteSpace(kartStr)) kartStr = strokiKart[0];
                resultForm.tbKart.Text = kartStr;
                MatchCollection wordKart = null;
                if (words.Count() == 1)
                {
                    wordKart = Regex.Matches(resultForm.tbKart.Text.ToLower(), SearchWord.ToLower());
                }
                if (words.Count() == 2)
                {
                    wordKart = Regex.Matches(resultForm.tbKart.Text.ToLower(), words[0].ToLower() + "|" + words[1].ToLower());

                }
                if (words.Count() == 3)
                {
                    wordKart = Regex.Matches(resultForm.tbKart.Text.ToLower(), words[0].ToLower() + "|" + words[1].ToLower() + "|" + words[2].ToLower());

                }

                if (wordKart != null)//выделение слов в Карточке
                    foreach (Match word in wordKart)
                    {
                        resultForm.tbKart.SelectionStart = word.Index;
                        resultForm.tbKart.SelectionLength = word.Length;
                        resultForm.tbKart.SelectionColor = Color.Yellow;
                        resultForm.tbKart.SelectionBackColor = Color.MediumTurquoise;
                    }
            }
            #endregion

            #region otherFields
            foreach (string col in cols)
            {
                if (col != "kto" && col != "trassa")
                {
                    resultForm.tbTrass.Text += "\n\nРезультаты в колонке " + col + ":\n";
                    resultForm.tbTrass.Text += dataGridView1.Rows[StartRowIndex].Cells[col].Value.ToString();
                }
            }
            #endregion

            resultForm.Text += " по слову - " + SearchWord;
            //выделение слов в Трассе
            MatchCollection wordTrass = null;
            if (words.Count() == 1)
            {
                wordTrass = Regex.Matches(resultForm.tbTrass.Text.ToLower(), SearchWord.ToLower());
            }
            if (words.Count() == 2)
            {
                string pattern = words[0].ToLower() + "|" + words[1].ToLower();
                wordTrass = Regex.Matches(resultForm.tbTrass.Text.ToLower(), pattern);

            }
            if (words.Count() == 3)
            {
                wordTrass = Regex.Matches(resultForm.tbTrass.Text.ToLower(), words[0].ToLower() + "|" + words[1].ToLower() + "|" + words[2].ToLower());

            }

            if (wordTrass != null)
                foreach (Match word in wordTrass)
                {
                    resultForm.tbTrass.SelectionStart = word.Index;
                    resultForm.tbTrass.SelectionLength = word.Length;
                    resultForm.tbTrass.SelectionColor = Color.Yellow;
                    resultForm.tbTrass.SelectionBackColor = Color.MediumTurquoise;
                }
            resultForm.Show();
        }

        void btTraktClick(object sender, EventArgs e)
        {
            FindAllLines();
        }
        #endregion

        #region --------menu ---------
        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB_name = openFileDialog1.FileName;
            OpenFile(DB_name);
            Cfg.Write("settings", "lastfile", DB_name);
        }

        private void коToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(DB_name)) return;
            if (Wdbf == null) return;
            if (dataGridView1.Columns.Contains("id")) return;

            var DRes = MessageBox.Show("Текущая база будет сохранена в новый файл " + DB_NEW,
                "Продолжить операцию?",
                MessageBoxButtons.YesNo);
            var DT = Wdbf.GetAll(DB_name.Split('\\').Last());
            if (DRes == DialogResult.Yes) //Диалоговое окно ОК
            {
                if (File.Exists(CurrentDir + "\\" + DB_NEW)) File.Delete(CurrentDir + "\\" + DB_NEW);
                Wdbf.CreateDBF(DB_name, DB_NEW);
                if (File.Exists(CurrentDir + "\\" + DB_NEW))
                {
                    файлToolStripMenuItem.Enabled = false;

                    (new Thread(() => Wdbf.WriteDataToFile(DB_NEW, DT)
                )).Start();
                    Writelog.WriteLog(CurrentUser + ":::" + "преобразование файла " + DB_name + " в " + DB_NEW);
                    toolStripStatusLabel1.Text = "Сохранение в новый файл";
                }
                else
                {
                    MessageBox.Show("file " + CurrentDir + "\\" + DB_NEW + " not found");
                }
            }
        }

        void РежимРедактированияToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!dataGridView1.Columns.Contains("id"))
            {
                MessageBox.Show("Данный файл недоступен для редактирования! Необходимо его преобразовать.");
                return;
            }
            Authorize.Login login = new Authorize.Login();
            login.ShowDialog();
        }

        void РежимПросмотраToolStripMenuItemClick(object sender, EventArgs e)
        {
            IsAdmin = false;
        }

        void ВыходToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void перечитатьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(DB_name)) return;
            if (Wdbf == null) return;
            CurrentDT = Wdbf.GetAll(DB_name);
            dataGridView1.DataSource = CurrentDT;
            Text = DB_name;
            toolStripStatusLabel1.Text = "file readed: " + DB_name.Split('\\').Last();
            toolStripStatusLabel2.Text = "";
            EditDGV();
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "openfile dialog");
                throw;
            }
        }
        void СохранитьToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (IsAdmin)
            {
                Save();
                Writelog.WriteLog(CurrentUser + ":::" + "Сохранение изменений в базе. См. файл history.txt");
            }
            else MessageBox.Show("В режиме просмотра сохранять изменения нельзя.");
        }

        private void управлениеПаролямиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PassManager passManager = new PassManager();
            passManager.ShowDialog();
            Writelog.WriteLog(CurrentUser + ":::" + "вход в управление паролями");
        }


        #endregion

        #region--------context MENU--------
        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tb = new TextBox();
            tb.Location = MousePosition;
            tb.KeyDown += tb_KeyDown;
            tb.LostFocus += tb_LostFocus;
            Controls.Add(tb);
            tb.BringToFront();
            tb.Size = new Size(150, 60);
            tb.Font = new Font(FontFamily.GenericSansSerif, 13);
            tb.Focus();
            tb.Text = listBox1.SelectedItem.ToString();
            tb.Show();
        }

        void УдалитьСтрокуToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 1)
                {
                    var row = dataGridView1.SelectedRows[0]; //берем выделенную строку Datagridview
                    int Id = int.Parse(row.Cells["id"].Value.ToString()); //определяем id строки
                                                                          //проверяем чтобы не было строки с таким id в списке удаленных
                    if (!DeletedRows.Exists(x => x.id == Id))
                    {
                        DeletedRows.Add(new deletedRowsStruct
                        {
                            id = Id,
                            Index = row.Index,
                            Row = "Система: " + row.Cells["nomer"].Value +
                             "; ТГ(ТП): " + row.Cells["modul"].Value +
                             "; ВГ(ВП): " + row.Cells["port"].Value +
                             "; ПГ(ПП): " + row.Cells["kabel"].Value +
                             "; Направление: " + row.Cells["psl_gp"].Value +
                             "; Распоряжение: " + row.Cells["zam"].Value
                        });
                    }
                    CurrentDT.Rows[row.Index].Delete();
                    CurrentDT.AcceptChanges();
                    dataGridView1.DataSource = CurrentDT;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "удалить строку");
            }
        }

        void ДобавитьСтрокуToolStripMenuItemClick(object sender, EventArgs e)
        {
            try
            {
                //определяем id строки в БД
                int idSelected = dataGridView1.SelectedRows[0].Index;
                int id = int.Parse(dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
                for (int i = CurrentDT.Rows.Count - 1; i > idSelected; i--)
                {
                    CurrentDT.Rows[i]["id"] = int.Parse(CurrentDT.Rows[i]["id"].ToString()) + 1;
                }

                DataRow dr = CurrentDT.NewRow();
                dr["id"] = id + 1;
                DataRow cdt = CurrentDT.Select("id=" + id.ToString()).FirstOrDefault();
                if (cdt == null) return;
                CurrentDT.Rows.InsertAt(dr, CurrentDT.Rows.IndexOf(cdt) + 1);
                dataGridView1.DataSource = CurrentDT;
                var delRows = DeletedRows.FindAll(x => x.id == id + 1);
                if (delRows.Count != 0) DeletedRows.Remove(DeletedRows.First(x => x.id == id + 1));
                NeedReSave = true;
                CurrentDT.AcceptChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "AddRow");
            }
        }

        void tb_LostFocus(object sender, EventArgs e)
        {
            tb.Dispose();
        }

        //ввод нового имени листбокса
        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrWhiteSpace(tb.Text) && (listBox1.SelectedItem != null))
                {
                    Writelog.WriteLog(CurrentUser + ":::" + "переименование меню -" + listBox1.SelectedItem.ToString());
                    listBox1.Items[listBox1.SelectedIndex] = tb.Text;
                    Writelog.WriteLog(CurrentUser + ":::" + "новое имя -" + tb.Text);
                }
                tb.Dispose();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null) Writelog.WriteLog(CurrentUser + ":::" + "удаление пункта меню " + listBox1.SelectedItem.ToString());
            if (listBox1.SelectedItem != null) listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void загрузитьОглавлениеИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region ----load list of systems
            Writelog.WriteLog(CurrentUser + ":::" + "загрузка пунктов меню и файла БД");
            foreach (DataRow row in CurrentDT.Rows)
            {
                if (!String.IsNullOrWhiteSpace(row["nomer"].ToString()))
                {
                    int i = listBox1.Items.Add(row["nomer"].ToString().Trim());
                    Cfg.Write("MENU", row["nomer"].ToString().Trim(), "");
                }
            }
            #endregion
        }

        public bool BackUpDB(bool silent)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(BackUpDir)) //запрос на выбор папки Бэкапа
                {
                    if (silent) return false;
                    FolderBrowserDialog folder = new FolderBrowserDialog();
                    folder.ShowDialog();
                    if (String.IsNullOrWhiteSpace(folder.SelectedPath)) return false;
                    BackUpDir = folder.SelectedPath;
                    Cfg.Write("settings", "backup", BackUpDir);
                    Writelog.WriteLog(CurrentUser + ":::" + "выбрана папка для backup " + BackUpDir);
                }

                string backName = "Kartoteka_" + DateTime.Now.ToString("g").Replace(':', '-') + ".zip"; //имя архива базы
                string DB_name2 = DB_name.Substring(0, DB_name.Length - 3) + "fpt"; //имя файла БД-2
                string name = DB_name.Split('\\').Last();
                string name2 = DB_name2.Split('\\').Last();
                if (!File.Exists(DB_name)) //БД не найдена
                {
                    if (silent) return false;

                    MessageBox.Show("File " + DB_name + " not found");
                    return false;
                }
                if (File.Exists(BackUpDir + "\\" + backName)) //уже есть такое имя
                {
                    if (silent) return false;

                    MessageBox.Show("File " + backName + " already exists"); //пропуск
                    return false;
                }

                #region backUp

                if (Directory.Exists(CurrentDir + "\\tmp"))
                {
                    DirectorySecurity ds = new DirectorySecurity(CurrentDir + "\\tmp", AccessControlSections.All);
                    Directory.SetAccessControl(CurrentDir + "\\tmp", ds);
                    File.SetAttributes(CurrentDir + "\\tmp", FileAttributes.Normal);
                    Directory.Delete(CurrentDir + "\\tmp", true);
                }
                var tmpdir = Directory.CreateDirectory(CurrentDir + "\\tmp");
                File.Copy(DB_name, tmpdir.FullName + "\\" + name);
                File.Copy(DB_name2, tmpdir.FullName + "\\" + name2);
                ZipFile Z1 = new ZipFile();
                Z1.AddDirectory(tmpdir.FullName);
                Z1.Save(BackUpDir + "\\" + backName);
                Directory.Delete(tmpdir.FullName, true);
                toolStripStatusLabel4.Text = "Файл базы данных сохранен " + BackUpDir + "\\" + backName;
                Writelog.WriteLog(CurrentUser + ":::" + "сделан backup в файл " + BackUpDir + "\\" + backName);
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "backup error");
                Writelog.WriteLog("backUp error:::" + ex.Message);
                return false;
            }
        }

        void BackupToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (BackUpDB(false)) MessageBox.Show("Бэкап успешно выполнен!", "Результат бэкапа");
            else MessageBox.Show("Бэкап не выполнен!", "Результат бэкапа");
        }

        void ВыбратьПапкуДляBackupToolStripMenuItemClick(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.ShowDialog();
            if (String.IsNullOrWhiteSpace(folder.SelectedPath)) return;
            BackUpDir = folder.SelectedPath;
            Cfg.Write("settings", "backup", BackUpDir);
            Writelog.WriteLog(CurrentUser + ":::" + "выбрана папка для backup " + BackUpDir);
        }

        void ОПрограммеToolStripMenuItemClick(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        #endregion

        #region -------Functions--------

        /// <summary>
        /// Соединяет массив строк strokiTras в массив каналов
        /// </summary>
        /// <param name="strokiTras">исходный массив строк</param>
        /// <returns>возвращает массив, поделенный на пронумерованные каналы</returns>
        public string[] GetChanals(string[] strokiTras)
        {
            string[] chanals = new string[strokiTras.Length];
            int m = -1;
            for (int n = 0; n < strokiTras.Length; n++)
            {
                if (!String.IsNullOrWhiteSpace(strokiTras[n]) && char.IsDigit(strokiTras[n].TrimStart()[0]))
                {
                    m++;
                    chanals[m] = strokiTras[n];
                }
                else
                {
                    if (m != -1) chanals[m] += strokiTras[n];
                    else
                    {
                        m++;
                        if (m < chanals.Length) chanals[m] = strokiTras[n];
                    }
                }
            }
            return chanals;
        }

        public bool ColumnIsHide(string colName, List<string> columns)
        {
            foreach (var col in columns)
            {
                if (col == colName)
                    return true;
            }
            return false;
        }

        public void Save()
        {
            try
            {
                //DataTable DT=(DataTable)dataGridView1.DataSource;
                DataTable DT = CurrentDT;
                int i = 0;
                int C = 0;
                if (Wdbf != null && DT != null)
                    if (!NeedReSave)
                    {
                        toolStripProgressBar1.Visible = true;
                        файлToolStripMenuItem.Enabled = false;
                        правкаToolStripMenuItem.Enabled = false;
                        for (int n = 0; n < editedRows.Count; n++)
                        {
                            while (RowsContain(editedRows[n]) > 1)
                            {

                                for (int m = n + 1; m < editedRows.Count; m++)
                                {
                                    if (editedRows[n] == editedRows[m]) editedRows.Remove(editedRows[m]);
                                }
                            }
                        }
                        #region  изменение строк
                        foreach (int N in editedRows)
                        {
                            toolStripProgressBar1.Value = 100 * (i / editedRows.Count);
                            toolStripStatusLabel2.Text = 100 * (i / editedRows.Count) + "%";
                            Wdbf.UpdateRow(dataGridView1.Rows[N], DB_name, GetColumnNames(DT));
                            List<cellvalues> cells = editedCells.FindAll(x => x.rowIndex == N); //все ячейки данной строки N
                            if (cells.Count() < 2)
                                foreach (DataGridViewCell c in dataGridView1.Rows[N].Cells)
                                {
                                    if (c.RowIndex == cells[0].rowIndex && c.ColumnIndex == cells[0].columnIndex)
                                    {
                                        WriteLog(CurrentUser + "::старое значение в столбце " + GetColumnNames(DT)[cells[0].columnIndex] + " - " + cells[0].oldCellValue, HistoryFile);
                                        WriteLog(CurrentUser + "::новое  значение в столбце " + GetColumnNames(DT)[cells[0].columnIndex] + " - " + cells[0].newValue, HistoryFile);
                                    }

                                    else
                                                 if (!String.IsNullOrWhiteSpace(dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString()))
                                        WriteLog(CurrentUser + "::столбец " + GetColumnNames(DT)[c.ColumnIndex] + " - " + dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString(), HistoryFile);
                                }
                            else
                                foreach (DataGridViewCell c in dataGridView1.Rows[N].Cells)
                                {
                                    if (cells.Any(x => x.columnIndex == c.ColumnIndex))
                                    {
                                        WriteLog(CurrentUser + "::старое значение в столбце " + GetColumnNames(DT)[c.ColumnIndex] + " - " + cells.First(x => x.columnIndex == c.ColumnIndex).oldCellValue, HistoryFile);
                                        WriteLog(CurrentUser + "::новое  значение в столбце " + GetColumnNames(DT)[c.ColumnIndex] + " - " + cells.First(x => x.columnIndex == c.ColumnIndex).newValue, HistoryFile);
                                    }

                                    else
                                                 if (!String.IsNullOrWhiteSpace(dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString()))
                                        WriteLog(CurrentUser + "::столбец " + GetColumnNames(DT)[c.ColumnIndex] + " - " + dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString(), HistoryFile);
                                }
                            WriteLog("===========================================================", HistoryFile);

                            i++;
                        }
                        #endregion
                        #region удаление строк
                        foreach (var row in DeletedRows)
                        {
                            C++;
                            WriteLog("Удалена строка №=" + row.Index.ToString() + ":::" +
                                     row.Row, HistoryFile);
                            toolStripProgressBar1.Value = 100 * (C / DeletedRows.Count);
                            toolStripStatusLabel2.Text = 100 * (C / DeletedRows.Count) + "%";
                            Wdbf.DeleteRow(row.id, DB_name);

                        }
                        #endregion
                        toolStripProgressBar1.Visible = false;
                        файлToolStripMenuItem.Enabled = true;
                        правкаToolStripMenuItem.Enabled = true;
                        toolStripStatusLabel2.Text = "";
                        toolStripStatusLabel1.Text = "Сохранено.";
                        editedCells.Clear();
                        editedRows.Clear();
                        DeletedRows.Clear();
                        setStatusStripChanged(isDataChanged());
                        //Writelog.WriteLog(CurrentUser+":::"+"сохранение изменений");
                    }
                    else
                    //перезапись всего файла
                    {
                        #region запись в ЛОГ файл перед пересохранением БД
                        if (editedRows.Count != 0)
                        {

                            for (int n = 0; n < editedRows.Count; n++)
                            {
                                while (RowsContain(editedRows[n]) > 1)
                                {
                                    //поиск дубликатов записей
                                    for (int m = n + 1; m < editedRows.Count; m++)
                                    {
                                        if (editedRows[n] == editedRows[m]) editedRows.Remove(editedRows[m]);
                                    }
                                }
                            }
                            #region  изменение строк
                            foreach (int N in editedRows)
                            {

                                List<cellvalues> cells = editedCells.FindAll(x => x.rowIndex == N); //все ячейки данной строки N
                                if (cells.Count() < 2)
                                    foreach (DataGridViewCell c in dataGridView1.Rows[N].Cells)
                                    {
                                        if (c.RowIndex == cells[0].rowIndex && c.ColumnIndex == cells[0].columnIndex)
                                        {
                                            WriteLog(CurrentUser + "::старое значение в столбце " + GetColumnNames(DT)[cells[0].columnIndex] + " - " + cells[0].oldCellValue, HistoryFile);
                                            WriteLog(CurrentUser + "::новое  значение в столбце " + GetColumnNames(DT)[cells[0].columnIndex] + " - " + cells[0].newValue, HistoryFile);
                                        }

                                        else
                                                     if (!String.IsNullOrWhiteSpace(dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString()))
                                            WriteLog(CurrentUser + "::столбец " + GetColumnNames(DT)[c.ColumnIndex] + " - " + dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString(), HistoryFile);
                                    }
                                else
                                    foreach (DataGridViewCell c in dataGridView1.Rows[N].Cells)
                                    {
                                        if (cells.Any(x => x.columnIndex == c.ColumnIndex))
                                        {
                                            WriteLog(CurrentUser + "::старое значение в столбце " + GetColumnNames(DT)[c.ColumnIndex] + " - " + cells.First(x => x.columnIndex == c.ColumnIndex).oldCellValue, HistoryFile);
                                            WriteLog(CurrentUser + "::новое  значение в столбце " + GetColumnNames(DT)[c.ColumnIndex] + " - " + cells.First(x => x.columnIndex == c.ColumnIndex).newValue, HistoryFile);
                                        }

                                        else
                                                     if (!String.IsNullOrWhiteSpace(dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString()))
                                            WriteLog(CurrentUser + "::столбец " + GetColumnNames(DT)[c.ColumnIndex] + " - " + dataGridView1.Rows[N].Cells[c.ColumnIndex].Value.ToString(), HistoryFile);
                                    }
                                WriteLog("===========================================================", HistoryFile);

                                i++;
                            }
                            #endregion
                            //удаление строк
                            foreach (var row in DeletedRows)
                            {
                                C++;
                                WriteLog("Удалена строка №=" + row.Index.ToString() + ":::" +
                                         row.Row, HistoryFile);

                            }

                            toolStripProgressBar1.Visible = false;
                            файлToolStripMenuItem.Enabled = true;
                            правкаToolStripMenuItem.Enabled = true;

                            editedCells.Clear();
                            editedRows.Clear();
                            DeletedRows.Clear();
                        }
                        #endregion

                        if (File.Exists(CurrentDir + "\\" + DB_name_temp)) File.Delete(CurrentDir + "\\" + DB_name_temp);
                        Wdbf.CreateDBF(DB_name, DB_name_temp);
                        if (File.Exists(CurrentDir + "\\" + DB_name_temp))
                        {
                            файлToolStripMenuItem.Enabled = false;
                            flagResave = true;
                            CurrentDT.AcceptChanges();
                            (new Thread(() => Wdbf.WriteDataToFile(DB_name_temp, CurrentDT)
                            )).Start();
                            NeedReSave = false;
                        }
                    }
            }
            catch (Exception ex)
            {
                Writelog.WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Save");
            }

        }

        private List<string> GetColumnNames(DataTable dt)
        {
            List<string> res = new List<string>();
            try
            {
                foreach (DataColumn column in dt.Columns)
                {
                    res.Add(column.ColumnName);
                }
                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "GetcolumnNames");
                Writelog.WriteLog("GetColumnName :::::" + "res=" + res);
                throw;
            }

        }

        public void LoadConfig(string File)
        {
            toolStripStatusLabel4.Text = "загрузка names.ini";
            toolStripStatusLabel4.Text = "загрузка config.ini";
            Cfg = new IniFile(ConfigFile);
            if (Cfg.KeyExists("lastfile", "settings")) LastFile = Cfg.ReadINI("settings", "lastfile");
            if (Cfg.KeyExists("fontsize", "settings"))
            {
                cbFontSize.Text = Cfg.ReadINI("settings", "fontsize");
                SetFontSize(int.Parse(Cfg.ReadINI("settings", "fontsize")));
            }
            if (Cfg.KeyExists("backup", "settings")) BackUpDir = Cfg.ReadINI("settings", "backup");
            toolStripStatusLabel4.Text = "";
        }

        public void SetFontSize(int size)
        {
            Font f = new Font(CellStyle.Font.FontFamily, size);
            CellStyle.Font = f;
            dataGridView1.DefaultCellStyle = CellStyle;
        }

        void HideColumns(List<string> columns)
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (ColumnIsHide(dataGridView1.Columns[i].Name, columns))
                {
                    dataGridView1.Columns[i].Visible = false;
                }
            }
        }

        void OpenFile(string name)
        {
            try
            {
                Text = name;
                var AllBytes = File.ReadAllBytes(name);
                //проверка кодировки и установка 866dos 
                if (AllBytes[29].ToString("X") != "65")
                {
                    AllBytes[29] = byte.Parse("101");
                    File.WriteAllBytes(name, AllBytes);
                }
                CurrentDir = Path.GetDirectoryName(name);
                DB_name = name;
                Wdbf = new WorkDBF(CurrentDir);
                Wdbf.MyEvent += Wdbf_MyEvent;
                if (!Wdbf.CheckConnection())
                {
                    MessageBox.Show("Ошибка чтения базы данных. Возможно не установлен драйвер Vfpoledb.");
                    return;
                }

                CurrentDT = Wdbf.GetAll(name);
                dataGridView1.DataSource = CurrentDT;
                EditDGV();
                IsAdmin = false;
                if (dataGridView1.Columns.Contains("id"))
                    коToolStripMenuItem.Enabled = false;
                else
                    коToolStripMenuItem.Enabled = true;
                Writelog.WriteLog(CurrentUser + ":::" + "открыт файл " + DB_name);
                LoadTraks();
            }
            catch (Exception ex)
            {
                Writelog.WriteLog(ex.Message);
                MessageBox.Show(ex.Message, "Ошибка при открытии файла " + name);
            }
        }

        private void EditDGV()
        {
            try
            {
                if (dataGridView1.Columns.Contains("id")) dataGridView1.Sort(dataGridView1.Columns["id"], System.ComponentModel.ListSortDirection.Ascending);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    if (ColsFormat.All(x => x.SourceName != column.Name))
                        continue;
                    var displayName = ColsFormat.FirstOrDefault(x => x.SourceName == column.Name).DisplayName;
                    if (displayName != null)
                    {
                        column.HeaderText = displayName;
                    }
                }
                dataGridView1.DefaultCellStyle = CellStyle;
                //dataGridView1.Columns["nomer"].AutoSizeMode=DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns["nomer"].MinimumWidth = 200;
                if (Cfg.KeyExists("Column1Size", "settings"))
                    dataGridView1.Columns["nomer"].Width = int.Parse(Cfg.ReadINI("settings", "Column1Size"));
                if (Cfg.KeyExists("Column2Size", "settings"))
                    dataGridView1.Columns["modul"].Width = int.Parse(Cfg.ReadINI("settings", "Column2Size"));
                if (Cfg.KeyExists("Column3Size", "settings"))
                    dataGridView1.Columns["port"].Width = int.Parse(Cfg.ReadINI("settings", "Column3Size"));
                if (Cfg.KeyExists("Column4Size", "settings"))
                    dataGridView1.Columns["kabel"].Width = int.Parse(Cfg.ReadINI("settings", "Column4Size"));
                if (Cfg.KeyExists("Column5Size", "settings"))
                    dataGridView1.Columns["psl_gp"].Width = int.Parse(Cfg.ReadINI("settings", "Column5Size"));
                if (Cfg.KeyExists("Column6Size", "settings"))
                    dataGridView1.Columns["zam"].Width = int.Parse(Cfg.ReadINI("settings", "Column6Size"));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "EdidDGV()");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="startRowIndex"></param>
        /// <returns>StartRowIndex with result</returns>
        public int Search(string word, int startRowIndex)
        {
            if (String.IsNullOrWhiteSpace(word))
            {
                toolStripStatusLabel4.Text = "ничего не найдено";
                return -1;
            }
            toolStripStatusLabel4.Text = "поиск по слову: " + word;
            SearchWord = word;
            StartRowIndex = startRowIndex;
            DataGridViewRow row;
            string ColumnName = string.Empty;
            for (int i = startRowIndex; i < dataGridView1.Rows.Count; i++)
            {
                row = dataGridView1.Rows[i];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    ColumnName = dataGridView1.Columns[cell.ColumnIndex].Name;
                    if (ColumnName != "sys" &&
                       ColumnName != "kto" &&
                       ColumnName != "trassa" &&
                       ColumnName != "zam" &&
                       ColumnName != "nomer" &&
                       ColumnName != "psl_gp") continue;
                    //===========SEARCH========================================
                    string[] words = word.Split(' ');
                    if (words.Count() == 1)
                    {
                        if (cell.Value != null && cell.Value.ToString().ToLower().Contains(word.ToLower()))
                        {//если найдено
                            dataGridView1.Rows[row.Index].Selected = true;
                            dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                            StartRowIndex = i;
                            return row.Index;
                        }
                    }
                    else
                    {

                        if (words.Count() == 2 && cell.Value != null)
                        {
                            string[] lines = cell.Value.ToString().Split('\n');
                            foreach (string line in lines)
                            {
                                if (line.ToLower().Contains(words[0].ToLower())
                                   && line.ToLower().Contains(words[1].ToLower()))
                                {
                                    //если найдено
                                    dataGridView1.Rows[row.Index].Selected = true;
                                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                                    StartRowIndex = i;
                                    return row.Index;
                                }
                            }

                        }
                        else if (words.Count() == 3 && cell.Value != null)
                        {
                            string[] lines = cell.Value.ToString().Split('\n');
                            foreach (string line in lines)
                            {
                                if (line.ToLower().Contains(words[0].ToLower())
                                   && line.ToLower().Contains(words[1].ToLower())
                                  && line.ToLower().Contains(words[2].ToLower()))
                                {
                                    //если найдено
                                    dataGridView1.Rows[row.Index].Selected = true;
                                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                                    StartRowIndex = i;
                                    return row.Index;
                                }
                            }
                        }
                    }
                }
            }
            toolStripStatusLabel4.Text = "ничего не найдено";
            StartRowIndex = 0;
            return -1;
        }

        /// <summary>
        /// поиск всех трасс по шаблону руглярного выражения и сбор их в структуру LinesStat
        /// </summary>
        /// <returns>Список всех найденных трасс со индексами строк, в которых они найдены</returns>
        public List<LinesStat> FindAllLines()
        {
            List<LinesStat> lines = new List<LinesStat>();
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var row = dataGridView1.Rows[i];
                    string napravl = row.Cells["psl_gp"].Value.ToString();
                    Regex rgLine = new Regex(patternLine);
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        var ColumnName = dataGridView1.Columns[cell.ColumnIndex].Name;
                        if (ColumnName != "kto" && ColumnName != "trassa") continue;

                        //===========SEARCH========================================
                        MatchCollection matchedLines = rgLine.Matches(cell.Value.ToString());
                        foreach (var match in matchedLines)
                        {
                            string m = match.ToString();
                            var sLine = lines.FindAll(x => x.lineName == m);
                            if (sLine.Count == 0)
                                lines.Add(new LinesStat()
                                {
                                    lineName = m,
                                    rowIndexes = new Dictionary<int, string>(){
                                              {i,napravl}
                                          },
                                    count = 1
                                });
                            else
                            {
                                if (!sLine[0].rowIndexes.ContainsKey(i))
                                    sLine[0].rowIndexes.Add(i, napravl);
                                sLine[0].count += 1;
                            }
                        }

                    }
                }
                lines.Sort((a, b) => a.lineName.CompareTo(b.lineName));
                return lines;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Find ALL Lines");
                Writelog.WriteLog(ex.Message);
                return lines;
            }
        }

        /// <summary>
        /// Поиск списка строк в БД по имени
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public LinesStat FindLine(string name)
        {
            LinesStat line = new LinesStat()
            {
                lineName = name,
                rowIndexes = new Dictionary<int, string>(),
                count = 0
            };
            try
            {

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    var row = dataGridView1.Rows[i];
                    string napravl = row.Cells["psl_gp"].Value.ToString();

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        var ColumnName = dataGridView1.Columns[cell.ColumnIndex].Name;
                        if (ColumnName != "kto" && ColumnName != "trassa" && ColumnName != "psl_gp") continue;

                        //===========SEARCH========================================
                        if (!cell.Value.ToString().Contains(name)) continue;

                        if (!line.rowIndexes.ContainsKey(i))
                        {
                            line.rowIndexes.Add(i, napravl);
                            line.count += 1;
                        }
                    }
                }

                return line;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Find line: " + name);
                Writelog.WriteLog(ex.Message + "\tName=" + name);
                return line;
            }
        }

        /// <summary>
        /// Поиск всех трактов в тексте по шаблону
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MatchCollection SearchTrakts(string text)
        {
            Regex rgTrakt = new Regex(patternFullTrakt);
            MatchCollection matchedLines = rgTrakt.Matches(text);
            return matchedLines;
        }
        /// <summary>
        /// Поиск всех узлов в тексте по шаблону
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MatchCollection SearchNodes(string text)
        {
            Regex rgNode = new Regex(patternNode);
            MatchCollection matchedNodes = rgNode.Matches(text);
            return matchedNodes;
        }
        public void DeleteDublicateCell(int i)
        {
            foreach (int c in editedRows)
            {
                while (RowsContain(c) > 1)
                {
                    //TODO Implement
                }
            }
        }
        private int RowsContain(int val)
        {
            int res = 0;
            foreach (int n in editedRows)
            {
                if (val == n) res++;
            }
            return res;
        }

        /// <summary>
        /// Поиск и загрузка в chekbox всех трактов
        /// </summary>
        public void LoadTraks()
        {
            Lines = FindAllLines();
            cbTrakts.Items.Clear();
            foreach (LinesStat line in Lines)
            {
                cbTrakts.Items.Add(line.lineName);
            }
            lbCountTraks.Text = "Кол-во трактов: " + Lines.Count().ToString();

        }
        /// <summary>
        /// Отображение статуса изменений в базе в нижней строке статуса
        /// </summary>
        /// <param name="changed"></param>
        public void setStatusStripChanged(bool changed)
        {
            if (changed)
            {
                statusLabelRight.Text = "Есть изменения в базе";
                statusLabelRight.ForeColor = Color.DarkRed;
            }
            else
            {
                statusLabelRight.Text = "OK";
                statusLabelRight.ForeColor = Color.DarkGreen;
            }
        }
        public void SelectRowDataGrid(int rowIndex)
        {
            dataGridView1.Rows[rowIndex].Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = rowIndex;
        }
        public bool isDataChanged()
        {
            if (editedRows.Count > 0 || editedCells.Count > 0 || DeletedRows.Count > 0) return true;
            return false;
        }
        public static void WriteLog(string line, string filename)
        {
            try
            {
                int log_size = 100000;
                //пишем все сообщения, генерируемые службой во время работы, в локальный файл на диске
                FileStream fs1 = new FileStream(filename, FileMode.Append);
                long lenght = fs1.Length;
                fs1.Dispose();
                if (lenght >= log_size) //log_size - предельный размер лог-файла в байтах
                {
                    File.Move(filename,
                        filename + "_" + DateTime.Now.ToShortDateString() + "." + DateTime.Now.Hour + "." +
                        DateTime.Now.Minute + "." + DateTime.Now.Second + @".old");
                }
                FileStream fs2 = new FileStream(filename, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(DateTime.Now.ToString() + ": " + line);
                sw.Close();
                fs2.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи лога", filename);
            }
        }

        #endregion
    }


}
