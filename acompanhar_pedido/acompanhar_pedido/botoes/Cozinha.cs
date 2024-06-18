using Org.BouncyCastle.Crmf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Security;
using System.Threading;
using System.Transactions;

namespace acompanhar_pedido.botoes
{
    public partial class Cozinha : Form
    {
        Thread t1;
        List<string> pedidoJaNaLista = new List<string>();
        public Cozinha()
        {
            InitializeComponent();
        }
        private void Cozinha_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(198, 213, 239);
                pnlGeral.BackColor = Color.FromArgb(141, 172, 222);
                pnlAnt.BackColor = Color.FromArgb(141, 172, 222);
                pnlGeral.Width = this.Width - 150;
                pnlAnt.Width = this.Width - 150;
                setaSaida.Location = new Point(Left = this.Width - 110, Top = this.Height - 270);
                RecarregaFila();
                reload.Start();
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        public void RecarregaFila()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaProdutos());
                foreach (Dictionary<string, string> i in filaPedidos)
                {
                    if (pedidoJaNaLista.Contains(i["numero_pedido"]))
                    {
                        continue;
                    }
                    int quantLetras = i["produtos_nome"].ToList().Count;
                    if (quantLetras < 15) { quantLetras = 15; }
                    double altura = (quantLetras / 15) * 25;
                    FlowLayoutPanel pnl = new FlowLayoutPanel();
                    Label num_pedido_nome = new Label();
                    FlowLayoutPanel nome_produto = new FlowLayoutPanel();
                    Label obs = new Label();
                    Label hora = new Label();
                    //cria o flowpanel com o cliente
                    pnl.Width = 240;
                    pnl.Height = 130;
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
                    List<string> produtos = new List<string>(i["produtos_nome"].Split(','));
                    nome_produto.AutoSize = true;
                    nome_produto.BorderStyle = BorderStyle.FixedSingle;
                    foreach (string produto in produtos)
                    {
                        if (produto != string.Empty)
                        {
                            int quantLet = produto.ToList().Count;
                            if (quantLet < 15) { quantLet = 15; }
                            double h = (quantLet / 15) * 20;
                            CheckBox prod = new CheckBox();
                            prod.Margin = new Padding(10, 0, 0, 10);
                            prod.Padding = new Padding(0, 0, 0, 0);
                            prod.Text = produto;
                            prod.TextAlign = ContentAlignment.MiddleCenter;
                            prod.Font = new Font("Arial", 10, FontStyle.Bold);
                            prod.Click += new System.EventHandler(this.Marca_Click);
                            prod.AutoSize = false;
                            prod.Height = Convert.ToInt32(h);
                            pnl.Height += Convert.ToInt32(h) + 10;
                            prod.Width = 212;
                            nome_produto.Controls.Add(prod);
                        }
                    }
                    //label obs
                    obs.AutoSize = false;
                    obs.Text = i["observacoes"];
                    obs.BorderStyle = BorderStyle.None;
                    obs.Width = 225;
                    obs.Height = 50;
                    //label hora
                    hora.AutoSize = false;
                    hora.Text = $"Hora do pedido: {i["hora_pedido"]}";
                    hora.BorderStyle = BorderStyle.FixedSingle;
                    hora.TextAlign = ContentAlignment.MiddleCenter;
                    hora.Width = 225;
                    hora.Height = 20;
                    hora.Font = new Font("Arial", 10);
                    //cria as labels no panel
                    pnl.Controls.Add(num_pedido_nome);
                    pnl.Controls.Add(nome_produto);
                    pnl.Controls.Add(obs);
                    pnl.Controls.Add(hora);
                    pedidoJaNaLista.Add(i["numero_pedido"]);
                }
                CarregaAnt();

            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        public void CarregaAnt()
        {
            string asc = "asc";
            try
            {
                if(pnlAnt.Controls.Count != 0)
                {
                    pnlAnt.Controls.Remove(pnlAnt.Controls[1]);
                    pnlAnt.Controls.Remove(pnlAnt.Controls[1]);
                    pnlAnt.Controls.Remove(pnlAnt.Controls[1]);
                    pnlAnt.Controls.Remove(pnlAnt.Controls[1]);
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
            ConectarSqlClasse sql = new ConectarSqlClasse();
            List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaAnt(asc));
            foreach (Dictionary<string, string> i in filaPedidos)
            {
                try
                {
                    if (i == filaPedidos[filaPedidos.Count-1] || i == filaPedidos[filaPedidos.Count-2] || i == filaPedidos[filaPedidos.Count-3] || i == filaPedidos[filaPedidos.Count-4])
                    {
                        FlowLayoutPanel pnl = new FlowLayoutPanel();
                        Label num_pedido_nome = new Label();
                        Label nome_produto = new Label();
                        Label obs = new Label();
                        Label hora = new Label();
                        Label hora_pronta = new Label();
                        //cria o flowpanel com o cliente
                        pnl.Width = 210;
                        pnl.Height = 180;
                        pnl.BackColor = Color.FromArgb(247, 247, 247);
                        pnl.Margin = new Padding(40, 10, 0, 10);
                        pnl.Padding = new Padding(5, 5, 5, 5);
                        pnl.BorderStyle = BorderStyle.FixedSingle;
                        pnlAnt.Controls.Add(pnl);
                        //label numero de pedido
                        num_pedido_nome.AutoSize = false;
                        num_pedido_nome.Text = $"{i["numero_pedido"]} - {i["nome_cliente"]}";
                        num_pedido_nome.BorderStyle = BorderStyle.FixedSingle;
                        num_pedido_nome.TextAlign = ContentAlignment.MiddleCenter;
                        num_pedido_nome.Width = 195;
                        num_pedido_nome.Height = 30;
                        num_pedido_nome.Font = new Font("Arial", 10);
                        //flowpanel itens
                        nome_produto.BorderStyle = BorderStyle.FixedSingle;
                        nome_produto.Font = new Font("Arial", 8);
                        nome_produto.Padding = new Padding(5, 5, 5, 5);
                        nome_produto.Width = 195;
                        nome_produto.Height = 40;
                        nome_produto.Text = i["produtos_nome"];
                        //label obs
                        obs.AutoSize = false;
                        obs.Text = i["observacoes"];
                        obs.BorderStyle = BorderStyle.None;
                        obs.Width = 195;
                        obs.Height = 30;
                        //label hora
                        hora.AutoSize = false;
                        hora.Text = $"Hora do pedido: {i["hora_pedido"]}";
                        hora.BorderStyle = BorderStyle.FixedSingle;
                        hora.TextAlign = ContentAlignment.MiddleCenter;
                        hora.Width = 195;
                        hora.Height = 20;
                        hora.Font = new Font("Arial", 10);
                        //label hora que ficou pronta
                        hora_pronta.AutoSize = false;
                        hora_pronta.Text = $"Hora que ficou pronto: {i["hora_ficou_pronto"]}";
                        hora_pronta.BorderStyle = BorderStyle.FixedSingle;
                        hora_pronta.TextAlign = ContentAlignment.MiddleCenter;
                        hora_pronta.Width = 195;
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
                catch (Exception er)
                {
                    ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    MessageBox.Show("Erro ao criar um item da lista de histórico de pedidos prontos");
                    continue;
                }
            }
        }
        private void Marca_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                for (int j = 0; j < pnlGeral.Controls.Count; j++)
                {
                    int check = 0;
                    foreach (var h in pnlGeral.Controls[j].Controls[1].Controls)
                    {
                        //checked == 1
                        int c = int.Parse(h.ToString().Split()[2]);
                        check += c;
                    }
                    int total = pnlGeral.Controls[j].Controls[1].Controls.Count;
                    string textid = pnlGeral.Controls[j].Controls[0].Text;
                    int numero_pedido = int.Parse(textid.Split()[0]);
                    if (total == check)
                    {
                        var r = MessageBox.Show("Pedido completo?", "Atenção", MessageBoxButtons.YesNo);
                        if (r == DialogResult.Yes)
                        {
                            DateTime hora = DateTime.Now;
                            string horario_fechamento = hora.ToString("HH:mm:ss");
                            pnlGeral.Controls.Remove(pnlGeral.Controls[j]);
                            sql.ApagaPedidoFila(numero_pedido, horario_fechamento);
                            RecarregaFila();
                        }
                    }
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        private void btnHistorico_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(abrirHistorico);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        private void abrirHistorico(object obj)
        {
            try { Application.Run(new Historico_cozinha()); } catch (Exception er){ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);}
        }
        private async void reload_Tick(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= 200; i++)
                {
                    if (reloadBar.Value != 200)
                    {
                        reloadBar.Value += 1;
                        await Task.Delay(41);
                    }
                }
                reloadBar.Value = 0;
                RecarregaFila();
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
    }
}