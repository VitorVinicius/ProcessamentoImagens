using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimeiroTrabalhoPDI
{
    public partial class InputDialog : Form
    {
        public InputDialog(string text)
        {
           
            InitializeComponent();
            label1.Text = text;
        }

        public string Value { get { return textBox1.Text; } }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
