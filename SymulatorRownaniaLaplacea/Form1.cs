using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SymulatorRownaniaLaplacea
{
    public partial class Form1 : Form
    {
        Graphics g;
        int x1, y1, x2, y2;
        Int16 flag = 0;
        Workplace workplace;
        Pen pen;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            pictureBox1.MouseClick += new MouseEventHandler(this.Picture_MouseClick);
            pictureBox1.MouseMove += new MouseEventHandler(this.Picture_MouseMove);
            pen = new Pen(Color.Black, 3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picture_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = e.X + "," + e.Y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        /// <summary>
        /// 
        /// </summary>
        private void clear()
        {
            g.Clear(Color.White);
            workplace = new Workplace();
            flag = 0;
            label1.Text = "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picture_MouseClick(object sender, MouseEventArgs e)
        {

            if (flag == 0)
            {
                clear();
                x1 = e.X;
                y1 = e.Y;

                flag++;
            }
            else if (flag == 1)
            {
                x2 = e.X;
                y2 = e.Y;

                workplace.dodajGranice(x1, y1, x1, y2);
                g.DrawLine(pen, x1, y1, x1, y2);

                workplace.dodajGranice(x1, y2, x2, y2);
                g.DrawLine(pen, x1, y2, x2, y2);

                workplace.dodajGranice(x2, y2, x2, y1);
                g.DrawLine(pen, x2, y2, x2, y1);

                workplace.dodajGranice(x2, y1, x1, y1);
                g.DrawLine(pen, x2, y1, x1, y1);

                label1.Text = "Granice: górna: " + workplace.gora + " dolna: " + workplace.dol + " lewa: " + workplace.lewo + " prawa: " + workplace.prawo;

                flag++;
            }
            else
            {
                if ((e.X > workplace.lewo - 3 && e.X < workplace.lewo + 3)
                                            &&
                    (e.Y < workplace.gora - 3 && e.Y > workplace.dol + 3))
                {
                    addingValue a = new addingValue(workplace, "lewo");
                    a.Show();
                }

                if ((e.X > workplace.prawo - 3 && e.X < workplace.prawo + 3)
                                            &&
                    (e.Y < workplace.gora - 3 && e.Y > workplace.dol + 3))
                {
                    addingValue a = new addingValue(workplace, "prawo");
                    a.Show();
                }

                if ((e.Y > workplace.gora - 3 && e.Y < workplace.gora + 3)
                                            &&
                    (e.X < workplace.prawo - 3 && e.X > workplace.lewo + 3))
                {
                    addingValue a = new addingValue(workplace, "gora");
                    a.Show();
                }

                if ((e.Y > workplace.dol - 3 && e.Y < workplace.dol + 3)
                                            &&
                    (e.X < workplace.prawo - 3 && e.X > workplace.lewo + 3))
                {
                    addingValue a = new addingValue(workplace, "dol");
                    a.Show();
                }
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (flag > 1)
            {
                workplace.draw();
                g.DrawImage(workplace.wynik, workplace.lewo + 1, workplace.gora + 1);

                flag = 0;
            }
        }


    }
}
