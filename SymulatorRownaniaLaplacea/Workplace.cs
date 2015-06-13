using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;

namespace SymulatorRownaniaLaplacea
{
    /// <summary>
    /// Klasa rozwiązująca Laplace'a
    /// </summary>
    public class Workplace
    {

        #region pola
        private Granica[] granica;
        private Granica gornaGranica, dolnaGranica, prawaGranica, lewaGranica;
        private Int16 licznikWolnychGranic;
        private Bitmap mapaWartosci;
        private decimal[,] przestrzenRownania;
        private int maxX, maxY, minX, minY;
        private const decimal e = 0.0001M;
        private int iloscIteracji;
        private TimeSpan dlugosc;
        #endregion

        #region wlasciwosci
        public int prawo { get { return maxX; } }
        public int lewo { get { return minX; } }
        public int gora { get { return minY; } }
        public int dol { get { return maxY; } }
        public Bitmap wynik { get { return mapaWartosci; } }
        public decimal GornaGranica
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
        public decimal DolnaGranica
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
        public decimal PrawaGranica
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
        public decimal LewaGranica
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
        public int iteracje { get { return iloscIteracji; } }
        public TimeSpan czas { get { return dlugosc; } }
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
        /// Dodaje jedną granicę (odcinek)
        /// </summary>
        /// <param name="x1">współrzędna x początku</param>
        /// <param name="y1">współrzędna y początku</param>
        /// <param name="x2">współrzędna x końca</param>
        /// <param name="y2">współrzędna x końca</param>
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
        /// Rysowanie wykresu potencjałów
        /// </summary>
        private void draw()
        {
            int parametr;
            try
            {
                parametr = Int32.MaxValue / (int)(this.przestrzenRownania.Cast<decimal>().Max() - this.przestrzenRownania.Cast<decimal>().Min());
            }
            catch (DivideByZeroException e)
            {
                parametr = 1;
            }
            decimal min = this.przestrzenRownania.Cast<decimal>().Min();
            for (int x = 0; x < mapaWartosci.Width; x++)
            {
                for (int y = 0; y < mapaWartosci.Height; y++)
                {
                    mapaWartosci.SetPixel(x, y, Color.FromArgb((Int32)(przestrzenRownania[x, y] - min) * parametr));
                }
            }
        }

        /// <summary>
        /// znajduje oriętacje granic
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
            przestrzenRownania = new decimal[(maxX - minX) - 1, (maxY - minY) - 1];
        }

        /// <summary>
        /// przyporządkowuje poszczególne granice do połorzeń: góra, dół, lewo, prawo
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
        /// Tworzy tablicę z wartościami brzegowymi
        /// </summary>
        /// <returns>zwraca 2-wymiarową tablicę tylu decimal</returns>
        private decimal[,] newTab()
        {
            decimal[,] tmp = new decimal[(maxX - minX) + 1, (maxY - minY) + 1];

            for (int i = 0; i < (maxX - minX) + 1; i++)
            {
                tmp[i, 0] = this.GornaGranica;
                tmp[i, (maxY - minY)] = this.DolnaGranica;
            }

            for (int i = 0; i < (maxY - minY) + 1; i++)
            {
                tmp[0, i] = LewaGranica;
                tmp[(maxX - minX), i] = PrawaGranica;
            }

                return tmp;
        }

        /// <summary>
        /// Obliczenia na podstawie danych zawartych w klasie
        /// </summary>
        public void doTheMath()
        {
            int ileSpelnone = 0;
            int iloscPunktow = ((maxX - minX) - 1) * ((maxY - minY) - 1);
            decimal[,] tmp1, tmp2;
            tmp2 = newTab();
            
            iloscIteracji = 0;

            Stopwatch stoper = Stopwatch.StartNew();

            while ((ileSpelnone < iloscPunktow/2 || iloscIteracji<2500) && iloscIteracji < 10000)
            {
                ileSpelnone = 0;
                
                iloscIteracji++;

                tmp1 = tmp2;
                tmp2 = newTab();

                for (int x = 1; x < (maxX - minX); x++)
                {
                    for (int y = 1; y < (maxY - minY); y++)
                    {
                        tmp2[x, y] = (tmp1[x+1,y] + tmp1[x-1,y] + tmp1[x,y+1] + tmp1[x,y-1]) / 4;

                        if (tmp2[x, y] - tmp1[x, y] < e)
                        {
                            ileSpelnone++;
                        }
                    }
                }
            }

            stoper.Stop();
            dlugosc = stoper.Elapsed;
            przestrzenRownania = tmp2;
            draw();
        }
        #endregion
    }

    /// <summary>
    /// klasa przechowująca granice
    /// </summary>
    public class Granica
    {
        private int x1, y1, x2, y2;
        private decimal val;

        /// <summary>
        /// Tworzy granicę (odcinek)
        /// </summary>
        /// <param name="x1">współrzędna x początku</param>
        /// <param name="y1">współrzędna y początku</param>
        /// <param name="x2">współrzędna x końca</param>
        /// <param name="y2">współrzędna x końca</param>
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
        public decimal potencjal
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
