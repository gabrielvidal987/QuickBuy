namespace acompanhar_pedido
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.pnlTopo = new System.Windows.Forms.Panel();
            this.tbnMusic = new System.Windows.Forms.Button();
            this.btnFila = new System.Windows.Forms.Button();
            this.btnProdutos = new System.Windows.Forms.Button();
            this.btnCozinha = new System.Windows.Forms.Button();
            this.data = new System.Windows.Forms.TextBox();
            this.hora = new System.Windows.Forms.TextBox();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.btnPedido = new System.Windows.Forms.Button();
            this.lbTotalText = new System.Windows.Forms.Label();
            this.lbPrepText = new System.Windows.Forms.Label();
            this.lbProntosText = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbPreparando = new System.Windows.Forms.Label();
            this.lbProntos = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.fotoClube = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlTopo.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fotoClube)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTopo
            // 
            this.pnlTopo.Controls.Add(this.tbnMusic);
            this.pnlTopo.Controls.Add(this.btnFila);
            this.pnlTopo.Controls.Add(this.btnProdutos);
            this.pnlTopo.Controls.Add(this.btnCozinha);
            this.pnlTopo.Controls.Add(this.data);
            this.pnlTopo.Controls.Add(this.hora);
            this.pnlTopo.Controls.Add(this.btnRelatorio);
            this.pnlTopo.Controls.Add(this.btnPedido);
            this.pnlTopo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopo.Location = new System.Drawing.Point(50, 10);
            this.pnlTopo.Name = "pnlTopo";
            this.pnlTopo.Size = new System.Drawing.Size(1234, 92);
            this.pnlTopo.TabIndex = 0;
            // 
            // tbnMusic
            // 
            this.tbnMusic.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.tbnMusic.Location = new System.Drawing.Point(925, 28);
            this.tbnMusic.Name = "tbnMusic";
            this.tbnMusic.Size = new System.Drawing.Size(96, 36);
            this.tbnMusic.TabIndex = 6;
            this.tbnMusic.Text = "Adicionar som de troca de senha";
            this.tbnMusic.UseVisualStyleBackColor = true;
            this.tbnMusic.Click += new System.EventHandler(this.tbnMusic_Click);
            // 
            // btnFila
            // 
            this.btnFila.AccessibleDescription = "Retirada de relatórios";
            this.btnFila.AccessibleName = "Relatório";
            this.btnFila.BackColor = System.Drawing.Color.Transparent;
            this.btnFila.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFila.Image = ((System.Drawing.Image)(resources.GetObject("btnFila.Image")));
            this.btnFila.Location = new System.Drawing.Point(686, 7);
            this.btnFila.Margin = new System.Windows.Forms.Padding(0);
            this.btnFila.Name = "btnFila";
            this.btnFila.Size = new System.Drawing.Size(85, 79);
            this.btnFila.TabIndex = 4;
            this.btnFila.UseVisualStyleBackColor = false;
            this.btnFila.Click += new System.EventHandler(this.btnFila_Click);
            // 
            // btnProdutos
            // 
            this.btnProdutos.AccessibleDescription = "Cadastro de Produtos";
            this.btnProdutos.AccessibleName = "Produtos";
            this.btnProdutos.BackColor = System.Drawing.Color.Transparent;
            this.btnProdutos.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProdutos.Image = ((System.Drawing.Image)(resources.GetObject("btnProdutos.Image")));
            this.btnProdutos.Location = new System.Drawing.Point(438, 6);
            this.btnProdutos.Margin = new System.Windows.Forms.Padding(0);
            this.btnProdutos.Name = "btnProdutos";
            this.btnProdutos.Size = new System.Drawing.Size(85, 79);
            this.btnProdutos.TabIndex = 2;
            this.btnProdutos.UseVisualStyleBackColor = false;
            this.btnProdutos.Click += new System.EventHandler(this.btnProdutos_Click);
            // 
            // btnCozinha
            // 
            this.btnCozinha.AccessibleDescription = "Fila de pedidos";
            this.btnCozinha.AccessibleName = "Cozinha";
            this.btnCozinha.BackColor = System.Drawing.Color.Transparent;
            this.btnCozinha.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCozinha.Image = ((System.Drawing.Image)(resources.GetObject("btnCozinha.Image")));
            this.btnCozinha.Location = new System.Drawing.Point(561, 7);
            this.btnCozinha.Margin = new System.Windows.Forms.Padding(0);
            this.btnCozinha.Name = "btnCozinha";
            this.btnCozinha.Size = new System.Drawing.Size(85, 79);
            this.btnCozinha.TabIndex = 3;
            this.btnCozinha.UseVisualStyleBackColor = false;
            this.btnCozinha.Click += new System.EventHandler(this.btnCozinha_Click);
            // 
            // data
            // 
            this.data.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.data.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.data.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.data.Location = new System.Drawing.Point(27, 45);
            this.data.Multiline = true;
            this.data.Name = "data";
            this.data.ReadOnly = true;
            this.data.Size = new System.Drawing.Size(117, 30);
            this.data.TabIndex = 16;
            this.data.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // hora
            // 
            this.hora.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hora.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.hora.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.hora.Location = new System.Drawing.Point(27, 19);
            this.hora.Multiline = true;
            this.hora.Name = "hora";
            this.hora.ReadOnly = true;
            this.hora.Size = new System.Drawing.Size(117, 30);
            this.hora.TabIndex = 15;
            this.hora.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.AccessibleDescription = "Retirada de relatórios";
            this.btnRelatorio.AccessibleName = "Relatório";
            this.btnRelatorio.BackColor = System.Drawing.Color.Transparent;
            this.btnRelatorio.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRelatorio.Image = ((System.Drawing.Image)(resources.GetObject("btnRelatorio.Image")));
            this.btnRelatorio.Location = new System.Drawing.Point(814, 7);
            this.btnRelatorio.Margin = new System.Windows.Forms.Padding(0);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(85, 79);
            this.btnRelatorio.TabIndex = 5;
            this.btnRelatorio.UseVisualStyleBackColor = false;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // btnPedido
            // 
            this.btnPedido.AccessibleDescription = "Cadastro de pedidos";
            this.btnPedido.AccessibleName = "Pedidos";
            this.btnPedido.BackColor = System.Drawing.Color.Transparent;
            this.btnPedido.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPedido.Image = ((System.Drawing.Image)(resources.GetObject("btnPedido.Image")));
            this.btnPedido.Location = new System.Drawing.Point(312, 7);
            this.btnPedido.Margin = new System.Windows.Forms.Padding(0);
            this.btnPedido.Name = "btnPedido";
            this.btnPedido.Size = new System.Drawing.Size(85, 79);
            this.btnPedido.TabIndex = 1;
            this.btnPedido.UseVisualStyleBackColor = false;
            this.btnPedido.Click += new System.EventHandler(this.btnPedido_Click);
            // 
            // lbTotalText
            // 
            this.lbTotalText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbTotalText.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbTotalText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbTotalText.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbTotalText.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalText.ForeColor = System.Drawing.Color.White;
            this.lbTotalText.Location = new System.Drawing.Point(22, 30);
            this.lbTotalText.Margin = new System.Windows.Forms.Padding(0);
            this.lbTotalText.Name = "lbTotalText";
            this.lbTotalText.Padding = new System.Windows.Forms.Padding(5, 5, 70, 5);
            this.lbTotalText.Size = new System.Drawing.Size(211, 80);
            this.lbTotalText.TabIndex = 1;
            this.lbTotalText.Text = "TOTAL";
            this.lbTotalText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPrepText
            // 
            this.lbPrepText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPrepText.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbPrepText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbPrepText.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbPrepText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrepText.ForeColor = System.Drawing.Color.White;
            this.lbPrepText.Location = new System.Drawing.Point(22, 133);
            this.lbPrepText.Margin = new System.Windows.Forms.Padding(0);
            this.lbPrepText.Name = "lbPrepText";
            this.lbPrepText.Padding = new System.Windows.Forms.Padding(5, 5, 4, 5);
            this.lbPrepText.Size = new System.Drawing.Size(211, 80);
            this.lbPrepText.TabIndex = 2;
            this.lbPrepText.Text = "PREPARANDO";
            this.lbPrepText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbProntosText
            // 
            this.lbProntosText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbProntosText.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbProntosText.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbProntosText.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbProntosText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProntosText.ForeColor = System.Drawing.Color.White;
            this.lbProntosText.Location = new System.Drawing.Point(22, 242);
            this.lbProntosText.Margin = new System.Windows.Forms.Padding(0);
            this.lbProntosText.Name = "lbProntosText";
            this.lbProntosText.Padding = new System.Windows.Forms.Padding(5, 5, 43, 5);
            this.lbProntosText.Size = new System.Drawing.Size(211, 80);
            this.lbProntosText.TabIndex = 3;
            this.lbProntosText.Text = "PRONTOS";
            this.lbProntosText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbTotal.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbTotal.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.ForeColor = System.Drawing.Color.White;
            this.lbTotal.Location = new System.Drawing.Point(269, 30);
            this.lbTotal.Margin = new System.Windows.Forms.Padding(0);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Padding = new System.Windows.Forms.Padding(5);
            this.lbTotal.Size = new System.Drawing.Size(228, 80);
            this.lbTotal.TabIndex = 4;
            this.lbTotal.Text = "R$11590,00";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPreparando
            // 
            this.lbPreparando.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbPreparando.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbPreparando.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbPreparando.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbPreparando.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPreparando.ForeColor = System.Drawing.Color.White;
            this.lbPreparando.Location = new System.Drawing.Point(269, 133);
            this.lbPreparando.Margin = new System.Windows.Forms.Padding(0);
            this.lbPreparando.Name = "lbPreparando";
            this.lbPreparando.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.lbPreparando.Size = new System.Drawing.Size(228, 80);
            this.lbPreparando.TabIndex = 5;
            this.lbPreparando.Text = "10";
            this.lbPreparando.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbProntos
            // 
            this.lbProntos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbProntos.BackColor = System.Drawing.SystemColors.HotTrack;
            this.lbProntos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbProntos.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbProntos.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProntos.ForeColor = System.Drawing.Color.White;
            this.lbProntos.Location = new System.Drawing.Point(269, 242);
            this.lbProntos.Margin = new System.Windows.Forms.Padding(0);
            this.lbProntos.Name = "lbProntos";
            this.lbProntos.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            this.lbProntos.Size = new System.Drawing.Size(228, 80);
            this.lbProntos.TabIndex = 6;
            this.lbProntos.Text = "25";
            this.lbProntos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.lbPreparando);
            this.panel1.Controls.Add(this.lbProntos);
            this.panel1.Controls.Add(this.lbProntosText);
            this.panel1.Controls.Add(this.lbTotalText);
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.lbPrepText);
            this.panel1.Location = new System.Drawing.Point(50, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 356);
            this.panel1.TabIndex = 7;
            // 
            // fotoClube
            // 
            this.fotoClube.Enabled = false;
            this.fotoClube.ErrorImage = ((System.Drawing.Image)(resources.GetObject("fotoClube.ErrorImage")));
            this.fotoClube.Image = ((System.Drawing.Image)(resources.GetObject("fotoClube.Image")));
            this.fotoClube.InitialImage = ((System.Drawing.Image)(resources.GetObject("fotoClube.InitialImage")));
            this.fotoClube.Location = new System.Drawing.Point(611, 122);
            this.fotoClube.Name = "fotoClube";
            this.fotoClube.Size = new System.Drawing.Size(413, 356);
            this.fotoClube.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fotoClube.TabIndex = 8;
            this.fotoClube.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "MP3(*.mp3)|*.mp3";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1219, 630);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 36);
            this.button1.TabIndex = 9;
            this.button1.Text = "local_app (devOnly)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(7, 673);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1334, 696);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fotoClube);
            this.Controls.Add(this.pnlTopo);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Menu";
            this.Padding = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Menu_Load);
            this.pnlTopo.ResumeLayout(false);
            this.pnlTopo.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fotoClube)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTopo;
        private System.Windows.Forms.Label lbTotalText;
        private System.Windows.Forms.Label lbPrepText;
        private System.Windows.Forms.Label lbProntosText;
        private System.Windows.Forms.Button btnPedido;
        private System.Windows.Forms.Button btnRelatorio;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbPreparando;
        private System.Windows.Forms.Label lbProntos;
        private System.Windows.Forms.TextBox data;
        private System.Windows.Forms.TextBox hora;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnCozinha;
        private System.Windows.Forms.Button btnProdutos;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnFila;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox fotoClube;
        private System.Windows.Forms.Button tbnMusic;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
    }
}