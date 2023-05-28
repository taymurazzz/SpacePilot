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
            this.BackColor = Color.Blue; // Пример установки синего цвета
            //this.FormBorderStyle = FormBorderStyle.None; // удаление верхней белой панели
            this.ControlBox = false; // убрать кнопки с панели
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // стиль 
            InitializeComponent();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Создайте экземпляр Form1 (если он еще не создан)
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
            Application.ExitThread();
        }
    }
}
