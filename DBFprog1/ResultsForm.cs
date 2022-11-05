/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 12.01.2022
 * Time: 12:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DBFprog1
{
    /// <summary>
    /// Description of TraktsForm.
    /// </summary>
    public partial class ResultsForm : Form
    {
        public ResultsForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        public ResultsForm(LinesStat line)
        {
            InitializeComponent();
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            label1.Text = "Найденные связи в " + line.lineName + ":" + " (" +
                line.rowIndexes.Count(x => !String.IsNullOrWhiteSpace(x.Value.ToString())) + ")";
            if (line.count == 0)
            {
                listView1.Items.Add("0", "Ничего не найдено", "");

            }
            foreach (KeyValuePair<int, string> row in line.rowIndexes)
            {
                if (!String.IsNullOrWhiteSpace(row.Value.ToString())) listView1.Items.Add(row.Key.ToString(), row.Value, "");
            }
        }


        void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            var rowIndex = int.Parse(listView1.SelectedItems[0].Name);
            Program.MF.SelectRowDataGrid(rowIndex);
        }
    }
}
