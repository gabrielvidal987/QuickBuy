namespace acompanhar_pedido.botoes
{
    partial class MovimentoEstoqueDialog
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
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.numQuantidade = new System.Windows.Forms.NumericUpDown();
            this.lblCusto = new System.Windows.Forms.Label();
            this.numCusto = new System.Windows.Forms.NumericUpDown();
            this.lblFornecedor = new System.Windows.Forms.Label();
            this.txtFornecedor = new System.Windows.Forms.TextBox();
            this.lblMotivo = new System.Windows.Forms.Label();
            this.txtMotivo = new System.Windows.Forms.TextBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCusto)).BeginInit();
            this.SuspendLayout();
            //
            // lbTitulo
            //
            this.lbTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitulo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(12, 12);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(340, 50);
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblQuantidade
            //
            this.lblQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblQuantidade.Location = new System.Drawing.Point(12, 75);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(200, 23);
            this.lblQuantidade.Text = "Quantidade:";
            //
            // numQuantidade
            //
            this.numQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numQuantidade.Location = new System.Drawing.Point(12, 100);
            this.numQuantidade.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numQuantidade.Name = "numQuantidade";
            this.numQuantidade.Size = new System.Drawing.Size(150, 29);
            //
            // lblCusto
            //
            this.lblCusto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblCusto.Location = new System.Drawing.Point(190, 75);
            this.lblCusto.Name = "lblCusto";
            this.lblCusto.Size = new System.Drawing.Size(162, 23);
            this.lblCusto.Text = "Custo unitário:";
            //
            // numCusto
            //
            this.numCusto.DecimalPlaces = 2;
            this.numCusto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numCusto.Location = new System.Drawing.Point(190, 100);
            this.numCusto.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.numCusto.Name = "numCusto";
            this.numCusto.Size = new System.Drawing.Size(162, 29);
            //
            // lblFornecedor
            //
            this.lblFornecedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblFornecedor.Location = new System.Drawing.Point(12, 140);
            this.lblFornecedor.Name = "lblFornecedor";
            this.lblFornecedor.Size = new System.Drawing.Size(200, 23);
            this.lblFornecedor.Text = "Fornecedor (opcional):";
            //
            // txtFornecedor
            //
            this.txtFornecedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtFornecedor.Location = new System.Drawing.Point(12, 165);
            this.txtFornecedor.Name = "txtFornecedor";
            this.txtFornecedor.Size = new System.Drawing.Size(340, 27);
            //
            // lblMotivo
            //
            this.lblMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblMotivo.Location = new System.Drawing.Point(12, 205);
            this.lblMotivo.Name = "lblMotivo";
            this.lblMotivo.Size = new System.Drawing.Size(340, 23);
            this.lblMotivo.Text = "Motivo (opcional):";
            //
            // txtMotivo
            //
            this.txtMotivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtMotivo.Location = new System.Drawing.Point(12, 230);
            this.txtMotivo.Multiline = true;
            this.txtMotivo.Name = "txtMotivo";
            this.txtMotivo.Size = new System.Drawing.Size(340, 60);
            //
            // lblDocumento
            //
            this.lblDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblDocumento.Location = new System.Drawing.Point(12, 300);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(340, 23);
            this.lblDocumento.Text = "Documento/Nota (opcional):";
            //
            // txtDocumento
            //
            this.txtDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtDocumento.Location = new System.Drawing.Point(12, 325);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(340, 27);
            //
            // btnConfirmar
            //
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfirmar.Location = new System.Drawing.Point(12, 370);
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
            this.btnCancelar.Location = new System.Drawing.Point(192, 370);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(160, 45);
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // MovimentoEstoqueDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 427);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.lblDocumento);
            this.Controls.Add(this.txtMotivo);
            this.Controls.Add(this.lblMotivo);
            this.Controls.Add(this.txtFornecedor);
            this.Controls.Add(this.lblFornecedor);
            this.Controls.Add(this.numCusto);
            this.Controls.Add(this.lblCusto);
            this.Controls.Add(this.numQuantidade);
            this.Controls.Add(this.lblQuantidade);
            this.Controls.Add(this.lbTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MovimentoEstoqueDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Movimentação de Estoque";
            ((System.ComponentModel.ISupportInitialize)(this.numQuantidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCusto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.NumericUpDown numQuantidade;
        private System.Windows.Forms.Label lblCusto;
        private System.Windows.Forms.NumericUpDown numCusto;
        private System.Windows.Forms.Label lblFornecedor;
        private System.Windows.Forms.TextBox txtFornecedor;
        private System.Windows.Forms.Label lblMotivo;
        private System.Windows.Forms.TextBox txtMotivo;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.Button btnCancelar;
    }
}
