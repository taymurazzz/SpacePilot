using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Создайте экземпляры форм Form1 и Form2
            Form1 form1 = new Form1();
            Form2 form2 = new Form2();

            // Отобразите Form2
            Application.Run(form2);
        }
    }
}
