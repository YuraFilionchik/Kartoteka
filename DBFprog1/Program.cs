/*
 * Created by SharpDevelop.
 * User: Ситал
 * Date: 11.04.2016
 * Time: 16:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

namespace DBFprog1
{
    /// <summary>
    /// Class with program entry point.
    /// </summary>
    internal sealed class Program
    {
        public static MainForm MF;
        /// <summary>
        /// Program entry point.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MF = new MainForm();
            Application.Run(MF);
        }

    }
}
