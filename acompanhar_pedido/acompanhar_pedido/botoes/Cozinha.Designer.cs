namespace acompanhar_pedido.botoes
{
    partial class Cozinha
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cozinha));
            this.pnlGeral = new DoubleBufferedFlowLayoutPanel();
            this.pedidos = new System.Windows.Forms.Label();
            this.tPedPronto = new System.Windows.Forms.Timer(this.components);
            this.pnlAnt = new DoubleBufferedFlowLayoutPanel();
            this.btnHistorico = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.setaEntrada = new System.Windows.Forms.PictureBox();
            this.setaSaida = new System.Windows.Forms.PictureBox();
            this.reloadBar = new System.Windows.Forms.ProgressBar();
            this.reload = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlAnt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHistorico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaEntrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaSaida)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGeral
            // 
            this.pnlGeral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGeral.AutoScroll = true;
            this.pnlGeral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGeral.Location = new System.Drawing.Point(92, 81);
            this.pnlGeral.Name = "pnlGeral";
            this.pnlGeral.Padding = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.pnlGeral.Size = new System.Drawing.Size(2162, 305);
            this.pnlGeral.TabIndex = 0;
            // 
            // pedidos
            // 
            this.pedidos.Dock = System.Windows.Forms.DockStyle.Top;
            this.pedidos.Font = new System.Drawing.Font("Segoe UI Black", 30F, System.Drawing.FontStyle.Bold);
            this.pedidos.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.pedidos.Location = new System.Drawing.Point(0, 0);
            this.pedidos.Name = "pedidos";
            this.pedidos.Size = new System.Drawing.Size(1344, 75);
            this.pedidos.TabIndex = 1;
            this.pedidos.Text = "PEDIDOS:";
            this.pedidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tPedPronto
            // 
            this.tPedPronto.Enabled = true;
            this.tPedPronto.Interval = 10000;
            // 
            // pnlAnt
            // 
            this.pnlAnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAnt.Controls.Add(this.btnHistorico);
            this.pnlAnt.Location = new System.Drawing.Point(35, 455);
            this.pnlAnt.Name = "pnlAnt";
            this.pnlAnt.Padding = new System.Windows.Forms.Padding(3, 10, 10, 10);
            this.pnlAnt.Size = new System.Drawing.Size(1827, 215);
            this.pnlAnt.TabIndex = 1;
            // 
            // btnHistorico
            // 
            this.btnHistorico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorico.Image = ((System.Drawing.Image)(resources.GetObject("btnHistorico.Image")));
            this.btnHistorico.Location = new System.Drawing.Point(23, 75);
            this.btnHistorico.Margin = new System.Windows.Forms.Padding(20, 65, 3, 3);
            this.btnHistorico.Name = "btnHistorico";
            this.btnHistorico.Size = new System.Drawing.Size(68, 62);
            this.btnHistorico.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnHistorico.TabIndex = 3;
            this.btnHistorico.TabStop = false;
            this.btnHistorico.Click += new System.EventHandler(this.btnHistorico_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Black", 30F, System.Drawing.FontStyle.Bold);
            this.label1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.label1.Location = new System.Drawing.Point(292, 410);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 43);
            this.label1.TabIndex = 2;
            this.label1.Text = "PRONTOS:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // setaEntrada
            // 
            this.setaEntrada.Image = ((System.Drawing.Image)(resources.GetObject("setaEntrada.Image")));
            this.setaEntrada.Location = new System.Drawing.Point(17, 81);
            this.setaEntrada.Name = "setaEntrada";
            this.setaEntrada.Size = new System.Drawing.Size(70, 70);
            this.setaEntrada.TabIndex = 0;
            this.setaEntrada.TabStop = false;
            // 
            // setaSaida
            // 
            this.setaSaida.Image = ((System.Drawing.Image)(resources.GetObject("setaSaida.Image")));
            this.setaSaida.Location = new System.Drawing.Point(719, 222);
            this.setaSaida.Name = "setaSaida";
            this.setaSaida.Size = new System.Drawing.Size(70, 70);
            this.setaSaida.TabIndex = 1;
            this.setaSaida.TabStop = false;
            // 
            // reloadBar
            // 
            this.reloadBar.Location = new System.Drawing.Point(92, 42);
            this.reloadBar.Maximum = 200;
            this.reloadBar.Name = "reloadBar";
            this.reloadBar.Size = new System.Drawing.Size(177, 13);
            this.reloadBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.reloadBar.TabIndex = 3;
            // 
            // reload
            // 
            this.reload.Enabled = true;
            this.reload.Interval = 10000;
            this.reload.Tick += new System.EventHandler(this.reload_Tick);
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(92, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tempo para recarregar pedidos";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            // 
            // Cozinha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1344, 681);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reloadBar);
            this.Controls.Add(this.setaSaida);
            this.Controls.Add(this.setaEntrada);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlAnt);
            this.Controls.Add(this.pedidos);
            this.Controls.Add(this.pnlGeral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Cozinha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cozinha";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Cozinha_Load);
            this.pnlAnt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHistorico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaEntrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaSaida)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedFlowLayoutPanel pnlGeral;
        private System.Windows.Forms.Label pedidos;
        private System.Windows.Forms.Timer tPedPronto;
        private DoubleBufferedFlowLayoutPanel pnlAnt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox setaEntrada;
        private System.Windows.Forms.PictureBox setaSaida;
        private System.Windows.Forms.PictureBox btnHistorico;
        private System.Windows.Forms.ProgressBar reloadBar;
        private System.Windows.Forms.Timer reload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
    }
}