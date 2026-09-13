using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    //tela de configuração das promoções de atacado em combo: escolhe os produtos participantes, a quantidade mínima
    //SOMADA entre eles e o preço unitário que cada produto assume quando o combo é atingido na venda.
    public partial class PromocaoCombo : Form
    {
        private string idComboEmEdicao;
        //id_combo -> nome, para preencher a lista da esquerda
        private List<Dictionary<string, string>> combosCarregados = new List<Dictionary<string, string>>();

        //colunas da grade de produtos
        private const int COL_PARTICIPA = 0;
        private const int COL_ID = 1;
        private const int COL_PRODUTO = 2;
        private const int COL_VAREJO = 3;
        private const int COL_PRECO_COMBO = 4;

        public PromocaoCombo()
        {
            InitializeComponent();
        }

        private void PromocaoCombo_Load(object sender, EventArgs e)
        {
            Estetica();
            MontaGradeProdutos();
            CarregaListaCombos();
            NovoCombo();
        }

        public void Estetica()
        {
            this.BackColor = Color.FromArgb(198, 213, 239);
            lbTitulo.BackColor = Color.FromArgb(141, 172, 222);
            pnlEdicao.BackColor = Color.FromArgb(192, 213, 239);
            btnSalvar.BackColor = Color.FromArgb(132, 246, 58);
            btnExcluir.BackColor = Color.MistyRose;
            btnNovo.BackColor = Color.FromArgb(141, 172, 222);
        }

        //monta a grade com todos os produtos cadastrados (a marcação e o preço do combo ficam por conta do usuário)
        private void MontaGradeProdutos()
        {
            gridProdutos.Rows.Clear();
            ConectarSqlClasse sql = new ConectarSqlClasse();
            foreach (Dictionary<string, string> prod in sql.ListaProdutosCadastro(""))
            {
                decimal varejo = ParseDecimal(prod["valor"]);
                gridProdutos.Rows.Add(false, prod["id_produto"], prod["nome"], varejo.ToString("N2"), "");
            }
        }

        //preenche a lista de combos já cadastrados
        private void CarregaListaCombos()
        {
            ConectarSqlClasse sql = new ConectarSqlClasse();
            combosCarregados = sql.ListaCombos(false);
            lstCombos.Items.Clear();
            List<string> jaAdicionados = new List<string>();
            foreach (Dictionary<string, string> linha in combosCarregados)
            {
                if (jaAdicionados.Contains(linha["id_combo"])) { continue; }
                jaAdicionados.Add(linha["id_combo"]);
                string rotulo = $"{linha["nome"]} ({linha["qtd_min_total"]}+ un)";
                if (linha["ativo"] != "1") { rotulo += " [inativo]"; }
                lstCombos.Items.Add(new ItemLista(linha["id_combo"], rotulo));
            }
        }

        //limpa o formulário para cadastrar um combo novo
        private void NovoCombo()
        {
            idComboEmEdicao = null;
            txtNome.Text = "";
            numQtdMin.Value = 10;
            chkAtivo.Checked = true;
            lstCombos.ClearSelected();
            foreach (DataGridViewRow linha in gridProdutos.Rows)
            {
                linha.Cells[COL_PARTICIPA].Value = false;
                linha.Cells[COL_PRECO_COMBO].Value = "";
            }
            btnExcluir.Enabled = false;
            lbEditando.Text = "Novo combo";
            txtNome.Focus();
        }

        //carrega no formulário o combo selecionado na lista
        private void lstCombos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemLista selecionado = lstCombos.SelectedItem as ItemLista;
            if (selecionado == null) { return; }
            idComboEmEdicao = selecionado.Id;
            foreach (DataGridViewRow linha in gridProdutos.Rows)
            {
                linha.Cells[COL_PARTICIPA].Value = false;
                linha.Cells[COL_PRECO_COMBO].Value = "";
            }
            foreach (Dictionary<string, string> item in combosCarregados)
            {
                if (item["id_combo"] != idComboEmEdicao) { continue; }
                txtNome.Text = item["nome"];
                numQtdMin.Value = Math.Max(1, ParseInt(item["qtd_min_total"]));
                chkAtivo.Checked = item["ativo"] == "1";
                if (string.IsNullOrEmpty(item["id_produto"])) { continue; }
                foreach (DataGridViewRow linha in gridProdutos.Rows)
                {
                    if (Convert.ToString(linha.Cells[COL_ID].Value) == item["id_produto"])
                    {
                        linha.Cells[COL_PARTICIPA].Value = true;
                        linha.Cells[COL_PRECO_COMBO].Value = ParseDecimal(item["valor_unitario"]).ToString("N2");
                    }
                }
            }
            btnExcluir.Enabled = true;
            lbEditando.Text = $"Editando: {txtNome.Text}";
        }

        //atalho pedido: preenche o mesmo valor em todos os produtos marcados
        private void btnMesmoValor_Click(object sender, EventArgs e)
        {
            gridProdutos.EndEdit();
            if (numValorUnico.Value <= 0)
            {
                MessageBox.Show("Informe o valor que será aplicado aos produtos marcados.");
                return;
            }
            int marcados = 0;
            foreach (DataGridViewRow linha in gridProdutos.Rows)
            {
                if (Convert.ToBoolean(linha.Cells[COL_PARTICIPA].Value))
                {
                    linha.Cells[COL_PRECO_COMBO].Value = numValorUnico.Value.ToString("N2");
                    marcados++;
                }
            }
            if (marcados == 0) { MessageBox.Show("Marque primeiro os produtos que participam do combo."); }
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            NovoCombo();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            gridProdutos.EndEdit();
            List<Dictionary<string, string>> itens = new List<Dictionary<string, string>>();
            foreach (DataGridViewRow linha in gridProdutos.Rows)
            {
                if (!Convert.ToBoolean(linha.Cells[COL_PARTICIPA].Value)) { continue; }
                decimal preco = ParseDecimal(Convert.ToString(linha.Cells[COL_PRECO_COMBO].Value));
                if (preco <= 0)
                {
                    MessageBox.Show($"Informe o valor do combo para o produto '{linha.Cells[COL_PRODUTO].Value}'.");
                    return;
                }
                itens.Add(new Dictionary<string, string>
                {
                    { "id_produto", Convert.ToString(linha.Cells[COL_ID].Value) },
                    { "valor_unitario", preco.ToString(System.Globalization.CultureInfo.InvariantCulture) }
                });
            }
            ConectarSqlClasse sql = new ConectarSqlClasse();
            string resultado = sql.SalvarCombo(idComboEmEdicao, txtNome.Text, numQtdMin.Value.ToString(), chkAtivo.Checked, itens);
            MessageBox.Show(resultado);
            if (resultado.Contains("sucesso"))
            {
                CarregaListaCombos();
                NovoCombo();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idComboEmEdicao)) { return; }
            var confirma = MessageBox.Show($"Excluir o combo '{txtNome.Text}'?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirma != DialogResult.Yes) { return; }
            ConectarSqlClasse sql = new ConectarSqlClasse();
            MessageBox.Show(sql.ExcluirCombo(idComboEmEdicao));
            CarregaListaCombos();
            NovoCombo();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private static decimal ParseDecimal(string valor)
        {
            decimal resultado;
            if (decimal.TryParse((valor ?? "").Replace('.', ','), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), out resultado)) { return resultado; }
            if (decimal.TryParse((valor ?? "").Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out resultado)) { return resultado; }
            return 0;
        }

        private static int ParseInt(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado) ? resultado : 0;
        }

        //item da ListBox que carrega o id junto com o texto exibido
        private class ItemLista
        {
            public string Id;
            private readonly string texto;
            public ItemLista(string id, string texto) { this.Id = id; this.texto = texto; }
            public override string ToString() { return texto; }
        }
    }
}
