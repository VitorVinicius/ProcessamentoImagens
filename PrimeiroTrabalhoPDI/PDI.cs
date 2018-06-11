//NOME:   Vitor Vinicius Gomes da Silva
//RA:     1581775
//TRABALHO PRÁTICO 1 - Imprementações neste arquivo

using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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

        Mat ObterHistograma(out int[] histograma, Mat imagem)
        {
            histograma = new int[256];
            for (int i = 0; i < 256; i++) histograma[i] = 0;

            int nivel = 0;
            for (int x = 0; x < imagem.Rows; x++)
            {
                for (int y = 0; y < imagem.Cols; y++)
                {
                    nivel = imagem.At<byte>(x, y);
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
                hn[i] = (int)Math.Round((decimal)(histograma[i] * 255 / maior));
            }
            int altura = 256; int largura = 512;
            Mat imagemH = new Mat(altura, largura, MatType.CV_8UC3, new Scalar(255, 255, 255));
            var pt1 = new Point(0, 0);
            var pt2 = new Point(0, 0);
            for (int i = 0; i < altura; i++)
            {
                pt1.X = i * 2; pt1.Y = altura - 1;
                pt2.X = i * 2; pt2.Y = pt1.Y - hn[i];
                Cv2.Line(imagemH, pt1, pt2, new Scalar(255, 100, 50 + hn[i]), 1, LineTypes.Link8);

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


       void DetectorSobel()
        {
            Mat cinza = this.greyImage.Clone();
            Mat saidaBlur = new Mat() ;
            Cv2.Blur(cinza, saidaBlur, new OpenCvSharp.Size(5, 5));
            Mat saidaSobelX = new Mat();
            Cv2.Sobel(saidaBlur, saidaSobelX, saidaBlur.Type(), 1,0);

            Mat saidaSobelY = new Mat();
            Cv2.Sobel(saidaBlur, saidaSobelY, saidaBlur.Type(), 0, 1);
            saidaSobelX = saidaSobelX + saidaSobelY;

            //Cv2.ImShow("Saida Canny",saidaCanny);
            pictureBox3.Image = saidaSobelX.ToBitmap();

        }

        void DetectorCanny()
        {
            Mat cinza = this.greyImage.Clone();
            Mat saidaBlur = new Mat();
            Cv2.Blur(cinza, saidaBlur, new OpenCvSharp.Size(5, 5));
            Mat saidaCanny = new Mat();
            Cv2.Canny(saidaBlur, saidaCanny, 0, 255);
            //Cv2.ImShow("Saida Canny",saidaCanny);
            pictureBox3.Image = saidaCanny.ToBitmap();

        }

        Mat DetectorCanny(Mat cinza)
        {
            Mat saidaBlur = new Mat();
            Cv2.Blur(cinza, saidaBlur, new OpenCvSharp.Size(5, 5));
            Mat saidaCanny = new Mat();
            Cv2.Canny(saidaBlur, saidaCanny, 0, 255);
            //Cv2.ImShow("Saida Canny",saidaCanny);
            return saidaCanny;

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
            //obtem apenas o plano V
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
            //percorre apenas o plano V

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
            //obtem apenas o plano V
            
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
            //obtem apenas o plano V

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
                abrirImagem(openFileDialog1.FileName);
            }
        }

        private void abrirImagem(string path)
        {
            image = new Mat(path);
            pictureBox1.Image = image.ToBitmap();
            image.CopyTo(greyImage);
            Cv2.CvtColor(image, greyImage, ColorConversionCodes.BGR2GRAY);
            pictureBox2.Image = greyImage.ToBitmap();
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

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectorCanny();
        }

        private void robertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectorSobel();
        }

        private void limiarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void PDI_Load(object sender, EventArgs e)
        {

        }

        private void crescimentoDeRegiõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Click += CrescimentoRegioesPegarSeed;
        }
        PropertyInfo imageRectangleProperty = typeof(PictureBox).GetProperty("ImageRectangle", BindingFlags.GetProperty | BindingFlags.NonPublic | BindingFlags.Instance);

        private void CrescimentoRegioesPegarSeed(object sender, EventArgs e)
        {

            if (pictureBox1.Image != null)
            {
                MouseEventArgs me = (MouseEventArgs)e;

                System.Drawing.Bitmap original = (System.Drawing.Bitmap)pictureBox1.Image;

                System.Drawing.Color? color = null;
                switch (pictureBox1.SizeMode)
                {
                    case PictureBoxSizeMode.Normal:
                    case PictureBoxSizeMode.AutoSize:
                        {
                            color = original.GetPixel(me.X, me.Y);
                            break;
                        }
                    case PictureBoxSizeMode.CenterImage:
                    case PictureBoxSizeMode.StretchImage:
                    case PictureBoxSizeMode.Zoom:
                        {

                            System.Drawing.Rectangle rectangle = (System.Drawing.Rectangle)imageRectangleProperty.GetValue(pictureBox1, null);
                            if (rectangle.Contains(me.Location))
                            {
                                using (System.Drawing.Bitmap copy = new System.Drawing.Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height))
                                {
                                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(copy))
                                    {
                                        g.DrawImage(pictureBox1.Image, rectangle);

                                        color = copy.GetPixel(me.X, me.Y);
                                    }
                                }
                            }
                            break;
                        }
                }

                if (!color.HasValue)
                {
                    //User clicked somewhere there is no image
                }
                else
                {
                    //use color.Value

                    Mat newImage = CrescimentoRegioes(greyImage, new  Point(me.X, me.Y));

                    pictureBox3.Image = newImage.ToBitmap();

                    pictureBox1.Click -= CrescimentoRegioesPegarSeed;
                }
            }

            
        }

        private Mat CrescimentoRegioes(Mat img, Point seed)
        {
            MessageBox.Show("Ainda nao esta funcionando direito. Fica num loop infinito e trava o pc. Terminar isso.");
            return img;
            //# Parameters for region growing
            List<Point> neighbors = new List<Point> { new Point(-1, 0), new Point(1, 0), new Point(0, -1), new Point(0, 1) };
             
            System.Collections.Queue neighbor_points_list = new System.Collections.Queue();
            System.Collections.Queue neighbor_intensity_list = new System.Collections.Queue();
            int height = img.Rows;
            int width = img.Cols;
            long image_size = img.Cols * img.Rows;

            //#Initialize segmented output image
            Mat segmented_img = new Mat(height, width, MatType.CV_8UC1, Scalar.Black);
            //# Region growing until intensity difference becomes greater than certain threshold
            neighbor_points_list.Enqueue(seed);
            var seedValue = img.At<byte>(seed.X, seed.Y);
            neighbor_intensity_list.Enqueue(seedValue);
            while (neighbor_points_list.Count != 0)
            {
                Point p = (Point) neighbor_points_list.Dequeue();
                byte pValue = (byte) neighbor_intensity_list.Dequeue();

                for (int i = 0; i < 4; i++)
                {
                    var x_new = p.X + neighbors[i].X;
                    var y_new = p.Y + neighbors[i].Y;

                    bool check_inside = (x_new >= 0) && (y_new >= 0) && (x_new < height) && (y_new < width);
                    if (check_inside)
                    {
                        neighbor_points_list.Enqueue(new Point(x_new, y_new));
                        neighbor_intensity_list.Enqueue(img.At<byte>(x_new, y_new));
                    }
                }
                
                if(p.X == seed.X && p.Y == seed.Y && pValue == seedValue)
                {
                    segmented_img.Set<byte>(p.X, p.Y, seedValue);
                }
                else
                {
                    if(Math.Abs(pValue - seedValue) <= 5)
                        segmented_img.Set<byte>(p.X, p.Y, pValue);
                }
            }
            return segmented_img;
        }

        private static int GetLimiarValue()
        {
            bool continuar = true;
           // while (continuar)
            {
                try
                {
                    InputDialog input = new InputDialog("Informe o valor do limiar:");
                    if (input.ShowDialog() == DialogResult.OK)
                    {
                        
                        return int.Parse(input.Value);
                    }
                    continuar = false;
                }
                catch
                {
                    continuar = true;
                }
            }
            return 5;
        }

        private void aberturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat cinza = this.greyImage.Clone();
            Mat saida = Erosao(cinza);
            saida = Dilatacao(saida);
            pictureBox3.Image = saida.ToBitmap();
        }

        private Mat Dilatacao(Mat img)
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
                    imgResult.Set<byte>(x, y, nums.Max());
                }
            }
            return imgResult;
        }

        private Mat Erosao(Mat img)
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
                    imgResult.Set<byte>(x, y, nums.Min());
                }
            }
            return imgResult;
        }

        private void erosaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat cinza = (this.greyImage.Clone());
            Mat saida = Erosao(cinza);
            pictureBox3.Image = saida.ToBitmap();
        }

        private void dilataçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat cinza = (this.greyImage.Clone());
            Mat saida = Dilatacao(cinza);
            pictureBox3.Image = saida.ToBitmap();
        }

        private void fechamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat cinza = this.greyImage.Clone();
            Mat saida = Dilatacao(cinza);
            saida = Erosao(saida);
            pictureBox3.Image = saida.ToBitmap();
        }

        private void thresholdingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat cinza = this.greyImage.Clone();
            Mat saida = Thresholding(cinza, GetLimiarValue());
            pictureBox3.Image = saida.ToBitmap();
        }

        private Mat Thresholding(Mat cinza, int limiar)
        {
            Mat copy = cinza.Clone();
            Mat imgResult = cinza.Clone();

            for (int x = 0; x < copy.Rows; x++)
            {
                for (int y = 0; y < copy.Cols; y++)
                {
                    var pixelValue = copy.At<byte>(x, y);
                    imgResult.Set<byte>(x, y, (byte)(pixelValue>= limiar?255:0));
                }
            }
            return imgResult;
        }

        private void imagemAtualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mat cinza = this.greyImage.Clone();
            Cv2.ImShow("1", cinza);
            Mat img = Thresholding(cinza, GetLimiarValue());
            Cv2.ImShow("2", img);
            img = DetectorCanny(img);
            Cv2.ImShow("3", img);
            img = Dilatacao(img);
            Cv2.ImShow("4", img);
            img = Erosao(img);
            Cv2.ImShow("5", img);
            Point[][] contornos = null;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(img, out contornos, out hierarchy, RetrievalModes.CComp, ContourApproximationModes.ApproxNone);

            /// Draw contours
            Random rng = new Random();

            Mat drawing = Mat.Zeros(img.Size(), MatType.CV_8UC3);
            for (int i = 0; i < contornos.Length; i++)
            {
                Scalar color = new Scalar(rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
                Cv2.DrawContours(drawing, contornos, i, color, 2, LineTypes.Link8, hierarchy, 0, new Point());
            }
            Cv2.ImShow("6", drawing);
            Point[] maiorContorno = contornos.OrderByDescending(x=>x.Length).First();

            

            pictureBox3.Image = drawing.ToBitmap();
            List<int> cadeia = CodigoCadeia(maiorContorno);
           


            Clipboard.SetText(string.Join("", cadeia));
            MessageBox.Show("Código da Cadeia "+ string.Join("", cadeia) +". Código copiado para área de transferência");
        }

        private List<int> CodigoCadeia(Point[] contorno)
        {

            List<int> cadeia = new List<int> { };
            for(int i =0; i< contorno.Length; i++)

            {
                var atual = contorno[i];

                if(i + 1 < contorno.Length)
                {
                    Point proximo = contorno[i+1];


                    if ((atual.X + 1) == proximo.X && (atual.Y) == proximo.Y)
                    {
                        cadeia.Add(0);
                    }else
                    if ((atual.X + 1) == proximo.X && (atual.Y + 1) == proximo.Y)
                    {
                        cadeia.Add(1);
                    }
                    else
                    if ((atual.X) == proximo.X && (atual.Y+1) == proximo.Y)
                    {
                        cadeia.Add(2);
                    }
                    else
                    if ((atual.X-1) == proximo.X && (atual.Y + 1) == proximo.Y)
                    {
                        cadeia.Add(3);
                    }
                    else
                    if ((atual.X - 1) == proximo.X && (atual.Y) == proximo.Y)
                    {
                        cadeia.Add(4);
                    }
                    else
                    if ((atual.X - 1) == proximo.X && (atual.Y-1) == proximo.Y)
                    {
                        cadeia.Add(5);
                    }
                    else
                    if ((atual.X) == proximo.X && (atual.Y-1) == proximo.Y)
                    {
                        cadeia.Add(6);
                    }
                    else
                    if ((atual.X+1) == proximo.X && (atual.Y - 1) == proximo.Y)
                    {
                        cadeia.Add(7);
                    }
                    else
                    {
                        cadeia.Add(8);
                    }

                }


            }

            List<int> cadeiaOrganizada = new List<int>();
            int menor = cadeia.Min();
            int index = cadeia.IndexOf(menor);
            for (int i = index; i < cadeia.Count; i++)
            {
                cadeiaOrganizada.Add(cadeia[i]);
            }
            for (int i = 0; i < index; i++)
            {
                cadeiaOrganizada.Add(cadeia[i]);
            }

            return cadeiaOrganizada;
        }


        Mat colorReduce(Mat image, int div = 64)
        {

            image = image.Clone();

            for (int x = 0; x < image.Rows; x++)
            {
                for (int y = 0; y < image.Cols; y++)
                {
                    var data = image.At<Vec3b>(x, y);

                    data[0] = (byte) (data[0] / div * div + div / 2);
                    data[1] = (byte)(data[1] / div * div + div / 2);
                    data[2] = (byte)(data[2] / div * div + div / 2);

                    image.Set<Vec3b>(x, y,data);

                }
            }
            return image;
        }

        private void multiplasImagensCódigoDaCadeiaEmARFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            OpenFileDialog openFile = new OpenFileDialog();
            var codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            var codecFilter = "Image Files|";
            foreach (var codec in codecs)
            {
                codecFilter += "*_"+codec.FilenameExtension + ";";
            }
            openFile.Filter = codecFilter;

            openFile.Multiselect = true;

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                List<string> codigos = new List<string>();
                int limiar = GetLimiarValue();
                List<string> classes = new List<string>();
                foreach (var imagePath in openFile.FileNames)
                {

                    Mat imagem = new Mat(imagePath);
                    Mat cinza = imagem.CvtColor(ColorConversionCodes.BGR2GRAY);
                    
                    Mat img = Thresholding(cinza, limiar);
                    img = DetectorCanny(img);
                    img = Dilatacao(img);
                    img = Erosao(img);
                    Point[][] contornos = null;
                    HierarchyIndex[] hierarchy;
                    Cv2.FindContours(img, out contornos, out hierarchy, RetrievalModes.CComp, ContourApproximationModes.ApproxNone);

                    Point[] maiorContorno = contornos.OrderByDescending(x => x.Length).First();

                    List<int> cadeia = CodigoCadeia(maiorContorno);

                    int pFrom = imagePath.IndexOf("_") + "_".Length;
                    int pTo = imagePath.LastIndexOf(".");

                    string classe = imagePath.Substring(pFrom, pTo - pFrom);
                    if(!classes.Contains(classe))
                        classes.Add(classe);
                    
                    codigos.Add(string.Join("", cadeia) + ", " + classe);
                }
                SaveFileDialog saveDialog = new SaveFileDialog();
                InputDialog input = new InputDialog("Informe o nome da relação que será criada:");
                string relacao = null;
                if (input.ShowDialog() == DialogResult.OK)
                {

                    relacao = (input.Value);
                }
                if (string.IsNullOrEmpty(relacao))
                {
                    MessageBox.Show("O nome da relacao é obrigatório.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                
                if (string.IsNullOrEmpty(relacao))
                {
                    MessageBox.Show("O nome da classe é obrigatório.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                saveDialog.FileName = relacao + ".ARFF";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    String cabecalho = "@RELATION "+relacao+ "\r\n\r\n   @ATTRIBUTE codigo  STRING\r\n\r\n   @ATTRIBUTE class        {"+string.Join(",", classes) +"}\n\n @DATA";
                    File.WriteAllText(saveDialog.FileName,cabecalho);
                    foreach(var linha in codigos)
                    {
                       
                        
                        File.AppendAllText(saveDialog.FileName,"\n" + linha);
                    }
                }
            }
        }

        private void bicMenuItem_Click(object sender, EventArgs e)
        {
            var newImage = greyImage.Clone();
            byte[] histInterior, histBorda;
            List<byte> histogramaSaida;
            BIC(newImage, out histInterior, out histBorda, out histogramaSaida);

            string saidaTexto = string.Join(",", histogramaSaida);
            Clipboard.SetText(saidaTexto);
            MessageBox.Show("Histograma BIC (Borda): " + string.Join(",", histBorda) + "\nHistograma BIC (Interior): " + string.Join(",", histInterior) + "\n\n Vetor Final copiado para área de transferência: " + saidaTexto);

            for (int x = 0; x < newImage.Rows; x++)
            {
                for (int y = 0; y < newImage.Cols; y++)
                {
                    byte pixel = newImage.At<byte>(x, y);
                    newImage.Set<byte>(x, y, (byte)histogramaSaida[pixel]);
                }
            }

            pictureBox3.Image = newImage.ToBitmap();
        }

        private static void BIC(Mat newImage, out byte[] histInterior, out byte[] histBorda, out List<byte> histogramaSaida)
        {
            histInterior = new byte[64];
            histBorda = new byte[64];



            for (int x = 0; x < newImage.Rows; x++)
            {
                for (int y = 0; y < newImage.Cols; y++)
                {
                    var data = newImage.At<byte>(x, y);

                    data = (byte)((data * 63) / 255);

                    newImage.Set<byte>(x, y, data);

                }
            }

            byte[] histogramaGeral = new byte[64];

            histogramaSaida = new List<byte>();
            int nivel = 0;
            for (int x = 0; x < newImage.Rows; x++)
            {
                for (int y = 0; y < newImage.Cols; y++)
                {
                    nivel = newImage.At<byte>(x, y);

                    List<byte> nums = new List<byte>();
                    if (x + 1 < newImage.Cols)
                        nums.Add(newImage.At<byte>(x + 1, y));
                    if (x - 1 < newImage.Cols)
                        nums.Add(newImage.At<byte>(x - 1, y));
                    if (y + 1 < newImage.Rows)
                        nums.Add(newImage.At<byte>(x, y + 1));
                    if (y - 1 < newImage.Rows)
                        nums.Add(newImage.At<byte>(x, y - 1));
                    if (nums.Count == 4)
                    {
                        if (nums.Distinct().Skip(1).Any())
                        {
                            histInterior[nivel] += 1;
                        }
                        else
                        {
                            histBorda[nivel] += 1;
                        }
                    }
                    else
                    {

                    }
                    histogramaGeral[nivel] += 1;
                }
            }

            histogramaSaida.AddRange(histBorda);
            histogramaSaida.AddRange(histInterior);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image?.Save("temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            abrirImagem("temp.jpg");
        }
    }
}
