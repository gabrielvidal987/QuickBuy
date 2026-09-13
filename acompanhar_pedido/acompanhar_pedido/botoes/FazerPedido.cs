using System;
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
        //desconto aplicado ao pedido inteiro pelo botão "DESCONTO" (em R$ e/ou em %); 0 = sem desconto
        decimal descontoReais = 0m;
        decimal descontoPercent = 0m;
        //promoções de atacado em combo ativas, carregadas ao abrir a tela
        List<ComboAtacado> combosAtivos = new List<ComboAtacado>();

        //um combo: conjunto de produtos que, somando as quantidades entre eles na mesma venda,
        //atinge um mínimo e faz cada produto sair pelo preço definido para ele no combo
        private class ComboAtacado
        {
            public string Id;
            public string Nome;
            public int QtdMinTotal;
            public Dictionary<string, decimal> PrecoPorProduto = new Dictionary<string, decimal>();
        }
        //inicializador da classe, chama o inicializador de componente, função de estética e função para carregar botoes
        public FazerPedido()
        {
            InitializeComponent();
            Estetica();
            CarregaCombos();
            CarregaBotoes();
        }
        //carrega os combos ativos para aplicar o preço promocional durante a venda
        public void CarregaCombos()
        {
            try
            {
                combosAtivos.Clear();
                ConectarSqlClasse sql = new ConectarSqlClasse();
                foreach (Dictionary<string, string> linha in sql.ListaCombos(true))
                {
                    if (string.IsNullOrEmpty(linha["id_produto"])) { continue; }
                    ComboAtacado combo = combosAtivos.Find(c => c.Id == linha["id_combo"]);
                    if (combo == null)
                    {
                        combo = new ComboAtacado
                        {
                            Id = linha["id_combo"],
                            Nome = linha["nome"],
                            QtdMinTotal = (int)ConectarSqlClasse.ValorDoBanco(linha["qtd_min_total"])
                        };
                        combosAtivos.Add(combo);
                    }
                    combo.PrecoPorProduto[linha["id_produto"]] = ConectarSqlClasse.ValorDoBanco(linha["valor_unitario"]);
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //Recalcula o preço de TODOS os itens do carrinho. Precisa ser no carrinho inteiro (e não item a item) porque o
        //combo depende da soma das quantidades entre produtos diferentes. Para cada item vale o MENOR preço entre:
        //varejo, atacado individual do produto e o preço de cada combo atingido de que ele participe.
        private void RecalculaPrecosCarrinho()
        {
            //descobre quais combos foram atingidos nesta venda
            List<string> combosAtingidos = new List<string>();
            foreach (ComboAtacado combo in combosAtivos)
            {
                int somaNoCarrinho = 0;
                foreach (Dictionary<string, string> item in extratoLista)
                {
                    if (combo.PrecoPorProduto.ContainsKey(item["id_produto"]))
                    {
                        somaNoCarrinho += int.Parse(item["quantidade"]);
                    }
                }
                if (combo.QtdMinTotal > 0 && somaNoCarrinho >= combo.QtdMinTotal) { combosAtingidos.Add(combo.Id); }
            }
            //aplica em cada item o melhor preço disponível
            foreach (Dictionary<string, string> item in extratoLista)
            {
                int quantidade = int.Parse(item["quantidade"]);
                decimal melhorPreco = decimal.Parse(item["valor_varejo"], CultureInfo.InvariantCulture);
                decimal valorAtacado = decimal.Parse(item["valor_atacado"], CultureInfo.InvariantCulture);
                int qtdMinAtacado = int.Parse(item["qtd_min_atacado"]);
                string promocao = "";
                string idCombo = "";
                if (qtdMinAtacado > 0 && quantidade >= qtdMinAtacado && valorAtacado > 0 && valorAtacado < melhorPreco)
                {
                    melhorPreco = valorAtacado;
                    promocao = "atacado";
                }
                foreach (ComboAtacado combo in combosAtivos)
                {
                    if (!combosAtingidos.Contains(combo.Id)) { continue; }
                    decimal precoCombo;
                    if (!combo.PrecoPorProduto.TryGetValue(item["id_produto"], out precoCombo)) { continue; }
                    if (precoCombo > 0 && precoCombo < melhorPreco)
                    {
                        melhorPreco = precoCombo;
                        promocao = "combo";
                        idCombo = combo.Id;
                    }
                }
                item["valor"] = melhorPreco.ToString(CultureInfo.InvariantCulture);
                item["total"] = (melhorPreco * quantidade).ToString(CultureInfo.InvariantCulture);
                item["promocao"] = promocao;
                item["id_combo"] = idCombo;
            }
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
            //botão de histórico de pedidos removido da tela de venda.
            //Para reativar, troque MOSTRAR_BOTAO_HISTORICO para true (o botão e o handler btnHist_Click continuam no código).
            btnHist.Visible = MOSTRAR_BOTAO_HISTORICO;
            pcholdObs.PlaceHolderText = "Adicionar observação...";
            pcholdObs.BackColor = Color.FromArgb(198, 213, 239);
            pnlGeral.BackColor = Color.FromArgb(192, 213, 239);
        }
        //liga/desliga o botão "HISTÓRICO DE PEDIDOS" na tela de venda
        private static readonly bool MOSTRAR_BOTAO_HISTORICO = true;
        //função de remover uma unidade do produto ao clicar no nome dele, caso tenha só uma unidade o produto é removido.
        //ao diminuir a quantidade, reavalia se o item ainda bate o mínimo de atacado e recalcula o preço unitário usado.
        private void nomeProduto_Click(object sender, EventArgs e)
        {
            try
            {
                //pega o texto da label
                string nomeLongo = sender.ToString();
                string textoLabel = nomeLongo.Replace("System.Windows.Forms.Label, Text: ", "");
                Dictionary<string, string> res = extratoLista.Find(r => r["nome"] == textoLabel);
                if (res == null) { return; }
                int total = int.Parse(res["quantidade"]);
                if (total > 1)
                {
                    res["quantidade"] = (total - 1).ToString();
                }
                else
                {
                    extratoLista.Remove(res);
                    nQuantProd.Text = "0";
                }
                //tirar item pode derrubar um combo que estava valendo, então recalcula o carrinho inteiro
                RecalculaPrecosCarrinho();
                CriarLabelExtrato();
                ValorTotal();
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //adia a confirmação da quantidade pra depois do evento (senão o próprio campo é destruído no meio do handler)
        private void AgendaCommitQtd(TextBox tb)
        {
            try
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke((Action)(() => CommitQtdExtrato(tb)));
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //Enter no campo de quantidade do extrato: confirma a nova quantidade
        private void qtdExtrato_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                AgendaCommitQtd((TextBox)sender);
            }
        }
        //saiu do campo de quantidade do extrato: confirma a nova quantidade
        private void qtdExtrato_Leave(object sender, EventArgs e)
        {
            AgendaCommitQtd((TextBox)sender);
        }
        //aplica a quantidade digitada no extrato, abatendo do estoque disponível: se pedir mais do que tem,
        //avisa e ajusta para a quantidade possível; 0 ou vazio remove o item.
        private void CommitQtdExtrato(TextBox tb)
        {
            try
            {
                if (tb == null || tb.IsDisposed) { return; }
                string idProduto = tb.Tag as string;
                Dictionary<string, string> item = extratoLista.Find(r => r["id_produto"] == idProduto);
                if (item == null) { return; }
                int qtdAtual = int.Parse(item["quantidade"]);
                int novaQtd;
                if (!int.TryParse((tb.Text ?? "").Trim(), out novaQtd))
                {
                    tb.Text = qtdAtual.ToString();
                    return;
                }
                if (novaQtd == qtdAtual) { return; }
                if (novaQtd <= 0)
                {
                    extratoLista.Remove(item);
                }
                else
                {
                    //relê o estoque do produto no banco na hora de confirmar a quantidade digitada
                    int disponivel = EstoqueDoProduto(idProduto, item.ContainsKey("disponivel") ? item["disponivel"] : novaQtd.ToString());
                    item["disponivel"] = disponivel.ToString();
                    if (novaQtd > disponivel)
                    {
                        MessageBox.Show($"Estoque insuficiente para '{item["nome"]}'. Disponível: {disponivel}. A quantidade foi ajustada para {disponivel}.");
                        novaQtd = disponivel;
                    }
                    if (novaQtd <= 0) { extratoLista.Remove(item); }
                    else { item["quantidade"] = novaQtd.ToString(); }
                }
                //mudar a quantidade pode ligar/desligar um combo ou o preço de atacado, então recalcula o carrinho inteiro
                RecalculaPrecosCarrinho();
                CriarLabelExtrato();
                ValorTotal();
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
                    TextBox qtd = new TextBox();
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
                    decimal precoAtualExibido = decimal.Parse(res["valor"], CultureInfo.InvariantCulture);
                    valorProduto.Text = $"R${precoAtualExibido.ToString("F2").Replace('.', ',')}";
                    string promocaoAplicada = res.ContainsKey("promocao") ? res["promocao"] : "";
                    if (promocaoAplicada == "combo")
                    {
                        valorProduto.Text += " (combo)";
                        valorProduto.ForeColor = Color.FromArgb(180, 95, 0);
                    }
                    else if (promocaoAplicada == "atacado")
                    {
                        valorProduto.Text += " (atacado)";
                        valorProduto.ForeColor = Color.FromArgb(0, 100, 0);
                    }
                    //campo de quantidade editável: dá pra digitar direto quantas unidades desse produto (limitado ao estoque)
                    qtd.Anchor = AnchorStyles.None;
                    qtd.BorderStyle = BorderStyle.FixedSingle;
                    qtd.Font = new System.Drawing.Font(DefaultFont.FontFamily, 12);
                    qtd.Width = 46;
                    qtd.Height = 23;
                    qtd.TextAlign = HorizontalAlignment.Center;
                    //mesmo fundo do nome do produto (cor do extrato), mas continua sendo um campo editável
                    qtd.BackColor = tbExtrato.BackColor;
                    qtd.Text = res["quantidade"];
                    qtd.Tag = res["id_produto"];
                    qtd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.qtdExtrato_KeyDown);
                    qtd.Leave += new System.EventHandler(this.qtdExtrato_Leave);
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
        //soma dos itens do extrato antes de qualquer desconto (já com preço de varejo/atacado/combo aplicado por item)
        private decimal SubtotalExtrato()
        {
            decimal soma = 0m;
            foreach (Dictionary<string, string> res in extratoLista)
            {
                soma += decimal.Parse(res["total"], CultureInfo.InvariantCulture);
            }
            return soma;
        }
        //desconto total (R$ informado + % sobre o subtotal), limitado ao próprio subtotal
        private decimal DescontoCalculado(decimal subtotal)
        {
            decimal d = descontoReais + subtotal * descontoPercent / 100m;
            if (d < 0m) { d = 0m; }
            if (d > subtotal) { d = subtotal; }
            return d;
        }
        //subtotal menos o desconto (o que o cliente paga)
        private decimal TotalComDesconto()
        {
            decimal subtotal = SubtotalExtrato();
            return subtotal - DescontoCalculado(subtotal);
        }
        //atualiza o campo do valor total no extrato (já com o desconto) e o texto/cor do botão de desconto
        public void ValorTotal()
        {
            try
            {
                //extrato vazio zera qualquer desconto pendente
                if (extratoLista.Count == 0) { descontoReais = 0m; descontoPercent = 0m; }
                decimal subtotal = SubtotalExtrato();
                decimal desconto = DescontoCalculado(subtotal);
                totalValorExtrato.Text = (subtotal - desconto).ToString("R$0.00");
                AtualizaBotaoDesconto(desconto);
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //botão de desconto: "APLICAR DESCONTO" quando não há desconto; "APLICADO R$ X DE DESCONTO" (verde) quando há
        private void AtualizaBotaoDesconto(decimal desconto)
        {
            if (desconto > 0m)
            {
                btnDesconto.Text = "APLICADO R$ " + desconto.ToString("N2", CultureInfo.GetCultureInfo("pt-BR")) + " DE DESCONTO";
                btnDesconto.BackColor = Color.FromArgb(132, 246, 58);
            }
            else
            {
                btnDesconto.Text = "APLICAR DESCONTO";
                btnDesconto.BackColor = Color.FromArgb(198, 213, 239);
            }
        }
        //abre o modal de desconto com o valor nominal (subtotal) e aplica o que o operador salvar
        private void btnDesconto_Click(object sender, EventArgs e)
        {
            try
            {
                decimal subtotal = SubtotalExtrato();
                if (subtotal <= 0m)
                {
                    MessageBox.Show("Adicione produtos ao extrato antes de aplicar um desconto.");
                    return;
                }
                using (DescontoDialog dlg = new DescontoDialog(subtotal, descontoReais, descontoPercent))
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        descontoReais = dlg.DescontoReais;
                        descontoPercent = dlg.DescontoPercent;
                        ValorTotal();
                    }
                }
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
            descontoReais = 0m;
            descontoPercent = 0m;
            AtualizaBotaoDesconto(0m);
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
            List<Dictionary<string, string>> itensVenda = new List<Dictionary<string, string>>();
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
                    //desconto do pedido rateado proporcionalmente entre os itens, pra que o preço unitário
                    //gravado em pedido_itens (e usado no relatório de lucro) já saia com o desconto embutido
                    decimal subtotalPedido = SubtotalExtrato();
                    decimal descontoPedido = DescontoCalculado(subtotalPedido);
                    decimal totalLiquido = subtotalPedido - descontoPedido;
                    decimal fatorDesconto = subtotalPedido > 0m ? totalLiquido / subtotalPedido : 1m;

                    nf = $"CLIENTE: {nome_cliente}";
                    nf += $"\nENDEREÇO:\n{endereco}\n";
                    foreach (var item in extratoLista)
                    {
                        string nome_produto = item["nome"];
                        string quantidade = $"{item["quantidade"]}X";
                        produto_quantidade += $"{quantidade} {nome_produto},";
                        string promocaoItem = item.ContainsKey("promocao") ? item["promocao"] : "";
                        decimal precoUnitLiquido = Math.Round(decimal.Parse(item["valor"], CultureInfo.InvariantCulture) * fatorDesconto, 2);
                        itensVenda.Add(new Dictionary<string, string>
                        {
                            { "id_produto", item["id_produto"] },
                            { "quantidade", item["quantidade"] },
                            { "valor_unitario", precoUnitLiquido.ToString(CultureInfo.InvariantCulture) },
                            { "atacado", string.IsNullOrEmpty(promocaoItem) ? "0" : "1" },
                            { "id_combo", item.ContainsKey("id_combo") ? item["id_combo"] : "" }
                        });
                        string texto_item = $"\nITEM: {item["nome"]} QTD: {item["quantidade"]}";
                        string texto_item_formatado = AddLineBreaksEveryNChars(texto_item, 30);
                        nf += texto_item_formatado;
                    }
                    if (obs == "")
                    {
                        obs = "sem observações";
                    }
                    var horario = DateTime.Now;
                    string valorTotal = totalLiquido.ToString("F2", CultureInfo.InvariantCulture);
                    ConectarSqlClasse sql = new ConectarSqlClasse();
                    string hora_pedido = horario.ToString("HH:mm:ss");
                    string formaPag = boxPgto.Text.Replace('é', 'e');
                    nf += $"\nOBS: {obs}";
                    nf += $"\n\n ----- {formaPag} ----- ";
                    if (Convert.ToBoolean(pagamento_ja_efetuado)) { nf += "\nPEDIDO PAGO"; } else { nf += "\nPAGAMENTO PENDENTE"; }
                    nf += "\n---------------------------------";
                    if (descontoPedido > 0m)
                    {
                        CultureInfo br = CultureInfo.GetCultureInfo("pt-BR");
                        nf += $"\nSUBTOTAL: R${subtotalPedido.ToString("N2", br)}";
                        nf += $"\nDESCONTO: -R${descontoPedido.ToString("N2", br)}";
                    }
                    nf += $"\nVALOR TOTAL: {totalValorExtrato.Text}\n---------------------------------";
                    nf += $"\n\nSAÍDA: {retirada}";
                    string resultado_cadastro = sql.CadPedido(nome_cliente, endereco, obs, valorTotal, formaPag, entrega, pagamento_ja_efetuado, produto_entregue, itensVenda, descontoPedido.ToString("F2", CultureInfo.InvariantCulture));
                    if (!resultado_cadastro.StartsWith("*"))
                    {
                        //estoque insuficiente ou erro: nada foi gravado, mantem o carrinho como estava para o operador corrigir
                        MessageBox.Show(resultado_cadastro, "Venda não realizada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    nf += "\n\n" + resultado_cadastro;
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
                            MessageBox.Show("Erro ao imprimir pedido");
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
                pnlGeral.Controls.Clear();
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
                    btn.Tag = item;
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
                ultimaCargaEstoque = DateTime.Now;
            }
        }
        //lê o estoque atual do produto no banco; se der erro de conexão usa o valor de fallback informado
        private int EstoqueDoProduto(string idProduto, string fallback)
        {
            try
            {
                return new ConectarSqlClasse().EstoqueAtualProduto(idProduto);
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                int f;
                return int.TryParse(fallback, out f) ? f : 0;
            }
        }
        //relê o estoque de todos os itens que já estão no carrinho (chamado ao voltar para a tela de venda)
        private void AtualizaEstoqueCarrinho()
        {
            try
            {
                foreach (Dictionary<string, string> item in extratoLista)
                {
                    item["disponivel"] = EstoqueDoProduto(item["id_produto"], item.ContainsKey("disponivel") ? item["disponivel"] : "0").ToString();
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //ao voltar o foco para a tela de venda, relê o estoque de tudo pra sempre trabalhar com número atualizado.
        //debounce de 2s pra não recarregar a cada messagebox/diálogo que fecha.
        private DateTime ultimaCargaEstoque = DateTime.MinValue;
        private bool recarregandoEstoque = false;
        private void FazerPedido_Activated(object sender, EventArgs e)
        {
            if (recarregandoEstoque) { return; }
            if ((DateTime.Now - ultimaCargaEstoque).TotalSeconds < 2) { return; }
            recarregandoEstoque = true;
            try
            {
                CarregaBotoes();
                AtualizaEstoqueCarrinho();
                ultimaCargaEstoque = DateTime.Now;
            }
            finally { recarregandoEstoque = false; }
        }
        //função de quando clica em um produto (sem ser no campo do nome, valor ou foto), adiciona ele ao extrato.
        //bloqueia se já estiver no limite do estoque disponível e aplica o preço de atacado automaticamente ao bater a qtd mínima do item.
        private void produtobtn_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                control.BackColor = Color.FromArgb(240, 240, 240);
                Dictionary<string, string> item = control.Tag as Dictionary<string, string>;
                if (item == null) { return; }
                try
                {
                    string idProduto = item["id_produto"];
                    string nome = item["nome"];
                    decimal valorVarejo = ConectarSqlClasse.ValorDoBanco(item["valor"]);
                    decimal valorAtacado = ConectarSqlClasse.ValorDoBanco(item["valor_atacado"]);
                    int qtdMinAtacado = (int)ConectarSqlClasse.ValorDoBanco(item["qtd_min_atacado"]);
                    //relê o estoque desse produto no banco na hora de adicionar, pra nunca usar número velho
                    int disponivel = EstoqueDoProduto(idProduto, item.ContainsKey("disponivel") ? item["disponivel"] : "0");
                    item["disponivel"] = disponivel.ToString();
                    Dictionary<string, string> itemCarrinho = extratoLista.Find(r => r["id_produto"] == idProduto);
                    int quantidadeAtual = itemCarrinho != null ? int.Parse(itemCarrinho["quantidade"]) : 0;
                    if (quantidadeAtual + 1 > disponivel)
                    {
                        MessageBox.Show($"Estoque insuficiente para '{nome}'. Disponível: {disponivel}.");
                        return;
                    }
                    int novaQtd = quantidadeAtual + 1;
                    if (itemCarrinho != null)
                    {
                        itemCarrinho["quantidade"] = novaQtd.ToString();
                        itemCarrinho["disponivel"] = disponivel.ToString();
                    }
                    else
                    {
                        extratoLista.Add(new Dictionary<string, string>
                        {
                            { "id_produto", idProduto },
                            { "nome", nome },
                            { "valor", valorVarejo.ToString(CultureInfo.InvariantCulture) },
                            { "valor_varejo", valorVarejo.ToString(CultureInfo.InvariantCulture) },
                            { "valor_atacado", valorAtacado.ToString(CultureInfo.InvariantCulture) },
                            { "qtd_min_atacado", qtdMinAtacado.ToString() },
                            { "quantidade", "1" },
                            { "total", valorVarejo.ToString(CultureInfo.InvariantCulture) },
                            { "promocao", "" },
                            { "id_combo", "" },
                            { "disponivel", disponivel.ToString() }
                        });
                    }
                    //o preço sai do recálculo do carrinho inteiro (o combo depende da soma entre produtos)
                    RecalculaPrecosCarrinho();
                    CriarLabelExtrato();
                    ValorTotal();
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
