using System;
using System.Windows.Forms;

namespace DBFprog1.Authorize
{
    public partial class PassManager : Form
    {
        private IniFile cfg = Program.MF.Cfg;
        private string oldName = "";

        public PassManager()
        {
            InitializeComponent();
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;

            dataGridView1.Columns.Add("name", "Имя");
            dataGridView1.Columns.Add("pass", "Пароль");
            dataGridView1.Columns.Add("manage", "");
            LoadCfg();

        }



        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var value = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
            if (value != null)
                oldName = value.ToString();
        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var currName = dataGridView1[0, e.RowIndex].Value;
            var currPass = dataGridView1[1, e.RowIndex].Value;
            switch (e.ColumnIndex)
            {
                case 0:

                    if (currName != null && currPass != null && !String.IsNullOrWhiteSpace(oldName))
                    {
                        AddUser(currName.ToString(), currPass.ToString());
                        DelUser(oldName);
                        oldName = "";
                    }
                    else if (String.IsNullOrWhiteSpace(oldName) && currPass != null)
                    {
                        AddUser(currName.ToString(), currPass.ToString());
                    }
                    break;
                case 1:
                    if (currName != null && currPass != null)
                    {
                        AddUser(currName.ToString(), currPass.ToString());
                    }
                    break;

                default: break;
            }


        }

        private void AddUser(string name, string pass)
        {
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(pass)) return;
            // if (cfg.KeyExists(name, "Pass"))
            {
                cfg.Write("Pass", name, pass);
            }

        }

        private void DelUser(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) return;
            cfg.DeleteKey(name, "Pass");
        }



        void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell &&
                e.RowIndex >= 0)
            {
                cfg.DeleteKey(dataGridView1[e.ColumnIndex - 2, e.RowIndex].Value.ToString(), "Pass");
                LoadCfg();
            }
        }

        public void LoadCfg()
        {
            dataGridView1.Rows.Clear();

            var names = cfg.GetAllKeys("Pass");
            for (int i = 0; i < names.Length; i++)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.Cells.Add(new DataGridViewTextBoxCell());
                newRow.Cells.Add(new DataGridViewTextBoxCell());
                newRow.Cells.Add(new DataGridViewButtonCell());
                newRow.Cells[0].Value = names[i];
                newRow.Cells[1].Value = cfg.ReadINI("Pass", names[i]);
                newRow.Cells[2].Value = "удалить";
                dataGridView1.Rows.Add(newRow);
            }
        }
    }
}
