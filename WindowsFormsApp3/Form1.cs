using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp3.Properties;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public event EventHandler Changed;
        public Form1()
        {
            // нажатие кнопки 
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.BackColor = Color.Blue; // Пример установки синего цвета
            this.ControlBox = false; // убрать кнопки с панели
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // стиль 
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Focus();
            InitializeComponent();
            label1.AutoSize = false; // Отключаем автоматическую подгонку размера
            label1.Width = 100; // Устанавливаем желаемую ширину
            label2.AutoSize = false; // Отключаем автоматическую подгонку размера
            label2.Width = 100; // Устанавливаем желаемую ширину
            label2.Text = "100";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Button clicked!"); // Добавьте эту строку для отладки
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }
        public Timer disappearTimer;
        public Timer explosionTimer;
        public PictureBox PubLaser = new PictureBox();
        public List<PictureBox> PubMeteor = new List<PictureBox>();
        public void DisappearTimer_Tick(object sender, EventArgs e)
        {
            this.Controls.Remove(PubLaser);
            disappearTimer.Stop(); // Останавливаем таймер
        }
        public void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            while(PubMeteor.Count > 0)
            {
                //PubMeteor[0].BackgroundImage = Image.FromFile("C:\\Users\\ahsav\\Desktop\\game\\aaa.png");
                PubMeteor[0].BackgroundImage = Properties.Resources.aaa; 
                this.Controls.Remove(PubMeteor[0]);
                pictureBoxes.Remove(PubMeteor[0]);
                PubMeteor.Remove(PubMeteor[0]);
            }
            explosionTimer.Stop(); // Останавливаем таймер
        }
        public int x = 100;
        public void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X && !this.Controls.Contains(PubLaser) && x>0) // Проверяем, нажата ли клавиша Enter
            {
                x--;
                label2.Text = x.ToString();
                PictureBox Laser = new PictureBox();
                Laser.Height = 50;
                Laser.Width = 900;
                Laser.BackgroundImageLayout = ImageLayout.Stretch;
                Laser.BackColor = Color.Transparent;
                //Laser.BackgroundImage = Image.FromFile("C:\\Users\\ahsav\\Desktop\\game\\ebb950d7662f362.png");
                Laser.BackgroundImage = Properties.Resources.ebb950d7662f362;
                Laser.Location = new Point(Rocket.Location.X + 100, Rocket.Location.Y + 5);
                //Laser.Click += Laser_Click; // Добавляем обработчик события для нового пикчербокса
                Laser.Visible = true;
                this.Controls.Add(Laser);
                this.Focus();

                PubLaser = Laser;

                int i = 0;
                while (i < pictureBoxes.Count)
                {
                    if (pictureBoxes[i].Location.Y > PubLaser.Location.Y-30 && pictureBoxes[i].Location.Y< PubLaser.Location.Y + 30 && PubLaser.Location.X< pictureBoxes[i].Location.X)
                    {
                        //pictureBoxes[i].BackgroundImage = Image.FromFile("C:\\Users\\ahsav\\Desktop\\game\\aaa.png");
                        pictureBoxes[i].BackgroundImage = Properties.Resources.aaa;
                        PubMeteor.Add(pictureBoxes[i]);
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }
                explosionTimer = new Timer();
                explosionTimer.Interval = 200; // Интервал в 1000 миллисекунд (1 секунда)
                explosionTimer.Tick += ExplosionTimer_Tick;
                explosionTimer.Start();
                // удаление через секунду
                disappearTimer = new Timer();
                disappearTimer.Interval = 50; // Интервал в 1000 миллисекунд (1 секунда)
                disappearTimer.Tick += DisappearTimer_Tick;
                disappearTimer.Start();
            }
            if (e.KeyCode == Keys.S && this.Size.Height - Rocket.Height > Rocket.Location.Y + Rocket.Height)
            {
                Rocket.Location = new Point(Rocket.Location.X, Rocket.Location.Y + 20);
                this.Focus();
            }
            if (e.KeyCode == Keys.W && Rocket.Height / 2 + 5 < Rocket.Location.Y)
            {
                Rocket.Location = new Point(Rocket.Location.X, Rocket.Location.Y - 20);
                this.Focus();
            }
            if (e.KeyCode == Keys.D && this.Size.Width - Rocket.Width > Rocket.Location.X + Rocket.Width)
            {
                Rocket.Location = new Point(Rocket.Location.X+20, Rocket.Location.Y);
                this.Focus();
            }
            if (e.KeyCode == Keys.A && Rocket.Width / 2 + 5 < Rocket.Location.X)
            {
                Rocket.Location = new Point(Rocket.Location.X-20, Rocket.Location.Y);
                this.Focus();
            }
        }
        public int time = 0;
        public List<PictureBox> pictureBoxes = new List<PictureBox>();
        public Random random = new Random();
        private void timer1_Tick(object sender, EventArgs e)
        {
            //time++;
            //label1.Text = Convert.ToString(time);
            PictureBox Meteor = new PictureBox();
            Meteor.Height = 50;
            Meteor.Width = 50;
            Meteor.BackgroundImageLayout = ImageLayout.Stretch;
            Meteor.BackColor = Color.Transparent;
            //Meteor.BackgroundImage = Image.FromFile("C:\\Users\\ahsav\\Desktop\\game\\Asteroid Brown.png");
            Meteor.BackgroundImage = Properties.Resources.Asteroid_Brown;
            Meteor.Location = new Point(800, random.Next(30, this.Height-110));//Rocket.Location.Y);
            Meteor.Visible = true;
            pictureBoxes.Add( Meteor );
            this.Controls.Add(Meteor);
            this.Focus();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // ПОЧЕМУ ФОРИЧ НЕ РАБОТАЕТ А ФОР РАБОТАЕТ???
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                pictureBoxes[i].Location = new Point(pictureBoxes[i].Location.X - 10, pictureBoxes[i].Location.Y);
                if (
                    pictureBoxes[i].Location.X < Rocket.Location.X + Rocket.Width && 
                    pictureBoxes[i].Location.X + pictureBoxes[i].Width > Rocket.Location.X && 
                    pictureBoxes[i].Location.Y < Rocket.Location.Y + Rocket.Height && 
                    pictureBoxes[i].Location.Y + pictureBoxes[i].Height > Rocket.Location.Y
                    )
                {
                    timer1.Stop();
                    timer2.Stop();
                    //Rocket.Image = Image.FromFile("C:\\Users\\ahsav\\Desktop\\game\\pngwing.com.png");
                    Rocket.Image = Properties.Resources.pngwing_com;
                    //pictureBoxes[i].BackgroundImage = Image.FromFile("C:\\Users\\ahsav\\Desktop\\game\\aaa.png");
                    pictureBoxes[i].BackgroundImage = Properties.Resources.aaa;
                    //MessageBox.Show("GAME OVEEER");

                    DialogResult result = MessageBox.Show($"GAME OVEEER! Your score is {time}", "", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        button1_Click(sender, e);
                    }
                }
                if (pictureBoxes[i].Location.X < 0)
                {
                    // Удаление PictureBox из формы
                    this.Controls.Remove(pictureBoxes[i]);
                    pictureBoxes.Remove(pictureBoxes[i]);
                }
            }
        }
        public Timer Score;

        public void Score_Tick(object sender, EventArgs e)
        {
            time++;
            label1.Text = Convert.ToString(time);
            if (timer1.Interval > 10)
            {
                timer1.Interval -= 10;
            }
            if (timer2.Interval > 2)
            {
                timer2.Interval -= 1;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;

            Score = new Timer();
            Score.Interval = 1000; // Интервал в 1000 миллисекунд (1 секунда)
            Score.Tick += Score_Tick;
            Score.Start();

            this.Controls.Remove(pictureBox1);
        }
    }
}
