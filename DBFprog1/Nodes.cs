/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 12.05.2016
 * Время: 12:52
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;


namespace DBFprog1
{
	/// <summary>
	/// Description of Nodes.
	/// </summary>
	public class Nodes
	{
		private Dictionary<string, string> dicFromFile = new Dictionary<string, string>();
		private IniFile Cfg;
		public static Dictionary<string, string> allNodes;
		public Nodes()
		{
			try
			{
				if (System.IO.File.Exists("Names.ini")) Cfg = new IniFile("Names.ini");
				else
				{
					System.Windows.Forms.MessageBox.Show("Файл names.ini не найден");
					allNodes = null;
					return;
				};
				LoadNamesFromFile();
				allNodes = dicFromFile;

			}
			catch (Exception ex) { logging.Writelog.WriteLog(ex.Message); }



		}


		public void LoadNamesFromFile()
		{
			try
			{

				foreach (string node in Cfg.GetAllKeys("names"))
				{
					if (!dicFromFile.ContainsKey(node)) dicFromFile.Add(node, Cfg.ReadINI("names", node));
					else
						Cfg.DeleteKey(node, "names");

				}
			}
			catch (Exception)
			{
				logging.Writelog.WriteLog("ошибка при чтении файла Names.ini");
			}
		}
	}
}
