namespace acompanhar_pedido.botoes
{
    partial class Caixa
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caixa));
            this.lbTitulo = new System.Windows.Forms.Label();
            this.lbSaldoTitulo = new System.Windows.Forms.Label();
            this.lbSaldo = new System.Windows.Forms.Label();
            this.btnNovaEntrada = new System.Windows.Forms.Button();
            this.btnNovaSaida = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.tabelaCaixa = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tabelaCaixa)).BeginInit();
            this.SuspendLayout();
            //
            // lbTitulo
            //
            this.lbTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitulo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(20, 15);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(380, 58);
            this.lbTitulo.TabIndex = 0;
            this.lbTitulo.Text = "CONTROLE DE CAIXA";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lbSaldoTitulo
            //
            this.lbSaldoTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbSaldoTitulo.Font = new System.Drawing.Font("Arial", 12F);
            this.lbSaldoTitulo.Location = new System.Drawing.Point(420, 15);
            this.lbSaldoTitulo.Name = "lbSaldoTitulo";
            this.lbSaldoTitulo.Size = new System.Drawing.Size(150, 27);
            this.lbSaldoTitulo.TabIndex = 1;
            this.lbSaldoTitulo.Text = "SALDO ATUAL:";
            this.lbSaldoTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lbSaldo
            //
            this.lbSaldo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSaldo.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.lbSaldo.Location = new System.Drawing.Point(420, 46);
            this.lbSaldo.Name = "lbSaldo";
            this.lbSaldo.Size = new System.Drawing.Size(220, 50);
            this.lbSaldo.TabIndex = 2;
            this.lbSaldo.Text = "R$0,00";
            this.lbSaldo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnNovaEntrada
            //
            this.btnNovaEntrada.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovaEntrada.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNovaEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnNovaEntrada.Location = new System.Drawing.Point(700, 15);
            this.btnNovaEntrada.Name = "btnNovaEntrada";
            this.btnNovaEntrada.Size = new System.Drawing.Size(190, 40);
            this.btnNovaEntrada.TabIndex = 3;
            this.btnNovaEntrada.Text = "+ NOVA ENTRADA";
            this.btnNovaEntrada.UseVisualStyleBackColor = false;
            this.btnNovaEntrada.Click += new System.EventHandler(this.btnNovaEntrada_Click);
            //
            // btnNovaSaida
            //
            this.btnNovaSaida.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNovaSaida.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNovaSaida.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnNovaSaida.Location = new System.Drawing.Point(700, 61);
            this.btnNovaSaida.Name = "btnNovaSaida";
            this.btnNovaSaida.Size = new System.Drawing.Size(190, 40);
            this.btnNovaSaida.TabIndex = 4;
            this.btnNovaSaida.Text = "− NOVA SAÍDA (SANGRIA)";
            this.btnNovaSaida.UseVisualStyleBackColor = false;
            this.btnNovaSaida.Click += new System.EventHandler(this.btnNovaSaida_Click);
            //
            // btnAtualizar
            //
            this.btnAtualizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAtualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnAtualizar.Location = new System.Drawing.Point(900, 15);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(110, 86);
            this.btnAtualizar.TabIndex = 5;
            this.btnAtualizar.Text = "ATUALIZAR";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            //
            // tabelaCaixa
            //
            this.tabelaCaixa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabelaCaixa.AllowUserToAddRows = false;
            this.tabelaCaixa.AllowUserToDeleteRows = false;
            this.tabelaCaixa.ReadOnly = true;
            this.tabelaCaixa.Location = new System.Drawing.Point(20, 110);
            this.tabelaCaixa.Name = "tabelaCaixa";
            this.tabelaCaixa.RowHeadersWidth = 25;
            this.tabelaCaixa.Size = new System.Drawing.Size(1190, 600);
            this.tabelaCaixa.TabIndex = 6;
            //
            // label5
            //
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(20, 720);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            //
            // Caixa
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 745);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tabelaCaixa);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnNovaSaida);
            this.Controls.Add(this.btnNovaEntrada);
            this.Controls.Add(this.lbSaldo);
            this.Controls.Add(this.lbSaldoTitulo);
            this.Controls.Add(this.lbTitulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Caixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caixa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Caixa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tabelaCaixa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbSaldoTitulo;
        private System.Windows.Forms.Label lbSaldo;
        private System.Windows.Forms.Button btnNovaEntrada;
        private System.Windows.Forms.Button btnNovaSaida;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.DataGridView tabelaCaixa;
        private System.Windows.Forms.Label label5;
    }
}
