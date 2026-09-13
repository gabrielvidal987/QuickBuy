using System;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    //modal usado pelos botões "Nova Entrada"/"Nova Saída" da tela de Caixa para lançar um movimento (inclui sangria como categoria de saída)
    public partial class MovimentoCaixaDialog : Form
    {
        private readonly string tipo;

        public MovimentoCaixaDialog(string tipo)
        {
            InitializeComponent();
            this.tipo = tipo;
            Estetica();
        }

        private void Estetica()
        {
            bool ehEntrada = tipo == "ENTRADA";
            this.Text = ehEntrada ? "Nova Entrada de Caixa" : "Nova Saída de Caixa";
            lbTitulo.Text = ehEntrada ? "ENTRADA DE CAIXA" : "SAÍDA DE CAIXA";
            lbTitulo.BackColor = ehEntrada ? System.Drawing.Color.Honeydew : System.Drawing.Color.MistyRose;
            btnConfirmar.BackColor = ehEntrada ? System.Drawing.Color.LightGreen : System.Drawing.Color.LightSalmon;
            cbCategoria.Items.Clear();
            if (ehEntrada)
            {
                cbCategoria.Items.AddRange(new object[] { "Suprimento (reforço de caixa)", "Recebimento", "Outro" });
            }
            else
            {
                cbCategoria.Items.AddRange(new object[] { "Sangria", "Pagamento a fornecedor", "Despesa", "Outro" });
            }
            cbCategoria.SelectedIndex = 0;
            cbFormaPagamento.Items.Clear();
            cbFormaPagamento.Items.AddRange(new object[] { "", "Dinheiro", "PIX", "Débito", "Crédito" });
            cbFormaPagamento.SelectedIndex = 0;
            numValor.Focus();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (numValor.Value <= 0)
            {
                MessageBox.Show("Informe um valor maior que zero.");
                return;
            }
            ConectarSqlClasse sql = new ConectarSqlClasse();
            string resultado = sql.RegistraMovimentoCaixa(
                tipo,
                cbCategoria.Text.Trim(),
                numValor.Value.ToString().Replace(',', '.'),
                cbFormaPagamento.Text.Trim(),
                txtDescricao.Text.Trim(),
                txtDocumento.Text.Trim());
            MessageBox.Show(resultado);
            if (resultado.Contains("registrada com sucesso"))
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
