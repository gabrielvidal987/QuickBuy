namespace acompanhar_pedido
{
    partial class FrmLogin
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSenha = new acompanhar_pedido.PlaceHolderTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.password = new acompanhar_pedido.PlaceHolderTextBox();
            this.uid = new acompanhar_pedido.PlaceHolderTextBox();
            this.database = new acompanhar_pedido.PlaceHolderTextBox();
            this.testConexaobtn = new System.Windows.Forms.Button();
            this.server = new acompanhar_pedido.PlaceHolderTextBox();
            this.resultConexao = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlLogin.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlLogin.BackgroundImage")));
            this.pnlLogin.Controls.Add(this.txtEmail);
            this.pnlLogin.Controls.Add(this.txtSenha);
            this.pnlLogin.Controls.Add(this.button1);
            this.pnlLogin.Location = new System.Drawing.Point(496, 158);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(360, 392);
            this.pnlLogin.TabIndex = 0;
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(61, 261);
            this.txtEmail.Multiline = true;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(238, 16);
            this.txtEmail.TabIndex = 5;
            // 
            // txtSenha
            // 
            this.txtSenha.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.txtSenha.ForeColor = System.Drawing.Color.Gray;
            this.txtSenha.Location = new System.Drawing.Point(61, 310);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.PlaceHolderText = "";
            this.txtSenha.Size = new System.Drawing.Size(238, 16);
            this.txtSenha.TabIndex = 4;
            this.txtSenha.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSenha_KeyDown);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(128, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "ENTRAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.password);
            this.panel2.Controls.Add(this.uid);
            this.panel2.Controls.Add(this.database);
            this.panel2.Controls.Add(this.testConexaobtn);
            this.panel2.Controls.Add(this.server);
            this.panel2.Controls.Add(this.resultConexao);
            this.panel2.Location = new System.Drawing.Point(1132, 349);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(225, 345);
            this.panel2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.Enabled = false;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(5, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "DATABASE:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Enabled = false;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(5, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "PASSWORD:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Enabled = false;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(5, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "UID:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "SERVER:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // password
            // 
            this.password.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.password.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.password.ForeColor = System.Drawing.Color.Gray;
            this.password.Location = new System.Drawing.Point(5, 133);
            this.password.Multiline = true;
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.PlaceHolderText = "*********";
            this.password.Size = new System.Drawing.Size(215, 34);
            this.password.TabIndex = 14;
            this.password.Text = "*************";
            this.password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.password_KeyDown);
            // 
            // uid
            // 
            this.uid.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.uid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.uid.ForeColor = System.Drawing.Color.Gray;
            this.uid.Location = new System.Drawing.Point(5, 79);
            this.uid.Multiline = true;
            this.uid.Name = "uid";
            this.uid.PlaceHolderText = "root";
            this.uid.Size = new System.Drawing.Size(215, 34);
            this.uid.TabIndex = 13;
            this.uid.Text = "root";
            this.uid.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uid_KeyDown);
            // 
            // database
            // 
            this.database.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.database.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.database.ForeColor = System.Drawing.Color.Gray;
            this.database.Location = new System.Drawing.Point(5, 185);
            this.database.Multiline = true;
            this.database.Name = "database";
            this.database.PlaceHolderText = "acompanha_pedidosschema";
            this.database.Size = new System.Drawing.Size(215, 34);
            this.database.TabIndex = 15;
            this.database.Text = "acompanha_pedidosschema";
            this.database.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.database.KeyDown += new System.Windows.Forms.KeyEventHandler(this.database_KeyDown);
            // 
            // testConexaobtn
            // 
            this.testConexaobtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testConexaobtn.Location = new System.Drawing.Point(5, 223);
            this.testConexaobtn.Name = "testConexaobtn";
            this.testConexaobtn.Size = new System.Drawing.Size(215, 24);
            this.testConexaobtn.TabIndex = 10;
            this.testConexaobtn.Text = "testar conexão";
            this.testConexaobtn.UseVisualStyleBackColor = true;
            this.testConexaobtn.Click += new System.EventHandler(this.testConexaobtn_Click);
            // 
            // server
            // 
            this.server.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.server.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic);
            this.server.ForeColor = System.Drawing.Color.Gray;
            this.server.Location = new System.Drawing.Point(5, 24);
            this.server.Multiline = true;
            this.server.Name = "server";
            this.server.PlaceHolderText = "localhost";
            this.server.Size = new System.Drawing.Size(215, 34);
            this.server.TabIndex = 12;
            this.server.Text = "localhost";
            this.server.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.server.KeyDown += new System.Windows.Forms.KeyEventHandler(this.server_KeyDown);
            // 
            // resultConexao
            // 
            this.resultConexao.BackColor = System.Drawing.SystemColors.MenuText;
            this.resultConexao.ForeColor = System.Drawing.Color.Lime;
            this.resultConexao.Location = new System.Drawing.Point(5, 253);
            this.resultConexao.Multiline = true;
            this.resultConexao.Name = "resultConexao";
            this.resultConexao.ReadOnly = true;
            this.resultConexao.Size = new System.Drawing.Size(215, 86);
            this.resultConexao.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(12, 707);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "© CopyRight - programa desenvolvido por Gabriel Vidal Teixeira";
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.Text = "Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLogin_KeyDown);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Button button1;
        private PlaceHolderTextBox txtSenha;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private PlaceHolderTextBox password;
        private PlaceHolderTextBox uid;
        private PlaceHolderTextBox database;
        private System.Windows.Forms.Button testConexaobtn;
        private PlaceHolderTextBox server;
        private System.Windows.Forms.TextBox resultConexao;
        private System.Windows.Forms.Label label5;
    }
}

