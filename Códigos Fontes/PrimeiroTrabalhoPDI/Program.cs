//NOME:   Vitor Vinicius Gomes da Silva
//RA:     1581775
//TRABALHO PRÁTICO 1 - Imprementações no arquivo PDI.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimeiroTrabalhoPDI
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PDI());
        }
    }
}
