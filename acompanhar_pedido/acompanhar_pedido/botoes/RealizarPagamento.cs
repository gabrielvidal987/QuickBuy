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
    public partial class RealizarPagamento : Form
    {
        Bitmap apaga_ico = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "delete.png"));
        Bitmap print_ico = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "print_ico.png"));
        string dados_nf;
        public RealizarPagamento()
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
                pnlGeral.Controls.Clear();
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaCadPed(true));
                if (filaPedidos.Count > 0)
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
                        Label pagamento = new Label();
                        CheckBox btnPagamentoRealizado = new CheckBox();
                        PictureBox btnPrint = new PictureBox();
                        PictureBox remProd = new PictureBox();
                        //cria o flowpanel com o cliente
                        pnl.Width = 240;
                        pnl.Height = 340 + Convert.ToInt32(altura);
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
                        num_pedido_nome.Font = new Font("Arial", 10, FontStyle.Bold);
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
                        valorTotal_formPagamento.Font = new Font("Arial", 12, FontStyle.Bold);
                        valorTotal_formPagamento.ForeColor = ColorTranslator.FromHtml("#006400");
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
                        //label pagamento_status
                        string pagamento_status = "PAGAMENTO PENDENTE";
                        string cor_fonte = "#ff2626";
                        if (bool.Parse(i["pagamento_aprovado"]) == true) { retirada = "PAGAMENTO APROVADO"; cor_fonte = "#006400"; }
                        pagamento.AutoSize = false;
                        pagamento.Text = pagamento_status;
                        pagamento.BorderStyle = BorderStyle.FixedSingle;
                        pagamento.TextAlign = ContentAlignment.MiddleCenter;
                        pagamento.Width = 225;
                        pagamento.Height = 40;
                        pagamento.Font = new Font("Arial", 10, FontStyle.Bold);
                        pagamento.ForeColor = ColorTranslator.FromHtml(cor_fonte);
                        //cria botão de realizar pagamento  
                        btnPagamentoRealizado.Margin = new Padding(10, 5, 0, 5);
                        btnPagamentoRealizado.Text = "PAGAMENTO REALIZADO";
                        btnPagamentoRealizado.TextAlign = ContentAlignment.MiddleCenter;
                        btnPagamentoRealizado.Font = new Font("Arial", 10, FontStyle.Bold);
                        btnPagamentoRealizado.Click += new System.EventHandler(aprovarPagamento_Click);
                        btnPagamentoRealizado.AutoSize = false;
                        btnPagamentoRealizado.Height = 30;
                        btnPagamentoRealizado.Width = 212;
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
                        pnl.Controls.Add(pagamento);
                        pnl.Controls.Add(formRetirada);
                        pnl.Controls.Add(btnPagamentoRealizado);
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
        private void RemPedido_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Certeza que deseja DELETAR o pedido?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Control control = (Control)sender;
                    for (int i = 0; i < pnlGeral.Controls.Count; i++)
                    {
                        if (pnlGeral.Controls[i].Controls[8].Name == control.Name)
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
        }
        private void aprovarPagamento_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Pagamento realizado com sucesso?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Control control = (Control)sender;
                    for (int i = 0; i < pnlGeral.Controls.Count; i++)
                    {
                        if (pnlGeral.Controls[i].Controls[8].Name == control.Name)
                        {
                            string numero_pedido = pnlGeral.Controls[i].Controls[0].Text.ToString().Split('-')[0].Trim();
                            ConectarSqlClasse sql = new ConectarSqlClasse();
                            sql.AprovaPagamento(numero_pedido);
                            RecarregaFila();
                            break;
                        }
                    }
                }
                catch (Exception er)
                { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
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
                        if (pnlGeral.Controls[i].Controls[8].Name == control.Name)
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
        }
        private void impressora_PrintPage(object sender, PrintPageEventArgs e)
        {

        }
        private void reset_tela_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pnlGeral.AutoScrollPosition.Y != 0)
                {
                    return;
                }
                ConectarSqlClasse sql = new ConectarSqlClasse();
                RecarregaFila();
            }
            catch (Exception er)
            {
                reset_tela.Stop();
                bool sucess_log = ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                if (sucess_log)
                {
                    reset_tela.Start();
                }
            }
        }
    }
}
