using Aspose.Cells;
using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    public partial class Relatorio : Form
    {
        string foto_caminho = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"semFoto.png");
        string exten = "png";
        double entradaTotal = 0;
        //datatable que será exibida. foi usada uma datatable pois ela é facil de modificar, então a consulta
        //do bd retorna os dados e nela é adicionado mais alguns sendo manipulados
        DataTable dt = new DataTable();
        List<Dictionary<string, string>> listaBruta = new List<Dictionary<string, string>>();
        //true quando o que está na tabela é o relatório de vendas (e não o de lucro); usado para regerar ao mudar as colunas
        bool relatorioVendasAtivo = false;
        //nomes das colunas do relatório de vendas, na ordem em que aparecem
        private static readonly string[] COLUNAS_RELATORIO = {
            "Senha", "Nome", "Endereço", "Itens", "Observações", "Hora do pedido",
            "Hora de entrega", "Valor Líquido", "Forma de pagamento", "Pagamento Realizado",
            "Pedido Pronto", "Delivery", "Tempo de espera", "Produto", "QTD" };
        //colunas que começam DESMARCADAS (fora do relatório geral); o usuário pode remarcar no botão "COLUNAS DO RELATÓRIO"
        private static readonly string[] COLUNAS_OCULTAS_PADRAO = { "Tempo de espera", "Produto", "QTD" };
        //inicializador da classe, ele chama a função de estetica() após iniciar o componente e habilita o botão de nova pasta nos dialogs que apresentam as pastas
        public Relatorio()
        {
            InitializeComponent();
            Estetica();
            MontaMenuColunas();
            CarregaTaxasSalvas();
            this.escolherLocal.ShowNewFolderButton = true;
        }
        //pré-carrega nos campos de taxa a última taxa fixada pelo usuário (fica valendo até ele digitar outra);
        //se nunca foi configurada, os campos continuam mostrando o placeholder (padrão de 1,5%)
        private void CarregaTaxasSalvas()
        {
            try
            {
                Dictionary<string, string> taxas = new ConectarSqlClasse().PegaTaxasCartao();
                if (!string.IsNullOrEmpty(taxas["taxa_debito"]))
                {
                    decimal d = decimal.Parse(taxas["taxa_debito"], System.Globalization.CultureInfo.InvariantCulture);
                    txDeb.SetTextoReal(d.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture).Replace('.', ','));
                }
                if (!string.IsNullOrEmpty(taxas["taxa_credito"]))
                {
                    decimal c = decimal.Parse(taxas["taxa_credito"], System.Globalization.CultureInfo.InvariantCulture);
                    txCred.SetTextoReal(c.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture).Replace('.', ','));
                }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função com os detalhes de estética de cor, localização e formas
        //data inicial do filtro no formato do banco; string vazia quando o filtro está desligado (relatório do histórico inteiro)
        private string PeriodoInicio()
        {
            return chkPeriodo.Checked ? dtInicio.Value.Date.ToString("yyyy-MM-dd HH:mm:ss") : "";
        }
        //data final: soma um dia para incluir o último dia inteiro (a comparação no banco é "menor que")
        private string PeriodoFim()
        {
            return chkPeriodo.Checked ? dtFim.Value.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") : "";
        }
        //texto do período para mostrar no resumo
        private string PeriodoDescrito()
        {
            if (!chkPeriodo.Checked) { return "todo o histórico"; }
            return $"{dtInicio.Value:dd/MM/yyyy} a {dtFim.Value:dd/MM/yyyy}";
        }
        private void btnHoje_Click(object sender, EventArgs e)
        {
            chkPeriodo.Checked = true;
            dtInicio.Value = DateTime.Today;
            dtFim.Value = DateTime.Today;
        }
        private void btn7dias_Click(object sender, EventArgs e)
        {
            chkPeriodo.Checked = true;
            dtInicio.Value = DateTime.Today.AddDays(-6);
            dtFim.Value = DateTime.Today;
        }
        private void btnMes_Click(object sender, EventArgs e)
        {
            chkPeriodo.Checked = true;
            dtInicio.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtFim.Value = DateTime.Today;
        }
        public void Estetica()
        {
            pcholdPesquisa.PlaceHolderText = "Digite o nome";
            flowLayoutPanel2.Size = new Size(1140, 650);
            flowLayoutPanel2.Location = new Point(200, 20);
            button1.Width = flowLayoutPanel2.Width - 10;
            tabelaVendas.Width = flowLayoutPanel2.Width - 10;
            tabelaVendas.Height = flowLayoutPanel2.Height - 150;
            //cadastro de usuário/clube removido: sistema passou a ser de usuário único.
            //Para reativar, troque para true (o painel e os handlers continuam no código).
            panel2.Visible = MOSTRAR_CADASTRO_USUARIO;
            //limpeza de dados (apagar tabelas de produtos/vendas/pendentes) removida da tela.
            //Para reativar, troque MOSTRAR_LIMPAR_DADOS para true (checkboxes, botão e handler apagaBD_Click continuam no código).
            flowLayoutPanel1.Visible = MOSTRAR_LIMPAR_DADOS;
            apagaBD.Visible = MOSTRAR_LIMPAR_DADOS;
        }
        //liga/desliga o painel de "criar usuário/clube" na tela de relatórios
        private static readonly bool MOSTRAR_CADASTRO_USUARIO = true;
        //liga/desliga o bloco de "apagar tabelas / limpar dados selecionados" na tela de relatórios
        private static readonly bool MOSTRAR_LIMPAR_DADOS = true;
        //apaga as tabelas selecionadas nas checkbox
        private void apagaBD_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("TEM CERTEZA DE QUE DESEJA APAGAR OS ITENS SELECIONADOS?", "ATENÇÃO", MessageBoxButtons.YesNo);
            if (resposta == DialogResult.Yes)
            {
                try
                {
                    if (delProduto.Checked|| delVendas.Checked|| delPendentes.Checked)
                    {
                        ConectarSqlClasse sql = new ConectarSqlClasse();
                        string produto = "*";
                        string vendas = "*";
                        string pendentes = "*";
                        if (delProduto.Checked)
                        {
                            produto = "produtos";
                        }
                        if (delVendas.Checked)
                        {
                            vendas = "pedidos";
                        }
                        if (delPendentes.Checked)
                        {
                            pendentes = "pedidos";
                        }
                        MessageBox.Show(sql.LimparBD(produto, vendas, pendentes));
                        delProduto.Checked = false;
                        delVendas.Checked = false;
                        delPendentes.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma das opções foram marcadas, nada será apagado!");
                    }
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
        //função chamada a partir do botão de gerar relatório.
        //Monta o relatório em duas partes na mesma tabela:
        //  1) uma linha por pedido do período (colunas 0..12);
        //  2) linhas de resumo ANEXADAS AO FINAL: contagem entrega/balcão e a quantidade vendida de cada produto (colunas 13/14).
        //As linhas de resumo nunca sobrescrevem pedidos, então funciona com qualquer quantidade de pedidos ou produtos.
        private void GeraRelatorio_Click(object sender, EventArgs e)
        {
            entradaTotal = 0;
            entradas.Text = string.Empty;
            saidas.Text = string.Empty;
            try
            {
                tabelaVendas.DataSource = null;
                tabelaVendas.Columns.Clear();
                ConectarSqlClasse sql = new ConectarSqlClasse();

                string ordem = "";
                string nome = "";
                if (btnNomeAZ.Checked) { ordem = "az"; }
                else if (btnNomeZA.Checked) { ordem = "za"; }
                else if (btnValor.Checked) { ordem = "venda"; }
                else if (btnUmNome.Checked)
                {
                    if (string.IsNullOrWhiteSpace(pcholdPesquisa.Text))
                    {
                        MessageBox.Show("Preencha o nome a ser pesquisado.");
                        return;
                    }
                    ordem = "nome";
                    nome = pcholdPesquisa.Text;
                }

                dt = sql.Relatorio(ordem, nome, PeriodoInicio(), PeriodoFim());
                if (dt == null) { dt = new DataTable(); }

                //remove a coluna 'usuario' e adiciona as 3 colunas extras do relatório
                if (dt.Columns.Contains("usuario")) { dt.Columns.Remove("usuario"); }
                dt.Columns.Add("Tempo de espera");
                dt.Columns.Add("Produto");
                dt.Columns.Add("QTD");

                int qtdPedidos = dt.Rows.Count; //linhas reais de pedido, antes de anexar o resumo

                AplicaTaxasETempoEspera(qtdPedidos);
                CalculaEntradaTotal(qtdPedidos);
                //o bloco de resumo (contagem entrega/balcão e qtd por produto) mora nas colunas "Produto"/"QTD";
                //só é anexado quando alguma dessas colunas está marcada no botão "COLUNAS DO RELATÓRIO"
                if (ColunaMarcada("Produto") || ColunaMarcada("QTD")) { AnexaResumo(sql, qtdPedidos); }
                RenomeiaColunas();

                tabelaVendas.DataSource = dt;
                tabelaVendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                AplicaVisibilidadeColunas();
                relatorioVendasAtivo = true;
                exportExc.Enabled = qtdPedidos > 0;
                exportExc.BackColor = qtdPedidos > 0 ? Color.FromArgb(192, 255, 192) : Color.FromArgb(255, 192, 192);
                lbResumoPeriodo.Text = $"VENDAS ({PeriodoDescrito()})  |  {qtdPedidos} pedido(s)  |  entrada total (pagos, com taxas): R${entradaTotal:N2}";
                if (qtdPedidos == 0) { MessageBox.Show("Nenhum pedido encontrado para os filtros selecionados."); }
            }
            catch (Exception er)
            {
                MessageBox.Show("Erro ao gerar o relatório: " + er.Message);
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //lê a taxa (%) de um campo, aceitando "1,5", "1.5" ou "1,5%". Quando o usuário digitou um valor válido,
        //FIXA ele no banco (coluna "taxa_debito"/"taxa_credito" do usuário logado) — passa a valer em qualquer
        //estação/sessão até ser digitado outro. Campo vazio/placeholder: usa o padrão (1,5%) sem mexer no que já
        //estava salvo. Retorna a fração (ex.: 0.015).
        private static double LeTaxaEFixa(PlaceHolderTextBox campo, string colunaBanco, double padrao)
        {
            string limpo = (campo.Text ?? "").Replace("%", "").Replace(',', '.').Trim();
            if (double.TryParse(limpo, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double v) && v > 0)
            {
                try { new ConectarSqlClasse().SalvarTaxaCartao(colunaBanco, (decimal)v); }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                return v / 100.0;
            }
            return padrao;
        }
        //aplica o desconto de taxa de débito/crédito no valor líquido e calcula o tempo de espera de cada pedido
        private void AplicaTaxasETempoEspera(int qtdPedidos)
        {
            double deb = LeTaxaEFixa(txDeb, "taxa_debito", 0.015);
            double cred = LeTaxaEFixa(txCred, "taxa_credito", 0.015);
            for (int c = 0; c < qtdPedidos && c < dt.Rows.Count; c++)
            {
                //tempo de espera (só quando o pedido tem hora de entrega)
                string ficouPronto = dt.Rows[c][6].ToString();
                if (!string.IsNullOrEmpty(ficouPronto))
                {
                    try { dt.Rows[c][12] = (Convert.ToDateTime(ficouPronto) - Convert.ToDateTime(dt.Rows[c][5])).ToString(); }
                    catch { }
                }
                //desconto da taxa conforme a forma de pagamento (case-insensitive, tolera acento)
                string forma = dt.Rows[c][8].ToString().Trim().ToLowerInvariant();
                double valor = Convert.ToDouble(dt.Rows[c][7]);
                if (forma.Contains("déb") || forma.Contains("deb")) { dt.Rows[c][7] = valor - valor * deb; }
                else if (forma.Contains("créd") || forma.Contains("cred")) { dt.Rows[c][7] = valor - valor * cred; }
            }
        }
        //soma o valor líquido de todos os pedidos já pagos do período
        private void CalculaEntradaTotal(int qtdPedidos)
        {
            entradaTotal = 0;
            for (int c = 0; c < qtdPedidos && c < dt.Rows.Count; c++)
            {
                bool pago = false;
                try { pago = Convert.ToBoolean(dt.Rows[c][9]); } catch { }
                if (pago) { entradaTotal += Convert.ToDouble(dt.Rows[c][7]); }
            }
        }
        //anexa ao final da tabela: contagem entrega x balcão e a quantidade vendida de cada produto no período
        private void AnexaResumo(ConectarSqlClasse sql, int qtdPedidos)
        {
            listaBruta = sql.ListaProdVendidos(PeriodoInicio(), PeriodoFim()) ?? new List<Dictionary<string, string>>();

            //conta entregas x balcão a partir das próprias linhas do relatório (respeita o período automaticamente)
            int entrega = 0, balcao = 0;
            for (int c = 0; c < qtdPedidos && c < dt.Rows.Count; c++)
            {
                bool ehDelivery = false;
                try { ehDelivery = Convert.ToBoolean(dt.Rows[c][11]); } catch { }
                if (ehDelivery) { entrega++; } else { balcao++; }
            }

            AdicionaLinhaResumo("Saída Delivery", entrega.ToString());
            AdicionaLinhaResumo("Saída Balcão", balcao.ToString());
            foreach (Dictionary<string, string> prod in listaBruta)
            {
                AdicionaLinhaResumo(prod["nome"], prod["qtd_vendido"]);
            }
        }
        //cria uma linha em branco (colunas de pedido vazias) só com o rótulo/valor nas colunas 13 e 14
        private void AdicionaLinhaResumo(string rotulo, string valor)
        {
            DataRow linha = dt.NewRow();
            linha[13] = rotulo;
            linha[14] = valor;
            dt.Rows.Add(linha);
        }
        //renomeia as colunas para os títulos exibidos e marca as não editáveis
        private void RenomeiaColunas()
        {
            for (int i = 0; i < COLUNAS_RELATORIO.Length && i < dt.Columns.Count; i++)
            {
                dt.Columns[i].ColumnName = COLUNAS_RELATORIO[i];
            }
            try { dt.Columns[9].ReadOnly = true; dt.Columns[11].ReadOnly = true; } catch { }
        }
        //monta o menu de checkboxes do botão "COLUNAS DO RELATÓRIO" (uma opção por coluna do relatório de vendas)
        private void MontaMenuColunas()
        {
            menuColunas.Items.Clear();
            foreach (string col in COLUNAS_RELATORIO)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(col);
                item.CheckOnClick = true;
                item.Checked = Array.IndexOf(COLUNAS_OCULTAS_PADRAO, col) < 0;
                menuColunas.Items.Add(item);
            }
            //ao fechar o menu, regera o relatório de vendas para refletir as colunas escolhidas (inclui/remove o resumo)
            menuColunas.Closed += (s, e) => { if (relatorioVendasAtivo) { GeraRelatorio_Click(this, EventArgs.Empty); } };
        }
        //diz se a coluna com esse título está marcada no menu (colunas desconhecidas contam como marcadas)
        private bool ColunaMarcada(string nome)
        {
            foreach (ToolStripItem it in menuColunas.Items)
            {
                ToolStripMenuItem mi = it as ToolStripMenuItem;
                if (mi != null && mi.Text == nome) { return mi.Checked; }
            }
            return true;
        }
        //aplica no grid a visibilidade de cada coluna conforme o menu
        private void AplicaVisibilidadeColunas()
        {
            foreach (DataGridViewColumn col in tabelaVendas.Columns)
            {
                col.Visible = ColunaMarcada(col.HeaderText);
            }
        }
        //abre o menu de colunas abaixo do botão
        private void btnColunas_Click(object sender, EventArgs e)
        {
            menuColunas.Show(btnColunas, new Point(0, btnColunas.Height));
        }
        //timer responsavel pela responsividade da area da tabela e calculo de entrada - saida
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                button1.Width = flowLayoutPanel2.Width - 10;
                tabelaVendas.Width = flowLayoutPanel2.Width - 10;
                tabelaVendas.Height = flowLayoutPanel2.Height - 160;
                entradas.Text = entradaTotal.ToString("F");
                if (saidas.Text != "")
                {
                    try
                    {
                        double ent = double.Parse(entradas.Text);
                        double sai = double.Parse(saidas.Text);
                        resFinal.Text = (ent - sai).ToString("C");
                    }
                    catch { }

                }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };


        }
        //abre o menu para selecionar a foto do cliente
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddpic.ShowDialog() == DialogResult.OK)
                {
                    string caminho_foto = btnAddpic.FileName.Replace(@"\", @"\\");
                    foto_caminho = caminho_foto;
                    List<string> list = new List<string>(caminho_foto.Split('.'));
                    exten = list[list.Count - 1];
                }
                Image imagem = new Bitmap(foto_caminho);
                pictureBox2.Image = imagem;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        //cadastra o usuario com o nome, senha e imagem/logo
        private void btn_Criar_Usuario_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                try
                {
                    if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_usuario")))
                    {
                        Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_usuario"));
                    }
                    File.Copy(foto_caminho, Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_usuario",$"{nomeClube.Text}.{exten}"), overwrite: true);
                    foto_caminho = $"{nomeClube.Text}.{exten}";
                }
                catch { }
                sql.CriaUsuario(nomeClube.Text.ToLower().Trim(), foto_caminho);
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        //função para criar arquivo de excel com os dados do relatório
        private void exportExc_Click(object sender, EventArgs e)
        {
            DialogResult result = escolherLocal.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    string fl = escolherLocal.SelectedPath;
                    try
                    {
                        // a ordem para as celulas é primeiro numero vertical e segundo horizontal, ou primeiro numero e depois letra, sempre começando do 0
                        Workbook wb = new Workbook();
                        Worksheet ws = wb.Worksheets[0];
                        //exporta exatamente as colunas visíveis do grid (respeita o botão "COLUNAS DO RELATÓRIO"), usando o título mostrado
                        int colSaida = 0;
                        for (int i = 0; i < tabelaVendas.Columns.Count; i++)
                        {
                            if (!tabelaVendas.Columns[i].Visible) { continue; }
                            string titulo = tabelaVendas.Columns[i].HeaderText;
                            ws.Cells[0, colSaida].PutValue(titulo);
                            for (int c = 0; c < tabelaVendas.RowCount; c++)
                            {
                                object bruto = tabelaVendas.Rows[c].Cells[i].Value;
                                string valor_celula = bruto == null ? "" : bruto.ToString();
                                if (valor_celula == "") { continue; }
                                //a coluna "Delivery" só existe nas linhas de pedido; nas linhas de resumo fica vazia
                                if (titulo == "Delivery" && bool.TryParse(valor_celula, out bool ehEntrega))
                                {
                                    valor_celula = ehEntrega ? "Entrega" : "Balcão";
                                }
                                ws.Cells[c + 1, colSaida].PutValue(valor_celula);
                            }
                            colSaida++;
                        }
                        wb.Save(fl + @"\Relatório de vendas.xlsx", SaveFormat.Xlsx);
                        //altera para centralizar texto
                        try
                        {
                            Workbook wa = new Workbook(fl + @"\Relatório de vendas.xlsx");
                            Worksheet wc = wa.Worksheets[0];
                            Style st = wa.CreateStyle();
                            st.HorizontalAlignment = TextAlignmentType.Center;
                            st.VerticalAlignment = TextAlignmentType.Center;
                            st.IsTextWrapped = true;
                            StyleFlag flag = new StyleFlag();
                            flag.Alignments = true;
                            wc.Cells.ApplyStyle(st, flag);
                            for (int i = 0; i < 10; i++)
                            {
                                wc.AutoFitColumn(i);
                            }
                            wa.Save(fl + @"\Relatório de vendas.xlsx", SaveFormat.Xlsx);
                        }
                        catch { }
                        MessageBox.Show("Planilha criada com sucesso no caminho: " + fl);
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Erro ao criar a planilha com o relatório.");
                        ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    }
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
        //mostra na mesma tabela o relatório de lucro por produto (receita das vendas registradas x custo atual de cada produto).
        //Obs: a receita usa o preço realmente cobrado em cada venda (varejo/atacado); vendas feitas antes desta atualização não têm esse
        //histórico e por isso não entram na receita. O custo usado é o CUSTO ATUAL do produto (não guarda custo histórico por venda).
        private void btnRelatorioLucro_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                DataTable dtLucro = sql.RelatorioLucroProdutos(PeriodoInicio(), PeriodoFim());
                relatorioVendasAtivo = false;
                tabelaVendas.DataSource = null;
                tabelaVendas.Columns.Clear();
                tabelaVendas.DataSource = dtLucro;
                tabelaVendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                exportExc.Enabled = false;
                //totaliza o período para mostrar embaixo da tabela
                decimal qtd = 0, receita = 0, custo = 0, lucro = 0;
                foreach (DataRow linha in dtLucro.Rows)
                {
                    qtd += Convert.ToDecimal(linha["QtdVendida"]);
                    receita += Convert.ToDecimal(linha["ReceitaTotal"]);
                    custo += Convert.ToDecimal(linha["CustoTotal"]);
                    lucro += Convert.ToDecimal(linha["Lucro"]);
                }
                string margem = receita > 0 ? (lucro / receita * 100).ToString("N1") + "%" : "-";
                lbResumoPeriodo.Text = $"LUCRO ({PeriodoDescrito()})  |  {qtd:N0} itens vendidos  |  Receita: R${receita:N2}  |  " +
                    $"Custo: R${custo:N2}  |  Lucro: R${lucro:N2}  |  Margem: {margem}";
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função chamada ao apertar enter no campo de filtro de nome
        private void pcholdPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GeraRelatorio_Click(sender, e);
            }
        }
    }
}