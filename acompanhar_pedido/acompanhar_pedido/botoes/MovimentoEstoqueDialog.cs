using System;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    //modal usado pelos botões "+ ENTRADA" e "− SAÍDA" da tela de Produtos para registrar uma movimentação de estoque
    public partial class MovimentoEstoqueDialog : Form
    {
        private readonly string tipo;
        private readonly string idProduto;

        public MovimentoEstoqueDialog(string tipo, string idProduto, string nomeProduto, decimal custoAtual)
        {
            InitializeComponent();
            this.tipo = tipo;
            this.idProduto = idProduto;
            Estetica(nomeProduto, custoAtual);
        }

        private void Estetica(string nomeProduto, decimal custoAtual)
        {
            bool ehEntrada = tipo == "ENTRADA";
            this.Text = ehEntrada ? "Entrada de Estoque" : "Saída de Estoque";
            lbTitulo.Text = $"{(ehEntrada ? "ENTRADA" : "SAÍDA")} DE ESTOQUE\n{nomeProduto}";
            lbTitulo.BackColor = ehEntrada ? System.Drawing.Color.Honeydew : System.Drawing.Color.MistyRose;
            btnConfirmar.BackColor = ehEntrada ? System.Drawing.Color.LightGreen : System.Drawing.Color.LightSalmon;
            //custo unitário e fornecedor só fazem sentido numa entrada (compra/reposição)
            lblCusto.Visible = ehEntrada;
            numCusto.Visible = ehEntrada;
            lblFornecedor.Visible = ehEntrada;
            txtFornecedor.Visible = ehEntrada;
            numCusto.Value = custoAtual;
            lblMotivo.Text = ehEntrada ? "Motivo (opcional):" : "Motivo (perda, quebra, uso interno, devolução...):";
            numQuantidade.Focus();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (numQuantidade.Value <= 0)
            {
                MessageBox.Show("Informe uma quantidade maior que zero.");
                return;
            }
            ConectarSqlClasse sql = new ConectarSqlClasse();
            string resultado = sql.RegistraMovimentoEstoque(
                idProduto,
                tipo,
                numQuantidade.Value.ToString().Replace(',', '.'),
                numCusto.Visible ? numCusto.Value.ToString().Replace(',', '.') : "0",
                txtFornecedor.Visible ? txtFornecedor.Text.Trim() : "",
                txtDocumento.Text.Trim(),
                txtMotivo.Text.Trim());
            MessageBox.Show(resultado);
            if (resultado.StartsWith("Entrada de") || resultado.StartsWith("Saída de"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
