﻿//NOME:   Vitor Vinicius Gomes da Silva
//RA:     1581775

namespace PrimeiroTrabalhoPDI
{
    partial class PDI
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.arquivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transformaçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logarítimicaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.potênciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativoDaImagemInverterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrosPretoEBrancoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.máximoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mínimoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desenharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equalizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equalizarColoridoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.gaussianoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medianaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.brilhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 204);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivoToolStripMenuItem,
            this.realceToolStripMenuItem,
            this.histogramaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(748, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // arquivoToolStripMenuItem
            // 
            this.arquivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem});
            this.arquivoToolStripMenuItem.Name = "arquivoToolStripMenuItem";
            this.arquivoToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.arquivoToolStripMenuItem.Text = "Imagem";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // realceToolStripMenuItem
            // 
            this.realceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformaçõesToolStripMenuItem,
            this.negativoDaImagemInverterToolStripMenuItem,
            this.filtrosPretoEBrancoToolStripMenuItem,
            this.filtrosToolStripMenuItem,
            this.contrasteToolStripMenuItem,
            this.brilhoToolStripMenuItem});
            this.realceToolStripMenuItem.Name = "realceToolStripMenuItem";
            this.realceToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.realceToolStripMenuItem.Text = "Ajustes";
            this.realceToolStripMenuItem.Click += new System.EventHandler(this.realceToolStripMenuItem_Click);
            // 
            // transformaçõesToolStripMenuItem
            // 
            this.transformaçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logarítimicaToolStripMenuItem,
            this.potênciaToolStripMenuItem});
            this.transformaçõesToolStripMenuItem.Name = "transformaçõesToolStripMenuItem";
            this.transformaçõesToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.transformaçõesToolStripMenuItem.Text = "Transformações";
            // 
            // logarítimicaToolStripMenuItem
            // 
            this.logarítimicaToolStripMenuItem.Name = "logarítimicaToolStripMenuItem";
            this.logarítimicaToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.logarítimicaToolStripMenuItem.Text = "Logarítmica";
            this.logarítimicaToolStripMenuItem.Click += new System.EventHandler(this.logarítimicaToolStripMenuItem_Click);
            // 
            // potênciaToolStripMenuItem
            // 
            this.potênciaToolStripMenuItem.Name = "potênciaToolStripMenuItem";
            this.potênciaToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.potênciaToolStripMenuItem.Text = "Potência";
            this.potênciaToolStripMenuItem.Click += new System.EventHandler(this.potênciaToolStripMenuItem_Click);
            // 
            // negativoDaImagemInverterToolStripMenuItem
            // 
            this.negativoDaImagemInverterToolStripMenuItem.Name = "negativoDaImagemInverterToolStripMenuItem";
            this.negativoDaImagemInverterToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.negativoDaImagemInverterToolStripMenuItem.Text = "Negativo da Imagem (Inverter)";
            this.negativoDaImagemInverterToolStripMenuItem.Click += new System.EventHandler(this.negativoDaImagemInverterToolStripMenuItem_Click);
            // 
            // filtrosPretoEBrancoToolStripMenuItem
            // 
            this.filtrosPretoEBrancoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mediaToolStripMenuItem,
            this.medianaToolStripMenuItem,
            this.máximoToolStripMenuItem,
            this.mínimoToolStripMenuItem,
            this.gaussianaToolStripMenuItem});
            this.filtrosPretoEBrancoToolStripMenuItem.Name = "filtrosPretoEBrancoToolStripMenuItem";
            this.filtrosPretoEBrancoToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.filtrosPretoEBrancoToolStripMenuItem.Text = "Filtros (Monocromático)";
            // 
            // mediaToolStripMenuItem
            // 
            this.mediaToolStripMenuItem.Name = "mediaToolStripMenuItem";
            this.mediaToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.mediaToolStripMenuItem.Text = "Media";
            this.mediaToolStripMenuItem.Click += new System.EventHandler(this.mediaToolStripMenuItem_Click);
            // 
            // medianaToolStripMenuItem
            // 
            this.medianaToolStripMenuItem.Name = "medianaToolStripMenuItem";
            this.medianaToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.medianaToolStripMenuItem.Text = "Mediana";
            this.medianaToolStripMenuItem.Click += new System.EventHandler(this.medianaToolStripMenuItem_Click);
            // 
            // máximoToolStripMenuItem
            // 
            this.máximoToolStripMenuItem.Name = "máximoToolStripMenuItem";
            this.máximoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.máximoToolStripMenuItem.Text = "Máximo";
            this.máximoToolStripMenuItem.Click += new System.EventHandler(this.máximoToolStripMenuItem_Click);
            // 
            // mínimoToolStripMenuItem
            // 
            this.mínimoToolStripMenuItem.Name = "mínimoToolStripMenuItem";
            this.mínimoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.mínimoToolStripMenuItem.Text = "Mínimo";
            this.mínimoToolStripMenuItem.Click += new System.EventHandler(this.mínimoToolStripMenuItem_Click);
            // 
            // gaussianaToolStripMenuItem
            // 
            this.gaussianaToolStripMenuItem.Name = "gaussianaToolStripMenuItem";
            this.gaussianaToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.gaussianaToolStripMenuItem.Text = "Gaussiana";
            this.gaussianaToolStripMenuItem.Click += new System.EventHandler(this.gaussianaToolStripMenuItem_Click);
            // 
            // filtrosToolStripMenuItem
            // 
            this.filtrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gaussianoToolStripMenuItem,
            this.medianaToolStripMenuItem1});
            this.filtrosToolStripMenuItem.Name = "filtrosToolStripMenuItem";
            this.filtrosToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.filtrosToolStripMenuItem.Text = "Filtros (Colorida)";
            // 
            // contrasteToolStripMenuItem
            // 
            this.contrasteToolStripMenuItem.Name = "contrasteToolStripMenuItem";
            this.contrasteToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.contrasteToolStripMenuItem.Text = "Contraste";
            this.contrasteToolStripMenuItem.Click += new System.EventHandler(this.contrasteToolStripMenuItem_Click);
            // 
            // histogramaToolStripMenuItem
            // 
            this.histogramaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desenharToolStripMenuItem,
            this.equalizarToolStripMenuItem,
            this.equalizarColoridoToolStripMenuItem});
            this.histogramaToolStripMenuItem.Name = "histogramaToolStripMenuItem";
            this.histogramaToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.histogramaToolStripMenuItem.Text = "Histograma";
            // 
            // desenharToolStripMenuItem
            // 
            this.desenharToolStripMenuItem.Name = "desenharToolStripMenuItem";
            this.desenharToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.desenharToolStripMenuItem.Text = "Desenhar";
            this.desenharToolStripMenuItem.Click += new System.EventHandler(this.desenharToolStripMenuItem_Click);
            // 
            // equalizarToolStripMenuItem
            // 
            this.equalizarToolStripMenuItem.Name = "equalizarToolStripMenuItem";
            this.equalizarToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.equalizarToolStripMenuItem.Text = "Equalizar Monocromático";
            this.equalizarToolStripMenuItem.Click += new System.EventHandler(this.equalizarToolStripMenuItem_Click);
            // 
            // equalizarColoridoToolStripMenuItem
            // 
            this.equalizarColoridoToolStripMenuItem.Name = "equalizarColoridoToolStripMenuItem";
            this.equalizarColoridoToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.equalizarColoridoToolStripMenuItem.Text = "Equalizar Colorido";
            this.equalizarColoridoToolStripMenuItem.Click += new System.EventHandler(this.equalizarColoridoToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(12, 291);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(317, 204);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(392, 148);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(344, 234);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 3;
            this.pictureBox3.TabStop = false;
            // 
            // gaussianoToolStripMenuItem
            // 
            this.gaussianoToolStripMenuItem.Name = "gaussianoToolStripMenuItem";
            this.gaussianoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.gaussianoToolStripMenuItem.Text = "Gaussiano";
            this.gaussianoToolStripMenuItem.Click += new System.EventHandler(this.gaussianoToolStripMenuItem_Click);
            // 
            // medianaToolStripMenuItem1
            // 
            this.medianaToolStripMenuItem1.Name = "medianaToolStripMenuItem1";
            this.medianaToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.medianaToolStripMenuItem1.Text = "Mediana";
            this.medianaToolStripMenuItem1.Click += new System.EventHandler(this.medianaToolStripMenuItem1_Click);
            // 
            // brilhoToolStripMenuItem
            // 
            this.brilhoToolStripMenuItem.Name = "brilhoToolStripMenuItem";
            this.brilhoToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.brilhoToolStripMenuItem.Text = "Brilho";
            this.brilhoToolStripMenuItem.Click += new System.EventHandler(this.brilhoToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(389, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Resultado";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Imagem";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Imagem Monocromatica";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 512);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Trab. PDI";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem arquivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripMenuItem realceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transformaçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logarítimicaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem potênciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativoDaImagemInverterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtrosPretoEBrancoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desenharToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equalizarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem máximoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mínimoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equalizarColoridoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medianaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem brilhoToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

