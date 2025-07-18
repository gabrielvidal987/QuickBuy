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
using MySqlX.XDevAPI.Common;
using System.IO;
using Aspose.Cells;

namespace acompanhar_pedido.teste
{
    public partial class FazerPedido : Form
    {
        List<Dictionary<string, string>> extratoLista = new List<Dictionary<string, string>>();
        string nf;
        Thread t1;
        string texto_filtra = "";
        //inicializador da classe, chama o inicializador de componente, função de estética e função para carregar botoes
        public FazerPedido()
        {
            InitializeComponent();
            Estetica();
            CarregaBotoes();
        }
        //função para realizar a atualização da estética da pagina
        public void Estetica()
        {
            pcholdCliente.AutoSize = false;
            pcholdCliente.Size = new System.Drawing.Size(221, 30);
            pcholdEndereco.AutoSize = false;
            pcholdEndereco.Size = new System.Drawing.Size(221, 30);
            pnlTotal.BackColor = Color.FromArgb(141, 172, 222);
            tbExtrato.BackColor = Color.FromArgb(198, 213, 239);
            pnlFimExtrato.BackColor = Color.FromArgb(198, 213, 239);
            boxPgto.BackColor = Color.FromArgb(198, 213, 239);
            pcholdCliente.PlaceHolderText = "Nome do cliente...";
            pcholdEndereco.PlaceHolderText = "Endereço...";
            btnCad.BackColor = Color.FromArgb(132, 246, 58);
            btnHist.BackColor = Color.FromArgb(192, 213, 239);
            pcholdObs.PlaceHolderText = "Adicionar observação...";
            pcholdObs.BackColor = Color.FromArgb(198, 213, 239);
            pnlGeral.BackColor = Color.FromArgb(192, 213, 239);
        }
        //função de remover uma unidade do produto ao clicar no nome dele, caso tenha só uma unidade o produto é removido
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
                            CriarLabelExtrato();
                            ValorTotal();
                            break;
                        }
                        else
                        {
                            extratoLista.Remove(res);
                            nQuantProd.Text = "0";
                            CriarLabelExtrato();
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
        //cria as labels do extrato, no primeiro bloco cria a label "nome, valor qtd" e no segundo cria as labels de cada produto
        public void CriarLabelExtrato()
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
                    dproduto.Font = new System.Drawing.Font(DefaultFont.FontFamily, 10);
                    dproduto.AutoSize = false;
                    dproduto.Width = 188;
                    dproduto.Height = 23;
                    dproduto.Text = "PRODUTO";
                    dproduto.TextAlign = ContentAlignment.MiddleLeft;
                    dvalorProduto.Anchor = AnchorStyles.None;
                    dvalorProduto.Font = new System.Drawing.Font(DefaultFont.FontFamily, 10);
                    dvalorProduto.AutoSize = false;
                    dvalorProduto.Width = 85;
                    dvalorProduto.Height = 23;
                    dvalorProduto.Text = "VALOR";
                    dvalorProduto.TextAlign = ContentAlignment.MiddleCenter;
                    dqtd.Anchor = AnchorStyles.None;
                    dqtd.Font = new System.Drawing.Font(DefaultFont.FontFamily, 10);
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
                    produto.Font = new System.Drawing.Font(DefaultFont.FontFamily, 12);
                    produto.AutoSize = false;
                    produto.Width = 188;
                    produto.Height = 23;
                    produto.Text = res["nome"];
                    produto.Cursor = Cursors.Hand;
                    produto.Name = res["nome"];
                    valorProduto.Anchor = AnchorStyles.None;
                    valorProduto.BorderStyle = BorderStyle.FixedSingle;
                    valorProduto.Font = new System.Drawing.Font(DefaultFont.FontFamily, 12);
                    valorProduto.AutoSize = false;
                    valorProduto.Width = 85;
                    valorProduto.Height = 23;
                    valorProduto.Text = $"R${res["valor"]}";
                    qtd.Anchor = AnchorStyles.None;
                    qtd.BorderStyle = BorderStyle.FixedSingle;
                    qtd.Font = new System.Drawing.Font(DefaultFont.FontFamily, 12);
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
        //atualiza o campo do valor total no extrato
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
        //Reset conteudo visual
        public void ResetTela()
        {
            tbExtrato.Controls.Clear();
            extratoLista.Clear();
            nQuantProd.Text = "0";
            totalValorExtrato.Text = "R$0,00";
            pcholdCliente.Text = "";
            pcholdObs.Text = "";
            clienteExtrato.Text = "Cliente: ";
            pcholdEndereco.Text = "";
            boxPgto.SelectedIndex = boxPgto.FindStringExact("Dinheiro");
            delivery.Checked = false;
            prod_entregue.Checked = false;
            pcholdBuscaProd.Text = "";
            pcholdObs.Focus();
            pcholdCliente.Focus();
            CarregaBotoes();
        }
        //função do botão de resetar a tela, ele limpa todos os campos e reseta o pedido
        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                var reset = MessageBox.Show("Certeza de que deseja limpar o extrato?", "ATENÇÃO", MessageBoxButtons.YesNo);
                if (reset == DialogResult.Yes)
                {
                    ResetTela();
                }
            }
            catch (Exception er) 
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }

        }
        //função do botão de cadastrar o pedido, ele cadastra o pedido se todos os campos estiverem preenchidos
        private void btnCad_Click(object sender, EventArgs e)
        {
            string nome_cliente = clienteExtrato.Text.Replace("Cliente: ", "");
            string produto_quantidade = "";
            Dictionary<string, string> produtos_consumo = new Dictionary<string, string>();
            string obs_bruto = pcholdObs.Text;
            string obs = AddLineBreaksEveryNChars(obs_bruto, 30);
            string endereco_bruto = pcholdEndereco.Text;
            string endereco = AddLineBreaksEveryNChars(endereco_bruto, 30);
            string retirada = "BALCÃO";
            bool pagamento_ja_efetuado = false;
            bool entrega = false;
            bool produto_entregue = false;
            if (delivery.Checked) { entrega = true; retirada = "ENTREGA"; }
            if (prod_entregue.Checked) { produto_entregue = true; }
            if (pcholdEndereco.Text.Length < 5) { endereco = "endereço não cadastrado"; }
            if (pagamento_efetuado.Checked) { pagamento_ja_efetuado = true; }
            try
            {
                if (nome_cliente != "" && extratoLista.Count > 0)
                {
                    nf = $"CLIENTE: {nome_cliente}";
                    nf += $"\nENDEREÇO:\n{endereco}\n";
                    foreach (var item in extratoLista)
                    {
                        string nome_produto = item["nome"];
                        string quantidade = $"{item["quantidade"]}X";
                        produto_quantidade += $"{quantidade} {nome_produto},";
                        produtos_consumo.Add(item["nome"], item["quantidade"]);
                        string texto_item = $"\nITEM: {item["nome"]} QTD: {item["quantidade"]}";
                        string texto_item_formatado = AddLineBreaksEveryNChars(texto_item, 30);
                        nf += texto_item_formatado;
                    }
                    if (obs == "")
                    {
                        obs = "sem observações";
                    }
                    var horario = DateTime.Now;
                    string valorTotal = totalValorExtrato.Text.Replace("R$", "").Replace(',', '.');
                    ConectarSqlClasse sql = new ConectarSqlClasse();
                    string hora_pedido = horario.ToString("HH:mm:ss");
                    string formaPag = boxPgto.Text.Replace('é', 'e');
                    nf += $"\nOBS: {obs}";
                    nf += $"\n\n ----- {formaPag} ----- ";
                    if (Convert.ToBoolean(pagamento_ja_efetuado)) { nf += "\nPEDIDO PAGO"; } else { nf += "\nPAGAMENTO PENDENTE"; }
                    nf += $"\n---------------------------------\nVALOR TOTAL: {totalValorExtrato.Text}\n---------------------------------";
                    nf += $"\n\nSAÍDA: {retirada}";
                    nf += "\n\n" + sql.CadPedido(nome_cliente,endereco, produto_quantidade, obs, valorTotal, formaPag,entrega, pagamento_ja_efetuado, produto_entregue, produtos_consumo);
                    nf += $"\nHorario do pedido\n{hora_pedido}";

                    if (imprimir.Checked == true)
                    {
                        try
                        {
                            impressora.Print();
                            pcholdCliente.Focus();
                        }
                        catch
                        {
                            MessageBox.Show("Erro ao imprimir senha");
                        }
                    }
                    MessageBox.Show($"{nf}","PEDIDO CADASTRADO COM SUCESSO!!");
                    ResetTela();
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
        //carrega os botões dos produtos, sendo um para cada produto
        public void CarregaBotoes()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                pnlGeral.SuspendLayout();
                while (pnlGeral.Controls.Count > 1) { pnlGeral.Controls.Remove(pnlGeral.Controls[1]); }
                string filtro = "";
                if (pcholdBuscaProd.Text != "") { filtro = pcholdBuscaProd.Text; }
                List<Dictionary<string, string>> listaProd = new List<Dictionary<string, string>>(sql.DadosProd(filtro));
                foreach (Dictionary<string, string> item in listaProd)
                {
                    int quantLetras = item["nome"].ToList().Count;
                    if (quantLetras < 15 ) { quantLetras = 15; }
                    double altura = quantLetras / 10 * 20;
                    FlowLayoutPanel btn = new FlowLayoutPanel();
                    Label nomeProd = new Label();
                    Label valorProd = new Label();
                    Label qtdProd = new Label();
                    PictureBox fotoProd = new PictureBox();
                    btn.Size = new Size(210, 182 + Convert.ToInt32(altura));
                    btn.BorderStyle = BorderStyle.Fixed3D;
                    btn.Padding = new Padding(8, 5, 0, 0);
                    btn.Margin = new Padding(10, 10, 10, 10);
                    btn.BackColor = Color.FromArgb(240, 240, 240);
                    btn.Cursor = Cursors.Hand;
                    btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
                    btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
                    btn.Click += new System.EventHandler(this.produtobtn_Click);
                    nomeProd.BackColor = Color.FromArgb(255, 255, 255);
                    nomeProd.Font = new System.Drawing.Font("Arial", 10);
                    nomeProd.ForeColor = Color.Black;
                    nomeProd.AutoSize = false;
                    nomeProd.Size = new Size(186, Convert.ToInt32(altura));
                    nomeProd.BorderStyle = BorderStyle.FixedSingle;
                    nomeProd.TextAlign = ContentAlignment.MiddleCenter;
                    nomeProd.Text = item["nome"];
                    nomeProd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
                    nomeProd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
                    nomeProd.Click += new System.EventHandler(this.produto_lb_pb_Click);
                    valorProd.BackColor = Color.FromArgb(255, 255, 255);
                    valorProd.Font = new System.Drawing.Font("Arial", 10);
                    valorProd.ForeColor = Color.Black;
                    valorProd.AutoSize = false;
                    valorProd.Size = new Size(186, 30);
                    valorProd.BorderStyle = BorderStyle.FixedSingle;
                    valorProd.TextAlign = ContentAlignment.MiddleCenter;
                    valorProd.Text = $"R${item["valor"].Replace('.',',')}";
                    valorProd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
                    valorProd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
                    valorProd.Click += new System.EventHandler(this.produto_lb_pb_Click);
                    qtdProd.BackColor = Color.FromArgb(255, 255, 255);
                    qtdProd.Font = new System.Drawing.Font("Arial", 10);
                    qtdProd.ForeColor = Color.Black;
                    qtdProd.AutoSize = false;
                    qtdProd.Size = new Size(186, 30);
                    qtdProd.BorderStyle = BorderStyle.FixedSingle;
                    qtdProd.TextAlign = ContentAlignment.MiddleCenter;
                    qtdProd.Text = $"QTD Dips. {item["disponivel"]}";
                    qtdProd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
                    qtdProd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
                    qtdProd.Click += new System.EventHandler(this.produto_lb_pb_Click);
                    fotoProd.BackColor = Color.FromArgb(255, 255, 255);
                    fotoProd.Size = new Size(186, 105);
                    fotoProd.BorderStyle = BorderStyle.FixedSingle;
                    fotoProd.SizeMode = PictureBoxSizeMode.CenterImage;
                    try
                    {
                        if (item["caminho_foto"] != "semFoto.png")
                        {
                            Image myimage = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos", item["caminho_foto"]));
                            myimage = new Bitmap(myimage, fotoProd.Width - 40, fotoProd.Height - 40);
                            fotoProd.Image = myimage;
                        }
                        else
                        {
                            Bitmap food_ico = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos", $"{item["categoria"]}.png"));
                            food_ico = new Bitmap(food_ico, fotoProd.Width - 40, fotoProd.Height - 40);
                            fotoProd.Image = food_ico;
                        }
                    }
                    catch 
                    {
                        try
                        {
                            Bitmap food_ico = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos", $"{item["categoria"]}.png"));
                            food_ico = new Bitmap(food_ico, fotoProd.Width - 40, fotoProd.Height - 40);
                            fotoProd.Image = food_ico;
                        }
                        catch { }
                    }
                    fotoProd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
                    fotoProd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
                    fotoProd.Click += new System.EventHandler(this.produto_lb_pb_Click);
                    pnlGeral.Controls.Add(btn);
                    btn.Controls.Add(nomeProd);
                    btn.Controls.Add(valorProd);
                    btn.Controls.Add(qtdProd);
                    btn.Controls.Add(fotoProd);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Erro ao carregar produtos. \n Erro: " + er);
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
            finally
            {
                pnlGeral.ResumeLayout();
            }
        }
        //função de quando clica em um produto (sem ser no campo do nome, valor ou foto) , adiciona ele ao extrato
        private void produtobtn_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                control.BackColor = Color.FromArgb(240, 240, 240);
                string valor = control.Controls[1].Text.Replace("R$", "");
                string nome = control.Controls[0].Text;
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
                        CriarLabelExtrato();
                        ValorTotal();
                    }
                }
                catch (Exception er)
                {
                    ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //função para chamar o produtobtn_click caso seja clicado na foto, nome ou valor do produto
        private void produto_lb_pb_Click(object sender, EventArgs e)
        {
            try
            {
                System.Type tipo = sender.GetType();
                if (tipo.Name == "PictureBox")
                {
                    PictureBox picturebox = sender as PictureBox;
                    var parentButton = picturebox.Parent;
                    produtobtn_Click(parentButton, e);
                }
                else
                {
                    Label label = sender as Label;
                    var parentButton = label.Parent;
                    produtobtn_Click(parentButton, e);
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //função chamada quando clica no produto, ele escurece as cores do produto
        private void mouseDown(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            System.Type tipo = sender.GetType();
            if (tipo.Name != "FlowLayoutPanel") { control = control.Parent; }
            control.BackColor = Color.FromArgb(249, 249, 249);
            control.Controls[0].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[1].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[2].BackColor = Color.FromArgb(234, 234, 234);
        }
        //função chamada quando solta o clique no produto, ele clareia as cores do produto
        private void mouseUp(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            System.Type tipo = sender.GetType();
            if (tipo.Name != "FlowLayoutPanel") { control = control.Parent; }
            control.BackColor = Color.FromArgb(240, 240, 240);
            control.Controls[0].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[1].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[2].BackColor = Color.FromArgb(255, 255, 255);
        }
        //chama o btnCad ao apertar enter no campo de cliente ou de endereco
        private void pcholdCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCad_Click(sender, e);
            }
        }
        //função que corta a frase a partir do intervalo desejado para limitar na visualização
        static string AddLineBreaksEveryNChars(string input, int interval)
        {
            if (string.IsNullOrEmpty(input) || interval <= 0)
            {
                return input;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(input[i]);
                if ((i + 1) % interval == 0)
                {
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }
        //imprime a nf do pedido
        private void impressora_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                System.Drawing.Font font = new System.Drawing.Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush cor = new SolidBrush(Color.Black);
                Point local = new Point(5, 10);
                e.Graphics.DrawString(nf, font, cor, local);
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //ao carregar a pagina seleciona o dinheiro como forma de pagamento e dá um focus no nome do cliente
        private void FazerPedido_Load_1(object sender, EventArgs e)
        {
            boxPgto.SelectedIndex = boxPgto.FindStringExact("Dinheiro");
            pcholdCliente.Focus();
        }
        //ao apertar enter na forma de pagamento selecionado ela chamao  btnCad_click
        private void boxPgto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCad_Click(sender, e);
            }
        }
        //clique no botão para ver histórico de pedidos, verifica antes se existe pedido, caso não exista chama o messagebox
        private void btnHist_Click(object sender, EventArgs e)
        {
            ConectarSqlClasse sql = new ConectarSqlClasse();
            List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaCadPed(false));
            if (filaPedidos.Count > 0)
            {
                try
                {
                    t1 = new Thread(hist_pedidos);
                    t1.SetApartmentState(ApartmentState.STA);
                    t1.Start();
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            else { MessageBox.Show("Sem pedidos registrados ainda!!"); }
        }
        //função para iniciar o formulario de histórico de pedidos
        private void hist_pedidos(object obj)
        {
            try { Application.Run(new historico_pedidos()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //chamada ao escrever algo no campo de filtro de produtos, ele chama o carregaBotoes com a palavra ou letra filtrando
        private void pcholdBuscaProd_KeyPress(object sender, KeyEventArgs e)
        {
            if (pcholdBuscaProd.Text != texto_filtra)
            {
                texto_filtra = pcholdBuscaProd.Text;
                CarregaBotoes();
            }
        }
        //função para quando alterar alguma letra no nome do cliente já adiciona no extrato
        private void pcholdCliente_KeyUp(object sender, KeyEventArgs e)
        {
            clienteExtrato.Text = $"Cliente: {pcholdCliente.Text}";
        }
    }
}
