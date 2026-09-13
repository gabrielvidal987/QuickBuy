namespace acompanhar_pedido.botoes
{
    partial class DescontoDialog
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
            this.lbTitulo = new System.Windows.Forms.Label();
            this.lblNominal = new System.Windows.Forms.Label();
            this.lbNominalValor = new System.Windows.Forms.Label();
            this.lblReais = new System.Windows.Forms.Label();
            this.numReais = new System.Windows.Forms.NumericUpDown();
            this.lblPct = new System.Windows.Forms.Label();
            this.numPct = new System.Windows.Forms.NumericUpDown();
            this.lblFinal = new System.Windows.Forms.Label();
            this.lbFinalValor = new System.Windows.Forms.Label();
            this.lbDescTotal = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numReais)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPct)).BeginInit();
            this.SuspendLayout();
            //
            // lbTitulo
            //
            this.lbTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitulo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(12, 12);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(356, 40);
            this.lbTitulo.Text = "DESCONTO NO PEDIDO";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblNominal
            //
            this.lblNominal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblNominal.Location = new System.Drawing.Point(12, 66);
            this.lblNominal.Name = "lblNominal";
            this.lblNominal.Size = new System.Drawing.Size(150, 24);
            this.lblNominal.Text = "Valor nominal:";
            this.lblNominal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbNominalValor
            //
            this.lbNominalValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lbNominalValor.Location = new System.Drawing.Point(168, 64);
            this.lbNominalValor.Name = "lbNominalValor";
            this.lbNominalValor.Size = new System.Drawing.Size(200, 26);
            this.lbNominalValor.Text = "R$ 0,00";
            this.lbNominalValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblReais
            //
            this.lblReais.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblReais.Location = new System.Drawing.Point(12, 104);
            this.lblReais.Name = "lblReais";
            this.lblReais.Size = new System.Drawing.Size(170, 20);
            this.lblReais.Text = "Desconto em R$:";
            //
            // numReais
            //
            this.numReais.DecimalPlaces = 2;
            this.numReais.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numReais.Location = new System.Drawing.Point(12, 126);
            this.numReais.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numReais.Name = "numReais";
            this.numReais.Size = new System.Drawing.Size(160, 29);
            this.numReais.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // lblPct
            //
            this.lblPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblPct.Location = new System.Drawing.Point(198, 104);
            this.lblPct.Name = "lblPct";
            this.lblPct.Size = new System.Drawing.Size(170, 20);
            this.lblPct.Text = "Desconto em %:";
            //
            // numPct
            //
            this.numPct.DecimalPlaces = 2;
            this.numPct.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numPct.Location = new System.Drawing.Point(198, 126);
            this.numPct.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numPct.Name = "numPct";
            this.numPct.Size = new System.Drawing.Size(160, 29);
            this.numPct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // lblFinal
            //
            this.lblFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFinal.Location = new System.Drawing.Point(12, 176);
            this.lblFinal.Name = "lblFinal";
            this.lblFinal.Size = new System.Drawing.Size(150, 26);
            this.lblFinal.Text = "Valor final:";
            this.lblFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbFinalValor
            //
            this.lbFinalValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lbFinalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(0)))));
            this.lbFinalValor.Location = new System.Drawing.Point(166, 172);
            this.lbFinalValor.Name = "lbFinalValor";
            this.lbFinalValor.Size = new System.Drawing.Size(202, 30);
            this.lbFinalValor.Text = "R$ 0,00";
            this.lbFinalValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lbDescTotal
            //
            this.lbDescTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbDescTotal.ForeColor = System.Drawing.Color.DimGray;
            this.lbDescTotal.Location = new System.Drawing.Point(12, 205);
            this.lbDescTotal.Name = "lbDescTotal";
            this.lbDescTotal.Size = new System.Drawing.Size(356, 18);
            this.lbDescTotal.Text = "desconto total: -R$ 0,00";
            this.lbDescTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnSalvar
            //
            this.btnSalvar.BackColor = System.Drawing.Color.LightGreen;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSalvar.Location = new System.Drawing.Point(12, 234);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(170, 45);
            this.btnSalvar.Text = "SALVAR";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Location = new System.Drawing.Point(198, 234);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(170, 45);
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // DescontoDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 291);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lbDescTotal);
            this.Controls.Add(this.lbFinalValor);
            this.Controls.Add(this.lblFinal);
            this.Controls.Add(this.numPct);
            this.Controls.Add(this.lblPct);
            this.Controls.Add(this.numReais);
            this.Controls.Add(this.lblReais);
            this.Controls.Add(this.lbNominalValor);
            this.Controls.Add(this.lblNominal);
            this.Controls.Add(this.lbTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DescontoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Desconto";
            ((System.ComponentModel.ISupportInitialize)(this.numReais)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPct)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lblNominal;
        private System.Windows.Forms.Label lbNominalValor;
        private System.Windows.Forms.Label lblReais;
        private System.Windows.Forms.NumericUpDown numReais;
        private System.Windows.Forms.Label lblPct;
        private System.Windows.Forms.NumericUpDown numPct;
        private System.Windows.Forms.Label lblFinal;
        private System.Windows.Forms.Label lbFinalValor;
        private System.Windows.Forms.Label lbDescTotal;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
