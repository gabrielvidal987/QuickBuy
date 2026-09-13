namespace acompanhar_pedido.botoes
{
    partial class PromocaoCombo
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
            this.lbListaCombos = new System.Windows.Forms.Label();
            this.lstCombos = new System.Windows.Forms.ListBox();
            this.pnlEdicao = new System.Windows.Forms.Panel();
            this.lbEditando = new System.Windows.Forms.Label();
            this.lbNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.lbQtdMin = new System.Windows.Forms.Label();
            this.numQtdMin = new System.Windows.Forms.NumericUpDown();
            this.chkAtivo = new System.Windows.Forms.CheckBox();
            this.lbExplicacao = new System.Windows.Forms.Label();
            this.gridProdutos = new System.Windows.Forms.DataGridView();
            this.colParticipa = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProduto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVarejo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrecoCombo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbValorUnico = new System.Windows.Forms.Label();
            this.numValorUnico = new System.Windows.Forms.NumericUpDown();
            this.btnMesmoValor = new System.Windows.Forms.Button();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.pnlEdicao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQtdMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValorUnico)).BeginInit();
            this.SuspendLayout();
            //
            // lbTitulo
            //
            this.lbTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbTitulo.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(12, 10);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(1000, 44);
            this.lbTitulo.TabIndex = 0;
            this.lbTitulo.Text = "PROMOÇÕES DE ATACADO EM COMBO";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lbListaCombos
            //
            this.lbListaCombos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbListaCombos.Location = new System.Drawing.Point(12, 64);
            this.lbListaCombos.Name = "lbListaCombos";
            this.lbListaCombos.Size = new System.Drawing.Size(240, 20);
            this.lbListaCombos.TabIndex = 1;
            this.lbListaCombos.Text = "COMBOS CADASTRADOS";
            //
            // lstCombos
            //
            this.lstCombos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lstCombos.FormattingEnabled = true;
            this.lstCombos.ItemHeight = 17;
            this.lstCombos.Location = new System.Drawing.Point(12, 86);
            this.lstCombos.Name = "lstCombos";
            this.lstCombos.Size = new System.Drawing.Size(240, 446);
            this.lstCombos.TabIndex = 2;
            this.lstCombos.SelectedIndexChanged += new System.EventHandler(this.lstCombos_SelectedIndexChanged);
            //
            // pnlEdicao
            //
            this.pnlEdicao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEdicao.Controls.Add(this.lbEditando);
            this.pnlEdicao.Controls.Add(this.lbNome);
            this.pnlEdicao.Controls.Add(this.txtNome);
            this.pnlEdicao.Controls.Add(this.lbQtdMin);
            this.pnlEdicao.Controls.Add(this.numQtdMin);
            this.pnlEdicao.Controls.Add(this.chkAtivo);
            this.pnlEdicao.Controls.Add(this.lbExplicacao);
            this.pnlEdicao.Controls.Add(this.gridProdutos);
            this.pnlEdicao.Controls.Add(this.lbValorUnico);
            this.pnlEdicao.Controls.Add(this.numValorUnico);
            this.pnlEdicao.Controls.Add(this.btnMesmoValor);
            this.pnlEdicao.Location = new System.Drawing.Point(264, 64);
            this.pnlEdicao.Name = "pnlEdicao";
            this.pnlEdicao.Size = new System.Drawing.Size(748, 468);
            this.pnlEdicao.TabIndex = 3;
            //
            // lbEditando
            //
            this.lbEditando.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lbEditando.Location = new System.Drawing.Point(14, 8);
            this.lbEditando.Name = "lbEditando";
            this.lbEditando.Size = new System.Drawing.Size(400, 20);
            this.lbEditando.TabIndex = 0;
            this.lbEditando.Text = "Novo combo";
            //
            // lbNome
            //
            this.lbNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbNome.Location = new System.Drawing.Point(14, 36);
            this.lbNome.Name = "lbNome";
            this.lbNome.Size = new System.Drawing.Size(120, 18);
            this.lbNome.TabIndex = 1;
            this.lbNome.Text = "NOME DO COMBO:";
            //
            // txtNome
            //
            this.txtNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtNome.Location = new System.Drawing.Point(14, 56);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(360, 26);
            this.txtNome.TabIndex = 2;
            //
            // lbQtdMin
            //
            this.lbQtdMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbQtdMin.Location = new System.Drawing.Point(392, 36);
            this.lbQtdMin.Name = "lbQtdMin";
            this.lbQtdMin.Size = new System.Drawing.Size(230, 18);
            this.lbQtdMin.TabIndex = 3;
            this.lbQtdMin.Text = "QTD. MÍNIMA TOTAL (soma dos itens):";
            //
            // numQtdMin
            //
            this.numQtdMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numQtdMin.Location = new System.Drawing.Point(392, 56);
            this.numQtdMin.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            this.numQtdMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numQtdMin.Name = "numQtdMin";
            this.numQtdMin.Size = new System.Drawing.Size(110, 26);
            this.numQtdMin.TabIndex = 4;
            this.numQtdMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numQtdMin.Value = new decimal(new int[] { 10, 0, 0, 0 });
            //
            // chkAtivo
            //
            this.chkAtivo.Checked = true;
            this.chkAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAtivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chkAtivo.Location = new System.Drawing.Point(524, 56);
            this.chkAtivo.Name = "chkAtivo";
            this.chkAtivo.Size = new System.Drawing.Size(200, 26);
            this.chkAtivo.TabIndex = 5;
            this.chkAtivo.Text = "Promoção ativa";
            this.chkAtivo.UseVisualStyleBackColor = true;
            //
            // lbExplicacao
            //
            this.lbExplicacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lbExplicacao.Location = new System.Drawing.Point(14, 90);
            this.lbExplicacao.Name = "lbExplicacao";
            this.lbExplicacao.Size = new System.Drawing.Size(716, 32);
            this.lbExplicacao.TabIndex = 6;
            this.lbExplicacao.Text = "Marque os produtos que participam e informe o valor que CADA UNIDADE deles vai custar quando o combo for atingido." +
                "\r\nO combo vale quando a soma das quantidades desses produtos na mesma venda alcançar a quantidade mínima acima.";
            //
            // gridProdutos
            //
            this.gridProdutos.AllowUserToAddRows = false;
            this.gridProdutos.AllowUserToDeleteRows = false;
            this.gridProdutos.AllowUserToResizeRows = false;
            this.gridProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridProdutos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colParticipa,
            this.colId,
            this.colProduto,
            this.colVarejo,
            this.colPrecoCombo});
            this.gridProdutos.Location = new System.Drawing.Point(14, 126);
            this.gridProdutos.Name = "gridProdutos";
            this.gridProdutos.RowHeadersVisible = false;
            this.gridProdutos.Size = new System.Drawing.Size(716, 280);
            this.gridProdutos.TabIndex = 7;
            //
            // colParticipa
            //
            this.colParticipa.HeaderText = "Participa";
            this.colParticipa.Name = "colParticipa";
            this.colParticipa.Width = 70;
            //
            // colId
            //
            this.colId.HeaderText = "id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            //
            // colProduto
            //
            this.colProduto.HeaderText = "Produto";
            this.colProduto.Name = "colProduto";
            this.colProduto.ReadOnly = true;
            this.colProduto.Width = 340;
            //
            // colVarejo
            //
            this.colVarejo.HeaderText = "Preço varejo (R$)";
            this.colVarejo.Name = "colVarejo";
            this.colVarejo.ReadOnly = true;
            this.colVarejo.Width = 130;
            //
            // colPrecoCombo
            //
            this.colPrecoCombo.HeaderText = "Preço no combo (R$)";
            this.colPrecoCombo.Name = "colPrecoCombo";
            this.colPrecoCombo.Width = 160;
            //
            // lbValorUnico
            //
            this.lbValorUnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lbValorUnico.Location = new System.Drawing.Point(14, 420);
            this.lbValorUnico.Name = "lbValorUnico";
            this.lbValorUnico.Size = new System.Drawing.Size(150, 18);
            this.lbValorUnico.TabIndex = 8;
            this.lbValorUnico.Text = "Mesmo valor para todos:";
            //
            // numValorUnico
            //
            this.numValorUnico.DecimalPlaces = 2;
            this.numValorUnico.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.numValorUnico.Location = new System.Drawing.Point(168, 416);
            this.numValorUnico.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            this.numValorUnico.Name = "numValorUnico";
            this.numValorUnico.Size = new System.Drawing.Size(100, 24);
            this.numValorUnico.TabIndex = 9;
            this.numValorUnico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //
            // btnMesmoValor
            //
            this.btnMesmoValor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMesmoValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnMesmoValor.Location = new System.Drawing.Point(276, 414);
            this.btnMesmoValor.Name = "btnMesmoValor";
            this.btnMesmoValor.Size = new System.Drawing.Size(280, 28);
            this.btnMesmoValor.TabIndex = 10;
            this.btnMesmoValor.Text = "APLICAR ESSE VALOR AOS PRODUTOS MARCADOS";
            this.btnMesmoValor.UseVisualStyleBackColor = true;
            this.btnMesmoValor.Click += new System.EventHandler(this.btnMesmoValor_Click);
            //
            // btnNovo
            //
            this.btnNovo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNovo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnNovo.Location = new System.Drawing.Point(12, 544);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(240, 40);
            this.btnNovo.TabIndex = 4;
            this.btnNovo.Text = "NOVO COMBO";
            this.btnNovo.UseVisualStyleBackColor = false;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            //
            // btnSalvar
            //
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.Location = new System.Drawing.Point(264, 544);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(300, 40);
            this.btnSalvar.TabIndex = 5;
            this.btnSalvar.Text = "SALVAR COMBO";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            //
            // btnExcluir
            //
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnExcluir.Location = new System.Drawing.Point(576, 544);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(220, 40);
            this.btnExcluir.TabIndex = 6;
            this.btnExcluir.Text = "EXCLUIR COMBO";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            //
            // btnFechar
            //
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnFechar.Location = new System.Drawing.Point(872, 544);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(140, 40);
            this.btnFechar.TabIndex = 7;
            this.btnFechar.Text = "FECHAR";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            //
            // PromocaoCombo
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 600);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.pnlEdicao);
            this.Controls.Add(this.lstCombos);
            this.Controls.Add(this.lbListaCombos);
            this.Controls.Add(this.lbTitulo);
            this.MinimizeBox = false;
            this.Name = "PromocaoCombo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Promoções de atacado em combo";
            this.Load += new System.EventHandler(this.PromocaoCombo_Load);
            this.pnlEdicao.ResumeLayout(false);
            this.pnlEdicao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQtdMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValorUnico)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbListaCombos;
        private System.Windows.Forms.ListBox lstCombos;
        private System.Windows.Forms.Panel pnlEdicao;
        private System.Windows.Forms.Label lbEditando;
        private System.Windows.Forms.Label lbNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lbQtdMin;
        private System.Windows.Forms.NumericUpDown numQtdMin;
        private System.Windows.Forms.CheckBox chkAtivo;
        private System.Windows.Forms.Label lbExplicacao;
        private System.Windows.Forms.DataGridView gridProdutos;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colParticipa;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVarejo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrecoCombo;
        private System.Windows.Forms.Label lbValorUnico;
        private System.Windows.Forms.NumericUpDown numValorUnico;
        private System.Windows.Forms.Button btnMesmoValor;
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnFechar;
    }
}
