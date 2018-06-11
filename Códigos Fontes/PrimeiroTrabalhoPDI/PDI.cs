//NOME:   Vitor Vinicius Gomes da Silva
//RA:     1581775
//TRABALHO PRÁTICO 1 - Imprementações neste arquivo

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimeiroTrabalhoPDI
{
    public partial class PDI : Form
    {
       
        Mat image;
        Mat greyImage = new Mat();

        
        
        Mat ObterNegativo()
        {
            Mat negativo = greyImage.Clone();
            for (int x = 0; x < negativo.Rows; x++)
            {
                for (int y = 0; y < negativo.Cols; y++)
                {
                    byte  pixel = negativo.At<byte>(x, y);
                    negativo.Set<byte >(x, y, (byte)( 255 - pixel)) ;
                }
            }
            return negativo;
        }

        Mat AjustarContraste()
        {
            Mat cont = greyImage.Clone();
            byte  fmax = 0; byte  fmin = 255;
            for (int x = 0; x < cont.Rows; x++)
            {
                for (int y = 0; y < cont.Cols; y++)
                {
                    byte  pixel = cont.At<byte >(x, y);
                    if (pixel > fmax) fmax = pixel;
                    if (pixel < fmin) fmin = pixel;
                }
            }
            byte  gmax = 255; byte  gmin = 0;
            for (int x = 0; x < cont.Rows; x++)
            {
                for (int y = 0; y < cont.Cols; y++)
                {
                    byte  f = cont.At<byte >(x, y);
                    cont.Set<byte>(x, y, (byte)(((gmax - gmin) / (fmax - fmin)) * (f - fmin) + gmin));
                }
            }
            return cont;
        }

        Mat ObterHistograma(out int[] histograma)
        {
            histograma =  new int[256];
            for (int i = 0; i < 256; i++) histograma[i] = 0;
            
            int nivel = 0;
            for (int x = 0; x < greyImage.Rows; x++)
            {
                for (int y = 0; y < greyImage.Cols; y++)
                {
                    nivel =greyImage.At<byte >(x, y);
                    histograma[nivel] += 1;
                }
            }
            int maior = 0;
            for (int i = 0; i < 256; i++)
            {
                if (histograma[i] > maior) maior = histograma[i];
            }
            
            int[] hn = new int[256];
            for (int i = 0; i < 256; i++)
            {
                hn[i] =(int)Math.Round((decimal)(histograma[i] * 255 / maior));
            }
            int altura = 256; int largura = 512;
            Mat imagemH = new Mat(altura, largura, MatType.CV_8UC3, new Scalar(255,255,255));
            var pt1 =new Point(0, 0);
            var pt2 = new Point(0, 0);
            for (int i = 0; i < altura; i++)
            {
                pt1.X = i * 2; pt1.Y = altura - 1;
                pt2.X = i * 2; pt2.Y = pt1.Y - hn[i];
               Cv2.Line(imagemH, pt1, pt2, new Scalar(255, 100, 50 + hn[i]),1, LineTypes.Link8);
                
            }

            return imagemH;
        }

        Mat TransfLogaritimica()
        {
            Mat logImage = greyImage.Clone();
          
            byte  fmax = 0; byte  fmin = 255;
            for (int x = 0; x < logImage.Rows; x++)
            {
                for (int y = 0; y < logImage.Cols; y++)
                {
                    byte  pixel = logImage.At<byte >(x, y);
                    if (pixel > fmax) fmax = pixel;
                    if (pixel < fmin) fmin = pixel;
                }
            }
            for (int x = 0; x < logImage.Rows; x++)
            {
                for (int y = 0; y < logImage.Cols; y++)
                {
                    byte  f = logImage.At<byte >(x, y);
                    double alpha = 255 / Math.Log(1 + fmax);
                    double resultado = alpha * Math.Log(f + 1);
                    logImage.Set<byte >(x, y, (byte)resultado) ;
                }
            }
            return logImage;
        }

        Mat TransfPotencia()
        {
            Mat imgResult = greyImage.Clone();
            for (int x = 0; x < imgResult.Rows; x++)
            {
                for (int y = 0; y < imgResult.Cols; y++)
                {
                    byte  pixel = imgResult.At<byte >(x, y);
                    double alpha = 2;
                    double constant = 1;
                    double result = alpha * Math.Pow(pixel, constant);
                    imgResult.Set<byte >(x, y, (byte)result) ;
                }
            }
            return imgResult;
        }


        Mat EqualizarHistograma()
        {
            Mat img = greyImage.Clone();

            int[] hist;
            ObterHistograma(out hist);
            var area = image.Rows * image.Cols;
            decimal[] p = hist.Select(x => ((decimal)x) / area).ToArray();

            decimal[] pa = new decimal[256];
            for(int i=0;i<256; i++)
            {
                decimal amount = 0;
                for (int j = 0; j <= i; j++)
                {
                    amount += p[j];
                }
                pa[i] = amount;
            }
            int[] s = new int[256];
            for (int i = 0; i < 256; i++)
            {
                s[i] = (int)Math.Round((255 * pa[i]));
            }

            for (int x = 0; x < img.Rows; x++)
            {
                for (int y = 0; y < img.Cols; y++)
                {
                    byte pixel = img.At<byte>(x, y);
                    img.Set<byte>(x, y, (byte)s[pixel]);
                }
            }


            return img;
        }


        Mat AplicarFiltroMedia()
        {
            Mat copy = greyImage.Clone();
            Mat imgResult = greyImage.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {
                    int sum = 0;

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                           

                            if (((x + i) < 0 && (y + j) < 0) || ((x + i) >= copy.Rows && (y + j) >= copy.Cols)) sum += copy.At<byte >(x - i, y - j);
                            else if (((x + i) < 0) || ((x + i) >= copy.Rows)) sum += copy.At<byte >(x - i, y + j);
                            else if (((y + i) < 0) || ((y + i) >= copy.Cols)) sum += copy.At<byte >(x + i, y - j);
                            

                            else sum += copy.At<byte >(x + i, y + j);
                        }
                    }
                    imgResult.Set<byte >(x, y,(byte)(sum/9));
                }
            }
            return imgResult;
        }

        Mat AplicarFiltroMediana()
        {
            Mat copy = greyImage.Clone();
            Mat imgResult = greyImage.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {

                    List <byte> nums =  new List<byte>();

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {


                            if (((x + i) < 0 && (y + j) < 0) || ((x + i) >= copy.Rows && (y + j) >= copy.Cols))
                                nums.Add(copy.At<byte>(x - i, y - j));

                            else 
                                if (((x + i) < 0) || ((x + i) >= copy.Rows))
                                nums.Add(copy.At<byte>(x - i, y + j));

                            else 
                                if (((y + i) < 0) || ((y + i) >= copy.Cols))
                                nums.Add(copy.At<byte>(x + i, y - j));

                            else
                                nums.Add(copy.At<byte>(x + i, y + j));
                        }
                    }
                    imgResult.Set<byte>(x, y, nums.OrderByDescending(value=>value).ToArray()[5]);
                }
            }
            return imgResult;
        }


        Mat AplicarFiltroMediana(Mat img)
        {
            Mat copy = img.Clone();
            Mat imgResult = img.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {

                    List<byte> nums = new List<byte>();

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {


                            if (((x + i) < 0 && (y + j) < 0) || ((x + i) >= copy.Rows && (y + j) >= copy.Cols))
                                nums.Add(copy.At<byte>(x - i, y - j));

                            else
                                if (((x + i) < 0) || ((x + i) >= copy.Rows))
                                nums.Add(copy.At<byte>(x - i, y + j));

                            else
                                if (((y + i) < 0) || ((y + i) >= copy.Cols))
                                nums.Add(copy.At<byte>(x + i, y - j));

                            else
                                nums.Add(copy.At<byte>(x + i, y + j));
                        }
                    }
                    imgResult.Set<byte>(x, y, nums.OrderByDescending(value => value).ToArray()[5]);
                }
            }
            return imgResult;
        }

        Mat AplicarFiltroGaussiana()
        {
            
            float[][] kernel = new float[5][];
            kernel[0] = new float[5] { 1, 4, 7, 4, 1 };
            kernel[1] = new float[5] { 4, 16, 26, 16, 4};
            kernel[2] = new float[5] { 7, 26, 41, 26, 7 };
            kernel[3] = new float[5] { 4, 16, 26, 16, 4 };
            kernel[4] = new float[5] { 1, 4, 7, 4, 1 };
            //Mascara 5x5 com σ=1
            Mat copy = greyImage.Clone();
            Mat imgResult = greyImage.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {
                    int sum = 0;
                    int interects = 0;
                    for (int i = -1; i < 4; i++)
                    {
                        for (int j = -1; j < 4; j++)
                        {

                            int value = 0;
                            if (((x + i) < 0 && (y + j) < 0) || ((x + i) >= copy.Rows && (y + j) >= copy.Cols))
                                value = copy.At<byte>(x - i, y - j);
                            else if (((x + i) < 0) || ((x + i) >= copy.Rows))
                                value = copy.At<byte>(x - i, y + j);
                            else if (((y + i) < 0) || ((y + i) >= copy.Cols))
                                value = copy.At<byte>(x + i, y - j);


                            else
                                value += copy.At<byte>(x + i, y + j);

                            value = (int)(value * kernel[i + 1][j + 1]/273);

                            sum += value;
                            interects++;
                        }
                    }
                    imgResult.Set<byte>(x, y, (byte)(sum ));
                }
            }


            return imgResult;
        }


        Mat AplicarFiltroGaussiana(Mat img)
        {

            float[][] kernel = new float[5][];
            kernel[0] = new float[5] { 1, 4, 7, 4, 1 };
            kernel[1] = new float[5] { 4, 16, 26, 16, 4 };
            kernel[2] = new float[5] { 7, 26, 41, 26, 7 };
            kernel[3] = new float[5] { 4, 16, 26, 16, 4 };
            kernel[4] = new float[5] { 1, 4, 7, 4, 1 };
            //Mascara 5x5 com σ=1
            Mat copy = img.Clone();
            Mat imgResult = img.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {
                    int sum = 0;
                    int interects = 0;
                    for (int i = -1; i < 4; i++)
                    {
                        for (int j = -1; j < 4; j++)
                        {

                            int value = 0;
                            if (((x + i) < 0 && (y + j) < 0) || ((x + i) >= copy.Rows && (y + j) >= copy.Cols))
                                value = copy.At<byte>(x - i, y - j);
                            else if (((x + i) < 0) || ((x + i) >= copy.Rows))
                                value = copy.At<byte>(x - i, y + j);
                            else if (((y + i) < 0) || ((y + i) >= copy.Cols))
                                value = copy.At<byte>(x + i, y - j);


                            else
                                value += copy.At<byte>(x + i, y + j);

                            value = (int)(value * kernel[i + 1][j + 1] / 273);

                            sum += value;
                            interects++;
                        }
                    }
                    imgResult.Set<byte>(x, y, (byte)(sum));
                }
            }


            return imgResult;
        }


        Mat AplicarFiltroMaximo()
        {
            Mat copy = greyImage.Clone();
            Mat imgResult = greyImage.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {

                    List<byte> nums = new List<byte>();

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {


                            if (((x + i) < 0 && (y + j) < 0) || ((x + i) >= copy.Rows && (y + j) >= copy.Cols))
                                nums.Add(copy.At<byte>(x - i, y - j));

                            else
                                if (((x + i) < 0) || ((x + i) >= copy.Rows))
                                nums.Add(copy.At<byte>(x - i, y + j));

                            else
                                if (((y + i) < 0) || ((y + i) >= copy.Cols))
                                nums.Add(copy.At<byte>(x + i, y - j));

                            else
                                nums.Add(copy.At<byte>(x + i, y + j));
                        }
                    }
                    imgResult.Set<byte>(x, y, nums.Max());
                }
            }
            return imgResult;
        }

        Mat AplicarFiltroMinimo()
        {
            Mat copy = greyImage.Clone();
            Mat imgResult = greyImage.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {

                    List<byte> nums = new List<byte>();

                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {


                            if (((x + i) < 0 && (y + j) < 0) || ((x + i) >= copy.Rows && (y + j) >= copy.Cols))
                                nums.Add(copy.At<byte>(x - i, y - j));

                            else
                                if (((x + i) < 0) || ((x + i) >= copy.Rows))
                                nums.Add(copy.At<byte>(x - i, y + j));

                            else
                                if (((y + i) < 0) || ((y + i) >= copy.Cols))
                                nums.Add(copy.At<byte>(x + i, y - j));

                            else
                                nums.Add(copy.At<byte>(x + i, y + j));
                        }
                    }
                    imgResult.Set<byte>(x, y, nums.Min());
                }
            }
            return imgResult;
        }

        Mat AjustarBrilhoHSV()
        {
            Mat imagemHSV = new Mat();
            //converte uma imagem RGB para HSV
            Cv2.CvtColor(image, imagemHSV,ColorConversionCodes.BGR2HSV);
            Mat[] planosHSV;
            //divide a imagem HSV em 3 planos de pixels
            Cv2.Split(imagemHSV, out planosHSV);
            //split(imagemHSV, planosHSV);
            //obtem apenas o plano V
            Mat V = planosHSV[2];
            //percorre apenas o plano V
            byte  brilho = 0;
            int k = 50; //constante para aumentar ou diminuir o brilho
            for (int x = 0; x < imagemHSV.Rows; x++)
            {
                for (int y = 0; y < imagemHSV.Cols; y++)
                {
                    brilho = V.At<byte >(x, y);
                    if ((brilho + k) < 0) brilho = 0;
                    else if ((brilho + k) > 255) brilho = 255;
                    else brilho += (byte)k;
                    V.Set<byte >(x, y, brilho);
                }
            }
            //combina os 3 planos de pixels (H,S,V) novamente
           Cv2.Merge(planosHSV,  imagemHSV);
            Mat imagemSaida = new Mat();
            //converte uma imagem HSV para RGB
            Cv2.CvtColor(imagemHSV, imagemSaida, ColorConversionCodes.HSV2BGR);
            return imagemSaida;
        }

        Mat EqualizarHistogramaHSV()
        {
            Mat imagemHSV = new Mat();
            //converte uma imagem RGB para HSV
            Cv2.CvtColor(image, imagemHSV, ColorConversionCodes.BGR2HSV);
            Mat[] planosHSV;
            //divide a imagem HSV em 3 planos de pixels
            Cv2.Split(imagemHSV, out planosHSV);
            //split(imagemHSV, planosHSV);
            //obtem apenas o plano H
            Mat H = planosHSV[2];
            int[] histograma = new int[256];
            for (int i = 0; i < 256; i++) histograma[i] = 0;

            int nivel = 0;
            for (int x = 0; x < H.Rows; x++)
            {
                for (int y = 0; y < H.Cols; y++)
                {
                    nivel = H.At<byte>(x, y);
                    histograma[nivel] += 1;
                }
            }
            //percorre apenas o plano H

            var area = H.Rows * H.Cols;
            decimal[] p = histograma.Select(x => ((decimal)x) / area).ToArray();

            decimal[] pa = new decimal[256];
            for (int i = 0; i < 256; i++)
            {
                decimal amount = 0;
                for (int j = 0; j <= i; j++)
                {
                    amount += p[j];
                }
                pa[i] = amount;
            }
            int[] s = new int[256];
            for (int i = 0; i < 256; i++)
            {
                s[i] = (int)Math.Round((255 * pa[i]));
            }

            for (int x = 0; x < H.Rows; x++)
            {
                for (int y = 0; y < H.Cols; y++)
                {
                    byte pixel = H.At<byte>(x, y);
                    H.Set<byte>(x, y, (byte)s[pixel]);
                }
            }

            //combina os 3 planos de pixels (H,S,V) novamente
            Cv2.Merge(planosHSV, imagemHSV);
            Mat imagemSaida = new Mat();
            //converte uma imagem HSV para RGB
            Cv2.CvtColor(imagemHSV, imagemSaida, ColorConversionCodes.HSV2BGR);
            return imagemSaida;
        }


        Mat AplicarFiltroGaussianaHSV()
        {
            Mat imagemHSV = new Mat();
            //converte uma imagem RGB para HSV
            Cv2.CvtColor(image, imagemHSV, ColorConversionCodes.BGR2HSV);
            Mat[] planosHSV;
            //divide a imagem HSV em 3 planos de pixels
            Cv2.Split(imagemHSV, out planosHSV);
            //split(imagemHSV, planosHSV);
            //obtem apenas o plano H
            
            planosHSV[2] = AplicarFiltroGaussiana(planosHSV[2]);
            //combina os 3 planos de pixels (H,S,V) novamente
            Cv2.Merge(planosHSV, imagemHSV);
            Mat imagemSaida = new Mat();
            //converte uma imagem HSV para RGB
            Cv2.CvtColor(imagemHSV, imagemSaida, ColorConversionCodes.HSV2BGR);
            return imagemSaida;
        }

        Mat AplicarFiltroMedianaHSV()
        {
            Mat imagemHSV = new Mat();
            //converte uma imagem RGB para HSV
            Cv2.CvtColor(image, imagemHSV, ColorConversionCodes.BGR2HSV);
            Mat[] planosHSV;
            //divide a imagem HSV em 3 planos de pixels
            Cv2.Split(imagemHSV, out planosHSV);
            //split(imagemHSV, planosHSV);
            //obtem apenas o plano H

            planosHSV[2] = AplicarFiltroMediana(planosHSV[2]);
            //combina os 3 planos de pixels (H,S,V) novamente
            Cv2.Merge(planosHSV, imagemHSV);
            Mat imagemSaida = new Mat();
            //converte uma imagem HSV para RGB
            Cv2.CvtColor(imagemHSV, imagemSaida, ColorConversionCodes.HSV2BGR);
            return imagemSaida;
        }


        #region Tratamento de Eventos da UI

        public PDI()
        {
            InitializeComponent();
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                image = new Mat(openFileDialog1.FileName);
                pictureBox1.Image = image.ToBitmap();
                image.CopyTo(greyImage);
                Cv2.CvtColor(image, greyImage, ColorConversionCodes.BGR2GRAY);
                pictureBox2.Image = greyImage.ToBitmap();

            }
        }

        private void realceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void logarítimicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.TransfLogaritimica().ToBitmap();
        }

        private void potênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.TransfPotencia().ToBitmap();
        }

        private void negativoDaImagemInverterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.ObterNegativo().ToBitmap();
        }

        private void desenharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] hist;
            pictureBox3.Image = ObterHistograma(out hist).ToBitmap();
        }

        private void contrasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AjustarContraste().ToBitmap();
        }

        private void mediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AplicarFiltroMedia().ToBitmap();
        }

        private void equalizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.EqualizarHistograma().ToBitmap();

        }


        private void medianaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AplicarFiltroMediana().ToBitmap();
        }

        private void máximoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AplicarFiltroMaximo().ToBitmap();
        }

        private void mínimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AplicarFiltroMinimo().ToBitmap();
        }

        private void gaussianaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AplicarFiltroGaussiana().ToBitmap();
        }

        private void equalizarColoridoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.EqualizarHistogramaHSV().ToBitmap();
        }

        private void gaussianoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AplicarFiltroGaussianaHSV().ToBitmap();
        }

        private void medianaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AplicarFiltroMedianaHSV().ToBitmap();

        }

        private void brilhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = this.AjustarBrilhoHSV().ToBitmap();
        } 
        #endregion
    }
}
