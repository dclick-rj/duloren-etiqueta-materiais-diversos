using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace duloren_etiqueta_materiais_diversos
{
    public partial class Form1 : Form
    {
        private string caminhoConfiguracao = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public Form1()
        {
            InitializeComponent();

            pgBar.Style = ProgressBarStyle.Marquee;
            pgBar.MarqueeAnimationSpeed = 50;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MontarEtiquetas();
        }

        private void MontarEtiquetas()
        {
            timer1.Stop();

            List<DTO.Etiqueta> list = new List<DTO.Etiqueta>();

            //rodar a query que busca os dados


            //passas list para processo das etiquetas
            GerarEtiquetaC(list);

            timer1.Start();
        }

        private void GerarEtiquetaC(List<DTO.Etiqueta> list)
        {
            int contador = 0;

            foreach (DTO.Etiqueta etq in list)
            {

                string valor = etq.Qtd.ToString();

                // Primeiro completa à esquerda
                string qtdComZeros = valor.PadLeft((7 + valor.Length) / 2, '0');

                // Depois completa à direita até dar 7
                qtdComZeros = qtdComZeros.PadRight(7, '0');

                StringBuilder msg = new StringBuilder();

                msg.AppendLine("L");
                msg.AppendLine("D11");
                msg.AppendLine("W");
                msg.AppendLine("PK");
                msg.AppendLine("SK");
                msg.AppendLine("H21");
                msg.AppendLine("R0000");

                //msg.AppendLine("1E2203301560021" + "0006604930009600"); funcionando
                //msg.AppendLine("1E2203301560202" + "0006604930009600");

                //msg.AppendLine("1E2200801560021" + "0006604930009600");  // Posição X centralizada
                //msg.AppendLine("1E2200801560202" + "0006604930009600");  // Posição X centralizada
                //msg.AppendLine("1E1104001600021" + "0006604930009600");  // FUNCIONANDO
                //msg.AppendLine("1E1104001600202" + "0006604930009600");  // FUNCIONANDO
                msg.AppendLine("1E1104001600021" + etq.Material.PadLeft(6, '0'));  // LINHA DO CODBAR 1
                msg.AppendLine("1E1104001600202" + etq.Material.PadLeft(6, '0'));  // LINHA DO CODBAR 2

                //msg.AppendLine("191100101970029" + "000" + etq.Np + qtdComZeros);
                //msg.AppendLine("191100101970210" + "000" + etq.Np + qtdComZeros);


                //if(contador < obj40X60.Contador)
                //{
                //msg.AppendLine("131100101430005NP: " + "000" + etq.Np);//linha da NP 1 ------------
                //msg.AppendLine("131100101430186NP: " + "000" + etq.Np);//linha da NP 2 ------------

                //msg.AppendLine("121100101430021OP.: 1 REV.:92");
                //msg.AppendLine("121100101430202OP.: 1 REV.:92");

                //    contador++;
                //}
                //else
                //{
                //msg.AppendLine("121100101430021");
                //msg.AppendLine("121100101430202");
                //}

                msg.AppendLine("131100101280005OP.: 1 REV.:92");
                msg.AppendLine("131100101280186OP.: 1 REV.:92");
                //msg.AppendLine("121100101300021QTD.: " + etq.Qtd);
                //msg.AppendLine("121100101300202QTD.: " + etq.Qtd);

                msg.AppendLine("131100101130005QTD.: " + etq.Qtd);
                msg.AppendLine("131100101130186QTD.: " + etq.Qtd);

                //msg.AppendLine("121100101170021DATA: " + DateTime.Now.ToString("dd/MM/yyyy"));
                //msg.AppendLine("121100101170202DATA: " + DateTime.Now.ToString("dd/MM/yyyy"));
                msg.AppendLine("131100101000005DATA: " + etq.Data);
                msg.AppendLine("131100101000186DATA: " + etq.Data);

                //msg.AppendLine("121100101040021MATERIAL: " + etq.Material);
                //msg.AppendLine("121100101040202MATERIAL: " + etq.Material);
                msg.AppendLine("131100100860005MATERIAL: " + etq.Material);
                msg.AppendLine("131100100860186MATERIAL: " + etq.Material);

                //msg.AppendLine("121100100910021COR: " + etq.Cor + " TARA:0,00");
                //msg.AppendLine("121100100910202COR: " + etq.Cor + " TARA:0,00");
                //msg.AppendLine("131100100720005COR: " + etq.Cor + " TARA:0,00");//cor 1 --------------
                //msg.AppendLine("131100100720186COR: " + etq.Cor + " TARA:0,00");//cor 2 --------------

                //msg.AppendLine("121100100650021" + obj40X60.Cp07Bar);
                //msg.AppendLine("121100100650202" + obj40X60.Cp07Bar);

                //msg.AppendLine("121100100520021" + obj40X60.Cp08Bar);
                //msg.AppendLine("121100100520202" + obj40X60.Cp08Bar);

                //msg.AppendLine("121100100390021" + obj40X60.Cp09Bar);
                //msg.AppendLine("121100100390202" + obj40X60.Cp09Bar);

                //msg.AppendLine("121100100260021" + obj40X60.Cp10Bar);
                //msg.AppendLine("121100100260202" + obj40X60.Cp10Bar);

                //msg.AppendLine("121100100130021" + obj40X60.Cp11Bar);
                //msg.AppendLine("121100100130202" + obj40X60.Cp11Bar);

                //msg.AppendLine("121100100000021 " + referenciaEncontrada);
                //msg.AppendLine("121100100000202 " + referenciaEncontrada);

                msg.AppendLine("Q");
                msg.AppendLine("E");
                msg.AppendLine("");

                string texto = msg.ToString();

                string nome_arquivo = caminhoConfiguracao + @"\etiq.prn";

                if (!File.Exists(nome_arquivo))
                {
                    File.Create(nome_arquivo).Close();
                }
                else
                {
                    File.Delete(nome_arquivo);
                    File.Create(nome_arquivo).Close();
                }

                TextWriter arquivo = File.AppendText(nome_arquivo);
                arquivo.WriteLine(texto);
                arquivo.Close();

                System.Diagnostics.Process.Start(caminhoConfiguracao + @"\etiq.bat");

                System.Threading.Thread.Sleep(2000); // pausa por 2 segundos (2000 ms)

                //atualizar etiquetas no AS400
            }
        }
    }
}
