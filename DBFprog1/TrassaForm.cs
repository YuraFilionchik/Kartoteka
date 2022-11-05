using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DBFprog1
{
    public partial class TrassaForm : Form
    {
        bool alt = false;//были ли изменены названия станций F1
        int rowIndex = 0;
        public TrassaForm()
        {
            InitializeComponent();
            Height = Screen.PrimaryScreen.Bounds.Height - 35;
            KeyDown += TrassaForm_KeyDown;
            Shown += delegate
            {
                tbTrassa.Focus();
                if (String.IsNullOrWhiteSpace(Program.MF.SearchWord))
                {
                    tbTrassa.SelectionStart = tbTrassa.TextLength;
                    tbTrassa.SelectionLength = 0;
                    //SelectAllNodes();
                }
            };
            if (Program.MF.IsAdmin) tbTrassa.ReadOnly = false;
            else tbTrassa.ReadOnly = true;
            FormClosing += TrassaformClosing;
            Load += LoadTrassa;
            TextChanged += textChanged;

        }


        #region Events
        void TrassaForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        if (!Program.MF.IsAdmin) Close();
                        break;
                    case Keys.Escape:
                        Close();
                        break;

                    case Keys.F5:
                        update();
                        Trass(-1);
                        if (!Program.MF.IsAdmin) return;
                        Program.MF.OldCellValue = tbTrassa.Text;
                        break;

                    case Keys.F6:
                        update();
                        Kart(-1);
                        if (!Program.MF.IsAdmin) return;
                        Program.MF.OldCellValue = tbTrassa.Text;
                        break;
                    case Keys.F2:

                        rowIndex = Program.MF.Search(Program.MF.SearchWord, rowIndex);

                        if (Program.MF.dataGridView1.SelectedRows.Count < 1) return;
                        string who = Text.Split(' ')[0];
                        Program.MF.dataGridView1.Rows[rowIndex].Selected = true;
                        if (who == "Карточка") { Kart(rowIndex); }
                        else { Trass(rowIndex); }
                        if (!Program.MF.IsAdmin) return;
                        Program.MF.OldCellValue = tbTrassa.Text;
                        rowIndex++;
                        break;

                    case Keys.F1:
                        if (!System.IO.File.Exists("Names.ini")) { alt = false; return; }
                        // SelectAllNodes();

                        if (alt) foreach (KeyValuePair<string, string> node in Nodes.allNodes)
                            {
                                tbTrassa.Text = tbTrassa.Text.Replace("(" + node.Value + ")", "(" + node.Key + ")");

                            }
                        else
                            NodesToText(tbTrassa);

                        // выделение цветом
                        foreach (KeyValuePair<string, string> node in Nodes.allNodes)
                        {
                            string str = node.Value;
                            int i = 0;
                            while (i <= tbTrassa.Text.Length - str.Length)
                            {

                                i = tbTrassa.Text.IndexOf(str, i);
                                if (i < 0) break;
                                tbTrassa.SelectionStart = i;
                                tbTrassa.SelectionLength = str.Length;
                                tbTrassa.SelectionColor = Color.Red;
                                i += str.Length;
                            }
                        }
                        alt = !alt;
                        break;
                }


            }
            catch (Exception ex)
            {
                logging.Writelog.WriteLog(ex.Message);
                MessageBox.Show(ex.Message);

            }
        }

        void SelectAllNodes()
        {
            var matchNodes = MainForm.SearchNodes(tbTrassa.Text);
            foreach (Match match in matchNodes)
            {
                tbTrassa.SelectionStart = match.Index;
                tbTrassa.SelectionLength = match.Length;
                tbTrassa.SelectionColor = Color.Red;
            }
        }
        void SelectAllTrakts()
        {
            var matchTrakts = MainForm.SearchTrakts(tbTrassa.Text);
            foreach (Match match in matchTrakts)
            {
                tbTrassa.SelectionStart = match.Index;
                tbTrassa.SelectionLength = match.Length;
                tbTrassa.SelectionColor = Color.Navy;
            }
        }

        void NodesToText(object t)
        {
            var tb = (RichTextBox)t;
            foreach (KeyValuePair<string, string> node in Nodes.allNodes)
            {
                tb.Text = tb.Text.Replace("(" + node.Key + ")", "(" + node.Value + ")");

            }
        }
        void textChanged(object sender, EventArgs e)
        {
            SelectWordInTrassa(tbTrassa.Text, Program.MF.SearchWord);
        }

        void SelectWordInTrassa(string Text, string words)
        {
            if (!String.IsNullOrWhiteSpace(Text) && !String.IsNullOrWhiteSpace(words))
            {
                string[] Words = words.Split(' ');
                MatchCollection wordTrass = null;
                if (Words.Count() == 1)
                {
                    wordTrass = Regex.Matches(Text.ToLower(), words.ToLower());
                    if (wordTrass != null)
                        foreach (Match word in wordTrass)
                        {

                            tbTrassa.SelectionStart = word.Index;
                            tbTrassa.SelectionLength = word.Length;
                            tbTrassa.SelectionColor = Color.Yellow;
                            tbTrassa.SelectionBackColor = Color.MediumTurquoise;
                        }
                }
                else if (Words.Count() > 1)
                {
                    foreach (string w in Words)
                    {
                        wordTrass = Regex.Matches(Text.ToLower(), w.ToLower());
                        if (wordTrass != null)
                            foreach (Match word in wordTrass)
                            {

                                tbTrassa.SelectionStart = word.Index;
                                tbTrassa.SelectionLength = word.Length;
                                tbTrassa.SelectionColor = Color.Yellow;
                                tbTrassa.SelectionBackColor = Color.MediumTurquoise;
                            }
                    }
                }


            }
        }
        void LoadTrassa(object sender, EventArgs e)
        {
            SelectWordInTrassa(tbTrassa.Text, Program.MF.SearchWord);
            //SelectAllNodes();
            //SelectAllTrakts();
            if (!Program.MF.IsAdmin) return;
            Program.MF.OldCellValue = tbTrassa.Text;
        }
        void TrassaformClosing(object sender, FormClosingEventArgs e)
        {
            update();
            if (alt)
                foreach (KeyValuePair<string, string> node in Nodes.allNodes)
                {
                    tbTrassa.Text = tbTrassa.Text.Replace("(" + node.Value + ")", "(" + node.Key + ")");
                }
        }

        void update()
        {
            if (!Program.MF.IsAdmin) return;


            string who = Text.Split(' ')[0];
            if (who == "Карточка")
            {
                Program.MF.dataGridView1.SelectedRows[0].Cells["kto"].Value = tbTrassa.Text;

            }
            else
            {
                Program.MF.dataGridView1.SelectedRows[0].Cells["trassa"].Value = tbTrassa.Text;
            }
        }
        private void Kart(int RowIndex)
        {
            if (RowIndex == -1)
            {
                tbTrassa.Text = Program.MF.dataGridView1.SelectedRows[0].Cells["kto"].Value.ToString();
                if (Program.MF.dataGridView1.SelectedRows[0].Cells["psl_gp"].Value != null)
                    Text = "Карточка    " + Program.MF.dataGridView1.SelectedRows[0].Cells["psl_gp"].Value.ToString();
                else Text = "Карточка";
                // int id1 = int.Parse(Program.MF.dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());

            }
            else
            {
                tbTrassa.Text = Program.MF.dataGridView1.SelectedRows[0].Cells["kto"].Value.ToString();
                if (Program.MF.dataGridView1.Rows[RowIndex].Cells["psl_gp"].Value != null)
                    Text = "Карточка    " + Program.MF.dataGridView1.Rows[RowIndex].Cells["psl_gp"].Value.ToString();
                else Text = "Карточка";
                //int id2 = int.Parse(Program.MF.dataGridView1.Rows[RowIndex].Cells["id"].Value.ToString());


            }

        }
        private void Trass(int RowIndex)
        {
            if (RowIndex == -1)
            {
                tbTrassa.Text = Program.MF.dataGridView1.SelectedRows[0].Cells["trassa"].Value.ToString();
                if (Program.MF.dataGridView1.SelectedRows[0].Cells["psl_gp"].Value != null)
                    Text = "Трасса    " + Program.MF.dataGridView1.SelectedRows[0].Cells["psl_gp"].Value.ToString();
                else Text = "Трасса";
                // int id1 = int.Parse(Program.MF.dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());

            }
            else
            {
                tbTrassa.Text = Program.MF.dataGridView1.SelectedRows[0].Cells["trassa"].Value.ToString();
                if (Program.MF.dataGridView1.Rows[RowIndex].Cells["psl_gp"].Value != null)
                    Text = "Трасса    " + Program.MF.dataGridView1.Rows[RowIndex].Cells["psl_gp"].Value.ToString();
                else Text = "Трасса";
                // int id2 = int.Parse(Program.MF.dataGridView1.Rows[RowIndex].Cells["id"].Value.ToString());
                //string tmp=Program.MF.Wdbf.ReadMemo(id2, "trassa", Program.MF.DB_name);

            }
        }
        #endregion
    }
}
