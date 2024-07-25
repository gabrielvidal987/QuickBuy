using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    public partial class historico_pedidos : Form
    {
        Bitmap apaga_ico = new Bitmap($@"{Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString())}\delete.png");
        Bitmap print_ico = new Bitmap($@"{Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString())}\print_ico.png");
        string dados_nf;
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
        private void RemPedido_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                for (int i = 0; i < pnlGeral.Controls.Count; i++)
                {
                    if (pnlGeral.Controls[i].Controls[6].Name == control.Name)
                    {
                        string numero_pedido = pnlGeral.Controls[i].Controls[0].Text.ToString().Split('-')[0].Trim();
                        ConectarSqlClasse sql = new ConectarSqlClasse();
                        sql.RemovePedido(numero_pedido);
                        RecarregaFila();
                        break;
                    }
                }
            }
            catch (Exception er)
            { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                for (int i = 0; i < pnlGeral.Controls.Count; i++)
                {
                    if (pnlGeral.Controls[i].Controls[6].Name == control.Name)
                    {
                        string numero_pedido = pnlGeral.Controls[i].Controls[0].Text.ToString().Split('-')[0].Trim();
                        ConectarSqlClasse sql = new ConectarSqlClasse();
                        dados_nf = sql.ImprimePedidoHistorico(numero_pedido);
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
        private void impressora_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
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
        public void RecarregaFila()
        {
            try
            {
                pnlGeral.Controls.Clear();
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaCadPed());
                if (filaPedidos.Count > 0 )
                {
                    int ind_btn = 0;
                    foreach (Dictionary<string, string> i in filaPedidos)
                    {
                        int quantLetras = i["produtos_nome"].Split(',').ToList().Count;
                        double altura = quantLetras * 30;
                        FlowLayoutPanel pnl = new FlowLayoutPanel();
                        Label num_pedido_nome = new Label();
                        Label endereco = new Label();
                        Label nome_produto = new Label();
                        Label obs = new Label();
                        Label hora = new Label();
                        Label valorTotal_formPagamento = new Label();
                        Label formRetirada = new Label();
                        PictureBox btnPrint = new PictureBox();
                        PictureBox remProd = new PictureBox();
                        //cria o flowpanel com o cliente
                        pnl.Width = 240;
                        pnl.Height = 250 + Convert.ToInt32(altura);
                        pnl.BackColor = Color.FromArgb(247, 247, 247);
                        pnl.Margin = new Padding(40, 10, 0, 10);
                        pnl.Padding = new Padding(5, 5, 5, 5);
                        pnl.BorderStyle = BorderStyle.FixedSingle;
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
                        btnPrint.Margin = new Padding(175, 5, 0, 0);
                        btnPrint.Image = print_ico;
                        //cria botão de remover pedido
                        remProd.BackColor = Color.Transparent;
                        remProd.Name = ind_btn.ToString();
                        remProd.Size = new Size(20, 20);
                        remProd.SizeMode = PictureBoxSizeMode.StretchImage;
                        remProd.Cursor = Cursors.Hand;
                        remProd.Click += new EventHandler(RemPedido_Click);
                        remProd.Margin = new Padding(10, 5, 0, 0);
                        remProd.Image = apaga_ico;
                        //cria as labels no panel
                        pnl.Controls.Add(num_pedido_nome);
                        pnl.Controls.Add(endereco);
                        pnl.Controls.Add(nome_produto);
                        pnl.Controls.Add(obs);
                        pnl.Controls.Add(hora);
                        pnl.Controls.Add(valorTotal_formPagamento);
                        pnl.Controls.Add(formRetirada);
                        pnl.Controls.Add(btnPrint);
                        pnl.Controls.Add(remProd);
                        pnlGeral.Controls.Add(pnl);
                        ind_btn++;
                    }
                }
                else { MessageBox.Show("Sem pedidos registrados ainda!!"); }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pnlGeral.AutoScrollPosition.Y != 0)
            {
                return;
            }
            RecarregaFila();
        }
    }
}