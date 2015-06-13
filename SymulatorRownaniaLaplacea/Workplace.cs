using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SymulatorRownaniaLaplacea
{
    /// <summary>
    /// 
    /// </summary>
    public class Workplace
    {

        #region pola
        private Granica[] granica;
        private Granica gornaGranica, dolnaGranica, prawaGranica, lewaGranica;
        private Int16 licznikWolnychGranic;
        private Bitmap mapaWartosci;
        private double[,] przestrzenRownania;
        private int maxX, maxY, minX, minY;
        private const double e = 10 ^ -6;
        #endregion

        #region wlasciwosci
        public int prawo { get { return maxX; } }
        public int lewo { get { return minX; } }
        public int gora { get { return minY; } }
        public int dol { get { return maxY; } }
        public Bitmap wynik { get { return mapaWartosci; } }
        public double GornaGranica
        {
            get
            {
                return gornaGranica.potencjal;
            }
            set
            {
                gornaGranica.potencjal = value;
            }
        }
        public double DolnaGranica
        {
            get
            {
                return dolnaGranica.potencjal;
            }
            set
            {
                dolnaGranica.potencjal = value;
            }
        }
        public double PrawaGranica
        {
            get
            {
                return prawaGranica.potencjal;
            }
            set
            {
                prawaGranica.potencjal = value;
            }
        }
        public double LewaGranica
        {
            get
            {
                return lewaGranica.potencjal;
            }
            set
            {
                lewaGranica.potencjal = value;
            }
        }
        #endregion

        #region metody

        /// <summary>
        /// 
        /// </summary>
        public Workplace()
        {
            granica = new Granica[4];
            licznikWolnychGranic = 0;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public void dodajGranice(int x1, int y1, int x2, int y2)
        {
            if (licznikWolnychGranic < 4)
            {
                granica[licznikWolnychGranic] = new Granica(x1, y1, x2, y2);
                licznikWolnychGranic++;
            }
            if (licznikWolnychGranic == 4)
            {
                findMaxes();
            }
        }

        /// <summary>
        /// test mothod
        /// </summary>
        private void draw()
        {
            for (int x = 0; x < mapaWartosci.Width; x++)
            {
                for (int y = 0; y < mapaWartosci.Height; y++)
                {
                    mapaWartosci.SetPixel(x, y, Color.FromArgb((Int32)przestrzenRownania[x,y]));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void findMaxes()
        {
            int tmp1 = granica[0].getX1;
            int tmp2 = granica[0].getX1;
            for (int i = 0; i < 4; i++)
            {
                if (granica[i].getX1 > tmp1)
                {
                    tmp1 = granica[i].getX1;
                    
                }

                if (granica[i].getX2 > tmp1)
                {
                    tmp1 = granica[i].getX2;
                    
                }

                if (granica[i].getX1 < tmp2)
                {
                    tmp2 = granica[i].getX1;
                    
                }

                if (granica[i].getX2 < tmp2)
                {
                    tmp2 = granica[i].getX2;
                    
                }
            }
            maxX = tmp1;
            minX = tmp2;

            tmp1 = granica[0].getY1;
            tmp2 = granica[0].getY1;
            for (int i = 0; i < 4; i++)
            {
                if (granica[i].getY1 > tmp1)
                {
                    tmp1 = granica[i].getY1;
                    
                }

                if (granica[i].getY2 > tmp1)
                {
                    tmp2 = granica[i].getY2;
                    
                }

                if (granica[i].getY1 < tmp2)
                {
                    tmp1 = granica[i].getY1;
                    
                }

                if (granica[i].getY2 < tmp2)
                {
                    tmp2 = granica[i].getY2;
                    
                }
            }
            maxY = tmp1;
            minY = tmp2;

            setPositions();

            mapaWartosci = new Bitmap((maxX - minX)-1, (maxY - minY)-1);
            przestrzenRownania = new double[(maxX - minX)-1, (maxY - minY)-1];
        }

        /// <summary>
        /// 
        /// </summary>
        private void setPositions()
        {
            foreach (Granica g in granica)
            {
                if (g.getX1 == g.getX2)
                {
                    if (g.getX1 == maxX)
                    {
                        dolnaGranica = g;
                    }
                    else
                    {
                        gornaGranica = g;
                    }
                }
                else if (g.getY1 == g.getY2)
                {
                    if (g.getY1 == maxY)
                    {
                        prawaGranica = g;
                    }
                    else
                    {
                        lewaGranica = g;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private double getValue(double[,] t, int x, int y)
        {
            try
            {
                return t[x, y];
            }
            catch (IndexOutOfRangeException e)
            {

                if (x < 0)
                {
                    return this.LewaGranica;
                }
                if (y < 0)
                {
                    return this.GornaGranica;
                }
                if (x > (maxX - minX) - 2)
                {
                    return this.PrawaGranica;
                }
                if (y > (maxY - minY) - 2)
                {
                    return this.DolnaGranica;
                }
            }

            return 0;
        }


        public void doTheMath(System.Windows.Forms.Label l)
        {
            int ileSpelnone = 0;
            int iloscPunktow = ((maxX - minX) - 1) * ((maxY - minY) - 1);
            double[,] tmp1, tmp2;
            tmp2 = new double[(maxX - minX) - 1, (maxY - minY) - 1];
            double t1, t2, t3, t4;
            int iloscIteracji = 0;

            while (ileSpelnone < iloscPunktow)
            {
                ileSpelnone = 0;
                l.Text = iloscIteracji.ToString();
                iloscIteracji++;

                tmp1 = tmp2;
                tmp2 = new double[(maxX - minX)-1, (maxY - minY)-1];

                for (int x = 0; x < ((maxX - minX) - 1); x++)
                {
                    for (int y = 0; y < ((maxY - minY) - 1); y++)
                    {
                        t1 = getValue(tmp1, x + 1, y);
                        t2 = getValue(tmp1, x - 1, y);
                        t3 = getValue(tmp1, x, y + 1);
                        t4 = getValue(tmp1, x, y - 1);

                        tmp2[x, y] = (t1 + t2 + t3 + t4) / 4;

                        if (tmp2[x, y] - tmp1[x, y] < e)
                        {
                            ileSpelnone++;
                        }
                    }
                }
            }

            przestrzenRownania = tmp2;
            draw();
        }
        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class Granica
    {
        private int x1, y1, x2, y2;
        private double val;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1">pierwszego punktu</param>
        /// <param name="y1">pierwszego punktu</param>
        /// <param name="x2">drugiego punktu</param>
        /// <param name="y2">drugiego punktu</param>
        public Granica(int x1, int y1, int x2, int y2)
        {
            this.x1 = x1;
            this.y1 = y1;

            this.x2 = x2;
            this.y2 = y2;
            val = 0;
        }

        #region wlasciwosci
        public int getX1 { get { return x1; } }
        public int getX2 { get { return x2; } }
        public int getY1 { get { return y1; } }
        public int getY2 { get { return y2; } }
        public double potencjal
        {
            get
            {
                return val;
            }

            set
            {
                if (value >= -100 && value <= 100)
                {
                    val = value;
                }
            }
        }
        #endregion
    }

}
