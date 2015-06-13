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
        private int gornaGranica, dolnaGranica, prawaGranica, lewaGranica;
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
                return granica[gornaGranica].potencjal;
            }
            set
            {
                granica[gornaGranica].potencjal = value;
            }
        }
        public double DolnaGranica
        {
            get
            {
                return granica[dolnaGranica].potencjal;
            }
            set
            {
                granica[dolnaGranica].potencjal = value;
            }
        }
        public double PrawaGranica
        {
            get
            {
                return granica[prawaGranica].potencjal;
            }
            set
            {
                granica[prawaGranica].potencjal = value;
            }
        }
        public double LewaGranica
        {
            get
            {
                return granica[lewaGranica].potencjal;
            }
            set
            {
                granica[lewaGranica].potencjal = value;
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
        public void draw()
        {
            for (int x = 0; x < mapaWartosci.Width; x++)
            {
                for (int y = 0; y < mapaWartosci.Height; y++)
                {
                    mapaWartosci.SetPixel(x, y, Color.BlueViolet);
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
                    prawaGranica = i;
                }

                if (granica[i].getX2 > tmp1)
                {
                    tmp1 = granica[i].getX2;
                    prawaGranica = i;
                }

                if (granica[i].getX1 < tmp2)
                {
                    tmp2 = granica[i].getX1;
                    lewaGranica = i;
                }

                if (granica[i].getX2 < tmp2)
                {
                    tmp2 = granica[i].getX2;
                    lewaGranica = i;
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
                    dolnaGranica = i;
                }

                if (granica[i].getY2 > tmp1)
                {
                    tmp2 = granica[i].getY2;
                    dolnaGranica = i;
                }

                if (granica[i].getY1 < tmp2)
                {
                    tmp1 = granica[i].getY1;
                    gornaGranica = i;
                }

                if (granica[i].getY2 < tmp2)
                {
                    tmp2 = granica[i].getY2;
                    gornaGranica = i;
                }
            }
            maxY = tmp1;
            minY = tmp2;

            mapaWartosci = new Bitmap((maxX - minX)-1, (maxY - minY)-1);
            przestrzenRownania = new double[(maxX - minX)+1, (maxY - minY)+1];
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
