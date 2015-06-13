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
    public partial class addingValue : Form
    {
        Workplace w;
        string option;


        public addingValue()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="w"></param>
        /// <param name="option"></param>
        public addingValue(Workplace w, string option) : this()
        {
            this.w = w;
            this.option = option;
            this.Text = option;
            
            switch (option)
            {
                case "gora":
                    this.numericUpDown1.Value = (decimal)w.GornaGranica;
                    break;
                case "dol":
                    this.numericUpDown1.Value = (decimal)w.DolnaGranica;
                    break;
                case "prawo":
                    this.numericUpDown1.Value = (decimal)w.PrawaGranica;
                    break;
                case "lewo":
                    this.numericUpDown1.Value = (decimal)w.LewaGranica;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case "gora":
                    w.GornaGranica = (double)this.numericUpDown1.Value;
                    break;
                case "dol":
                    w.DolnaGranica = (double)this.numericUpDown1.Value;
                    break;
                case "prawo":
                    w.PrawaGranica = (double)this.numericUpDown1.Value;
                    break;
                case "lewo":
                    w.LewaGranica = (double)this.numericUpDown1.Value;
                    break;
                default:
                    break;
            }

            this.Dispose();
        }
    }
}
