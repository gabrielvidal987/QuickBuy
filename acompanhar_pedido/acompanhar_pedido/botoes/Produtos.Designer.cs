﻿namespace acompanhar_pedido.botoes
{
    partial class Produtos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Produtos));
            this.pnlCadProd = new System.Windows.Forms.Panel();
            this.qtd_inicial_numerico = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.qtd_vendido_numerico = new System.Windows.Forms.NumericUpDown();
            this.qtd_disponivel_numerico = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.categoria_box = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.valorNumerico = new System.Windows.Forms.NumericUpDown();
            this.btnCadProd = new System.Windows.Forms.Button();
            this.lbCadProd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pcholdNomeProd = new acompanhar_pedido.PlaceHolderTextBox();
            this.fotoProd = new System.Windows.Forms.PictureBox();
            this.btnAddpic = new System.Windows.Forms.OpenFileDialog();
            this.pnlGeral = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.impListProd = new System.Windows.Forms.Button();
            this.impressora = new System.Drawing.Printing.PrintDocument();
            this.pnlCadProd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_inicial_numerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_vendido_numerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_disponivel_numerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valorNumerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoProd)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCadProd
            // 
            this.pnlCadProd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCadProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCadProd.Controls.Add(this.qtd_inicial_numerico);
            this.pnlCadProd.Controls.Add(this.label6);
            this.pnlCadProd.Controls.Add(this.label7);
            this.pnlCadProd.Controls.Add(this.qtd_vendido_numerico);
            this.pnlCadProd.Controls.Add(this.qtd_disponivel_numerico);
            this.pnlCadProd.Controls.Add(this.label4);
            this.pnlCadProd.Controls.Add(this.categoria_box);
            this.pnlCadProd.Controls.Add(this.label3);
            this.pnlCadProd.Controls.Add(this.valorNumerico);
            this.pnlCadProd.Controls.Add(this.btnCadProd);
            this.pnlCadProd.Controls.Add(this.lbCadProd);
            this.pnlCadProd.Controls.Add(this.label2);
            this.pnlCadProd.Controls.Add(this.label1);
            this.pnlCadProd.Controls.Add(this.pcholdNomeProd);
            this.pnlCadProd.Controls.Add(this.fotoProd);
            this.pnlCadProd.Location = new System.Drawing.Point(1036, 22);
            this.pnlCadProd.Name = "pnlCadProd";
            this.pnlCadProd.Size = new System.Drawing.Size(241, 550);
            this.pnlCadProd.TabIndex = 20;
            // 
            // qtd_inicial_numerico
            // 
            this.qtd_inicial_numerico.DecimalPlaces = 2;
            this.qtd_inicial_numerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtd_inicial_numerico.Location = new System.Drawing.Point(128, 162);
            this.qtd_inicial_numerico.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.qtd_inicial_numerico.Name = "qtd_inicial_numerico";
            this.qtd_inicial_numerico.Size = new System.Drawing.Size(85, 30);
            this.qtd_inicial_numerico.TabIndex = 35;
            this.qtd_inicial_numerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.qtd_inicial_numerico.ValueChanged += new System.EventHandler(this.qtd_inicial_numerico_ValueChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(127, 136);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 23);
            this.label6.TabIndex = 36;
            this.label6.Text = "Qtd Inicial";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(124, 200);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 44);
            this.label7.TabIndex = 34;
            this.label7.Text = "QTD VENDIDA";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // qtd_vendido_numerico
            // 
            this.qtd_vendido_numerico.DecimalPlaces = 2;
            this.qtd_vendido_numerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtd_vendido_numerico.Location = new System.Drawing.Point(128, 244);
            this.qtd_vendido_numerico.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.qtd_vendido_numerico.Name = "qtd_vendido_numerico";
            this.qtd_vendido_numerico.Size = new System.Drawing.Size(85, 30);
            this.qtd_vendido_numerico.TabIndex = 32;
            this.qtd_vendido_numerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // qtd_disponivel_numerico
            // 
            this.qtd_disponivel_numerico.DecimalPlaces = 2;
            this.qtd_disponivel_numerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtd_disponivel_numerico.Location = new System.Drawing.Point(24, 244);
            this.qtd_disponivel_numerico.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.qtd_disponivel_numerico.Name = "qtd_disponivel_numerico";
            this.qtd_disponivel_numerico.ReadOnly = true;
            this.qtd_disponivel_numerico.Size = new System.Drawing.Size(85, 30);
            this.qtd_disponivel_numerico.TabIndex = 30;
            this.qtd_disponivel_numerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(20, 201);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 44);
            this.label4.TabIndex = 31;
            this.label4.Text = "QTD DISPONIVEL";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // categoria_box
            // 
            this.categoria_box.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoria_box.FormattingEnabled = true;
            this.categoria_box.Items.AddRange(new object[] {
            "Bebidas",
            "Bolos",
            "Hamburgueres",
            "Pizza",
            "Porcoes",
            "Marmita",
            "Sanduiches",
            "Snacks",
            "Sorvetes",
            "Sobremesas"});
            this.categoria_box.Location = new System.Drawing.Point(24, 303);
            this.categoria_box.Name = "categoria_box";
            this.categoria_box.Size = new System.Drawing.Size(189, 33);
            this.categoria_box.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(68, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 23);
            this.label3.TabIndex = 28;
            this.label3.Text = "CATEGORIA:";
            // 
            // valorNumerico
            // 
            this.valorNumerico.DecimalPlaces = 2;
            this.valorNumerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.valorNumerico.Increment = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.valorNumerico.Location = new System.Drawing.Point(24, 162);
            this.valorNumerico.Name = "valorNumerico";
            this.valorNumerico.Size = new System.Drawing.Size(85, 30);
            this.valorNumerico.TabIndex = 2;
            this.valorNumerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCadProd
            // 
            this.btnCadProd.BackColor = System.Drawing.Color.Lime;
            this.btnCadProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCadProd.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadProd.ForeColor = System.Drawing.Color.Snow;
            this.btnCadProd.Location = new System.Drawing.Point(24, 520);
            this.btnCadProd.Margin = new System.Windows.Forms.Padding(0);
            this.btnCadProd.Name = "btnCadProd";
            this.btnCadProd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCadProd.Size = new System.Drawing.Size(214, 50);
            this.btnCadProd.TabIndex = 3;
            this.btnCadProd.Text = "CADASTRAR";
            this.btnCadProd.UseVisualStyleBackColor = false;
            this.btnCadProd.Click += new System.EventHandler(this.btnCadProd_Click);
            // 
            // lbCadProd
            // 
            this.lbCadProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCadProd.Font = new System.Drawing.Font("Arial", 15F);
            this.lbCadProd.Location = new System.Drawing.Point(12, 13);
            this.lbCadProd.Name = "lbCadProd";
            this.lbCadProd.Size = new System.Drawing.Size(214, 58);
            this.lbCadProd.TabIndex = 25;
            this.lbCadProd.Text = "CADASTRO DE PRODUTOS";
            this.lbCadProd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(34, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 23);
            this.label2.TabIndex = 24;
            this.label2.Text = "VALOR:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(57, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 23);
            this.label1.TabIndex = 23;
            this.label1.Text = "NOME/SABOR:";
            // 
            // pcholdNomeProd
            // 
            this.pcholdNomeProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Italic);
            this.pcholdNomeProd.ForeColor = System.Drawing.Color.Gray;
            this.pcholdNomeProd.Location = new System.Drawing.Point(24, 103);
            this.pcholdNomeProd.Name = "pcholdNomeProd";
            this.pcholdNomeProd.PlaceHolderText = null;
            this.pcholdNomeProd.Size = new System.Drawing.Size(189, 30);
            this.pcholdNomeProd.TabIndex = 1;
            this.pcholdNomeProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fotoProd
            // 
            this.fotoProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.fotoProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fotoProd.ErrorImage = ((System.Drawing.Image)(resources.GetObject("fotoProd.ErrorImage")));
            this.fotoProd.Image = ((System.Drawing.Image)(resources.GetObject("fotoProd.Image")));
            this.fotoProd.InitialImage = ((System.Drawing.Image)(resources.GetObject("fotoProd.InitialImage")));
            this.fotoProd.Location = new System.Drawing.Point(24, 367);
            this.fotoProd.Name = "fotoProd";
            this.fotoProd.Size = new System.Drawing.Size(214, 150);
            this.fotoProd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.fotoProd.TabIndex = 27;
            this.fotoProd.TabStop = false;
            this.fotoProd.Click += new System.EventHandler(this.fotoProd_Click);
            // 
            // btnAddpic
            // 
            this.btnAddpic.Filter = "Imagens (JPG,PNG,JPEG)|*.jpg;*.png;*.jpeg";
            // 
            // pnlGeral
            // 
            this.pnlGeral.AutoScroll = true;
            this.pnlGeral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGeral.Location = new System.Drawing.Point(26, 87);
            this.pnlGeral.Name = "pnlGeral";
            this.pnlGeral.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGeral.Size = new System.Drawing.Size(1004, 550);
            this.pnlGeral.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(-3, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 13);
            this.label5.TabIndex = 36;
            this.label5.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            // 
            // impListProd
            // 
            this.impListProd.BackColor = System.Drawing.Color.Lime;
            this.impListProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.impListProd.Font = new System.Drawing.Font("Impact", 12F);
            this.impListProd.ForeColor = System.Drawing.Color.Snow;
            this.impListProd.Location = new System.Drawing.Point(1070, 553);
            this.impListProd.Margin = new System.Windows.Forms.Padding(0);
            this.impListProd.Name = "impListProd";
            this.impListProd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.impListProd.Size = new System.Drawing.Size(214, 50);
            this.impListProd.TabIndex = 37;
            this.impListProd.Text = "IMPRIMIR LISTA DE PRODUTOS";
            this.impListProd.UseVisualStyleBackColor = false;
            this.impListProd.Click += new System.EventHandler(this.impListProd_Click);
            // 
            // impressora
            // 
            this.impressora.DocumentName = "senha_pedido";
            this.impressora.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.impressora_PrintPage);
            // 
            // Produtos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 592);
            this.Controls.Add(this.impListProd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pnlCadProd);
            this.Controls.Add(this.pnlGeral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Produtos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Produtos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Produtos_Load);
            this.pnlCadProd.ResumeLayout(false);
            this.pnlCadProd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_inicial_numerico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_vendido_numerico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_disponivel_numerico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valorNumerico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoProd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlCadProd;
        private PlaceHolderTextBox pcholdNomeProd;
        private System.Windows.Forms.Label lbCadProd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCadProd;
        private System.Windows.Forms.OpenFileDialog btnAddpic;
        private System.Windows.Forms.PictureBox fotoProd;
        private System.Windows.Forms.NumericUpDown valorNumerico;
        private System.Windows.Forms.FlowLayoutPanel pnlGeral;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button impListProd;
        private System.Drawing.Printing.PrintDocument impressora;
        private System.Windows.Forms.ComboBox categoria_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown qtd_vendido_numerico;
        private System.Windows.Forms.NumericUpDown qtd_disponivel_numerico;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown qtd_inicial_numerico;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}