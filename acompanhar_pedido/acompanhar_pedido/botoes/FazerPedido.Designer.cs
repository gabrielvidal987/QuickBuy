using acompanhar_pedido.botoes;

namespace acompanhar_pedido.teste
{
    partial class FazerPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FazerPedido));
            this.imprimir = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.boxPgto = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pcholdObs = new acompanhar_pedido.PlaceHolderTextBox();
            this.resumo = new System.Windows.Forms.Label();
            this.btnCad = new System.Windows.Forms.Button();
            this.tbExtrato = new System.Windows.Forms.FlowLayoutPanel();
            this.clienteExtrato = new System.Windows.Forms.Label();
            this.nQuantProd = new System.Windows.Forms.Label();
            this.itemsTexto = new System.Windows.Forms.Label();
            this.pnlFimExtrato = new System.Windows.Forms.Panel();
            this.totalValorExtrato = new System.Windows.Forms.Label();
            this.totalExtratoTexto = new System.Windows.Forms.Label();
            this.impressora = new System.Drawing.Printing.PrintDocument();
            this.label5 = new System.Windows.Forms.Label();
            this.btnHist = new System.Windows.Forms.Button();
            this.filtraProd = new System.Windows.Forms.Timer(this.components);
            this.pnlGeral = new acompanhar_pedido.botoes.DoubleBufferedFlowLayoutPanel();
            this.pcholdBuscaProd = new acompanhar_pedido.PlaceHolderTextBox();
            this.pcholdEndereco = new acompanhar_pedido.PlaceHolderTextBox();
            this.pcholdCliente = new acompanhar_pedido.PlaceHolderTextBox();
            this.delivery = new System.Windows.Forms.CheckBox();
            this.pagamento_efetuado = new System.Windows.Forms.CheckBox();
            this.pnlTotal.SuspendLayout();
            this.pnlFimExtrato.SuspendLayout();
            this.pnlGeral.SuspendLayout();
            this.SuspendLayout();
            // 
            // imprimir
            // 
            this.imprimir.AutoSize = true;
            this.imprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imprimir.Location = new System.Drawing.Point(162, 117);
            this.imprimir.Name = "imprimir";
            this.imprimir.Size = new System.Drawing.Size(132, 24);
            this.imprimir.TabIndex = 2;
            this.imprimir.Text = "Imprimir senha";
            this.imprimir.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(126, 46);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(30, 30);
            this.btnReset.TabIndex = 3;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // pnlTotal
            // 
            this.pnlTotal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTotal.Controls.Add(this.boxPgto);
            this.pnlTotal.Controls.Add(this.label1);
            this.pnlTotal.Controls.Add(this.pcholdObs);
            this.pnlTotal.Controls.Add(this.resumo);
            this.pnlTotal.Controls.Add(this.btnCad);
            this.pnlTotal.Controls.Add(this.tbExtrato);
            this.pnlTotal.Controls.Add(this.clienteExtrato);
            this.pnlTotal.Controls.Add(this.nQuantProd);
            this.pnlTotal.Controls.Add(this.itemsTexto);
            this.pnlTotal.Controls.Add(this.pnlFimExtrato);
            this.pnlTotal.Location = new System.Drawing.Point(785, 86);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(436, 593);
            this.pnlTotal.TabIndex = 29;
            // 
            // boxPgto
            // 
            this.boxPgto.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.boxPgto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxPgto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.boxPgto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxPgto.FormattingEnabled = true;
            this.boxPgto.Items.AddRange(new object[] {
            "DINHEIRO",
            "PIX",
            "DÉBITO",
            "CRÉDITO"});
            this.boxPgto.Location = new System.Drawing.Point(28, 418);
            this.boxPgto.Name = "boxPgto";
            this.boxPgto.Size = new System.Drawing.Size(380, 24);
            this.boxPgto.TabIndex = 5;
            this.boxPgto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.boxPgto_KeyDown);
            // 
            // label1
            // 
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(30, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "*para remover/subtrair um item basta clicar em seu nome no extrato*";
            // 
            // pcholdObs
            // 
            this.pcholdObs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pcholdObs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
            this.pcholdObs.ForeColor = System.Drawing.Color.Gray;
            this.pcholdObs.Location = new System.Drawing.Point(28, 354);
            this.pcholdObs.Multiline = true;
            this.pcholdObs.Name = "pcholdObs";
            this.pcholdObs.PlaceHolderText = null;
            this.pcholdObs.Size = new System.Drawing.Size(380, 56);
            this.pcholdObs.TabIndex = 4;
            // 
            // resumo
            // 
            this.resumo.AutoSize = true;
            this.resumo.BackColor = System.Drawing.Color.Transparent;
            this.resumo.Cursor = System.Windows.Forms.Cursors.Default;
            this.resumo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.resumo.ForeColor = System.Drawing.Color.Black;
            this.resumo.Location = new System.Drawing.Point(23, 54);
            this.resumo.Margin = new System.Windows.Forms.Padding(3);
            this.resumo.Name = "resumo";
            this.resumo.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.resumo.Size = new System.Drawing.Size(119, 39);
            this.resumo.TabIndex = 18;
            this.resumo.Text = "Resumo";
            this.resumo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCad
            // 
            this.btnCad.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCad.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.btnCad.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnCad.Location = new System.Drawing.Point(28, 512);
            this.btnCad.Name = "btnCad";
            this.btnCad.Size = new System.Drawing.Size(380, 67);
            this.btnCad.TabIndex = 6;
            this.btnCad.Text = "CADASTRAR PEDIDO";
            this.btnCad.UseVisualStyleBackColor = false;
            this.btnCad.Click += new System.EventHandler(this.btnCad_Click);
            // 
            // tbExtrato
            // 
            this.tbExtrato.AutoScroll = true;
            this.tbExtrato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbExtrato.Location = new System.Drawing.Point(28, 99);
            this.tbExtrato.Name = "tbExtrato";
            this.tbExtrato.Padding = new System.Windows.Forms.Padding(5, 8, 5, 8);
            this.tbExtrato.Size = new System.Drawing.Size(380, 249);
            this.tbExtrato.TabIndex = 16;
            // 
            // clienteExtrato
            // 
            this.clienteExtrato.AutoSize = true;
            this.clienteExtrato.BackColor = System.Drawing.Color.Transparent;
            this.clienteExtrato.Cursor = System.Windows.Forms.Cursors.Default;
            this.clienteExtrato.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.clienteExtrato.ForeColor = System.Drawing.Color.Black;
            this.clienteExtrato.Location = new System.Drawing.Point(23, 20);
            this.clienteExtrato.Margin = new System.Windows.Forms.Padding(3);
            this.clienteExtrato.Name = "clienteExtrato";
            this.clienteExtrato.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.clienteExtrato.Size = new System.Drawing.Size(84, 29);
            this.clienteExtrato.TabIndex = 12;
            this.clienteExtrato.Text = "Cliente: ";
            this.clienteExtrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nQuantProd
            // 
            this.nQuantProd.AutoSize = true;
            this.nQuantProd.BackColor = System.Drawing.Color.Transparent;
            this.nQuantProd.Cursor = System.Windows.Forms.Cursors.Default;
            this.nQuantProd.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.nQuantProd.ForeColor = System.Drawing.Color.Black;
            this.nQuantProd.Location = new System.Drawing.Point(370, 54);
            this.nQuantProd.Margin = new System.Windows.Forms.Padding(3);
            this.nQuantProd.Name = "nQuantProd";
            this.nQuantProd.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.nQuantProd.Size = new System.Drawing.Size(38, 39);
            this.nQuantProd.TabIndex = 15;
            this.nQuantProd.Text = "0";
            this.nQuantProd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // itemsTexto
            // 
            this.itemsTexto.AutoSize = true;
            this.itemsTexto.BackColor = System.Drawing.Color.Transparent;
            this.itemsTexto.Cursor = System.Windows.Forms.Cursors.Default;
            this.itemsTexto.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.itemsTexto.ForeColor = System.Drawing.Color.Black;
            this.itemsTexto.Location = new System.Drawing.Point(295, 53);
            this.itemsTexto.Margin = new System.Windows.Forms.Padding(3);
            this.itemsTexto.Name = "itemsTexto";
            this.itemsTexto.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.itemsTexto.Size = new System.Drawing.Size(88, 39);
            this.itemsTexto.TabIndex = 14;
            this.itemsTexto.Text = "Itens:";
            this.itemsTexto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlFimExtrato
            // 
            this.pnlFimExtrato.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFimExtrato.Controls.Add(this.totalValorExtrato);
            this.pnlFimExtrato.Controls.Add(this.totalExtratoTexto);
            this.pnlFimExtrato.Location = new System.Drawing.Point(28, 453);
            this.pnlFimExtrato.Name = "pnlFimExtrato";
            this.pnlFimExtrato.Size = new System.Drawing.Size(380, 53);
            this.pnlFimExtrato.TabIndex = 13;
            // 
            // totalValorExtrato
            // 
            this.totalValorExtrato.AutoSize = true;
            this.totalValorExtrato.BackColor = System.Drawing.Color.Transparent;
            this.totalValorExtrato.Cursor = System.Windows.Forms.Cursors.Default;
            this.totalValorExtrato.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.totalValorExtrato.ForeColor = System.Drawing.Color.Black;
            this.totalValorExtrato.Location = new System.Drawing.Point(262, 7);
            this.totalValorExtrato.Margin = new System.Windows.Forms.Padding(3);
            this.totalValorExtrato.Name = "totalValorExtrato";
            this.totalValorExtrato.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.totalValorExtrato.Size = new System.Drawing.Size(101, 39);
            this.totalValorExtrato.TabIndex = 11;
            this.totalValorExtrato.Text = "R$0,00";
            this.totalValorExtrato.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // totalExtratoTexto
            // 
            this.totalExtratoTexto.AutoSize = true;
            this.totalExtratoTexto.BackColor = System.Drawing.Color.Transparent;
            this.totalExtratoTexto.Cursor = System.Windows.Forms.Cursors.Default;
            this.totalExtratoTexto.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.totalExtratoTexto.ForeColor = System.Drawing.Color.Black;
            this.totalExtratoTexto.Location = new System.Drawing.Point(7, 7);
            this.totalExtratoTexto.Margin = new System.Windows.Forms.Padding(3);
            this.totalExtratoTexto.Name = "totalExtratoTexto";
            this.totalExtratoTexto.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.totalExtratoTexto.Size = new System.Drawing.Size(103, 39);
            this.totalExtratoTexto.TabIndex = 10;
            this.totalExtratoTexto.Text = "TOTAL";
            this.totalExtratoTexto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // impressora
            // 
            this.impressora.DocumentName = "senha_pedido";
            this.impressora.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.impressora_PrintPage);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(-2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            // 
            // btnHist
            // 
            this.btnHist.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnHist.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHist.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnHist.Location = new System.Drawing.Point(816, 9);
            this.btnHist.Name = "btnHist";
            this.btnHist.Size = new System.Drawing.Size(380, 51);
            this.btnHist.TabIndex = 29;
            this.btnHist.Text = "HISTÓRICO DE PEDIDOS";
            this.btnHist.UseVisualStyleBackColor = false;
            this.btnHist.Click += new System.EventHandler(this.btnHist_Click);
            // 
            // filtraProd
            // 
            this.filtraProd.Tick += new System.EventHandler(this.filtraProd_Tick);
            // 
            // pnlGeral
            // 
            this.pnlGeral.AutoScroll = true;
            this.pnlGeral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGeral.Controls.Add(this.pcholdBuscaProd);
            this.pnlGeral.Location = new System.Drawing.Point(126, 151);
            this.pnlGeral.Name = "pnlGeral";
            this.pnlGeral.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.pnlGeral.Size = new System.Drawing.Size(520, 507);
            this.pnlGeral.TabIndex = 33;
            // 
            // pcholdBuscaProd
            // 
            this.pcholdBuscaProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic);
            this.pcholdBuscaProd.ForeColor = System.Drawing.Color.Gray;
            this.pcholdBuscaProd.Location = new System.Drawing.Point(150, 13);
            this.pcholdBuscaProd.Margin = new System.Windows.Forms.Padding(130, 3, 3, 3);
            this.pcholdBuscaProd.Multiline = true;
            this.pcholdBuscaProd.Name = "pcholdBuscaProd";
            this.pcholdBuscaProd.PlaceHolderText = "Buscar produto...";
            this.pcholdBuscaProd.Size = new System.Drawing.Size(221, 30);
            this.pcholdBuscaProd.TabIndex = 35;
            this.pcholdBuscaProd.Text = "Buscar produto...";
            this.pcholdBuscaProd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.pcholdBuscaProd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pcholdBuscaProd_KeyPress);
            // 
            // pcholdEndereco
            // 
            this.pcholdEndereco.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.pcholdEndereco.ForeColor = System.Drawing.Color.Gray;
            this.pcholdEndereco.Location = new System.Drawing.Point(162, 84);
            this.pcholdEndereco.Multiline = true;
            this.pcholdEndereco.Name = "pcholdEndereco";
            this.pcholdEndereco.PlaceHolderText = null;
            this.pcholdEndereco.Size = new System.Drawing.Size(221, 30);
            this.pcholdEndereco.TabIndex = 1;
            this.pcholdEndereco.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pcholdEndereco_KeyDown);
            // 
            // pcholdCliente
            // 
            this.pcholdCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.pcholdCliente.ForeColor = System.Drawing.Color.Gray;
            this.pcholdCliente.Location = new System.Drawing.Point(162, 46);
            this.pcholdCliente.Multiline = true;
            this.pcholdCliente.Name = "pcholdCliente";
            this.pcholdCliente.PlaceHolderText = null;
            this.pcholdCliente.Size = new System.Drawing.Size(221, 30);
            this.pcholdCliente.TabIndex = 0;
            this.pcholdCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pcholdCliente_KeyDown);
            this.pcholdCliente.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pcholdCliente_KeyUp);
            // 
            // delivery
            // 
            this.delivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.delivery.Location = new System.Drawing.Point(300, 117);
            this.delivery.Name = "delivery";
            this.delivery.Size = new System.Drawing.Size(158, 24);
            this.delivery.TabIndex = 35;
            this.delivery.Text = "Delivery/Entrega";
            this.delivery.UseVisualStyleBackColor = true;
            // 
            // pagamento_efetuado
            // 
            this.pagamento_efetuado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagamento_efetuado.Location = new System.Drawing.Point(464, 110);
            this.pagamento_efetuado.Name = "pagamento_efetuado";
            this.pagamento_efetuado.Size = new System.Drawing.Size(182, 38);
            this.pagamento_efetuado.TabIndex = 36;
            this.pagamento_efetuado.Text = "Pagamento efetuado";
            this.pagamento_efetuado.UseVisualStyleBackColor = true;
            // 
            // FazerPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.pagamento_efetuado);
            this.Controls.Add(this.delivery);
            this.Controls.Add(this.btnHist);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.imprimir);
            this.Controls.Add(this.pnlGeral);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pcholdEndereco);
            this.Controls.Add(this.pcholdCliente);
            this.Controls.Add(this.pnlTotal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FazerPedido";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FazerPedido";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FazerPedido_Load_1);
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.pnlFimExtrato.ResumeLayout(false);
            this.pnlFimExtrato.PerformLayout();
            this.pnlGeral.ResumeLayout(false);
            this.pnlGeral.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox imprimir;
        //private System.Windows.Forms.FlowLayoutPanel pnlGeral;
        private DoubleBufferedFlowLayoutPanel pnlGeral;
        private System.Windows.Forms.Button btnReset;
        private PlaceHolderTextBox pcholdEndereco;
        private PlaceHolderTextBox pcholdCliente;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label label1;
        private PlaceHolderTextBox pcholdObs;
        private System.Windows.Forms.Label resumo;
        private System.Windows.Forms.Button btnCad;
        private System.Windows.Forms.FlowLayoutPanel tbExtrato;
        private System.Windows.Forms.Label clienteExtrato;
        private System.Windows.Forms.Label nQuantProd;
        private System.Windows.Forms.Label itemsTexto;
        private System.Windows.Forms.Panel pnlFimExtrato;
        private System.Windows.Forms.Label totalValorExtrato;
        private System.Windows.Forms.Label totalExtratoTexto;
        private System.Drawing.Printing.PrintDocument impressora;
        private System.Windows.Forms.ComboBox boxPgto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnHist;
        private PlaceHolderTextBox pcholdBuscaProd;
        private System.Windows.Forms.Timer filtraProd;
        private System.Windows.Forms.CheckBox delivery;
        private System.Windows.Forms.CheckBox pagamento_efetuado;
    }
}