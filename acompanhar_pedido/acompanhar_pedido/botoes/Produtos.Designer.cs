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
            this.btnHistoricoEstoque = new System.Windows.Forms.Button();
            this.btnSaidaEstoque = new System.Windows.Forms.Button();
            this.btnEntradaEstoque = new System.Windows.Forms.Button();
            this.qtd_inicial_numerico = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.qtd_vendido_numerico = new System.Windows.Forms.NumericUpDown();
            this.qtd_disponivel_numerico = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.categoria_box = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.valorNumerico = new System.Windows.Forms.NumericUpDown();
            this.valorCustoNumerico = new System.Windows.Forms.NumericUpDown();
            this.valorAtacadoNumerico = new System.Windows.Forms.NumericUpDown();
            this.qtdMinAtacadoNumerico = new System.Windows.Forms.NumericUpDown();
            this.qtdMinEstoqueNumerico = new System.Windows.Forms.NumericUpDown();
            this.lbEstoqueMin = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCadProd = new System.Windows.Forms.Button();
            this.btnNovoCadastro = new System.Windows.Forms.Button();
            this.lbCadProd = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pcholdNomeProd = new acompanhar_pedido.PlaceHolderTextBox();
            this.fotoProd = new System.Windows.Forms.PictureBox();
            this.btnAddpic = new System.Windows.Forms.OpenFileDialog();
            this.pnlGeral = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.impListProd = new System.Windows.Forms.Button();
            this.btnCombos = new System.Windows.Forms.Button();
            this.impressora = new System.Drawing.Printing.PrintDocument();
            this.pnlCadProd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_inicial_numerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_vendido_numerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtd_disponivel_numerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valorNumerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valorCustoNumerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valorAtacadoNumerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtdMinAtacadoNumerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtdMinEstoqueNumerico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fotoProd)).BeginInit();
            this.SuspendLayout();
            //
            // pnlCadProd
            // painel de cadastro mais largo (620px) pra caber os 4 campos de valor numa linha só, em vez de empilhados
            //
            // Fica na MESMA ALTURA da grade de produtos (mesmo topo e mesma base que o pnlGeral). Como o painel tem
            // AutoScroll=true, se a janela for menor que o conteúdo aparece uma barra de rolagem em vez de espremer os campos.
            this.pnlCadProd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCadProd.AutoScroll = true;
            this.pnlCadProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCadProd.Controls.Add(this.btnHistoricoEstoque);
            this.pnlCadProd.Controls.Add(this.btnSaidaEstoque);
            this.pnlCadProd.Controls.Add(this.btnEntradaEstoque);
            this.pnlCadProd.Controls.Add(this.qtd_inicial_numerico);
            this.pnlCadProd.Controls.Add(this.label6);
            this.pnlCadProd.Controls.Add(this.label7);
            this.pnlCadProd.Controls.Add(this.qtd_vendido_numerico);
            this.pnlCadProd.Controls.Add(this.qtd_disponivel_numerico);
            this.pnlCadProd.Controls.Add(this.label4);
            this.pnlCadProd.Controls.Add(this.categoria_box);
            this.pnlCadProd.Controls.Add(this.label3);
            this.pnlCadProd.Controls.Add(this.valorNumerico);
            this.pnlCadProd.Controls.Add(this.valorCustoNumerico);
            this.pnlCadProd.Controls.Add(this.valorAtacadoNumerico);
            this.pnlCadProd.Controls.Add(this.qtdMinAtacadoNumerico);
            this.pnlCadProd.Controls.Add(this.qtdMinEstoqueNumerico);
            this.pnlCadProd.Controls.Add(this.lbEstoqueMin);
            this.pnlCadProd.Controls.Add(this.label8);
            this.pnlCadProd.Controls.Add(this.label9);
            this.pnlCadProd.Controls.Add(this.label10);
            this.pnlCadProd.Controls.Add(this.btnCadProd);
            this.pnlCadProd.Controls.Add(this.btnNovoCadastro);
            this.pnlCadProd.Controls.Add(this.lbCadProd);
            this.pnlCadProd.Controls.Add(this.label2);
            this.pnlCadProd.Controls.Add(this.label1);
            this.pnlCadProd.Controls.Add(this.pcholdNomeProd);
            this.pnlCadProd.Controls.Add(this.fotoProd);
            this.pnlCadProd.Location = new System.Drawing.Point(1036, 87);
            this.pnlCadProd.Name = "pnlCadProd";
            this.pnlCadProd.Size = new System.Drawing.Size(620, 820);
            this.pnlCadProd.TabIndex = 20;
            //
            // btnHistoricoEstoque
            //
            this.btnHistoricoEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnHistoricoEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnHistoricoEstoque.Location = new System.Drawing.Point(416, 322);
            this.btnHistoricoEstoque.Name = "btnHistoricoEstoque";
            this.btnHistoricoEstoque.Size = new System.Drawing.Size(180, 34);
            this.btnHistoricoEstoque.TabIndex = 40;
            this.btnHistoricoEstoque.Text = "HISTÓRICO DE ESTOQUE";
            this.btnHistoricoEstoque.UseVisualStyleBackColor = true;
            this.btnHistoricoEstoque.Click += new System.EventHandler(this.btnHistoricoEstoque_Click);
            //
            // btnSaidaEstoque
            //
            this.btnSaidaEstoque.BackColor = System.Drawing.Color.MistyRose;
            this.btnSaidaEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaidaEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSaidaEstoque.Location = new System.Drawing.Point(220, 322);
            this.btnSaidaEstoque.Name = "btnSaidaEstoque";
            this.btnSaidaEstoque.Size = new System.Drawing.Size(180, 34);
            this.btnSaidaEstoque.TabIndex = 39;
            this.btnSaidaEstoque.Text = "− SAÍDA";
            this.btnSaidaEstoque.UseVisualStyleBackColor = false;
            this.btnSaidaEstoque.Click += new System.EventHandler(this.btnSaidaEstoque_Click);
            //
            // btnEntradaEstoque
            //
            this.btnEntradaEstoque.BackColor = System.Drawing.Color.Honeydew;
            this.btnEntradaEstoque.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEntradaEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnEntradaEstoque.Location = new System.Drawing.Point(24, 322);
            this.btnEntradaEstoque.Name = "btnEntradaEstoque";
            this.btnEntradaEstoque.Size = new System.Drawing.Size(180, 34);
            this.btnEntradaEstoque.TabIndex = 38;
            this.btnEntradaEstoque.Text = "+ ENTRADA";
            this.btnEntradaEstoque.UseVisualStyleBackColor = false;
            this.btnEntradaEstoque.Click += new System.EventHandler(this.btnEntradaEstoque_Click);
            //
            // qtd_inicial_numerico (coluna 1 da linha de estoque)
            //
            this.qtd_inicial_numerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtd_inicial_numerico.Location = new System.Drawing.Point(24, 216);
            this.qtd_inicial_numerico.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.qtd_inicial_numerico.Name = "qtd_inicial_numerico";
            this.qtd_inicial_numerico.Size = new System.Drawing.Size(131, 30);
            this.qtd_inicial_numerico.TabIndex = 35;
            this.qtd_inicial_numerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // label6
            //
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(24, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 34);
            this.label6.TabIndex = 36;
            this.label6.Text = "ESTOQUE INICIAL\r\n(só ao cadastrar)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // label7 (coluna 3 da linha de estoque)
            //
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(318, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 34);
            this.label7.TabIndex = 34;
            this.label7.Text = "TOTAL VENDIDO\r\n(histórico)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // qtd_vendido_numerico (coluna 3 da linha de estoque)
            //
            this.qtd_vendido_numerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtd_vendido_numerico.Location = new System.Drawing.Point(318, 216);
            this.qtd_vendido_numerico.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.qtd_vendido_numerico.Name = "qtd_vendido_numerico";
            this.qtd_vendido_numerico.ReadOnly = true;
            this.qtd_vendido_numerico.Size = new System.Drawing.Size(131, 30);
            this.qtd_vendido_numerico.TabIndex = 32;
            this.qtd_vendido_numerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // qtd_disponivel_numerico (coluna 2 da linha de estoque)
            //
            this.qtd_disponivel_numerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtd_disponivel_numerico.Location = new System.Drawing.Point(171, 216);
            this.qtd_disponivel_numerico.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.qtd_disponivel_numerico.Name = "qtd_disponivel_numerico";
            this.qtd_disponivel_numerico.ReadOnly = true;
            this.qtd_disponivel_numerico.Size = new System.Drawing.Size(131, 30);
            this.qtd_disponivel_numerico.TabIndex = 30;
            this.qtd_disponivel_numerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // label4 (coluna 2 da linha de estoque)
            //
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label4.Location = new System.Drawing.Point(171, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 34);
            this.label4.TabIndex = 31;
            this.label4.Text = "ESTOQUE ATUAL";
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
            this.categoria_box.Location = new System.Drawing.Point(24, 276);
            this.categoria_box.Name = "categoria_box";
            this.categoria_box.Size = new System.Drawing.Size(278, 33);
            this.categoria_box.TabIndex = 29;
            //
            // label3
            //
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label3.Location = new System.Drawing.Point(24, 256);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 18);
            this.label3.TabIndex = 28;
            this.label3.Text = "CATEGORIA:";
            //
            // valorNumerico (coluna 1 da linha de valores)
            //
            this.valorNumerico.DecimalPlaces = 2;
            this.valorNumerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.valorNumerico.Increment = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.valorNumerico.Location = new System.Drawing.Point(24, 140);
            this.valorNumerico.Name = "valorNumerico";
            this.valorNumerico.Size = new System.Drawing.Size(131, 30);
            this.valorNumerico.TabIndex = 2;
            this.valorNumerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // valorCustoNumerico (coluna 2 da linha de valores)
            //
            this.valorCustoNumerico.DecimalPlaces = 2;
            this.valorCustoNumerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.valorCustoNumerico.Increment = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.valorCustoNumerico.Location = new System.Drawing.Point(171, 140);
            this.valorCustoNumerico.Name = "valorCustoNumerico";
            this.valorCustoNumerico.Size = new System.Drawing.Size(131, 30);
            this.valorCustoNumerico.TabIndex = 33;
            this.valorCustoNumerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // valorAtacadoNumerico (coluna 3 da linha de valores)
            //
            this.valorAtacadoNumerico.DecimalPlaces = 2;
            this.valorAtacadoNumerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.valorAtacadoNumerico.Increment = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.valorAtacadoNumerico.Location = new System.Drawing.Point(318, 140);
            this.valorAtacadoNumerico.Name = "valorAtacadoNumerico";
            this.valorAtacadoNumerico.Size = new System.Drawing.Size(131, 30);
            this.valorAtacadoNumerico.TabIndex = 37;
            this.valorAtacadoNumerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // qtdMinAtacadoNumerico (coluna 4 da linha de valores)
            //
            this.qtdMinAtacadoNumerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtdMinAtacadoNumerico.Location = new System.Drawing.Point(465, 140);
            this.qtdMinAtacadoNumerico.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.qtdMinAtacadoNumerico.Name = "qtdMinAtacadoNumerico";
            this.qtdMinAtacadoNumerico.Size = new System.Drawing.Size(131, 30);
            this.qtdMinAtacadoNumerico.TabIndex = 41;
            this.qtdMinAtacadoNumerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // lbEstoqueMin (coluna 4 da linha de estoque)
            //
            this.lbEstoqueMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbEstoqueMin.Location = new System.Drawing.Point(465, 180);
            this.lbEstoqueMin.Name = "lbEstoqueMin";
            this.lbEstoqueMin.Size = new System.Drawing.Size(131, 34);
            this.lbEstoqueMin.TabIndex = 46;
            this.lbEstoqueMin.Text = "ESTOQUE MÍNIMO\r\n(alerta na tela inicial)";
            this.lbEstoqueMin.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // qtdMinEstoqueNumerico (coluna 4 da linha de estoque)
            //
            this.qtdMinEstoqueNumerico.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.qtdMinEstoqueNumerico.Location = new System.Drawing.Point(465, 216);
            this.qtdMinEstoqueNumerico.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.qtdMinEstoqueNumerico.Name = "qtdMinEstoqueNumerico";
            this.qtdMinEstoqueNumerico.Size = new System.Drawing.Size(131, 30);
            this.qtdMinEstoqueNumerico.TabIndex = 47;
            this.qtdMinEstoqueNumerico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // label8 (coluna 2 da linha de valores)
            //
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label8.Location = new System.Drawing.Point(171, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 30);
            this.label8.TabIndex = 42;
            this.label8.Text = "VALOR CUSTO:\r\n(por unidade)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // label9 (coluna 3 da linha de valores)
            //
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label9.Location = new System.Drawing.Point(318, 110);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 30);
            this.label9.TabIndex = 43;
            this.label9.Text = "VALOR ATACADO:\r\n(por unidade)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // label10 (coluna 4 da linha de valores)
            //
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.Location = new System.Drawing.Point(465, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 30);
            this.label10.TabIndex = 44;
            this.label10.Text = "QTD. MÍN. ATACADO:\r\n(em unidades)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // btnCadProd
            //
            this.btnCadProd.BackColor = System.Drawing.Color.Lime;
            this.btnCadProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCadProd.Font = new System.Drawing.Font("Impact", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadProd.ForeColor = System.Drawing.Color.Snow;
            this.btnCadProd.Location = new System.Drawing.Point(24, 478);
            this.btnCadProd.Margin = new System.Windows.Forms.Padding(0);
            this.btnCadProd.Name = "btnCadProd";
            this.btnCadProd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCadProd.Size = new System.Drawing.Size(572, 44);
            this.btnCadProd.TabIndex = 3;
            this.btnCadProd.Text = "CADASTRAR";
            this.btnCadProd.UseVisualStyleBackColor = false;
            this.btnCadProd.Click += new System.EventHandler(this.btnCadProd_Click);
            //
            // btnNovoCadastro
            //
            this.btnNovoCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(172)))), ((int)(((byte)(222)))));
            this.btnNovoCadastro.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNovoCadastro.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovoCadastro.ForeColor = System.Drawing.Color.Snow;
            this.btnNovoCadastro.Location = new System.Drawing.Point(24, 526);
            this.btnNovoCadastro.Margin = new System.Windows.Forms.Padding(0);
            this.btnNovoCadastro.Name = "btnNovoCadastro";
            this.btnNovoCadastro.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNovoCadastro.Size = new System.Drawing.Size(572, 40);
            this.btnNovoCadastro.TabIndex = 45;
            this.btnNovoCadastro.Text = "NOVO CADASTRO (LIMPAR CAMPOS)";
            this.btnNovoCadastro.UseVisualStyleBackColor = false;
            this.btnNovoCadastro.Click += new System.EventHandler(this.btnNovoCadastro_Click);
            //
            // lbCadProd
            //
            this.lbCadProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCadProd.Font = new System.Drawing.Font("Arial", 15F);
            this.lbCadProd.Location = new System.Drawing.Point(24, 10);
            this.lbCadProd.Name = "lbCadProd";
            this.lbCadProd.Size = new System.Drawing.Size(572, 40);
            this.lbCadProd.TabIndex = 25;
            this.lbCadProd.Text = "CADASTRO DE PRODUTOS";
            this.lbCadProd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label2 (coluna 1 da linha de valores)
            //
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(24, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 30);
            this.label2.TabIndex = 24;
            this.label2.Text = "VALOR VAREJO:\r\n(por unidade)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            //
            // label1
            //
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(24, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 18);
            this.label1.TabIndex = 23;
            this.label1.Text = "NOME/SABOR:";
            //
            // pcholdNomeProd (largura total, pra caber nomes mais compridos com folga)
            //
            this.pcholdNomeProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Italic);
            this.pcholdNomeProd.ForeColor = System.Drawing.Color.Gray;
            this.pcholdNomeProd.Location = new System.Drawing.Point(24, 78);
            this.pcholdNomeProd.Name = "pcholdNomeProd";
            this.pcholdNomeProd.PlaceHolderText = null;
            this.pcholdNomeProd.Size = new System.Drawing.Size(572, 32);
            this.pcholdNomeProd.TabIndex = 1;
            this.pcholdNomeProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // fotoProd (centralizada)
            //
            this.fotoProd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.fotoProd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fotoProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fotoProd.ErrorImage = ((System.Drawing.Image)(resources.GetObject("fotoProd.ErrorImage")));
            this.fotoProd.Image = ((System.Drawing.Image)(resources.GetObject("fotoProd.Image")));
            this.fotoProd.InitialImage = ((System.Drawing.Image)(resources.GetObject("fotoProd.InitialImage")));
            this.fotoProd.Location = new System.Drawing.Point(219, 362);
            this.fotoProd.Name = "fotoProd";
            this.fotoProd.Size = new System.Drawing.Size(180, 104);
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
            this.pnlGeral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGeral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGeral.Location = new System.Drawing.Point(26, 87);
            this.pnlGeral.Name = "pnlGeral";
            this.pnlGeral.Padding = new System.Windows.Forms.Padding(10);
            this.pnlGeral.Size = new System.Drawing.Size(1004, 820);
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
            // fica na faixa livre acima da grade de produtos: no rodapé da coluna da direita ele saía da tela em monitores menores
            this.impListProd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.impListProd.BackColor = System.Drawing.Color.Lime;
            this.impListProd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.impListProd.Font = new System.Drawing.Font("Impact", 12F);
            this.impListProd.ForeColor = System.Drawing.Color.Snow;
            this.impListProd.Location = new System.Drawing.Point(26, 30);
            this.impListProd.Margin = new System.Windows.Forms.Padding(0);
            this.impListProd.Name = "impListProd";
            this.impListProd.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.impListProd.Size = new System.Drawing.Size(300, 48);
            this.impListProd.TabIndex = 37;
            this.impListProd.Text = "IMPRIMIR LISTA DE PRODUTOS";
            this.impListProd.UseVisualStyleBackColor = false;
            this.impListProd.Click += new System.EventHandler(this.impListProd_Click);
            //
            // btnCombos
            //
            this.btnCombos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCombos.BackColor = System.Drawing.Color.Gold;
            this.btnCombos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCombos.Font = new System.Drawing.Font("Impact", 12F);
            this.btnCombos.ForeColor = System.Drawing.Color.Snow;
            this.btnCombos.Location = new System.Drawing.Point(340, 30);
            this.btnCombos.Margin = new System.Windows.Forms.Padding(0);
            this.btnCombos.Name = "btnCombos";
            this.btnCombos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCombos.Size = new System.Drawing.Size(320, 48);
            this.btnCombos.TabIndex = 38;
            this.btnCombos.Text = "PROMOÇÕES DE ATACADO EM COMBO";
            this.btnCombos.UseVisualStyleBackColor = false;
            this.btnCombos.Click += new System.EventHandler(this.btnCombos_Click);
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
            this.ClientSize = new System.Drawing.Size(1676, 930);
            this.Controls.Add(this.btnCombos);
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
            ((System.ComponentModel.ISupportInitialize)(this.valorCustoNumerico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valorAtacadoNumerico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtdMinAtacadoNumerico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qtdMinEstoqueNumerico)).EndInit();
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
        private System.Windows.Forms.Button btnNovoCadastro;
        private System.Windows.Forms.OpenFileDialog btnAddpic;
        private System.Windows.Forms.PictureBox fotoProd;
        private System.Windows.Forms.NumericUpDown valorNumerico;
        private System.Windows.Forms.FlowLayoutPanel pnlGeral;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button impListProd;
        private System.Windows.Forms.Button btnCombos;
        private System.Drawing.Printing.PrintDocument impressora;
        private System.Windows.Forms.ComboBox categoria_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown qtd_vendido_numerico;
        private System.Windows.Forms.NumericUpDown qtd_disponivel_numerico;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown qtd_inicial_numerico;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown valorCustoNumerico;
        private System.Windows.Forms.NumericUpDown valorAtacadoNumerico;
        private System.Windows.Forms.NumericUpDown qtdMinAtacadoNumerico;
        private System.Windows.Forms.NumericUpDown qtdMinEstoqueNumerico;
        private System.Windows.Forms.Label lbEstoqueMin;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnEntradaEstoque;
        private System.Windows.Forms.Button btnSaidaEstoque;
        private System.Windows.Forms.Button btnHistoricoEstoque;
    }
}
