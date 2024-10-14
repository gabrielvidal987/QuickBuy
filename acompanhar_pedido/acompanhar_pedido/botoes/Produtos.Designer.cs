namespace acompanhar_pedido.botoes
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
            ((System.ComponentModel.ISupportInitialize)(this.valorNumerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoProd)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCadProd
            // 
            this.pnlCadProd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlCadProd.Controls.Add(this.valorNumerico);
            this.pnlCadProd.Controls.Add(this.btnCadProd);
            this.pnlCadProd.Controls.Add(this.lbCadProd);
            this.pnlCadProd.Controls.Add(this.label2);
            this.pnlCadProd.Controls.Add(this.label1);
            this.pnlCadProd.Controls.Add(this.pcholdNomeProd);
            this.pnlCadProd.Controls.Add(this.fotoProd);
            this.pnlCadProd.Location = new System.Drawing.Point(1036, 42);
            this.pnlCadProd.Name = "pnlCadProd";
            this.pnlCadProd.Size = new System.Drawing.Size(241, 467);
            this.pnlCadProd.TabIndex = 20;
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
            this.valorNumerico.Location = new System.Drawing.Point(77, 186);
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
            this.btnCadProd.Location = new System.Drawing.Point(37, 401);
            this.btnCadProd.Margin = new System.Windows.Forms.Padding(0);
            this.btnCadProd.Name = "btnCadProd";
            this.btnCadProd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCadProd.Size = new System.Drawing.Size(166, 50);
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
            this.label2.Location = new System.Drawing.Point(88, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 23);
            this.label2.TabIndex = 24;
            this.label2.Text = "VALOR:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(57, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 23);
            this.label1.TabIndex = 23;
            this.label1.Text = "NOME/SABOR:";
            // 
            // pcholdNomeProd
            // 
            this.pcholdNomeProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Italic);
            this.pcholdNomeProd.ForeColor = System.Drawing.Color.Gray;
            this.pcholdNomeProd.Location = new System.Drawing.Point(24, 127);
            this.pcholdNomeProd.Name = "pcholdNomeProd";
            this.pcholdNomeProd.PlaceHolderText = null;
            this.pcholdNomeProd.Size = new System.Drawing.Size(189, 30);
            this.pcholdNomeProd.TabIndex = 1;
            this.pcholdNomeProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fotoProd
            // 
            this.fotoProd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("fotoProd.BackgroundImage")));
            this.fotoProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fotoProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fotoProd.ErrorImage = ((System.Drawing.Image)(resources.GetObject("fotoProd.ErrorImage")));
            this.fotoProd.InitialImage = ((System.Drawing.Image)(resources.GetObject("fotoProd.InitialImage")));
            this.fotoProd.Location = new System.Drawing.Point(37, 234);
            this.fotoProd.Name = "fotoProd";
            this.fotoProd.Size = new System.Drawing.Size(166, 140);
            this.fotoProd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
            this.impListProd.Location = new System.Drawing.Point(1073, 524);
            this.impListProd.Margin = new System.Windows.Forms.Padding(0);
            this.impListProd.Name = "impListProd";
            this.impListProd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.impListProd.Size = new System.Drawing.Size(166, 50);
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
    }
}