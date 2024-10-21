namespace acompanhar_pedido.botoes
{
    partial class RealizarPagamento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RealizarPagamento));
            this.lbTitulo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.impressora = new System.Drawing.Printing.PrintDocument();
            this.pnlGeral = new acompanhar_pedido.botoes.DoubleBufferedFlowLayoutPanel();
            this.reset_tela = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lbTitulo
            // 
            this.lbTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitulo.Font = new System.Drawing.Font("Segoe UI Black", 30F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(0, 0);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(800, 96);
            this.lbTitulo.TabIndex = 0;
            this.lbTitulo.Text = "REALIZAR PAGAMENTOS";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(-3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            // 
            // impressora
            // 
            this.impressora.DocumentName = "senha_pedido";
            // 
            // pnlGeral
            // 
            this.pnlGeral.AutoScroll = true;
            this.pnlGeral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGeral.Location = new System.Drawing.Point(0, 96);
            this.pnlGeral.Name = "pnlGeral";
            this.pnlGeral.Size = new System.Drawing.Size(800, 354);
            this.pnlGeral.TabIndex = 38;
            // 
            // reset_tela
            // 
            this.reset_tela.Enabled = true;
            this.reset_tela.Interval = 10000;
            this.reset_tela.Tick += new System.EventHandler(this.reset_tela_Tick);
            // 
            // RealizarPagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlGeral);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbTitulo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RealizarPagamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RealizarPagamento";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label label5;
        private DoubleBufferedFlowLayoutPanel pnlGeral;
        private System.Drawing.Printing.PrintDocument impressora;
        private System.Windows.Forms.Timer reset_tela;
    }
}