using System;
using System.IO;
using System.Windows.Forms;

namespace DBFprog1
{

    static class Writelog
    {
        public static void WriteLog(string line)
        {
            try
            {
                int log_size = 100000;
                //пишем все сообщения, генерируемые службой во время работы, в локальный файл на диске
                FileStream fs1 = new FileStream("log.txt", FileMode.Append);
                long lenght = fs1.Length;
                fs1.Dispose();
                if (lenght >= log_size) //log_size - предельный размер лог-файла в байтах
                {
                    File.Move("log.txt",
                        "log_" + DateTime.Now.ToShortDateString() + "." + DateTime.Now.Hour + "." +
                        DateTime.Now.Minute + "." + DateTime.Now.Second + @".old");
                }
                FileStream fs2 = new FileStream("log.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs2);
                sw.WriteLine(DateTime.Now.ToString() + ": " + line);
                sw.Close();
                fs2.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи лога");
            }
        }
    }
}
