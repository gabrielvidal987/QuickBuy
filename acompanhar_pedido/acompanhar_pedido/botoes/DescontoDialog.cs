using System;
using System.Globalization;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    //modal aberto pelo botão "DESCONTO" da tela de pedido. Mostra o valor nominal do extrato e deixa
    //informar um desconto em R$ e/ou em %. Ao salvar, quem chamou aplica o desconto no total do pedido.
    public partial class DescontoDialog : Form
    {
        private readonly decimal valorNominal;

        //desconto que o usuário confirmou (0 se cancelou)
        public decimal DescontoReais { get; private set; }
        public decimal DescontoPercent { get; private set; }

        public DescontoDialog(decimal valorNominal, decimal descontoReaisAtual, decimal descontoPercentAtual)
        {
            InitializeComponent();
            this.valorNominal = valorNominal;
            this.AcceptButton = btnSalvar;
            this.CancelButton = btnCancelar;

            lbNominalValor.Text = "R$ " + valorNominal.ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
            numReais.Maximum = valorNominal < 0 ? 0 : valorNominal;
            numReais.Value = Clamp(descontoReaisAtual, 0, numReais.Maximum);
            numPct.Value = Clamp(descontoPercentAtual, 0, 100);
            numReais.ValueChanged += (s, e) => AtualizaFinal();
            numPct.ValueChanged += (s, e) => AtualizaFinal();
            AtualizaFinal();
            numReais.Select(0, numReais.Text.Length);
        }

        private static decimal Clamp(decimal valor, decimal min, decimal max)
        {
            if (valor < min) { return min; }
            if (valor > max) { return max; }
            return valor;
        }

        //desconto total = desconto em R$ + (% sobre o nominal), nunca passando do valor nominal
        private decimal DescontoCalculado()
        {
            decimal d = numReais.Value + valorNominal * numPct.Value / 100m;
            if (d < 0m) { d = 0m; }
            if (d > valorNominal) { d = valorNominal; }
            return d;
        }

        private void AtualizaFinal()
        {
            CultureInfo br = CultureInfo.GetCultureInfo("pt-BR");
            decimal desconto = DescontoCalculado();
            decimal final = valorNominal - desconto;
            lbFinalValor.Text = "R$ " + final.ToString("N2", br);
            lbDescTotal.Text = "desconto total: -R$ " + desconto.ToString("N2", br);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            //guarda a % apenas se o desconto total não estourou o nominal; senão converte tudo pra R$ pra não confundir
            if (numReais.Value + valorNominal * numPct.Value / 100m > valorNominal)
            {
                DescontoReais = valorNominal;
                DescontoPercent = 0m;
            }
            else
            {
                DescontoReais = numReais.Value;
                DescontoPercent = numPct.Value;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
