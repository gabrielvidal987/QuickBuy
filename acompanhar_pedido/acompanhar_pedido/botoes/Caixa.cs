using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    //tela de controle de caixa: mostra o saldo atual e o extrato de entradas/saídas (inclusive sangria) do usuário logado,
    //e permite lançar novas entradas/saídas manuais através do MovimentoCaixaDialog
    public partial class Caixa : Form
    {
        public Caixa()
        {
            InitializeComponent();
        }

        private void Caixa_Load(object sender, EventArgs e)
        {
            Estetica();
            AtualizaTela();
        }

        public void Estetica()
        {
            this.BackColor = Color.FromArgb(198, 213, 239);
            lbTitulo.BackColor = Color.FromArgb(141, 172, 222);
            lbSaldoTitulo.BackColor = Color.FromArgb(141, 172, 222);
            lbSaldo.BackColor = Color.FromArgb(27, 133, 254);
            lbSaldo.ForeColor = Color.White;
            btnNovaEntrada.BackColor = Color.Honeydew;
            btnNovaSaida.BackColor = Color.MistyRose;
        }

        //recarrega o saldo e o extrato de lançamentos de caixa
        public void AtualizaTela()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                lbSaldo.Text = $"R${sql.SaldoCaixa()}";
                List<Dictionary<string, string>> movimentos = sql.ListaMovimentosCaixa();
                DataTable dt = new DataTable();
                dt.Columns.Add("Data/Hora");
                dt.Columns.Add("Tipo");
                dt.Columns.Add("Categoria");
                dt.Columns.Add("Valor (R$)");
                dt.Columns.Add("Forma Pagamento");
                dt.Columns.Add("Descrição");
                dt.Columns.Add("Documento");
                foreach (Dictionary<string, string> mov in movimentos)
                {
                    dt.Rows.Add(mov["data_hora"], mov["tipo"], mov["categoria"], mov["valor"].Replace('.', ','), mov["forma_pagamento"], mov["descricao"], mov["documento"]);
                }
                tabelaCaixa.DataSource = dt;
                tabelaCaixa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }

        private void btnNovaEntrada_Click(object sender, EventArgs e)
        {
            using (MovimentoCaixaDialog dialog = new MovimentoCaixaDialog("ENTRADA"))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK) { AtualizaTela(); }
            }
        }

        private void btnNovaSaida_Click(object sender, EventArgs e)
        {
            using (MovimentoCaixaDialog dialog = new MovimentoCaixaDialog("SAIDA"))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK) { AtualizaTela(); }
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizaTela();
        }
    }
}
