/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 04.05.2016
 * Время: 12:19
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Windows.Forms;

namespace DBFprog1
{
    /// <summary>
    /// Description of About.
    /// </summary>
    public partial class About : Form
    {
        public About()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
            richTextBox1.DeselectAll();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
    }
}
