/*
 * Created by SharpDevelop.
 * User: Laz
 * Date: 06.01.2022
 * Time: 11:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace DBFprog1
{
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class Trakt
    {
        public string[] Node1, Node2;
        public string Line;
        public Trakt()
        {
        }
    }

    public class LinesStat
    {
        public string lineName;
        public int count;//TODO get set
        /// <summary>
        /// int - номер строки в БД, string - значение из столбца "Направление"
        /// </summary>
        public Dictionary<int, string> rowIndexes;

        public override string ToString()
        {
            return string.Format("{0}, Кол-во={1}, Строки={2}", lineName, count, RowsToString());
        }

        public LinesStat(Dictionary<int, string> rowIndexes, string lineName = "", int count = 0)
        {
            this.lineName = lineName;
            this.count = count;
            this.rowIndexes = rowIndexes;

        }
        public LinesStat()
        {
            lineName = "";
            count = 0;
            rowIndexes = new Dictionary<int, string>();
        }
        public string RowsToString()
        {
            string res = "";
            foreach (KeyValuePair<int, string> row in rowIndexes)
            {
                res += row.Key.ToString() + "==" + row.Value + "\n";
            }
            return res;
        }

    }
}
