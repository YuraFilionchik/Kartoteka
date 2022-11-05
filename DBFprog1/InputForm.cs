/*
 * Created by SharpDevelop.
 * User: Ситал
 * Date: 25.04.2016
 * Time: 7:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace DBFprog1
{
    /// <summary>
    /// Description of InputForm.
    /// </summary>
    public partial class InputForm : Form
    {
        public InputForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            #region события

            KeyDown += keydown;
            textBox1.KeyDown += keydown;

            #endregion

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        public void keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Program.MF.Search(textBox1.Text.Trim(), 0);
                Close();
                return;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                return;
            }
        }
    }
}
