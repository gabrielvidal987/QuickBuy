﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Google.Protobuf.WellKnownTypes;
using System.Globalization;
using System.Drawing.Printing;
using acompanhar_pedido.botoes;
using System.Linq.Expressions;
using System.Threading;

namespace acompanhar_pedido.teste
{
    public partial class FazerPedido : Form
    {
        List<Dictionary<string, string>> extratoLista = new List<Dictionary<string, string>>();
        string nf;
        Thread t1;

        public FazerPedido()
        {
            InitializeComponent();
            Estetica();
            CarregaBotoes();
        }
        //função para estética
        public void Estetica()
        {
            pcholdCliente.AutoSize = false;
            pcholdCliente.Size = new System.Drawing.Size(221, 27);
            pcholdBuscaProd.AutoSize = false;
            pcholdBuscaProd.Size = new System.Drawing.Size(221, 27);
            pnlTotal.BackColor = Color.FromArgb(141, 172, 222);
            tbExtrato.BackColor = Color.FromArgb(198, 213, 239);
            pnlFimExtrato.BackColor = Color.FromArgb(198, 213, 239);
            boxPgto.BackColor = Color.FromArgb(198, 213, 239);
            pcholdCliente.PlaceHolderText = "Nome do cliente...";
            pcholdBuscaProd.PlaceHolderText = "Endereço...";
            btnCad.BackColor = Color.FromArgb(132, 246, 58);
            btnHist.BackColor = Color.FromArgb(192, 213, 239);
            pcholdObs.PlaceHolderText = "Adicionar observação...";
            pcholdObs.BackColor = Color.FromArgb(198, 213, 239);
            pnlGeral.BackColor = Color.FromArgb(192, 213, 239);
        }
        public void AdicionarProdutoExtrato(string nome, string valor)
        {
            try
            {
                //verifica se existe o nome
                if (nome != "" || valor != "")
                {
                    //limpa o extrato
                    //Verifica se o produto ja existe e então só incrementa na quantidade
                    bool add = false;
                    foreach (Dictionary<string, string> res in extratoLista)
                    {
                        if (res["nome"] == nome)
                        {
                            int quant = int.Parse(res["quantidade"]);
                            quant++;
                            res["quantidade"] = quant.ToString();
                            res["total"] = Convert.ToString(double.Parse(res["valor"]) * int.Parse(res["quantidade"]));
                            add = true;
                        }
                    }
                    if (add == false)
                    {
                        Dictionary<string, string> dicionarioProduto = new Dictionary<string, string>
                        {
                        { "nome", nome },
                        { "valor", valor },
                        { "quantidade", "1" },
                        { "total", valor }
                        };
                        extratoLista.Add(dicionarioProduto);
                    }
                    CriarLabel();
                    ValorTotal();
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        private void nomeProduto_Click(object sender, EventArgs e)
        {
            try
            {
                //pega o texto da label
                string nomeLongo = sender.ToString();
                string textoLabel = nomeLongo.Replace("System.Windows.Forms.Label, Text: ", "");
                foreach (Dictionary<string, string> res in extratoLista)
                {
                    if (res["nome"] == textoLabel)
                    {
                        int total = int.Parse(res["quantidade"]);
                        if (total > 1)
                        {
                            total--;
                            res["quantidade"] = total.ToString();
                            res["total"] = Convert.ToString(double.Parse(res["valor"]) * int.Parse(res["quantidade"]));
                            CriarLabel();
                            ValorTotal();
                            break;
                        }
                        else
                        {
                            extratoLista.Remove(res);
                            nQuantProd.Text = "0";
                            CriarLabel();
                            ValorTotal();
                            break;
                        }
                    }
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            } 
        }
        public void CriarLabel()
        {
            try
            {
                tbExtrato.Controls.Clear();
                //labels  com descricoes dos itens
                if (extratoLista.Count != 0)
                {
                    Label dproduto = new Label();
                    Label dvalorProduto = new Label();
                    Label dqtd = new Label();
                    dproduto.Anchor = AnchorStyles.None;
                    dproduto.Font = new Font(DefaultFont.FontFamily, 10);
                    dproduto.AutoSize = false;
                    dproduto.Width = 188;
                    dproduto.Height = 23;
                    dproduto.Text = "PRODUTO";
                    dproduto.TextAlign = ContentAlignment.MiddleLeft;
                    dvalorProduto.Anchor = AnchorStyles.None;
                    dvalorProduto.Font = new Font(DefaultFont.FontFamily, 10);
                    dvalorProduto.AutoSize = false;
                    dvalorProduto.Width = 85;
                    dvalorProduto.Height = 23;
                    dvalorProduto.Text = "VALOR";
                    dvalorProduto.TextAlign = ContentAlignment.MiddleCenter;
                    dqtd.Anchor = AnchorStyles.None;
                    dqtd.Font = new Font(DefaultFont.FontFamily, 10);
                    dqtd.AutoSize = false;
                    dqtd.Width = 46;
                    dqtd.Height = 23;
                    dqtd.Text = "QTD";
                    dqtd.TextAlign = ContentAlignment.MiddleCenter;
                    tbExtrato.Controls.Add(dproduto);
                    tbExtrato.Controls.Add(dvalorProduto);
                    tbExtrato.Controls.Add(dqtd);
                }
                int itens = 0;
                //Cria uma label pra cada produto
                foreach (Dictionary<string, string> res in extratoLista)
                {
                    Label produto = new Label();
                    Label valorProduto = new Label();
                    Label qtd = new Label();
                    produto.Anchor = AnchorStyles.None;
                    produto.BorderStyle = BorderStyle.FixedSingle;
                    produto.Font = new Font(DefaultFont.FontFamily, 12);
                    produto.AutoSize = false;
                    produto.Width = 188;
                    produto.Height = 23;
                    produto.Text = res["nome"];
                    produto.Cursor = Cursors.Hand;
                    produto.Name = res["nome"];
                    valorProduto.Anchor = AnchorStyles.None;
                    valorProduto.BorderStyle = BorderStyle.FixedSingle;
                    valorProduto.Font = new Font(DefaultFont.FontFamily, 12);
                    valorProduto.AutoSize = false;
                    valorProduto.Width = 85;
                    valorProduto.Height = 23;
                    valorProduto.Text = $"R${res["valor"]}";
                    qtd.Anchor = AnchorStyles.None;
                    qtd.BorderStyle = BorderStyle.FixedSingle;
                    qtd.Font = new Font(DefaultFont.FontFamily, 12);
                    qtd.AutoSize = false;
                    qtd.Width = 46;
                    qtd.Height = 23;
                    qtd.Text = res["quantidade"];
                    produto.Click += new System.EventHandler(this.nomeProduto_Click);
                    tbExtrato.Controls.Add(produto);
                    tbExtrato.Controls.Add(valorProduto);
                    tbExtrato.Controls.Add(qtd);
                    itens += int.Parse(res["quantidade"]);
                    nQuantProd.Text = itens.ToString();
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        public void ValorTotal()
        {
            try
            {
                double valorTotal = 0;
                foreach (Dictionary<string, string> res in extratoLista)
                {
                    valorTotal += double.Parse(res["total"]);
                }
                totalValorExtrato.Text = valorTotal.ToString("R$0.00");
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pcholdCliente.Text != "")
            {
                clienteExtrato.Text = $"Cliente: {pcholdCliente.Text}";
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                var reset = MessageBox.Show("Certeza de que deseja limpar o extrato?", "ATENÇÃO", MessageBoxButtons.YesNo);
                if (reset == DialogResult.Yes)
                {
                    tbExtrato.Controls.Clear();
                    extratoLista.Clear();
                    nQuantProd.Text = "0";
                    totalValorExtrato.Text = "R$0,00";
                    pcholdCliente.Text = "";
                    pcholdObs.Text = "";
                    clienteExtrato.Text = "Cliente: ";
                    pcholdBuscaProd.Text = "";
                    boxPgto.SelectedIndex = boxPgto.FindStringExact("Dinheiro");
                    pcholdObs.Focus();
                    pcholdCliente.Focus();
                    CarregaBotoes();
                }
            }
            catch (Exception er) 
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }

        }
        private void btnCad_Click(object sender, EventArgs e)
        {
            string nome_cliente = clienteExtrato.Text.Replace("Cliente: ", "");
            string nome_produto = "sem produto";
            string produto_quantidade = "";
            string obs = pcholdObs.Text;
            string endereco = pcholdBuscaProd.Text;
            if (pcholdBuscaProd.Text == "") { endereco = "endereço não cadastrado"; }
            nf = $"Cliente: {nome_cliente}";
            try 
            { 
                if (obs == "")
                {
                    obs = "sem observações";
                }
                var horario = DateTime.Now;
                string valorTotal = totalValorExtrato.Text.Replace("R$", "").Replace(',', '.');
                foreach (var item in extratoLista)
                {
                    nome_produto = item["nome"];
                    string quantidade = $"{item["quantidade"]}X";
                    produto_quantidade += $"{quantidade} {nome_produto},";
                    nf += $"\nItem: {item["nome"]} qtd: {item["quantidade"]}";
                }
                if (nome_cliente != "" && nome_produto != "sem produto")
                {
                    string hora_pedido = horario.ToString("HH:mm");
                    string formaPag = boxPgto.Text.ToLower().Replace('é', 'e');
                    nf += $"\n\nobs: {obs} \n\nHorario do pedido: {hora_pedido}\n\nForma de pagamento: {formaPag}\n\nVALOR TOTAL: R${totalValorExtrato.Text.Replace("R$", "")}\n\n ENDEREÇO: \n {endereco}\n\n";
                    ConectarSqlClasse sql = new ConectarSqlClasse();
                    nf += "\n\n" + sql.CadPedido(nome_cliente,endereco, produto_quantidade, obs, hora_pedido, valorTotal, formaPag);
                    if (imprimir.Checked == true)
                    {
                        try
                        {
                            impressora.Print();
                        }
                        catch
                        {
                            MessageBox.Show("Erro ao imprimir senha");
                        }
                    }
                    MessageBox.Show(nf);
                    boxPgto.SelectedIndex = boxPgto.FindStringExact("Dinheiro");
                    tbExtrato.Controls.Clear();
                    extratoLista.Clear();
                    nQuantProd.Text = "0";
                    pcholdObs.Text = "";
                    totalValorExtrato.Text = "R$0,00";
                    clienteExtrato.Text = "Cliente: ";
                    pcholdCliente.Text = "";
                    pcholdBuscaProd.Text = "";
                    pcholdCliente.Focus();
                }
                else
                {
                    MessageBox.Show("Preencha o nome do cliente e adicione os produtos");
                    pcholdCliente.Focus();
                }
                CarregaBotoes();
            }
            catch (Exception er) 
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }


        }
        public void CarregaBotoes()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> listaProd = new List<Dictionary<string, string>>(sql.DadosProd());
                pnlGeral.Controls.Clear();
                foreach (Dictionary<string, string> item in listaProd)
                {
                    int quantLetras = item["nome"].ToList().Count;
                    if (quantLetras < 15 ) { quantLetras = 15; }
                    double altura = (quantLetras / 15) * 20;
                    FlowLayoutPanel btn = new FlowLayoutPanel();
                    Label nomeProd = new Label();
                    Label valorProd = new Label();
                    PictureBox fotoProd = new PictureBox();
                    btn.Size = new Size(153, 125 + Convert.ToInt32(altura));
                    btn.BorderStyle = BorderStyle.Fixed3D;
                    btn.Padding = new Padding(8, 5, 0, 0);
                    btn.Margin = new Padding(10, 10, 10, 10);
                    btn.BackColor = Color.FromArgb(240, 240, 240);
                    btn.Cursor = Cursors.Hand;
                    btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
                    btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
                    btn.Click += new System.EventHandler(this.produtobtn_Click);
                    nomeProd.BackColor = Color.FromArgb(255, 255, 255);
                    nomeProd.Font = new Font("Arial", 10);
                    nomeProd.ForeColor = Color.Black;
                    nomeProd.AutoSize = false;
                    nomeProd.Size = new Size(129, Convert.ToInt32(altura));
                    nomeProd.BorderStyle = BorderStyle.FixedSingle;
                    nomeProd.TextAlign = ContentAlignment.MiddleCenter;
                    nomeProd.Text = item["nome"];
                    nomeProd.Enabled = false;
                    valorProd.BackColor = Color.FromArgb(255, 255, 255);
                    valorProd.Font = new Font("Arial", 10);
                    valorProd.ForeColor = Color.Black;
                    valorProd.AutoSize = false;
                    valorProd.Size = new Size(129, 30);
                    valorProd.BorderStyle = BorderStyle.FixedSingle;
                    valorProd.TextAlign = ContentAlignment.MiddleCenter;
                    valorProd.Text = $"R${item["valor"].Replace('.',',')}";
                    valorProd.Enabled = false;
                    fotoProd.BackColor = Color.FromArgb(255, 255, 255);
                    fotoProd.Size = new Size(129, 78);
                    fotoProd.BorderStyle = BorderStyle.FixedSingle;
                    fotoProd.SizeMode = PictureBoxSizeMode.StretchImage;
                    fotoProd.Enabled = false;
                    try
                    {
                        Image myimage = new Bitmap(item["caminho_foto"]);
                        fotoProd.BackgroundImage = myimage;
                    }
                    catch { }
                    fotoProd.BackgroundImageLayout = ImageLayout.Stretch;
                    pnlGeral.Controls.Add(btn);
                    btn.Controls.Add(nomeProd);
                    btn.Controls.Add(valorProd);
                    btn.Controls.Add(fotoProd);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Erro ao carregar janela de pedidos. \n Erro: " + er);
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);

            }
        }
        private void produtobtn_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                control.BackColor = Color.FromArgb(240, 240, 240);
                string res = control.Controls[1].Text.Replace("R$", "");
                AdicionarProdutoExtrato(control.Controls[0].Text, res);
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }

        }
        private void mouseDown(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackColor = Color.FromArgb(249, 249, 249);
            control.Controls[0].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[1].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[2].BackColor = Color.FromArgb(234, 234, 234);
        }
        private void mouseUp(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackColor = Color.FromArgb(240, 240, 240);
            control.Controls[0].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[1].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[2].BackColor = Color.FromArgb(255, 255, 255);
        }
        private void pcholdCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCad_Click(sender, e);
            }
        }
        private void impressora_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                Font font = new Font("Arial", 16, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush cor = new SolidBrush(Color.Black);
                Point local = new Point(200, 200);
                e.Graphics.DrawString(nf, font, cor, local);
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        private void FazerPedido_Load_1(object sender, EventArgs e)
        {
            boxPgto.SelectedIndex = boxPgto.FindStringExact("Dinheiro");
            pcholdCliente.Focus();
        }
        private void boxPgto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCad_Click(sender, e);
            }
        }
        private void btnHist_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(hist_pedidos);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        private void hist_pedidos(object obj)
        {
            try { Application.Run(new historico_pedidos()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
    }
}