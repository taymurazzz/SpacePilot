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
        public event EventHandler Changed; //событие нажатия кнопки
        public Form1()
        {
            // ловит нажатие кнопок
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            //Удаляет кнопки с белой линии формы, запрещает менять размер окна чтобы элементы правильно отображались на форме
            this.BackColor = Color.Blue; 
            this.ControlBox = false; // убрать кнопки с панели
            this.FormBorderStyle = FormBorderStyle.FixedSingle;  
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Focus();
            
            InitializeComponent();

            // хранит счет, 
            label1.AutoSize = false; // Отключаем автоматическую подгонку размера
            label1.Width = 100; // Устанавливаем желаемую ширину

            // хранит заряд лазера
            label2.AutoSize = false; // Отключаем автоматическую подгонку размера
            label2.Width = 100; // Устанавливаем желаемую ширину
            label2.Text = "100";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Focus();
        }
        
        // Закрытие формы с игрой открытие формы меню
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Close();
        }

        public Timer disappearTimer; //Таймер для лазера
        public Timer explosionTimer; //Таймер для взрыва метеоров
        public PictureBox PubLaser = new PictureBox(); // лазер
        public List<PictureBox> PubMeteor = new List<PictureBox>(); // список метеоров в которые попал лазер 


        public void DisappearTimer_Tick(object sender, EventArgs e)
        {
            this.Controls.Remove(PubLaser); //удаляет след лазера с формы
            disappearTimer.Stop(); // Останавливаем таймер
        }
        public void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            while(PubMeteor.Count > 0) // проходится по всем метеорам в которые попал лазер 
            {
                PubMeteor[0].BackgroundImage = Properties.Resources.aaa; 
                this.Controls.Remove(PubMeteor[0]);
                pictureBoxes.Remove(PubMeteor[0]);
                PubMeteor.Remove(PubMeteor[0]);
            }
            explosionTimer.Stop(); // Останавливаем таймер
        }
        public int x = 100; // хранит заряд лазера
        public void Form1_KeyDown(object sender, KeyEventArgs e) // отрабатывает при нажатии любой кнопки
        {
            if (e.KeyCode == Keys.X && !this.Controls.Contains(PubLaser) && x>0) // Проверяем, нажата ли клавиша X
            {
                x--; // заряд на 1 меньше
                label2.Text = x.ToString(); 

                // создание и отрисовка лазера
                PictureBox Laser = new PictureBox();
                Laser.Height = 50;
                Laser.Width = 900;
                Laser.BackgroundImageLayout = ImageLayout.Stretch;
                Laser.BackColor = Color.Transparent;
                Laser.BackgroundImage = Properties.Resources.ebb950d7662f362;
                Laser.Location = new Point(Rocket.Location.X + 100, Rocket.Location.Y + 5);
                Laser.Visible = true;
                this.Controls.Add(Laser);
                this.Focus();
                PubLaser = Laser;

                // поиск подбитых метеоров и анимация взрыва
                int i = 0;
                while (i < pictureBoxes.Count)
                {
                    if (pictureBoxes[i].Location.Y > PubLaser.Location.Y-30 && pictureBoxes[i].Location.Y< PubLaser.Location.Y + 30 && PubLaser.Location.X< pictureBoxes[i].Location.X)
                    {
                        pictureBoxes[i].BackgroundImage = Properties.Resources.aaa;
                        PubMeteor.Add(pictureBoxes[i]); // добавляем в подбитые метеориты
                        i++;
                    }
                    else
                    {
                        i++;
                    }
                }

                // таймер на взрыв 
                explosionTimer = new Timer();
                explosionTimer.Interval = 200; 
                explosionTimer.Tick += ExplosionTimer_Tick;
                explosionTimer.Start();

                // таймер на лазер, 
                disappearTimer = new Timer();
                disappearTimer.Interval = 50; 
                disappearTimer.Tick += DisappearTimer_Tick;
                disappearTimer.Start();
            }
            // 4 ифа на 4 кнопки, вверх вниз влево вправо w s a d
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
        public int time = 0; // хранит время игры
        public List<PictureBox> pictureBoxes = new List<PictureBox>(); // лист метеоритов на форме
        public Random random = new Random(); // объект создающий псевдо рандомное число
        private void timer1_Tick(object sender, EventArgs e) // создает новый метеор в случайном месте
        {
            PictureBox Meteor = new PictureBox();
            Meteor.Height = 50;
            Meteor.Width = 50;
            Meteor.BackgroundImageLayout = ImageLayout.Stretch;
            Meteor.BackColor = Color.Transparent;
            Meteor.BackgroundImage = Properties.Resources.Asteroid_Brown;
            Meteor.Location = new Point(800, random.Next(30, this.Height-110));
            Meteor.Visible = true;
            pictureBoxes.Add( Meteor ); // добавляет в список метеоров на форме
            this.Controls.Add(Meteor);
            this.Focus();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // ПОЧЕМУ ФОРИЧ НЕ РАБОТАЕТ А ФОР РАБОТАЕТ???
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                pictureBoxes[i].Location = new Point(pictureBoxes[i].Location.X - 10, pictureBoxes[i].Location.Y); // анимация движения метеоритов 
                // проверка на столкновенние 
                if (
                    pictureBoxes[i].Location.X < Rocket.Location.X + Rocket.Width && 
                    pictureBoxes[i].Location.X + pictureBoxes[i].Width > Rocket.Location.X && 
                    pictureBoxes[i].Location.Y < Rocket.Location.Y + Rocket.Height && 
                    pictureBoxes[i].Location.Y + pictureBoxes[i].Height > Rocket.Location.Y
                    )
                {
                    // остановка таймеров
                    timer1.Stop();
                    timer2.Stop();
                    Score.Stop();
                    // анимация взрывов
                    Rocket.Image = Properties.Resources.pngwing_com;
                    pictureBoxes[i].BackgroundImage = Properties.Resources.aaa;

                    // конец игры
                    DialogResult result = MessageBox.Show($"GAME OVEEER! Your score is {time}", "", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        button1_Click(sender, e);
                    }
                }

                // удаление метеорита пролетевшего форму
                if (pictureBoxes[i].Location.X < 0)
                {
                    // Удаление PictureBox из формы
                    this.Controls.Remove(pictureBoxes[i]);
                    pictureBoxes.Remove(pictureBoxes[i]);
                }
            }
        }
        public Timer Score; // таймер для счета

        // ускорение игры, то есть ускорение метеоритов, ускорение появления новых метеоритов 
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
        // pictureBox1 проверяет готовность игрока, для начала игры нужно нажать im ready
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;

            // Создание таймера счета игры, каждую секунду начисляется 1 балл
            Score = new Timer();
            Score.Interval = 1000; // Интервал в 1000 миллисекунд (1 секунда)
            Score.Tick += Score_Tick; 
            Score.Start();

            this.Controls.Remove(pictureBox1); // удаляет кнопку сообщающую о готовности
        }
    }
}
