/*
 * Created by SharpDevelop.
 * User: Ситал
 * Date: 11.04.2016
 * Time: 16:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Windows.Forms;
namespace DBFprog1
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		public class MyDataGridView :System.Windows.Forms.DataGridView
		{
			         [System.Security.Permissions.UIPermission(
        System.Security.Permissions.SecurityAction.LinkDemand,
        Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
			 protected override bool ProcessDialogKey(Keys keyData)
    {
        // Extract the key code from the key value. 
        Keys key = (keyData & Keys.KeyCode);

        // Handle the ENTER key as if it were a RIGHT ARROW key. 
        if (key == Keys.Enter)
        {
            return false;
        }
        return base.ProcessDialogKey(keyData);
    }
			 
			 [System.Security.Permissions.SecurityPermission(
        System.Security.Permissions.SecurityAction.LinkDemand, Flags = 
        System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    protected override bool ProcessDataGridViewKey(KeyEventArgs e)
    {
        // Handle the ENTER key as if it were a RIGHT ARROW key. 
        if (e.KeyCode == Keys.Enter)
        {
            return false;
        }
        return base.ProcessDataGridViewKey(e);
    }
		}
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.dataGridView1 = new DBFprog1.MainForm.MyDataGridView();
			this.contextMenuDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.добавитьСтрокуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьСтрокуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menu = new System.Windows.Forms.MenuStrip();
			this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.открытьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.коToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.перечитатьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.режимРедактированияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.режимПросмотраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.изменитьШрифтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cbFontSize = new System.Windows.Forms.ToolStripComboBox();
			this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.управлениеПаролямиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.выбратьПапкуДляBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.инфоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.listMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.загрузитьОглавлениеИзФайлаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bF3 = new System.Windows.Forms.Button();
			this.bF5 = new System.Windows.Forms.Button();
			this.btF6 = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.btF2 = new System.Windows.Forms.Button();
			this.btF7 = new System.Windows.Forms.Button();
			this.btTrakt = new System.Windows.Forms.Button();
			this.cbTrakts = new System.Windows.Forms.ComboBox();
			this.lbCountTraks = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.statusLabelRight = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.contextMenuDGV.SuspendLayout();
			this.menu.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.listMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AllowUserToOrderColumns = true;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
			this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
			this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
			this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
			this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Yellow;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.ContextMenuStrip = this.contextMenuDGV;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Yellow;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
			this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
			this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.dataGridView1.Location = new System.Drawing.Point(173, 27);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.RowHeadersVisible = false;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.ShowCellErrors = false;
			this.dataGridView1.ShowRowErrors = false;
			this.dataGridView1.Size = new System.Drawing.Size(1197, 487);
			this.dataGridView1.TabIndex = 0;
			// 
			// contextMenuDGV
			// 
			this.contextMenuDGV.BackColor = System.Drawing.Color.Silver;
			this.contextMenuDGV.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.contextMenuDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.добавитьСтрокуToolStripMenuItem,
									this.удалитьСтрокуToolStripMenuItem});
			this.contextMenuDGV.Name = "contextMenuDGV";
			this.contextMenuDGV.Size = new System.Drawing.Size(212, 52);
			// 
			// добавитьСтрокуToolStripMenuItem
			// 
			this.добавитьСтрокуToolStripMenuItem.Enabled = false;
			this.добавитьСтрокуToolStripMenuItem.Name = "добавитьСтрокуToolStripMenuItem";
			this.добавитьСтрокуToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
			this.добавитьСтрокуToolStripMenuItem.Text = "Добавить строку";
			this.добавитьСтрокуToolStripMenuItem.Click += new System.EventHandler(this.ДобавитьСтрокуToolStripMenuItemClick);
			// 
			// удалитьСтрокуToolStripMenuItem
			// 
			this.удалитьСтрокуToolStripMenuItem.Enabled = false;
			this.удалитьСтрокуToolStripMenuItem.Name = "удалитьСтрокуToolStripMenuItem";
			this.удалитьСтрокуToolStripMenuItem.Size = new System.Drawing.Size(211, 24);
			this.удалитьСтрокуToolStripMenuItem.Text = "Удалить строку";
			this.удалитьСтрокуToolStripMenuItem.Click += new System.EventHandler(this.УдалитьСтрокуToolStripMenuItemClick);
			// 
			// menu
			// 
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.файлToolStripMenuItem,
									this.правкаToolStripMenuItem,
									this.инфоToolStripMenuItem});
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(1370, 24);
			this.menu.TabIndex = 4;
			this.menu.Text = "menuStrip1";
			// 
			// файлToolStripMenuItem
			// 
			this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.открытьФайлToolStripMenuItem,
									this.коToolStripMenuItem,
									this.перечитатьФайлToolStripMenuItem,
									this.сохранитьToolStripMenuItem,
									this.выходToolStripMenuItem});
			this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
			this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
			this.файлToolStripMenuItem.Text = "Файл";
			// 
			// открытьФайлToolStripMenuItem
			// 
			this.открытьФайлToolStripMenuItem.Name = "открытьФайлToolStripMenuItem";
			this.открытьФайлToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.открытьФайлToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.открытьФайлToolStripMenuItem.Text = "Открыть файл...";
			this.открытьФайлToolStripMenuItem.Click += new System.EventHandler(this.открытьФайлToolStripMenuItem_Click);
			// 
			// коToolStripMenuItem
			// 
			this.коToolStripMenuItem.Name = "коToolStripMenuItem";
			this.коToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.коToolStripMenuItem.Text = "Преобразовать для редактирования";
			this.коToolStripMenuItem.Click += new System.EventHandler(this.коToolStripMenuItem_Click);
			// 
			// перечитатьФайлToolStripMenuItem
			// 
			this.перечитатьФайлToolStripMenuItem.Name = "перечитатьФайлToolStripMenuItem";
			this.перечитатьФайлToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.перечитатьФайлToolStripMenuItem.Text = "Перечитать файл";
			this.перечитатьФайлToolStripMenuItem.Click += new System.EventHandler(this.перечитатьФайлToolStripMenuItem_Click);
			// 
			// сохранитьToolStripMenuItem
			// 
			this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
			this.сохранитьToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.сохранитьToolStripMenuItem.Text = "Сохранить";
			this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.СохранитьToolStripMenuItemClick);
			// 
			// выходToolStripMenuItem
			// 
			this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
			this.выходToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.выходToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
			this.выходToolStripMenuItem.Text = "Выход";
			this.выходToolStripMenuItem.Click += new System.EventHandler(this.ВыходToolStripMenuItemClick);
			// 
			// правкаToolStripMenuItem
			// 
			this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.режимРедактированияToolStripMenuItem,
									this.режимПросмотраToolStripMenuItem,
									this.изменитьШрифтToolStripMenuItem,
									this.backupToolStripMenuItem,
									this.управлениеПаролямиToolStripMenuItem,
									this.выбратьПапкуДляBackupToolStripMenuItem});
			this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
			this.правкаToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.правкаToolStripMenuItem.Text = "Правка";
			// 
			// режимРедактированияToolStripMenuItem
			// 
			this.режимРедактированияToolStripMenuItem.Name = "режимРедактированияToolStripMenuItem";
			this.режимРедактированияToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
			this.режимРедактированияToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.режимРедактированияToolStripMenuItem.Text = "Режим редактирования...";
			this.режимРедактированияToolStripMenuItem.Click += new System.EventHandler(this.РежимРедактированияToolStripMenuItemClick);
			// 
			// режимПросмотраToolStripMenuItem
			// 
			this.режимПросмотраToolStripMenuItem.Name = "режимПросмотраToolStripMenuItem";
			this.режимПросмотраToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
			this.режимПросмотраToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.режимПросмотраToolStripMenuItem.Text = "Режим просмотра";
			this.режимПросмотраToolStripMenuItem.Click += new System.EventHandler(this.РежимПросмотраToolStripMenuItemClick);
			// 
			// изменитьШрифтToolStripMenuItem
			// 
			this.изменитьШрифтToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.cbFontSize});
			this.изменитьШрифтToolStripMenuItem.Name = "изменитьШрифтToolStripMenuItem";
			this.изменитьШрифтToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.изменитьШрифтToolStripMenuItem.Text = "Изменить шрифт";
			// 
			// cbFontSize
			// 
			this.cbFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbFontSize.Items.AddRange(new object[] {
									"10",
									"12",
									"14",
									"16",
									"17",
									"18",
									"19",
									"20",
									"21",
									"22",
									"25",
									"27"});
			this.cbFontSize.Name = "cbFontSize";
			this.cbFontSize.Size = new System.Drawing.Size(121, 21);
			// 
			// backupToolStripMenuItem
			// 
			this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
			this.backupToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.backupToolStripMenuItem.Text = "Backup";
			this.backupToolStripMenuItem.Click += new System.EventHandler(this.BackupToolStripMenuItemClick);
			// 
			// управлениеПаролямиToolStripMenuItem
			// 
			this.управлениеПаролямиToolStripMenuItem.Name = "управлениеПаролямиToolStripMenuItem";
			this.управлениеПаролямиToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
									| System.Windows.Forms.Keys.Shift) 
									| System.Windows.Forms.Keys.F12)));
			this.управлениеПаролямиToolStripMenuItem.ShowShortcutKeys = false;
			this.управлениеПаролямиToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.управлениеПаролямиToolStripMenuItem.Text = "Управление паролями...";
			this.управлениеПаролямиToolStripMenuItem.Click += new System.EventHandler(this.управлениеПаролямиToolStripMenuItem_Click);
			// 
			// выбратьПапкуДляBackupToolStripMenuItem
			// 
			this.выбратьПапкуДляBackupToolStripMenuItem.Enabled = false;
			this.выбратьПапкуДляBackupToolStripMenuItem.Name = "выбратьПапкуДляBackupToolStripMenuItem";
			this.выбратьПапкуДляBackupToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.выбратьПапкуДляBackupToolStripMenuItem.Text = "Выбрать папку для backup...";
			this.выбратьПапкуДляBackupToolStripMenuItem.Click += new System.EventHandler(this.ВыбратьПапкуДляBackupToolStripMenuItemClick);
			// 
			// инфоToolStripMenuItem
			// 
			this.инфоToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.оПрограммеToolStripMenuItem});
			this.инфоToolStripMenuItem.Name = "инфоToolStripMenuItem";
			this.инфоToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
			this.инфоToolStripMenuItem.Text = "Инфо";
			// 
			// оПрограммеToolStripMenuItem
			// 
			this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
			this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
			this.оПрограммеToolStripMenuItem.Text = "О программе";
			this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.ОПрограммеToolStripMenuItemClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabel1,
									this.toolStripProgressBar1,
									this.toolStripStatusLabel2,
									this.toolStripStatusLabel3,
									this.toolStripStatusLabel4,
									this.statusLabelRight});
			this.statusStrip1.Location = new System.Drawing.Point(0, 515);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1370, 24);
			this.statusStrip1.TabIndex = 5;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 19);
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 18);
			this.toolStripProgressBar1.Step = 1;
			this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.toolStripProgressBar1.Visible = false;
			// 
			// toolStripStatusLabel2
			// 
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 19);
			// 
			// toolStripStatusLabel3
			// 
			this.toolStripStatusLabel3.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.toolStripStatusLabel3.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
			this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.Green;
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(4, 19);
			this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolStripStatusLabel4
			// 
			this.toolStripStatusLabel4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.Red;
			this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
			this.toolStripStatusLabel4.Size = new System.Drawing.Size(0, 19);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "NCS";
			this.openFileDialog1.Filter = "Файлы базы данных|*.dbf";
			this.openFileDialog1.Title = "Выберите файл базы данных .dbf";
			this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.listBox1.BackColor = System.Drawing.Color.PowderBlue;
			this.listBox1.ContextMenuStrip = this.listMenu;
			this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listBox1.ForeColor = System.Drawing.Color.MediumBlue;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 15;
			this.listBox1.Location = new System.Drawing.Point(0, 27);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(167, 484);
			this.listBox1.TabIndex = 6;
			this.listBox1.UseTabStops = false;
			// 
			// listMenu
			// 
			this.listMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripMenuItem1,
									this.переименоватьToolStripMenuItem,
									this.загрузитьОглавлениеИзФайлаToolStripMenuItem});
			this.listMenu.Name = "listMenu";
			this.listMenu.Size = new System.Drawing.Size(242, 70);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Enabled = false;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(241, 22);
			this.toolStripMenuItem1.Text = "Убрать строку";
			// 
			// переименоватьToolStripMenuItem
			// 
			this.переименоватьToolStripMenuItem.Enabled = false;
			this.переименоватьToolStripMenuItem.Name = "переименоватьToolStripMenuItem";
			this.переименоватьToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.переименоватьToolStripMenuItem.Text = "Переименовать";
			this.переименоватьToolStripMenuItem.Click += new System.EventHandler(this.переименоватьToolStripMenuItem_Click);
			// 
			// загрузитьОглавлениеИзФайлаToolStripMenuItem
			// 
			this.загрузитьОглавлениеИзФайлаToolStripMenuItem.Enabled = false;
			this.загрузитьОглавлениеИзФайлаToolStripMenuItem.Name = "загрузитьОглавлениеИзФайлаToolStripMenuItem";
			this.загрузитьОглавлениеИзФайлаToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
			this.загрузитьОглавлениеИзФайлаToolStripMenuItem.Text = "Загрузить оглавление из базы";
			// 
			// bF3
			// 
			this.bF3.BackColor = System.Drawing.Color.Gray;
			this.bF3.Cursor = System.Windows.Forms.Cursors.Hand;
			this.bF3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bF3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bF3.ForeColor = System.Drawing.Color.Yellow;
			this.bF3.Location = new System.Drawing.Point(302, 0);
			this.bF3.Name = "bF3";
			this.bF3.Size = new System.Drawing.Size(95, 29);
			this.bF3.TabIndex = 7;
			this.bF3.Text = "F3-поиск";
			this.bF3.UseVisualStyleBackColor = false;
			this.bF3.Click += new System.EventHandler(this.bF3_Click);
			// 
			// bF5
			// 
			this.bF5.BackColor = System.Drawing.Color.Gray;
			this.bF5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.bF5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.bF5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.bF5.ForeColor = System.Drawing.Color.Yellow;
			this.bF5.Location = new System.Drawing.Point(403, 0);
			this.bF5.Name = "bF5";
			this.bF5.Size = new System.Drawing.Size(97, 29);
			this.bF5.TabIndex = 7;
			this.bF5.Text = "F5-трасса";
			this.bF5.UseVisualStyleBackColor = false;
			this.bF5.Click += new System.EventHandler(this.bF5_Click);
			// 
			// btF6
			// 
			this.btF6.BackColor = System.Drawing.Color.Gray;
			this.btF6.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btF6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btF6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btF6.ForeColor = System.Drawing.Color.Yellow;
			this.btF6.Location = new System.Drawing.Point(506, 0);
			this.btF6.Name = "btF6";
			this.btF6.Size = new System.Drawing.Size(130, 29);
			this.btF6.TabIndex = 7;
			this.btF6.Text = "F6-карточка";
			this.btF6.UseVisualStyleBackColor = false;
			this.btF6.Click += new System.EventHandler(this.bF6_Click);
			// 
			// btF2
			// 
			this.btF2.BackColor = System.Drawing.Color.Gray;
			this.btF2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btF2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btF2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btF2.ForeColor = System.Drawing.Color.Yellow;
			this.btF2.Location = new System.Drawing.Point(184, 0);
			this.btF2.Name = "btF2";
			this.btF2.Size = new System.Drawing.Size(112, 29);
			this.btF2.TabIndex = 7;
			this.btF2.Text = "F2-повтор";
			this.btF2.UseVisualStyleBackColor = false;
			this.btF2.Click += new System.EventHandler(this.BtF2Click);
			// 
			// btF7
			// 
			this.btF7.BackColor = System.Drawing.Color.Gray;
			this.btF7.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btF7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btF7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btF7.ForeColor = System.Drawing.Color.Yellow;
			this.btF7.Location = new System.Drawing.Point(643, 0);
			this.btF7.Name = "btF7";
			this.btF7.Size = new System.Drawing.Size(137, 29);
			this.btF7.TabIndex = 7;
			this.btF7.Text = "F7-Результат";
			this.btF7.UseVisualStyleBackColor = false;
			this.btF7.Click += new System.EventHandler(this.BtF7Click);
			// 
			// btTrakt
			// 
			this.btTrakt.BackColor = System.Drawing.Color.Gray;
			this.btTrakt.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btTrakt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btTrakt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btTrakt.ForeColor = System.Drawing.Color.Yellow;
			this.btTrakt.Location = new System.Drawing.Point(1233, 0);
			this.btTrakt.Name = "btTrakt";
			this.btTrakt.Size = new System.Drawing.Size(137, 29);
			this.btTrakt.TabIndex = 7;
			this.btTrakt.Text = "Тракты";
			this.btTrakt.UseVisualStyleBackColor = false;
			this.btTrakt.Visible = false;
			this.btTrakt.Click += new System.EventHandler(this.btTraktClick);
			// 
			// cbTrakts
			// 
			this.cbTrakts.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cbTrakts.FormattingEnabled = true;
			this.cbTrakts.ItemHeight = 20;
			this.cbTrakts.Location = new System.Drawing.Point(929, 0);
			this.cbTrakts.Name = "cbTrakts";
			this.cbTrakts.Size = new System.Drawing.Size(204, 28);
			this.cbTrakts.TabIndex = 8;
			// 
			// lbCountTraks
			// 
			this.lbCountTraks.BackColor = System.Drawing.Color.Transparent;
			this.lbCountTraks.Location = new System.Drawing.Point(1140, 0);
			this.lbCountTraks.Name = "lbCountTraks";
			this.lbCountTraks.Size = new System.Drawing.Size(125, 23);
			this.lbCountTraks.TabIndex = 9;
			this.lbCountTraks.Text = "..";
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Open Sans", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(846, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 23);
			this.label1.TabIndex = 10;
			this.label1.Text = "Поиск:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// statusLabelRight
			// 
			this.statusLabelRight.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
			this.statusLabelRight.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
			this.statusLabelRight.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.statusLabelRight.ForeColor = System.Drawing.Color.DarkGreen;
			this.statusLabelRight.Name = "statusLabelRight";
			this.statusLabelRight.Size = new System.Drawing.Size(1118, 19);
			this.statusLabelRight.Spring = true;
			this.statusLabelRight.Text = "ОК";
			this.statusLabelRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1370, 539);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lbCountTraks);
			this.Controls.Add(this.cbTrakts);
			this.Controls.Add(this.bF5);
			this.Controls.Add(this.btF7);
			this.Controls.Add(this.btF6);
			this.Controls.Add(this.btF2);
			this.Controls.Add(this.bF3);
			this.Controls.Add(this.btTrakt);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.menu);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MainMenuStrip = this.menu;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Картотека";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.contextMenuDGV.ResumeLayout(false);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.listMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripStatusLabel statusLabelRight;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label lbCountTraks;
		private System.Windows.Forms.ComboBox cbTrakts;
		public System.Windows.Forms.Button btF6;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        public DBFprog1.MainForm.MyDataGridView dataGridView1;
        public System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem открытьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem коToolStripMenuItem;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        public System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem перечитатьФайлToolStripMenuItem;
        public System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem режимРедактированияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem режимПросмотраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem изменитьШрифтToolStripMenuItem;
        public System.Windows.Forms.ToolStripComboBox cbFontSize;
        public System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem инфоToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьПапкуДляBackupToolStripMenuItem;
        public System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.ToolStripMenuItem управлениеПаролямиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip listMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem загрузитьОглавлениеИзФайлаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem;
        public System.Windows.Forms.Button bF3;
        public System.Windows.Forms.Button bF5;
        public System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public System.Windows.Forms.Button btF2;
        public System.Windows.Forms.Button btF7;
        public System.Windows.Forms.ContextMenuStrip contextMenuDGV;
        private System.Windows.Forms.ToolStripMenuItem добавитьСтрокуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьСтрокуToolStripMenuItem;
        public System.Windows.Forms.Button btTrakt;
       // private System.Windows.Forms.ToolStripMenuItem выбратьПапкуДляBackupToolStripMenuItem;

	
	}
	
	
	
}
	


