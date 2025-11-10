using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duloren_etiqueta_materiais_diversos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            pgBar.Style = ProgressBarStyle.Marquee;
            pgBar.MarqueeAnimationSpeed = 50;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GerarEtiquetas();
        }

        private void GerarEtiquetas()
        {
            timer1.Stop();

            //rodar a query que busca os dados

            //passas list para processo das etiquetas

            //atualizar etiquetas no AS400

            timer1.Start();
        }
    }
}
