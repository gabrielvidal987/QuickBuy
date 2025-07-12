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
using System.IO;

namespace acompanhar_pedido.botoes
{
    public partial class Cozinha : Form
    {
        Thread t1;
        Bitmap print_ico = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"print_ico.png"));
        string dados_nf;
        int ind_btn = 0;
        List<string> pedidoJaNaLista = new List<string>();
        //criador da classe. inicializa o component
        public Cozinha()
        {
            InitializeComponent();
        }
        //inicia quando a cozinha carrega, carrega um pouco da estética, chama a recarga da fila e em seguida dá play no timer
        private void Cozinha_Load(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.FromArgb(198, 213, 239);
                pnlGeral.BackColor = Color.FromArgb(141, 172, 222);
                pnlAnt.BackColor = Color.FromArgb(141, 172, 222);
                pnlGeral.Width = this.Width - 150;
                pnlAnt.Width = this.Width - 150;
                //lbprontos.Location = new Point(pnlGeral.Location.X + (pnlGeral.Width / 2) - 83, pnlGeral.Location.Y + pnlGeral.Height);
                lbprontos.Location = new Point(this.Width / 2 - 83, pnlGeral.Location.Y + pnlGeral.Height);
                setaSaida.Location = new Point(Left = this.Width - 110, Top = this.Height - 270);
                RecarregaFila();
                reload.Start();
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //limpa toda a fila de pedidos pendentes e refaz a partir dos pedidos que existem, os que ja estão na lista
        //estão na variavel pedidoJaNaLista e estes não são alterados
        public void RecarregaFila()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaPedidos());
                foreach (Dictionary<string, string> i in filaPedidos)
                {
                    if (pedidoJaNaLista.Contains(i["numero_pedido"]))
                    {
                        continue;
                    }
                    int quantLetras = i["produtos_nome"].Split(',').ToList().Count;
                    double altura = quantLetras * 30;
                    FlowLayoutPanel pnl = new FlowLayoutPanel();
                    Label num_pedido_nome = new Label();
                    FlowLayoutPanel nome_produto = new FlowLayoutPanel();
                    Label obs = new Label();
                    Label hora = new Label();
                    Label formRetirada = new Label();
                    PictureBox btnPrint = new PictureBox();
                    //cria o flowpanel com o cliente
                    pnl.Width = 240;
                    pnl.Height = 190;
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
                    //label com a retirada
                    string retirada = "BALCÃO";
                    if (bool.Parse(i["delivery"]) == true) { retirada = "ENTREGA"; }
                    formRetirada.AutoSize = false;
                    formRetirada.Text = retirada;
                    formRetirada.BorderStyle = BorderStyle.FixedSingle;
                    formRetirada.TextAlign = ContentAlignment.MiddleCenter;
                    formRetirada.Width = 225;
                    formRetirada.Height = 40;
                    formRetirada.Font = new Font("Arial", 10);
                    //cria botão de imprimir o pedido
                    btnPrint.BackColor = Color.Transparent;
                    btnPrint.Name = ind_btn.ToString();
                    btnPrint.Size = new Size(20, 20);
                    btnPrint.SizeMode = PictureBoxSizeMode.StretchImage;
                    btnPrint.Cursor = Cursors.Hand;
                    btnPrint.Click += new EventHandler(btnPrint_Click);
                    btnPrint.Margin = new Padding(205, 5, 0, 0);
                    btnPrint.Image = print_ico;
                    //cria as labels no panel
                    pnl.Controls.Add(num_pedido_nome);
                    pnl.Controls.Add(nome_produto);
                    pnl.Controls.Add(obs);
                    pnl.Controls.Add(hora);
                    pnl.Controls.Add(formRetirada);
                    pnl.Controls.Add(btnPrint);
                    pedidoJaNaLista.Add(i["numero_pedido"]);
                    ind_btn++;
                }
                CarregaAnt();

            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //limpa a fila de pedidos ja prontos e refaz sendo do mais recente para o ultimo
        public void CarregaAnt()
        {
            int ind_btn_ant = 0;
            string asc = "asc";
            try
            {
                while(pnlAnt.Controls.Count != 1)
                {
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
                        Label hora_pronta = new Label();
                        Label formRetirada = new Label();
                        PictureBox btnPrint = new PictureBox();
                        //cria o flowpanel com o cliente
                        pnl.Width = 210;
                        pnl.Height = 220;
                        pnl.BackColor = Color.FromArgb(247, 247, 247);
                        pnl.Margin = new Padding(50, 10, 0, 10);
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
                        //label hora que ficou pronta
                        hora_pronta.AutoSize = false;
                        hora_pronta.Text = $"Hora de entrega: {i["hora_ficou_pronto"]}";
                        hora_pronta.BorderStyle = BorderStyle.FixedSingle;
                        hora_pronta.TextAlign = ContentAlignment.MiddleCenter;
                        hora_pronta.Width = 195;
                        hora_pronta.Height = 40;
                        hora_pronta.Font = new Font("Arial", 10);
                        //label com a retirada
                        string retirada = "BALCÃO";
                        if (bool.Parse(i["delivery"]) == true) { retirada = "ENTREGA"; }
                        formRetirada.AutoSize = false;
                        formRetirada.Text = retirada;
                        formRetirada.BorderStyle = BorderStyle.FixedSingle;
                        formRetirada.TextAlign = ContentAlignment.MiddleCenter;
                        formRetirada.Width = 195;
                        formRetirada.Height = 40;
                        formRetirada.Font = new Font("Arial", 10);
                        //cria botão de imprimir o pedido
                        btnPrint.BackColor = Color.Transparent;
                        btnPrint.Name = ind_btn_ant.ToString();
                        btnPrint.Size = new Size(20, 20);
                        btnPrint.SizeMode = PictureBoxSizeMode.StretchImage;
                        btnPrint.Cursor = Cursors.Hand;
                        btnPrint.Click += new EventHandler(btnPrint_Click);
                        btnPrint.Margin = new Padding(175, 5, 0, 0);
                        btnPrint.Image = print_ico;
                        //cria as labels no panel
                        pnl.Controls.Add(num_pedido_nome);
                        pnl.Controls.Add(nome_produto);
                        pnl.Controls.Add(obs);
                        //pnl.Controls.Add(hora);
                        pnl.Controls.Add(hora_pronta);
                        pnl.Controls.Add(formRetirada);
                        pnl.Controls.Add(btnPrint);
                        ind_btn_ant++;
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
        //função que marca o click na checkbox do produto, caso tenha preenchido todas as checkbox é
        //questionado se o pedido está pronto, se estiver ele é passado para os pedidos prontos e chamado o recarregaFila
        private void Marca_Click(object sender, EventArgs e)
        {
            try
            {
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
                            ConectarSqlClasse sql = new ConectarSqlClasse();
                            foreach (Control h in pnlGeral.Controls[j].Controls[1].Controls)
                            {
                                string[] texto_prod = h.Text.Split('X');
                                int qtd_prod = int.Parse(texto_prod[0]);
                                string nome_prod = texto_prod[1].Trim();
                            }
                            pnlGeral.Controls.Remove(pnlGeral.Controls[j]);
                            sql.MarcaPedidoPronto(numero_pedido);
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
        //função do botão de histórico que chama o formulario com o histórico dos pedidos ja prontos
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
        //chama o formulario do histórico de pedido
        private void abrirHistorico()
        {
            try { Application.Run(new Historico_cozinha()); } catch (Exception er){ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);}
        }
        //relogio que controla a barra de recarga da tela da cozinha, caso chegue no 100% é chamado o metodo recarregaFila()
        private async void reload_Tick(object sender, EventArgs e)
        {
            try
            {
                reloadBar.Value = 0;
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
                reload.Stop();
                bool sucess_log = ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                if (sucess_log)
                {
                    reload.Start();
                }
            }
        }
        //função do botão para imprimir o pedido pendente
        private void btnPrint_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Certeza que deseja REIMPRIMIR o pedido?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Control control = (Control)sender;
                    for (int i = 0; i < pnlGeral.Controls.Count; i++)
                    {
                        if (pnlGeral.Controls[i].Controls[5].Name == control.Name)
                        {
                            string numero_pedido = pnlGeral.Controls[i].Controls[0].Text.ToString().Split('-')[0].Trim();
                            ConectarSqlClasse sql = new ConectarSqlClasse();
                            dados_nf = sql.imprimePedido(numero_pedido);
                            try
                            {
                                impressora.Print();
                                pnlGeral.Focus();
                            }
                            catch
                            {
                                MessageBox.Show("Erro ao imprimir pedido");
                            }
                            break;
                        }
                    }
                }
                catch (Exception er)
                { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
        //função para formar o texto da impressão
        private void impressora_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush cor = new SolidBrush(Color.Black);
                Point local = new Point(5, 10);
                e.Graphics.DrawString(dados_nf, font, cor, local);
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
    }
}