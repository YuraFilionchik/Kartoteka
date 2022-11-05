/*
 * Created by SharpDevelop.
 * User: Ситал
 * Date: 11.04.2016
 * Time: 16:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using logging;
using System.IO;


namespace DBFprog1
{
	/// <summary>
	/// Description of WorkDBF.
	/// </summary>
	public class WorkDBF
	{
		private OleDbConnection Conn = null;
		public DataTable Execute(string Command)
		{
			DataTable dt = null;
			if (Conn != null)
			{
				try
				{
					if (String.IsNullOrWhiteSpace(Command)) throw new Exception("Command string is empty");
					if (Conn.State != ConnectionState.Open)
						Conn.Open();
					dt = new DataTable();
					System.Data.OleDb.OleDbCommand oCmd = Conn.CreateCommand();
					oCmd.CommandText = Command;
					dt.Load(oCmd.ExecuteReader());

					if (Conn.State != ConnectionState.Closed)
						Conn.Close();
					Program.MF.VFPOLEDB_installed = true;
				}
				catch (Exception ex)
				{
					if (Conn.State != ConnectionState.Closed)
						Conn.Close();
					if (ex.Message.Contains("vfpoledb"))
					{
						MessageBox.Show("В системе не установлен драйвер VFPOLEDB для " +
					   "чтение файла базы. База не может быть открыта!", "Внимание!!!");
						Program.MF.VFPOLEDB_installed = false;
					}
					else MessageBox.Show(ex.ToString(), ex.Message);
					Writelog.WriteLog(ex.Message + " :::: " + Command);
					return null;
				}
			}
			return dt;
		}
		public bool CheckConnection()
		{
			if (Conn == null) return false;
			try
			{
				if (Conn.State != ConnectionState.Open)
					Conn.Open();
				Conn.Close();
				return true;
			}
			catch (Exception)
			{

				return false;

			}
		}
		public void Execute(string Command, List<OleDbParameter> paramsCollection)
		{
			if (!CheckConnection()) return;
			DataTable dt = null;
			if (Conn != null)
			{
				try
				{

					if (Conn.State != ConnectionState.Open)
						Conn.Open();
					dt = new DataTable();
					System.Data.OleDb.OleDbCommand oCmd = Conn.CreateCommand();
					oCmd.CommandText = Command;

					foreach (OleDbParameter parameter in paramsCollection)
					{
						oCmd.Parameters.Add(parameter);

					}
					// MessageBox.Show(paramstoStr(oCmd.Parameters));
					// MessageBox.Show(oCmd.Transaction.ToString());
					oCmd.ExecuteNonQuery();

					if (Conn.State != ConnectionState.Closed)
						Conn.Close();
				}
				catch (Exception ex)
				{
					Writelog.WriteLog(ex.ToString());
					if (ex.Message.Contains("vfpoledb"))
					{
						MessageBox.Show("В системе не установлен драйвер VFPOLEDB для " +
				 "чтение файла базы. База не может быть открыта!", "Внимание!!!");
						Program.MF.VFPOLEDB_installed = false;
					}
					else { MessageBox.Show(ex.ToString(), ex.Message); Program.MF.VFPOLEDB_installed = true; }
					if (Conn.State != ConnectionState.Closed)
						Conn.Close();
				}
			}

		}

		public delegate void Mydel();

		public event Mydel MyEvent;
		public string paramstoStr(OleDbParameterCollection ps)
		{
			string res = "";
			foreach (OleDbParameter oleDbParameter in ps)
			{
				res += oleDbParameter.ParameterName + "=" + oleDbParameter.Value + "; \n\r";
			}
			return res;
		}

		public DataTable GetAll(string DB_path)
		{
			try
			{
				if (DB_path.Substring(DB_path.Length - 3).ToLower() == "dbf")
					DB_path = DB_path.Substring(0, DB_path.Length - 4);
				if (DB_path.Contains("\\"))
					DB_path = DB_path.Split('\\').Last();
				DataTable dt = Execute("SELECT * FROM " + DB_path);

				#region  убираем лишние пробелы во всех ячейках кроме id столбца
				for (int n = 0; n < dt.Rows.Count; n++)
				{
					for (int m = 0; m < dt.Columns.Count; m++)
					{
						if (dt.Columns[m].ColumnName != "id")
							dt.Rows[n][m] = dt.Rows[n][m].ToString().Trim();
					}
				}
				#endregion
				return dt;
			}
			catch (Exception ex)
			{

				throw;
			}

		}

		public OleDbDataReader GetTableReader(string path)
		{
			string DB_path = TrimFileName(path);
			if (Conn != null)
			{
				try
				{
					if (Conn.State != ConnectionState.Open)
						Conn.Open();
					System.Data.OleDb.OleDbCommand oCmd = Conn.CreateCommand();
					oCmd.CommandText = "SELECT * FROM " + DB_path;
					OleDbDataReader dr = oCmd.ExecuteReader();

					return dr;
				}
				catch (Exception ex)
				{
					if (Conn.State != ConnectionState.Closed)
						Conn.Close();
					MessageBox.Show(ex.ToString(), ex.Message);
					return null;
				}
			}
			return null;
		}

		public void DeleteRow(int idRow, string filename)
		{


			List<OleDbParameter> paramsCollection = new List<OleDbParameter>();
			string command = "DELETE FROM " + TrimFileName(filename) + " WHERE id=?";
			paramsCollection.Add(new OleDbParameter("@p1", idRow));
			Execute(command, paramsCollection);

		}
		public void InsertRow(int idRow, string filename)
		{
			DataTable DT = GetAll(filename);
			string command = "INSERT INTO " + TrimFileName(filename) + "(";
			OleDbDataReader dr = GetTableReader(filename);
			//список полей в команду
			for (int i = 0; i < dr.FieldCount; i++)
			{
				if (i < dr.FieldCount - 1) command += dr.GetName(i) + ",";
				else command += dr.GetName(i) + ")";
			}

			command += "VALUES (" + (idRow + 1) + ",'','','','','','','','','','','','')";
			//Execute("SET IDENTITY_INSERT "+filename+" ON"); //отключаем работу автоинкремента БД

			var n = Execute("SELECT MAX(id) FROM " + filename);
			var N = n.Rows[0][0].ToString();
			int MaxId = int.Parse(N);
			string insert = "Update " + filename + " SET id=3 WHERE id=2";
			Execute("ALTER TABLE " + filename + " ALTER id I");//отключаем работу автоинкремента БД
															   //смещаем все нижние строки по id на +1
			MessageBox.Show("before: " + GetAll(filename).Rows[GetAll(filename).Rows.Count - 1]["id"]);
			int id = 0;

			for (int i = DT.Rows.Count - 1; i > idRow; i--)//проход по Datatable.rows
			{
				id = int.Parse(DT.Rows[i]["id"].ToString());
				Execute("Update " + filename + " SET id=" + (id + 1).ToString() + " WHERE id=" + id);
			}
			Execute(command);
			MessageBox.Show("after: " + GetAll(filename).Rows[GetAll(filename).Rows.Count - 1]["id"]);
			Execute("ALTER TABLE " + filename + " ALTER id I AUTOINC NEXTVALUE 1 STEP 1");//включаем работу автоинкремента БД
		}

		public static string TrimFileName(string DB_old)
		{

			if (DB_old.Contains("\\"))
				DB_old = DB_old.Split('\\').Last();
			if (DB_old.Length > 3 && DB_old.Substring(DB_old.Length - 3).ToLower() == "dbf")
				DB_old = DB_old.Substring(0, DB_old.Length - 4);
			return DB_old;

		}

		/// <summary>
		/// Создает новую пустую БД формата NCS
		/// </summary>
		/// <param name="old"> уже имеющийся файл, используется как образец полей</param>
		/// <param name="newfile">имя нового файла БД</param>
		public void CreateDBF(string old, string newfile)
		{
			string DB_old = TrimFileName(old);
			string DB_new = TrimFileName(newfile);


			if (DB_new == DB_old)
			{
				DB_new = "temp";
			}

			string create_table = @"CREATE TABLE " + DB_new;
			try
			{

				create_table += @" CODEPAGE=866 ";
				create_table += @"( ID I AUTOINC NEXTVALUE 1 STEP 1, ";

				OleDbDataReader dr = GetTableReader(DB_old);
				DataTable sc = dr.GetSchemaTable();
				for (int i = 0; i < dr.FieldCount; i++)
				{
					if (dr.GetName(i) == "id")
						continue;
					create_table += dr.GetName(i) + " ";
					if (dr.GetDataTypeName(i).Split('_')[1] == "LONGVARCHAR")
						create_table += "Memo";
					else
					{
						create_table += dr.GetDataTypeName(i).Split('_')[1];
						create_table += "(" + (int.Parse(sc.Rows[i][2].ToString()) + 4).ToString() + ")";
					}


					if (i == dr.FieldCount - 1)
						create_table += ")";
					else
						create_table += ", ";
				}


				//MessageBox.Show(create_table);
				if (Conn.State != ConnectionState.Closed)
					Conn.Close();
				if (File.Exists(Program.MF.CurrentDir + "\\" + DB_new + ".dbf"))
				{
					File.Delete(Program.MF.CurrentDir + "\\" + DB_new + ".dbf");
					File.Delete(Program.MF.CurrentDir + "\\" + DB_new + ".fpt");
				}
				Execute(create_table);
				if (DB_new == "temp" && File.Exists(Program.MF.CurrentDir + "\\" + DB_new + ".dbf"))
				{
					File.Delete(Program.MF.CurrentDir + "\\" + DB_old + ".dbf");
					File.Delete(Program.MF.CurrentDir + "\\" + DB_old + ".fpt");
					File.Move(Program.MF.CurrentDir + "\\" + DB_new + ".dbf", Program.MF.CurrentDir + "\\" + DB_old + ".dbf");
					File.Move(Program.MF.CurrentDir + "\\" + DB_new + ".fpt", Program.MF.CurrentDir + "\\" + DB_old + ".fpt");
				}
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message, "CreateDBF");
				if (Conn.State != ConnectionState.Closed)
					Conn.Close();
			}
		}
		public void AddFieldID(string filename)
		{
			var DT = GetAll(filename);
			if (DT.Columns.Contains("id"))
			{
				MessageBox.Show("В данном файле уже имеется колонка id!");
				return;
			}

			filename = TrimFileName(filename);
			string command = "ALTER TABLE " + filename +
				"ADD id I";
			Execute(command);
			int n = 0;
			//Fill all id values
			foreach (DataRow row in DT.Rows)
			{
				Execute("Update " + filename + " SET id=" + row["id"] + " WHERE id=1");
			}

		}
		/// <summary>
		/// Изменяет значение указанной строки row из datatable
		/// </summary>
		/// <param name="row">сохраняемая строка</param>
		/// <param name="fileName">файл БД, в который сохраяется строка</param>
		/// <param name="columnNames">имена столбцов БД</param>
		public void UpdateRow(DataGridViewRow row, string fileName, List<string> columnNames)
		{
			string filename = TrimFileName(fileName);
			string insert = "";

			try
			{
				List<OleDbParameter> paramsCollection = new List<OleDbParameter>();
				insert = "Update " + filename + " SET ";
				string id = "";
				for (int i = 0; i < columnNames.Count; i++)
				{

					if (columnNames[i] == "id")
					{
						id = row.Cells[i].Value.ToString();
						continue;
					}
					paramsCollection.Add(new OleDbParameter("@p" + i.ToString(), NormalizeString(row.Cells[i].Value.ToString())));
					insert += columnNames[i] + "=?";

					if (i == columnNames.Count - 1)
					{
						insert += " ";
					}
					else
					{
						insert += ", ";
					}
				}

				insert += "WHERE id=" + id;
				//MessageBox.Show(insert);

				Execute(insert, paramsCollection);

			}
			catch (Exception ex)
			{
				Writelog.WriteLog(ex.ToString() + "::::" + insert);
				MessageBox.Show(ex.Message, "UpdateRow");
				throw;
			}

		}


		/// <summary>
		/// Запись новых данных в БД 
		/// </summary>
		/// <param name="fileName"></param>
		/// <param name="dt"></param>
		public void WriteDataToFile(string fileName, DataTable dt)
		{
			//			
			//			string add_id = @"ALTER TABLE " + filename + @" ADD ID I NULL";
			//			string change_id = @"ALTER TABLE " + filename + @" ALTER ID I AUTOINC NEXTVALUE 100 STEP 1";
			//			string delete_id = @"ALTER TABLE " + filename + @" DROP COLUMN ID";
			//			string create_table = @"CREATE TABLE " + filename;

			string filename = TrimFileName(fileName);

			string insert = "";
			string values = "";
			try
			{
				Program.MF.Invoke(new Action<bool>(l => Program.MF.toolStripProgressBar1.Visible = l), true);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.файлToolStripMenuItem.Enabled = l), false);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.правкаToolStripMenuItem.Enabled = l), false);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.dataGridView1.Enabled = l), false);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.listBox1.Enabled = l), false);
				foreach (DataRow row in dt.Rows)
				{

					Program.MF.Invoke(new Action<int>(l => Program.MF.toolStripProgressBar1.Value = l), (int)(100 * (dt.Rows.IndexOf(row) + 1) / dt.Rows.Count));
					Program.MF.Invoke(new Action<string>(l => Program.MF.toolStripStatusLabel2.Text = l + "%"), (100 * (dt.Rows.IndexOf(row) + 1) / dt.Rows.Count).ToString());

					List<OleDbParameter> paramsCollection = new List<OleDbParameter>();
					insert = "insert into " + filename + " (";
					values = " values (";
					for (int i = 0; i < dt.Columns.Count; i++)
					{
						if (dt.Columns[i].ColumnName == "id")
							continue;
						paramsCollection.Add(new OleDbParameter("@p" + i.ToString(), NormalizeString(row[i].ToString())));
						insert += dt.Columns[i].ColumnName;
						values += "?";

						if (i == dt.Columns.Count - 1)
						{
							insert += ")";
							values += ")";
						}
						else
						{
							insert += ", ";
							values += ", ";
						}
					}
					insert += values;
					//MessageBox.Show(insert);

					Execute(insert, paramsCollection);

				}
				if (MyEvent != null)
					MyEvent();
				Program.MF.Invoke(new Action<bool>(l => Program.MF.файлToolStripMenuItem.Enabled = l), true);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.правкаToolStripMenuItem.Enabled = l), true);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.dataGridView1.Enabled = l), true);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.listBox1.Enabled = l), true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "writeDTtofile");
				Writelog.WriteLog(ex.Message + " :::: " + insert);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.файлToolStripMenuItem.Enabled = l), true);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.правкаToolStripMenuItem.Enabled = l), true);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.dataGridView1.Enabled = l), true);
				Program.MF.Invoke(new Action<bool>(l => Program.MF.listBox1.Enabled = l), true);
			}





		}

		private string NormalizeString(string s)
		{
			if (String.IsNullOrWhiteSpace(s))
				return "";
			s = s.Trim();
			//s= s.Replace("\r","\\r");
			// s = s.Replace("\n", "\\n");
			return s;
		}

		public string ReadMemo(int id, string memoName, string fileName)
		{
			string select = "";
			try
			{
				string FileName = TrimFileName(fileName);
				select = "SELECT " + memoName +
				" FROM " + FileName +
				" WHERE id=" + id.ToString();
				var dt = Execute(select);
				if (dt != null)
					return dt.Rows[0][0].ToString();
			}
			catch (Exception exception)
			{
				Writelog.WriteLog(exception.Message + ":::::" + select);
				MessageBox.Show(exception.Message + "\n" + select, "ReadMemo");
			}
			return null;
		}

		public WorkDBF(string dir)
		{
			string cs1 = @"Provider=vfpoledb;Data Source=" + dir + ";";
			//			string cs2 = @"Driver={Microsoft Visual FoxPro Driver}; SourceType=DBF; SourceDB=" + @"e:\" +
			//			             @"Exclusive=No;Collate=Machine;NULL=NO;DELETED=NO;BACKGROUNDFETCH=NO;";
			//			string cs3 = @"Driver={Microsoft dBase VFP Driver (*.dbf)};" +
			//			            "SourceType=DBF; Exclusive=No;" +
			//			            "Collate=Machine; NULL=NO; DELETED=NO;" +
			//			            "BACKGROUNDFETCH=NO;";
			//			string cs4 = "Driver={Microsoft Access Driver (*.dbf )};SourceType=DBF;SourceDB=" + dir + @"\;Exclusive=No; NULL=NO;DELETED=NO;BACKGROUNDFETCH=NO;";
			//			
			Conn = new OleDbConnection();
			Conn.ConnectionString = cs1;


		}
	}
}
