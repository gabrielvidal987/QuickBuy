namespace acompanhar_pedido.botoes
{
    partial class Relatorio
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Relatorio));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.delProduto = new System.Windows.Forms.CheckBox();
            this.delVendas = new System.Windows.Forms.CheckBox();
            this.delPendentes = new System.Windows.Forms.CheckBox();
            this.apagaBD = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnOrdemVenda = new System.Windows.Forms.RadioButton();
            this.btnNomeAZ = new System.Windows.Forms.RadioButton();
            this.btnNomeZA = new System.Windows.Forms.RadioButton();
            this.btnValor = new System.Windows.Forms.RadioButton();
            this.btnUmNome = new System.Windows.Forms.RadioButton();
            this.pcholdPesquisa = new acompanhar_pedido.PlaceHolderTextBox();
            this.exportExc = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.txDeb = new acompanhar_pedido.PlaceHolderTextBox();
            this.txCred = new acompanhar_pedido.PlaceHolderTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabelaVendas = new System.Windows.Forms.DataGridView();
            this.pnlES = new System.Windows.Forms.Panel();
            this.saidas = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.resFinal = new acompanhar_pedido.PlaceHolderTextBox();
            this.entradas = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nomeClube = new System.Windows.Forms.TextBox();
            this.btnAddpic = new System.Windows.Forms.OpenFileDialog();
            this.escolherLocal = new System.Windows.Forms.FolderBrowserDialog();
            this.label7 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabelaVendas)).BeginInit();
            this.pnlES.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.delProduto);
            this.flowLayoutPanel1.Controls.Add(this.delVendas);
            this.flowLayoutPanel1.Controls.Add(this.delPendentes);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 20);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 72);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // delProduto
            // 
            this.delProduto.AutoSize = true;
            this.delProduto.Location = new System.Drawing.Point(3, 3);
            this.delProduto.Name = "delProduto";
            this.delProduto.Size = new System.Drawing.Size(196, 17);
            this.delProduto.TabIndex = 5;
            this.delProduto.Text = "APAGAR TABELA DE PRODUTOS";
            this.delProduto.UseVisualStyleBackColor = true;
            // 
            // delVendas
            // 
            this.delVendas.AutoSize = true;
            this.delVendas.Location = new System.Drawing.Point(3, 26);
            this.delVendas.Name = "delVendas";
            this.delVendas.Size = new System.Drawing.Size(179, 17);
            this.delVendas.TabIndex = 6;
            this.delVendas.Text = "APAGAR TABELA DE VENDAS";
            this.delVendas.UseVisualStyleBackColor = true;
            // 
            // delPendentes
            // 
            this.delPendentes.AutoSize = true;
            this.delPendentes.Location = new System.Drawing.Point(3, 49);
            this.delPendentes.Name = "delPendentes";
            this.delPendentes.Size = new System.Drawing.Size(190, 17);
            this.delPendentes.TabIndex = 7;
            this.delPendentes.Text = "APAGAR PEDIDOS PENDENTES";
            this.delPendentes.UseVisualStyleBackColor = true;
            // 
            // apagaBD
            // 
            this.apagaBD.Location = new System.Drawing.Point(12, 97);
            this.apagaBD.Name = "apagaBD";
            this.apagaBD.Size = new System.Drawing.Size(200, 27);
            this.apagaBD.TabIndex = 3;
            this.apagaBD.Text = "LIMPAR DADOS SELECIONADOS";
            this.apagaBD.UseVisualStyleBackColor = true;
            this.apagaBD.Click += new System.EventHandler(this.apagaBD_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.btnOrdemVenda);
            this.flowLayoutPanel2.Controls.Add(this.btnNomeAZ);
            this.flowLayoutPanel2.Controls.Add(this.btnNomeZA);
            this.flowLayoutPanel2.Controls.Add(this.btnValor);
            this.flowLayoutPanel2.Controls.Add(this.btnUmNome);
            this.flowLayoutPanel2.Controls.Add(this.pcholdPesquisa);
            this.flowLayoutPanel2.Controls.Add(this.exportExc);
            this.flowLayoutPanel2.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel2.Controls.Add(this.tabelaVendas);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(224, 12);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1114, 657);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1100, 63);
            this.button1.TabIndex = 5;
            this.button1.Text = "GERAR RELATÓRIO";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.GeraRelatorio_Click);
            // 
            // btnOrdemVenda
            // 
            this.btnOrdemVenda.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrdemVenda.AutoSize = true;
            this.btnOrdemVenda.Checked = true;
            this.btnOrdemVenda.Location = new System.Drawing.Point(10, 79);
            this.btnOrdemVenda.Margin = new System.Windows.Forms.Padding(10);
            this.btnOrdemVenda.Name = "btnOrdemVenda";
            this.btnOrdemVenda.Size = new System.Drawing.Size(123, 17);
            this.btnOrdemVenda.TabIndex = 10;
            this.btnOrdemVenda.TabStop = true;
            this.btnOrdemVenda.Text = "ORDEM DE VENDA";
            this.btnOrdemVenda.UseVisualStyleBackColor = true;
            // 
            // btnNomeAZ
            // 
            this.btnNomeAZ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNomeAZ.AutoSize = true;
            this.btnNomeAZ.Location = new System.Drawing.Point(153, 79);
            this.btnNomeAZ.Margin = new System.Windows.Forms.Padding(10);
            this.btnNomeAZ.Name = "btnNomeAZ";
            this.btnNomeAZ.Size = new System.Drawing.Size(83, 17);
            this.btnNomeAZ.TabIndex = 6;
            this.btnNomeAZ.Text = "NOME (A-Z)";
            this.btnNomeAZ.UseVisualStyleBackColor = true;
            // 
            // btnNomeZA
            // 
            this.btnNomeZA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNomeZA.AutoSize = true;
            this.btnNomeZA.Location = new System.Drawing.Point(256, 79);
            this.btnNomeZA.Margin = new System.Windows.Forms.Padding(10);
            this.btnNomeZA.Name = "btnNomeZA";
            this.btnNomeZA.Size = new System.Drawing.Size(83, 17);
            this.btnNomeZA.TabIndex = 7;
            this.btnNomeZA.Text = "NOME (Z-A)";
            this.btnNomeZA.UseVisualStyleBackColor = true;
            // 
            // btnValor
            // 
            this.btnValor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnValor.AutoSize = true;
            this.btnValor.Location = new System.Drawing.Point(359, 79);
            this.btnValor.Margin = new System.Windows.Forms.Padding(10);
            this.btnValor.Name = "btnValor";
            this.btnValor.Size = new System.Drawing.Size(148, 17);
            this.btnValor.TabIndex = 11;
            this.btnValor.Text = "ORDEM DE VALOR (A-Z)";
            this.btnValor.UseVisualStyleBackColor = true;
            // 
            // btnUmNome
            // 
            this.btnUmNome.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUmNome.AutoSize = true;
            this.btnUmNome.Location = new System.Drawing.Point(527, 79);
            this.btnUmNome.Margin = new System.Windows.Forms.Padding(10);
            this.btnUmNome.Name = "btnUmNome";
            this.btnUmNome.Size = new System.Drawing.Size(123, 17);
            this.btnUmNome.TabIndex = 8;
            this.btnUmNome.Text = "APENAS UM NOME";
            this.btnUmNome.UseVisualStyleBackColor = true;
            // 
            // pcholdPesquisa
            // 
            this.pcholdPesquisa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcholdPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.pcholdPesquisa.ForeColor = System.Drawing.Color.Gray;
            this.pcholdPesquisa.Location = new System.Drawing.Point(670, 79);
            this.pcholdPesquisa.Margin = new System.Windows.Forms.Padding(10);
            this.pcholdPesquisa.Multiline = true;
            this.pcholdPesquisa.Name = "pcholdPesquisa";
            this.pcholdPesquisa.PlaceHolderText = null;
            this.pcholdPesquisa.Size = new System.Drawing.Size(144, 17);
            this.pcholdPesquisa.TabIndex = 9;
            this.pcholdPesquisa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pcholdPesquisa_KeyDown);
            // 
            // exportExc
            // 
            this.exportExc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.exportExc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exportExc.Enabled = false;
            this.exportExc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.exportExc.Location = new System.Drawing.Point(827, 72);
            this.exportExc.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.exportExc.Name = "exportExc";
            this.exportExc.Size = new System.Drawing.Size(146, 31);
            this.exportExc.TabIndex = 12;
            this.exportExc.Text = "EXPORTAR PARA EXCEL";
            this.exportExc.UseVisualStyleBackColor = false;
            this.exportExc.Click += new System.EventHandler(this.exportExc_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.txDeb);
            this.flowLayoutPanel3.Controls.Add(this.txCred);
            this.flowLayoutPanel3.Controls.Add(this.label1);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 109);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(948, 28);
            this.flowLayoutPanel3.TabIndex = 14;
            // 
            // txDeb
            // 
            this.txDeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.txDeb.ForeColor = System.Drawing.Color.Gray;
            this.txDeb.Location = new System.Drawing.Point(3, 3);
            this.txDeb.Name = "txDeb";
            this.txDeb.PlaceHolderText = "Taxa de débito";
            this.txDeb.Size = new System.Drawing.Size(127, 23);
            this.txDeb.TabIndex = 1;
            this.txDeb.Text = "Taxa de débito";
            this.txDeb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txCred
            // 
            this.txCred.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.txCred.ForeColor = System.Drawing.Color.Gray;
            this.txCred.Location = new System.Drawing.Point(136, 3);
            this.txCred.Name = "txCred";
            this.txCred.PlaceHolderText = "Taxa de credito";
            this.txCred.Size = new System.Drawing.Size(129, 23);
            this.txCred.TabIndex = 2;
            this.txCred.Text = "Taxa de crédito";
            this.txCred.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(271, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(363, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "*caso a taxa não seja especificada será utilizada uma taxa padrão de 1,5%*";
            // 
            // tabelaVendas
            // 
            this.tabelaVendas.AllowUserToAddRows = false;
            this.tabelaVendas.AllowUserToDeleteRows = false;
            this.tabelaVendas.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tabelaVendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tabelaVendas.Location = new System.Drawing.Point(3, 143);
            this.tabelaVendas.Name = "tabelaVendas";
            this.tabelaVendas.Size = new System.Drawing.Size(1070, 213);
            this.tabelaVendas.TabIndex = 15;
            // 
            // pnlES
            // 
            this.pnlES.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlES.Controls.Add(this.saidas);
            this.pnlES.Controls.Add(this.label8);
            this.pnlES.Controls.Add(this.resFinal);
            this.pnlES.Controls.Add(this.entradas);
            this.pnlES.Location = new System.Drawing.Point(12, 130);
            this.pnlES.Name = "pnlES";
            this.pnlES.Size = new System.Drawing.Size(200, 211);
            this.pnlES.TabIndex = 8;
            // 
            // saidas
            // 
            this.saidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.saidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saidas.ForeColor = System.Drawing.Color.Red;
            this.saidas.Location = new System.Drawing.Point(14, 89);
            this.saidas.Multiline = true;
            this.saidas.Name = "saidas";
            this.saidas.Size = new System.Drawing.Size(168, 30);
            this.saidas.TabIndex = 3;
            this.saidas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.saidas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.saidas_KeyDown);
            // 
            // label8
            // 
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(0, 3);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(198, 39);
            this.label8.TabIndex = 4;
            this.label8.Text = "*taxa de débito e crédito ja descontada* resultados tirados do relatório gerado";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // resFinal
            // 
            this.resFinal.Enabled = false;
            this.resFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Italic);
            this.resFinal.ForeColor = System.Drawing.Color.Gray;
            this.resFinal.Location = new System.Drawing.Point(14, 136);
            this.resFinal.Multiline = true;
            this.resFinal.Name = "resFinal";
            this.resFinal.PlaceHolderText = "RESULTADO";
            this.resFinal.ReadOnly = true;
            this.resFinal.Size = new System.Drawing.Size(168, 40);
            this.resFinal.TabIndex = 2;
            this.resFinal.Text = "RESULTADO";
            this.resFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // entradas
            // 
            this.entradas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.entradas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.entradas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.entradas.Location = new System.Drawing.Point(14, 42);
            this.entradas.Multiline = true;
            this.entradas.Name = "entradas";
            this.entradas.ReadOnly = true;
            this.entradas.Size = new System.Drawing.Size(168, 32);
            this.entradas.TabIndex = 4;
            this.entradas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.nomeClube);
            this.panel2.Location = new System.Drawing.Point(12, 316);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 353);
            this.panel2.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(14, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 32);
            this.button2.TabIndex = 10;
            this.button2.Text = "CRIAR USUÁRIO";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.Enabled = false;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(32, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 65);
            this.label6.TabIndex = 9;
            this.label6.Text = "Escreva o nome, sem espaços e no minúsculo, esta será a sua senha de login";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(46, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "IMAGEM DO CLUBE";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(14, 150);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(168, 151);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(53, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "NOME DO CLUBE";
            // 
            // nomeClube
            // 
            this.nomeClube.BackColor = System.Drawing.Color.White;
            this.nomeClube.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nomeClube.ForeColor = System.Drawing.Color.Black;
            this.nomeClube.Location = new System.Drawing.Point(14, 90);
            this.nomeClube.Multiline = true;
            this.nomeClube.Name = "nomeClube";
            this.nomeClube.Size = new System.Drawing.Size(168, 32);
            this.nomeClube.TabIndex = 4;
            this.nomeClube.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAddpic
            // 
            this.btnAddpic.Filter = "JPG(*.jpg)|*.jpg|PNG(*.png)|*.png";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(-2, -2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(307, 13);
            this.label7.TabIndex = 36;
            this.label7.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            // 
            // Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1350, 681);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlES);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.apagaBD);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabelaVendas)).EndInit();
            this.pnlES.ResumeLayout(false);
            this.pnlES.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button apagaBD;
        private System.Windows.Forms.CheckBox delProduto;
        private System.Windows.Forms.CheckBox delVendas;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton btnNomeAZ;
        private System.Windows.Forms.RadioButton btnNomeZA;
        private System.Windows.Forms.RadioButton btnUmNome;
        private PlaceHolderTextBox pcholdPesquisa;
        private System.Windows.Forms.RadioButton btnOrdemVenda;
        private System.Windows.Forms.RadioButton btnValor;
        private System.Windows.Forms.Panel pnlES;
        private PlaceHolderTextBox resFinal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox entradas;
        private System.Windows.Forms.TextBox saidas;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nomeClube;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.OpenFileDialog btnAddpic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button exportExc;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog escolherLocal;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private PlaceHolderTextBox txCred;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private PlaceHolderTextBox txDeb;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView tabelaVendas;
        private System.Windows.Forms.CheckBox delPendentes;
    }
}