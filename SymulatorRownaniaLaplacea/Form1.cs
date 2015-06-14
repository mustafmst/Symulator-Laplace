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
        SolidBrush brush;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            pictureBox1.MouseClick += new MouseEventHandler(this.Picture_MouseClick);
            pictureBox1.MouseMove += new MouseEventHandler(this.Picture_MouseMove);
            pen = new Pen(Color.Black, 3);
            brush = new SolidBrush(Color.Black); 
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
            label5.Text = "Stan: Czeka";
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

                if (x2 != x1 && y2 != y1)
                {
                    workplace.dodajGranice(x1, y1, x1, y2);
                    g.DrawLine(pen, x1, y1, x1, y2);

                    workplace.dodajGranice(x1, y2, x2, y2);
                    g.DrawLine(pen, x1, y2, x2, y2);

                    workplace.dodajGranice(x2, y2, x2, y1);
                    g.DrawLine(pen, x2, y2, x2, y1);

                    workplace.dodajGranice(x2, y1, x1, y1);
                    g.DrawLine(pen, x2, y1, x1, y1);

                    aktualizacjaPotencjalow();

                    flag++;
                }
            }
            else
            {
                if ((e.X > workplace.lewo - 3 && e.X < workplace.lewo + 3)
                                            &&
                    (e.Y > workplace.gora + 3 && e.Y < workplace.dol - 3))
                {
                    addingValue a = new addingValue(workplace, "lewo",this);
                    a.Show();
                }

                if ((e.X > workplace.prawo - 3 && e.X < workplace.prawo + 3)
                                            &&
                    (e.Y > workplace.gora + 3 && e.Y < workplace.dol - 3))
                {
                    addingValue a = new addingValue(workplace, "prawo", this);
                    a.Show();
                }

                if ((e.Y > workplace.gora - 3 && e.Y < workplace.gora + 3)
                                            &&
                    (e.X < workplace.prawo - 3 && e.X > workplace.lewo + 3))
                {
                    addingValue a = new addingValue(workplace, "gora", this);
                    a.Show();
                }

                if ((e.Y > workplace.dol - 3 && e.Y < workplace.dol + 3)
                                            &&
                    (e.X < workplace.prawo - 3 && e.X > workplace.lewo + 3))
                {
                    addingValue a = new addingValue(workplace, "dol", this);
                    a.Show();
                }
            }


        }

        public void aktualizacjaPotencjalow()
        {
            label1.Text = "Potencjały: góra: " + workplace.GornaGranica + ", dół: " + workplace.DolnaGranica + ", prawo: " + workplace.PrawaGranica + ", lewo:" + workplace.LewaGranica;
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
                label5.Text = "Stan: Pracuje";

                workplace.doTheMath();
                g.Clear(Color.White);
                
                if (checkBox1.Checked == true)
                {
                    g.DrawLine(pen, workplace.lewo, workplace.gora, workplace.prawo, workplace.gora);
                    g.DrawLine(pen, workplace.prawo, workplace.gora, workplace.prawo, workplace.dol);
                    g.DrawLine(pen, workplace.prawo, workplace.dol, workplace.lewo, workplace.dol);
                    g.DrawLine(pen, workplace.lewo, workplace.dol, workplace.lewo, workplace.gora);
                }

                g.DrawImage(workplace.wynik, workplace.lewo + 1, workplace.gora + 1);
                g.DrawImage(workplace.wynik, workplace.lewo + 1, workplace.gora + 1);
                g.DrawImage(workplace.wynik, workplace.lewo + 1, workplace.gora + 1);
                

                if (checkBox2.Checked == true)
                {
                    g.DrawImage(workplace.Skala, workplace.lewo, workplace.dol + 25);
                    g.DrawImage(workplace.Skala, workplace.lewo, workplace.dol + 25);
                    g.DrawImage(workplace.Skala, workplace.lewo, workplace.dol + 25);
                    g.DrawString(workplace.minValue.ToString(), this.Font, brush, new RectangleF(new PointF((float)workplace.lewo, (float)workplace.dol + 50), new Size(40, 20)));
                    g.DrawString(workplace.maxValue.ToString(), this.Font, brush, new RectangleF(new PointF((float)workplace.lewo + 200, (float)workplace.dol + 50), new Size(40, 20)));
                }


                label3.Text = "ilość iteracji: " + workplace.iteracje;
                label4.Text = "Obliczenia trwały: " + workplace.czas.Minutes + " minut " + workplace.czas.Seconds + " sekund";
                label5.Text = "Stan: Skończył";
                //flag = 0;
            }
        }


    }
}
