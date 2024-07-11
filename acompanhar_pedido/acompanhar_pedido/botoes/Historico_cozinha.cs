using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    public partial class Historico_cozinha : Form
    {
        public Historico_cozinha()
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
            pnlGeral.AutoScroll = true;
            RecarregaFila();
        }
        public void RecarregaFila()
        {
            try
            {
                string desc = "desc";
                pnlGeral.Controls.Clear();
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaAnt(desc));
                foreach (Dictionary<string, string> i in filaPedidos)
                {
                    int quantLetras = i["produtos_nome"].Split(',').ToList().Count;
                    double altura = quantLetras * 30;
                    FlowLayoutPanel pnl = new FlowLayoutPanel();
                    Label num_pedido_nome = new Label();
                    Label nome_produto = new Label();
                    Label obs = new Label();
                    Label hora = new Label();
                    Label hora_pronta = new Label();
                    //cria o flowpanel com o cliente
                    pnl.Width = 240;
                    pnl.Height = 140 + Convert.ToInt32(altura);
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
                    hora_pronta.AutoSize = false;
                    hora_pronta.Text = $"Hora que ficou pronto: {i["hora_ficou_pronto"]}";
                    hora_pronta.BorderStyle = BorderStyle.FixedSingle;
                    hora_pronta.TextAlign = ContentAlignment.MiddleCenter;
                    hora_pronta.Width = 225;
                    hora_pronta.Height = 40;
                    hora_pronta.Font = new Font("Arial", 10);
                    //cria as labels no panel
                    pnl.Controls.Add(num_pedido_nome);
                    pnl.Controls.Add(nome_produto);
                    pnl.Controls.Add(obs);
                    pnl.Controls.Add(hora);
                    pnl.Controls.Add(hora_pronta);
                }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void reset_timer_Tick(object sender, EventArgs e)
        {
            if (pnlGeral.AutoScrollPosition.Y != 0)
            {
                return;
            }
            RecarregaFila();
        }
    }
}
