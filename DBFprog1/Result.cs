/*
 * Создано в SharpDevelop.
 * Пользователь: user
 * Дата: 11.05.2016
 * Время: 14:38
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Windows.Forms;

namespace DBFprog1
{
    /// <summary>
    /// Description of Result.
    /// </summary>
    public partial class Result : Form
    {
        public Result()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            KeyDown += keyDown;
            Height = Screen.PrimaryScreen.Bounds.Height - 35;
        }
        void Label1Click(object sender, EventArgs e)
        {

        }

        void keyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Close();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}
