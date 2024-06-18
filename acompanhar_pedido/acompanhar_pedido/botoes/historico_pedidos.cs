using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    public partial class historico_pedidos : Form
    {
        public historico_pedidos()
        {
            InitializeComponent();
            Estetica();
        }
        public void Estetica()
        {
            this.BackColor = Color.FromArgb(198, 213, 239);
            pnlGeral.BackColor = Color.FromArgb(141, 172, 222);
            pnlGeral.Width = this.Width - 60;
            pnlGeral.Height = this.Height - 150;
            pnlGeral.Location = new Point(Top = lbTitulo.Width);
            RecarregaFila();
        }
        public void RecarregaFila()
        {
            try
            {
                pnlGeral.Controls.Clear();
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaCadPed());
                if (filaPedidos.Count > 0 )
                {
                    foreach (Dictionary<string, string> i in filaPedidos)
                    {
                        int quantLetras = i["produtos_nome"].ToList().Count;
                        if (quantLetras < 15) { quantLetras = 15; }
                        double altura = (quantLetras / 15) * 25;
                        FlowLayoutPanel pnl = new FlowLayoutPanel();
                        Label num_pedido_nome = new Label();
                        Label endereco = new Label();
                        Label nome_produto = new Label();
                        Label obs = new Label();
                        Label hora = new Label();
                        Label valorTotal_formPagamento = new Label();
                        //cria o flowpanel com o cliente
                        pnl.Width = 240;
                        pnl.Height = 200 + Convert.ToInt32(altura);
                        pnl.BackColor = Color.FromArgb(247, 247, 247);
                        pnl.Margin = new Padding(40, 10, 0, 10);
                        pnl.Padding = new Padding(5, 5, 5, 5);
                        pnl.BorderStyle = BorderStyle.FixedSingle;
                        pnlGeral.Controls.Add(pnl);
                        //label numero de pedido
                        num_pedido_nome.AutoSize = false;
                        num_pedido_nome.Text = $"{i["numero_pedido"]} - {i["nome_cliente"]}";
                        num_pedido_nome.BorderStyle = BorderStyle.FixedSingle;
                        num_pedido_nome.TextAlign = ContentAlignment.MiddleCenter;
                        num_pedido_nome.Width = 225;
                        num_pedido_nome.Height = 30;
                        num_pedido_nome.Font = new Font("Arial", 10);
                        //label endereco
                        endereco.AutoSize = false;
                        endereco.Text = $"{i["endereco"]}";
                        endereco.BorderStyle = BorderStyle.FixedSingle;
                        endereco.TextAlign = ContentAlignment.MiddleCenter;
                        endereco.Width = 225;
                        endereco.Height = 50;
                        endereco.Font = new Font("Arial", 10);
                        //flowpanel itens
                        nome_produto.BorderStyle = BorderStyle.FixedSingle;
                        nome_produto.Font = new Font("Arial", 8);
                        nome_produto.Padding = new Padding(5, 5, 5, 5);
                        nome_produto.Width = 225;
                        nome_produto.Height = Convert.ToInt32(altura);
                        nome_produto.Text = $"{i["produtos_nome"].Replace(",", "\n")}";
                        //label obs
                        obs.AutoSize = false;
                        obs.Text = i["observacoes"];
                        obs.BorderStyle = BorderStyle.None;
                        obs.Width = 225;
                        obs.Height = 30;
                        //label hora
                        hora.AutoSize = false;
                        hora.Text = $"Hora do pedido: {i["hora_pedido"]}";
                        hora.BorderStyle = BorderStyle.FixedSingle;
                        hora.TextAlign = ContentAlignment.MiddleCenter;
                        hora.Width = 225;
                        hora.Height = 20;
                        hora.Font = new Font("Arial", 10);
                        //label hora que ficou pronta
                        valorTotal_formPagamento.AutoSize = false;
                        valorTotal_formPagamento.Text = $"Valor: R${i["valorTotal"]} -- {i["formaPag"]} ";
                        valorTotal_formPagamento.BorderStyle = BorderStyle.FixedSingle;
                        valorTotal_formPagamento.TextAlign = ContentAlignment.MiddleCenter;
                        valorTotal_formPagamento.Width = 225;
                        valorTotal_formPagamento.Height = 40;
                        valorTotal_formPagamento.Font = new Font("Arial", 10);
                        //cria as labels no panel
                        pnl.Controls.Add(num_pedido_nome);
                        pnl.Controls.Add(endereco);
                        pnl.Controls.Add(nome_produto);
                        pnl.Controls.Add(obs);
                        pnl.Controls.Add(hora);
                        pnl.Controls.Add(valorTotal_formPagamento);
                    }
                }
                else { MessageBox.Show("Sem pedidos registrados ainda!!"); }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RecarregaFila();
        }
    }
}