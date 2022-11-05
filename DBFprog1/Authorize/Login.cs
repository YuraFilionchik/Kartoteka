/*
 * Created by SharpDevelop.
 * User: user
 * Date: 18.04.2016
 * Time: 9:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace DBFprog1.Authorize
{
	/// <summary>
	/// Description of Login.
	/// </summary>
	public partial class Login : Form
	{
		private int count = 0;
		IniFile Cfg;
		public Login()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			Cfg = Program.MF.Cfg;
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
			string res = CheckInput();
			if (res != "OK")
			{
				lbError.Text = res;
			}
			else
			{
				Program.MF.CurrentUser = tbName.Text;
				Program.MF.IsAdmin = true;

				if (tbName.Text == "LazGrodno") Program.MF.управлениеПаролямиToolStripMenuItem.Enabled = true;
				else
				{
					Program.MF.управлениеПаролямиToolStripMenuItem.Enabled = false;
				}
				Close();
			}
		}

		private string CheckInput()
		{
			if (String.IsNullOrWhiteSpace(tbName.Text)) return "Введите имя!";
			if (String.IsNullOrWhiteSpace(tbPass.Text)) return "Введите пароль!";



			if (!Cfg.KeyExists(tbName.Text, "Pass")) return "Пользователь не найден";
			if (Cfg.ReadINI("Pass", tbName.Text) != tbPass.Text)
			{
				if (count > 2 && count <= 5)
				{
					count++;
					return "Не угадали..";
				}

				if (count > 5)
				{

					Program.MF.Close();
				}
				count++;
				return "Неверный пароль";
			}

			return "OK";
		}
		void Button2Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
