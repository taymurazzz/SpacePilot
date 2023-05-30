using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            //Удаляет кнопки с белой линии формы, запрещает менять размер окна чтобы элементы правильно отображались на форме
            this.BackColor = Color.Blue; // установки синего цвета
            this.ControlBox = false; // убрать кнопки с панели
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // стиль 
            InitializeComponent();
        }
        // создает новую игру, скрывает форму меню открывает форму игры
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //  экземпляр Form1 
            Form1 form1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (form1 == null)
            {
                form1 = new Form1();
                form1.Show();
            }

            // Скрыть Form2
            this.Hide();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.ExitThread(); // убирает все открытые окна, полностью обрывает все процессы связанные с программой 
        }
    }
}
