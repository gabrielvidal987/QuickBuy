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
            this.lbpendentes = new System.Windows.Forms.Label();
            this.lbprontos = new System.Windows.Forms.Label();
            this.setaEntrada = new System.Windows.Forms.PictureBox();
            this.setaSaida = new System.Windows.Forms.PictureBox();
            this.reloadBar = new System.Windows.Forms.ProgressBar();
            this.reload = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.impressora = new System.Drawing.Printing.PrintDocument();
            this.pnlAnt = new acompanhar_pedido.botoes.DoubleBufferedFlowLayoutPanel();
            this.btnHistorico = new System.Windows.Forms.PictureBox();
            this.pnlGeral = new acompanhar_pedido.botoes.DoubleBufferedFlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.setaEntrada)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaSaida)).BeginInit();
            this.pnlAnt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHistorico)).BeginInit();
            this.SuspendLayout();
            // 
            // lbpendentes
            // 
            this.lbpendentes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbpendentes.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lbpendentes.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lbpendentes.Location = new System.Drawing.Point(0, 0);
            this.lbpendentes.Name = "lbpendentes";
            this.lbpendentes.Size = new System.Drawing.Size(1344, 60);
            this.lbpendentes.TabIndex = 1;
            this.lbpendentes.Text = "PENDENTES:";
            this.lbpendentes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbprontos
            // 
            this.lbprontos.Font = new System.Drawing.Font("Segoe UI Black", 20F, System.Drawing.FontStyle.Bold);
            this.lbprontos.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lbprontos.Location = new System.Drawing.Point(926, 389);
            this.lbprontos.Name = "lbprontos";
            this.lbprontos.Size = new System.Drawing.Size(166, 36);
            this.lbprontos.TabIndex = 2;
            this.lbprontos.Text = "PRONTOS:";
            this.lbprontos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.reloadBar.Location = new System.Drawing.Point(92, 36);
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
            this.label2.Location = new System.Drawing.Point(92, 15);
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
            // impressora
            // 
            this.impressora.DocumentName = "senha_pedido";
            this.impressora.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.impressora_PrintPage);
            // 
            // pnlAnt
            // 
            this.pnlAnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAnt.Controls.Add(this.btnHistorico);
            this.pnlAnt.Location = new System.Drawing.Point(35, 416);
            this.pnlAnt.Name = "pnlAnt";
            this.pnlAnt.Padding = new System.Windows.Forms.Padding(3, 1, 1, 10);
            this.pnlAnt.Size = new System.Drawing.Size(1827, 240);
            this.pnlAnt.TabIndex = 1;
            // 
            // btnHistorico
            // 
            this.btnHistorico.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorico.Image = ((System.Drawing.Image)(resources.GetObject("btnHistorico.Image")));
            this.btnHistorico.Location = new System.Drawing.Point(23, 76);
            this.btnHistorico.Margin = new System.Windows.Forms.Padding(20, 75, 3, 3);
            this.btnHistorico.Name = "btnHistorico";
            this.btnHistorico.Size = new System.Drawing.Size(68, 62);
            this.btnHistorico.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnHistorico.TabIndex = 3;
            this.btnHistorico.TabStop = false;
            this.btnHistorico.Click += new System.EventHandler(this.btnHistorico_Click);
            // 
            // pnlGeral
            // 
            this.pnlGeral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGeral.AutoScroll = true;
            this.pnlGeral.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGeral.Location = new System.Drawing.Point(92, 62);
            this.pnlGeral.Name = "pnlGeral";
            this.pnlGeral.Padding = new System.Windows.Forms.Padding(5, 1, 10, 10);
            this.pnlGeral.Size = new System.Drawing.Size(2162, 305);
            this.pnlGeral.TabIndex = 0;
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
            this.Controls.Add(this.lbprontos);
            this.Controls.Add(this.pnlAnt);
            this.Controls.Add(this.lbpendentes);
            this.Controls.Add(this.pnlGeral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Cozinha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cozinha";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Cozinha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.setaEntrada)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaSaida)).EndInit();
            this.pnlAnt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnHistorico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedFlowLayoutPanel pnlGeral;
        private System.Windows.Forms.Label lbpendentes;
        private DoubleBufferedFlowLayoutPanel pnlAnt;
        private System.Windows.Forms.Label lbprontos;
        private System.Windows.Forms.PictureBox setaEntrada;
        private System.Windows.Forms.PictureBox setaSaida;
        private System.Windows.Forms.PictureBox btnHistorico;
        private System.Windows.Forms.ProgressBar reloadBar;
        private System.Windows.Forms.Timer reload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Drawing.Printing.PrintDocument impressora;
    }
}