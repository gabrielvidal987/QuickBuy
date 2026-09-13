namespace acompanhar_pedido.botoes
{
    partial class MovimentoCaixaDialog
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
            this.lblValor = new System.Windows.Forms.Label();
            this.numValor = new System.Windows.Forms.NumericUpDown();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cbCategoria = new System.Windows.Forms.ComboBox();
            this.lblFormaPagamento = new System.Windows.Forms.Label();
            this.cbFormaPagamento = new System.Windows.Forms.ComboBox();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numValor)).BeginInit();
            this.SuspendLayout();
            //
            // lbTitulo
            //
            this.lbTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitulo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(12, 12);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(340, 40);
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblValor
            //
            this.lblValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblValor.Location = new System.Drawing.Point(12, 65);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(150, 23);
            this.lblValor.Text = "Valor:";
            //
            // numValor
            //
            this.numValor.DecimalPlaces = 2;
            this.numValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.numValor.Location = new System.Drawing.Point(12, 90);
            this.numValor.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numValor.Name = "numValor";
            this.numValor.Size = new System.Drawing.Size(160, 32);
            //
            // lblCategoria
            //
            this.lblCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCategoria.Location = new System.Drawing.Point(190, 65);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(162, 23);
            this.lblCategoria.Text = "Categoria:";
            //
            // cbCategoria
            //
            this.cbCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbCategoria.FormattingEnabled = true;
            this.cbCategoria.Location = new System.Drawing.Point(190, 90);
            this.cbCategoria.Name = "cbCategoria";
            this.cbCategoria.Size = new System.Drawing.Size(162, 28);
            //
            // lblFormaPagamento
            //
            this.lblFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFormaPagamento.Location = new System.Drawing.Point(12, 135);
            this.lblFormaPagamento.Name = "lblFormaPagamento";
            this.lblFormaPagamento.Size = new System.Drawing.Size(200, 23);
            this.lblFormaPagamento.Text = "Forma de pagamento (opcional):";
            //
            // cbFormaPagamento
            //
            this.cbFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbFormaPagamento.FormattingEnabled = true;
            this.cbFormaPagamento.Location = new System.Drawing.Point(12, 160);
            this.cbFormaPagamento.Name = "cbFormaPagamento";
            this.cbFormaPagamento.Size = new System.Drawing.Size(340, 28);
            //
            // lblDescricao
            //
            this.lblDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDescricao.Location = new System.Drawing.Point(12, 200);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(340, 23);
            this.lblDescricao.Text = "Descrição (opcional):";
            //
            // txtDescricao
            //
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDescricao.Location = new System.Drawing.Point(12, 225);
            this.txtDescricao.Multiline = true;
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(340, 60);
            //
            // lblDocumento
            //
            this.lblDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDocumento.Location = new System.Drawing.Point(12, 295);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(340, 23);
            this.lblDocumento.Text = "Documento/Comprovante (opcional):";
            //
            // txtDocumento
            //
            this.txtDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDocumento.Location = new System.Drawing.Point(12, 320);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(340, 27);
            //
            // btnConfirmar
            //
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirmar.Location = new System.Drawing.Point(12, 365);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(160, 45);
            this.btnConfirmar.Text = "CONFIRMAR";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            //
            // btnCancelar
            //
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancelar.Location = new System.Drawing.Point(192, 365);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(160, 45);
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // MovimentoCaixaDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 422);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblDocumento);
            this.Controls.Add(this.txtDescricao);
            this.Controls.Add(this.lblDescricao);
            this.Controls.Add(this.cbFormaPagamento);
            this.Controls.Add(this.lblFormaPagamento);
            this.Controls.Add(this.cbCategoria);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.numValor);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.lbTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MovimentoCaixaDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Movimento de Caixa";
            ((System.ComponentModel.ISupportInitialize)(this.numValor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.NumericUpDown numValor;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cbCategoria;
        private System.Windows.Forms.Label lblFormaPagamento;
        private System.Windows.Forms.ComboBox cbFormaPagamento;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
