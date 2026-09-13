using acompanhar_pedido.botoes;
using acompanhar_pedido.teste;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Crypto.Modes.Gcm;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.LinkLabel;

namespace acompanhar_pedido
{
    public partial class Menu : Form
    {
        Thread t1;
        //PAINEL DE SENHAS: desativado a pedido, mas todo o código continua no projeto (botão, texto, tela Fila.cs e o
        //método abrirFila). Para trazer de volta, basta mudar esta constante para true — nada mais precisa ser alterado.
        private static readonly bool MOSTRAR_PAINEL_SENHAS = true;
        //BOTÃO "adicionar som da troca de senha": desativado a pedido. O botão e o handler tbnMusic_Click continuam
        //no código; trocar para true reativa.
        private static readonly bool MOSTRAR_BOTAO_SOM_SENHA = true;
        //inicializador da classe
        public Menu()
        {
            InitializeComponent();
        }
        //itens carregados ao exibir a tela, carrega a função de estética e a logo do usuario
        private void Menu_Load(object sender, EventArgs e)
        {
            Estetica();
            PosicionaBotoesTopo();
            pnlTopo.SizeChanged += (s, ev) => PosicionaBotoesTopo();
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                string imagem_clube = sql.FotoClube();
                if (!string.IsNullOrEmpty(imagem_clube) )
                {
                    fotoClube.ImageLocation = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_usuario",imagem_clube);
                }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }

            //dashboards da tela inicial: monta as abas (Gráficos / Painel), liga a troca de período e faz a primeira carga
            MontaAbasDashboard();
            rbHoje.CheckedChanged += PeriodoDash_Changed;
            rbMes.CheckedChanged += PeriodoDash_Changed;
            rb30dias.CheckedChanged += PeriodoDash_Changed;
            rbTudo.CheckedChanged += PeriodoDash_Changed;
            AtualizaDashboard();
        }
        //troca de período (Hoje / Mês / Tudo) no dashboard
        private void PeriodoDash_Changed(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked) { AtualizaDashboard(); }
        }
        //período selecionado -> [inicio, fim] no formato do banco; ["",""] = todo o histórico
        private string[] PeriodoDash()
        {
            string amanha = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss");
            if (rbHoje.Checked) { return new[] { DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss"), amanha }; }
            if (rbTudo.Checked) { return new[] { "", "" }; }
            if (rb30dias.Checked) { return new[] { DateTime.Today.AddDays(-30).ToString("yyyy-MM-dd HH:mm:ss"), amanha }; }
            return new[] { new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToString("yyyy-MM-dd HH:mm:ss"), amanha };
        }
        private string DescrevePeriodo()
        {
            if (rbHoje.Checked) { return "hoje (" + DateTime.Today.ToString("dd/MM/yyyy") + ")"; }
            if (rbTudo.Checked) { return "todo o histórico"; }
            if (rb30dias.Checked) { return DateTime.Today.AddDays(-30).ToString("dd/MM") + " a " + DateTime.Today.ToString("dd/MM/yyyy") + " (últimos 30 dias)"; }
            return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToString("dd/MM") + " a " + DateTime.Today.ToString("dd/MM/yyyy");
        }
        private static string FormataMoeda(string valorInvariante)
        {
            decimal d;
            decimal.TryParse(valorInvariante, NumberStyles.Any, CultureInfo.InvariantCulture, out d);
            return d.ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
        }
        private static long ToLong(string s)
        {
            decimal d;
            return decimal.TryParse((s ?? "0").Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out d) ? (long)d : 0;
        }
        //--- abas do painel: "Gráficos" (colunas + pizza, como no Excel) e "Painel" (indicadores + grades) ---
        private TabControl tabDash;
        private TabPage tabGraficos;
        private TabPage tabNumeros;
        private Panel pnlChartBarras;
        private Panel pnlChartPizza;
        //dados que os dois gráficos usam: produto -> quantidade vendida no período
        private List<KeyValuePair<string, long>> dadosGrafico = new List<KeyValuePair<string, long>>();
        private static readonly Color[] PALETA_GRAFICO =
        {
            Color.FromArgb(46,110,183), Color.FromArgb(224,123,57), Color.FromArgb(90,168,90),
            Color.FromArgb(193,74,74), Color.FromArgb(140,100,180), Color.FromArgb(120,120,120)
        };
        //monta a TabControl dentro do pnlDash (abaixo do cabeçalho fixo de alerta/período) e move os quadros
        //que já existem para a aba "Painel", deixando a aba "Gráficos" com os dois gráficos desenhados na mão.
        private void MontaAbasDashboard()
        {
            if (tabDash != null) { return; } //já montado
            int topo = 114; //abaixo de: título (y4), alerta (y38) e período (y90)
            tabDash = new TabControl();
            tabDash.Location = new Point(4, topo);
            tabDash.Size = new Size(pnlDash.ClientSize.Width - 8, pnlDash.ClientSize.Height - topo - 4);
            tabDash.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            tabGraficos = new TabPage("  Gráficos  ") { BackColor = Color.White };
            tabNumeros = new TabPage("  Painel  ") { BackColor = pnlDash.BackColor };
            tabDash.TabPages.Add(tabGraficos);
            tabDash.TabPages.Add(tabNumeros);
            pnlDash.Controls.Add(tabDash);
            tabDash.Resize += (s, e) => LayoutAbaNumeros();

            //aba 1 - gráficos
            pnlChartPizza = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlChartBarras = new Panel { Dock = DockStyle.Left, Width = 400, BackColor = Color.White };
            pnlChartBarras.Paint += pnlChartBarras_Paint;
            pnlChartBarras.Resize += (s, e) => pnlChartBarras.Invalidate();
            pnlChartPizza.Paint += pnlChartPizza_Paint;
            pnlChartPizza.Resize += (s, e) => pnlChartPizza.Invalidate();
            tabGraficos.Controls.Add(pnlChartPizza);
            tabGraficos.Controls.Add(pnlChartBarras);

            //aba 2 - reaproveita os controles que o Designer já criou no pnlDash
            Control[] numeros = { lbTilePedidos, lbTileSacos, lbTileBruto, lbTileLiquido,
                                  lbLiderInfo, lbGrid1, lbGrid2, gridMaisVendidos, gridSaidaEstoque };
            foreach (Control c in numeros)
            {
                tabNumeros.Controls.Add(c); //sai do pnlDash automaticamente
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left; //a geometria passa a ser feita por LayoutAbaNumeros
            }
            LayoutAbaNumeros();
        }
        //posiciona os quadros da aba "Painel" ocupando toda a largura/altura disponível (roda ao montar,
        //ao redimensionar a janela e a cada atualização do dashboard)
        private void LayoutAbaNumeros()
        {
            if (gridMaisVendidos == null || gridMaisVendidos.Parent == null) { return; }
            Control host = gridMaisVendidos.Parent;
            int margem = 8, gap = 8;
            int larg = host.ClientSize.Width;
            int alt = host.ClientSize.Height;

            //linha de 4 indicadores no topo
            Label[] tiles = { lbTilePedidos, lbTileSacos, lbTileBruto, lbTileLiquido };
            int tw = (larg - margem * 2 - gap * 3) / 4;
            if (tw < 110) { tw = 110; }
            for (int i = 0; i < 4; i++)
            {
                tiles[i].Location = new Point(margem + i * (tw + gap), 10);
                tiles[i].Size = new Size(tw, 66);
            }
            lbLiderInfo.Location = new Point(margem, 82);
            lbLiderInfo.Width = larg - margem * 2;

            //duas grades lado a lado ocupando o resto
            int meio = (larg - margem * 2 - gap) / 2;
            int gy = 106, gh = alt - 128 - 8;
            if (gh < 90) { gh = 90; }
            int x2 = margem + meio + gap;
            lbGrid1.Location = new Point(margem, gy); lbGrid1.Width = meio;
            lbGrid2.Location = new Point(x2, gy); lbGrid2.Width = larg - x2 - margem;
            gridMaisVendidos.Location = new Point(margem, gy + 22); gridMaisVendidos.Size = new Size(meio, gh);
            gridSaidaEstoque.Location = new Point(x2, gy + 22); gridSaidaEstoque.Size = new Size(larg - x2 - margem, gh);
        }
        private static void DesenhaSemDados(Graphics g, Rectangle area)
        {
            using (Font f = new Font("Microsoft Sans Serif", 10, FontStyle.Italic))
            using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
            {
                g.DrawString("Sem vendas no período selecionado", f, Brushes.Gray, area, sf);
            }
        }
        //gráfico de colunas: sacos vendidos por produto (equivale ao gráfico de barras do Excel)
        private void pnlChartBarras_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            Rectangle full = pnlChartBarras.ClientRectangle;
            using (Font ft = new Font("Microsoft Sans Serif", 10, FontStyle.Bold))
            using (StringFormat sc = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString("SACOS VENDIDOS POR PRODUTO", ft, Brushes.Black, new RectangleF(0, 6, full.Width, 20), sc);
            }
            if (dadosGrafico.Count == 0) { DesenhaSemDados(g, Rectangle.FromLTRB(full.Left, full.Top + 26, full.Right, full.Bottom)); return; }

            long maxV = dadosGrafico.Max(d => d.Value);
            if (maxV <= 0) { maxV = 1; }
            int mEsq = 42, mDir = 14, mTopo = 36, mBaixo = 48;
            Rectangle plot = Rectangle.FromLTRB(full.Left + mEsq, full.Top + mTopo, full.Right - mDir, full.Bottom - mBaixo);
            if (plot.Width < 40 || plot.Height < 40) { return; }

            using (Font fs = new Font("Microsoft Sans Serif", 8))
            using (StringFormat sc = new StringFormat { Alignment = StringAlignment.Center })
            {
                for (int k = 0; k <= 2; k++)
                {
                    long val = maxV * k / 2;
                    float y = plot.Bottom - plot.Height * k / 2f;
                    g.DrawString(val.ToString(), fs, Brushes.Gray, plot.Left - mEsq + 2, y - 7);
                    g.DrawLine(k == 0 ? Pens.Gray : Pens.Gainsboro, plot.Left, y, plot.Right, y);
                }
                g.DrawLine(Pens.Gray, plot.Left, plot.Top, plot.Left, plot.Bottom);

                int n = dadosGrafico.Count;
                float slot = plot.Width / (float)n;
                float bw = Math.Min(slot * 0.6f, 70f);
                for (int i = 0; i < n; i++)
                {
                    KeyValuePair<string, long> d = dadosGrafico[i];
                    float bh = plot.Height * (d.Value / (float)maxV);
                    float x = plot.Left + slot * i + (slot - bw) / 2f;
                    RectangleF bar = new RectangleF(x, plot.Bottom - bh, bw, bh);
                    using (SolidBrush br = new SolidBrush(PALETA_GRAFICO[i % PALETA_GRAFICO.Length])) { g.FillRectangle(br, bar); }
                    g.DrawString(d.Value.ToString(), fs, Brushes.Black, new RectangleF(plot.Left + slot * i, bar.Top - 15, slot, 14), sc);
                    g.DrawString(d.Key, fs, Brushes.Black, new RectangleF(plot.Left + slot * i, plot.Bottom + 4, slot, mBaixo - 6), sc);
                }
            }
        }
        //gráfico de pizza: participação de cada produto nas vendas (equivale ao gráfico de pizza do Excel)
        private void pnlChartPizza_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            Rectangle full = pnlChartPizza.ClientRectangle;
            using (Font ft = new Font("Microsoft Sans Serif", 10, FontStyle.Bold))
            using (StringFormat sc = new StringFormat { Alignment = StringAlignment.Center })
            {
                g.DrawString("PARTICIPAÇÃO NAS VENDAS", ft, Brushes.Black, new RectangleF(0, 6, full.Width, 20), sc);
            }
            long total = dadosGrafico.Sum(d => d.Value);
            if (dadosGrafico.Count == 0 || total <= 0) { DesenhaSemDados(g, Rectangle.FromLTRB(full.Left, full.Top + 26, full.Right, full.Bottom)); return; }

            int lado = Math.Min(full.Width - 170, full.Height - 70);
            if (lado < 60) { lado = Math.Max(60, Math.Min(full.Width - 20, full.Height - 40)); }
            Rectangle pie = new Rectangle(full.Left + 18, full.Top + 40, lado, lado);
            float ang = -90f;
            using (Font fl = new Font("Microsoft Sans Serif", 8))
            {
                for (int i = 0; i < dadosGrafico.Count; i++)
                {
                    KeyValuePair<string, long> d = dadosGrafico[i];
                    float sweep = (float)(360.0 * d.Value / total);
                    using (SolidBrush br = new SolidBrush(PALETA_GRAFICO[i % PALETA_GRAFICO.Length]))
                    {
                        g.FillPie(br, pie, ang, sweep);
                    }
                    ang += sweep;
                    float ly = pie.Top + i * 20;
                    float lx = pie.Right + 16;
                    using (SolidBrush br = new SolidBrush(PALETA_GRAFICO[i % PALETA_GRAFICO.Length])) { g.FillRectangle(br, lx, ly, 12, 12); }
                    g.DrawString($"{d.Key}  {(100.0 * d.Value / total):0.0}%", fl, Brushes.Black, lx + 16, ly - 1);
                }
            }
            g.DrawEllipse(Pens.White, pie);
        }
        //recarrega todos os quadros do dashboard da tela inicial (chamado no load, ao trocar o período e a cada 60s)
        private void AtualizaDashboard()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                string[] p = PeriodoDash();
                string ini = p[0], fim = p[1];
                lbPeriodoInfo.Text = "período: " + DescrevePeriodo();

                Dictionary<string, string> resumo = sql.ResumoDashboard(ini, fim);
                lbTilePedidos.Text = "PEDIDOS\n" + resumo["pedidos"];
                lbTileSacos.Text = "SACOS VENDIDOS\n" + resumo["sacos"];
                lbTileBruto.Text = "FATURAMENTO BRUTO\nR$ " + FormataMoeda(resumo["bruto"]);
                lbTileLiquido.Text = "FATURAMENTO LÍQUIDO\nR$ " + FormataMoeda(resumo["liquido"]);
                lbLiderInfo.Text = "Produto líder no período: " + resumo["produto_lider"];

                List<Dictionary<string, string>> vendidos = sql.ListaProdVendidos(ini, fim);
                long totalVend = 0;
                foreach (Dictionary<string, string> v in vendidos) { totalVend += ToLong(v["qtd_vendido"]); }
                //alimenta os dois gráficos da aba "Gráficos" (limita a 6 produtos para o gráfico não ficar poluído)
                dadosGrafico = vendidos.Take(6).Select(v => new KeyValuePair<string, long>(v["nome"], ToLong(v["qtd_vendido"]))).ToList();
                if (pnlChartBarras != null) { pnlChartBarras.Invalidate(); pnlChartPizza.Invalidate(); }
                LayoutAbaNumeros();
                DataTable dtV = new DataTable();
                dtV.Columns.Add("Produto"); dtV.Columns.Add("Qtd"); dtV.Columns.Add("% vendas");
                foreach (Dictionary<string, string> v in vendidos)
                {
                    long q = ToLong(v["qtd_vendido"]);
                    string pct = totalVend > 0 ? (q * 100.0 / totalVend).ToString("N1", CultureInfo.GetCultureInfo("pt-BR")) + "%" : "-";
                    dtV.Rows.Add(v["nome"], q, pct);
                }
                gridMaisVendidos.DataSource = dtV;

                List<Dictionary<string, string>> saida = sql.SaidaEstoqueDashboard(ini, fim);
                DataTable dtS = new DataTable();
                dtS.Columns.Add("Produto"); dtS.Columns.Add("Saiu"); dtS.Columns.Add("Estoque"); dtS.Columns.Add("Mín");
                foreach (Dictionary<string, string> s in saida)
                {
                    dtS.Rows.Add(s["nome"], s["vendido"], s["estoque_atual"], s["estoque_minimo"] == "0" ? "-" : s["estoque_minimo"]);
                }
                gridSaidaEstoque.DataSource = dtS;
                PintaEstoqueBaixoGrid();

                AtualizaAlertaEstoque(sql);
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //pinta de vermelho as linhas da grade de saída de estoque cujo estoque está no/abaixo do mínimo
        private void PintaEstoqueBaixoGrid()
        {
            foreach (DataGridViewRow row in gridSaidaEstoque.Rows)
            {
                if (row.IsNewRow) { continue; }
                string min = row.Cells["Mín"].Value == null ? "-" : row.Cells["Mín"].Value.ToString();
                string est = row.Cells["Estoque"].Value == null ? "" : row.Cells["Estoque"].Value.ToString();
                int m, ee;
                if (min != "-" && int.TryParse(min, out m) && int.TryParse(est, out ee) && ee <= m)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(150, 0, 0);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        //atualiza o botão/mensagem de alerta de estoque baixo na tela inicial
        private void AtualizaAlertaEstoque(ConectarSqlClasse sql)
        {
            List<Dictionary<string, string>> baixos = sql.ProdutosEstoqueBaixo();
            if (baixos.Count == 0)
            {
                btnAlertaEstoque.Text = "ESTOQUE OK";
                btnAlertaEstoque.BackColor = Color.FromArgb(180, 235, 180);
                lbAlertaEstoque.Text = "Nenhum produto abaixo do estoque mínimo.";
                lbAlertaEstoque.ForeColor = Color.Green;
                return;
            }
            btnAlertaEstoque.Text = "ESTOQUE BAIXO (" + baixos.Count + ")";
            btnAlertaEstoque.BackColor = Color.FromArgb(255, 170, 170);
            List<string> partes = new List<string>();
            for (int i = 0; i < baixos.Count && i < 3; i++)
            {
                partes.Add(baixos[i]["nome"] + ": " + baixos[i]["qtd_estoque"] + " (mín " + baixos[i]["estoque_minimo"] + ")");
            }
            string texto = string.Join("   •   ", partes);
            if (baixos.Count > 3) { texto += "   • +" + (baixos.Count - 3) + " (ver botão)"; }
            lbAlertaEstoque.Text = texto;
            lbAlertaEstoque.ForeColor = Color.FromArgb(180, 0, 0);
        }
        //botão do alerta: abre a lista completa de produtos no/abaixo do estoque mínimo
        private void btnAlertaEstoque_Click(object sender, EventArgs e)
        {
            try
            {
                List<Dictionary<string, string>> baixos = new ConectarSqlClasse().ProdutosEstoqueBaixo();
                if (baixos.Count == 0)
                {
                    MessageBox.Show("Nenhum produto está abaixo do estoque mínimo.", "Estoque");
                    return;
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Produtos no ou abaixo do estoque mínimo:\n");
                foreach (Dictionary<string, string> b in baixos)
                {
                    sb.AppendLine("• " + b["nome"] + "  —  estoque " + b["qtd_estoque"] + "  (mínimo " + b["estoque_minimo"] + ")");
                }
                sb.AppendLine("\nO estoque mínimo de cada produto é definido em 'Gerenciar produtos', ao editar o produto.");
                MessageBox.Show(sb.ToString(), "Alerta de estoque baixo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //recarrega o dashboard periodicamente
        private void timerDash_Tick(object sender, EventArgs e)
        {
            AtualizaDashboard();
        }
        //função com as mudanças estéticas, cores, tamanhos, posições...
        public void Estetica()
        {
            this.BackColor = Color.FromArgb(239, 239, 239);
            //painel topo
            pnlTopo.BackColor = Color.FromArgb(198, 213, 239);
            btnProdutos.BackColor = Color.FromArgb(141, 172, 222);
            btnPagamento.BackColor = Color.FromArgb(141, 172, 222);
            btnPedido.BackColor = Color.FromArgb(141, 172, 222);
            btnCozinha.BackColor = Color.FromArgb(141, 172, 222);
            btnRelatorio.BackColor = Color.FromArgb(141, 172, 222);
            btnFila.BackColor = Color.FromArgb(141, 172, 222);
            btnCaixa.BackColor = Color.FromArgb(141, 172, 222);
            //esconde o painel de senhas sem remover nada do projeto (ver MOSTRAR_PAINEL_SENHAS no topo da classe)
            btnFila.Visible = MOSTRAR_PAINEL_SENHAS;
            label5.Visible = MOSTRAR_PAINEL_SENHAS;
            tbnMusic.Visible = MOSTRAR_BOTAO_SOM_SENHA;
            tbnMusic.BackColor = Color.FromArgb(141, 172, 222);
            data.BackColor = Color.FromArgb(198, 213, 239);
            hora.BackColor = Color.FromArgb(198, 213, 239);
            //botões layout e cores
            lbPag.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            lbPag.BackColor = Color.FromArgb(27, 133, 254);
            lbPag.Text = "0";
            lbPreparando.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            lbPreparando.BackColor = Color.FromArgb(27, 133, 254);
            lbPreparando.Text = "0";
            lbProntos.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            lbProntos.BackColor = Color.FromArgb(27, 133, 254);
            lbProntos.Text = "0";
            lbPagText.BackColor = Color.FromArgb(27, 133, 254);
            lbProntosText.BackColor = Color.FromArgb(27, 133, 254);
            lbPrepText.BackColor = Color.FromArgb(27, 133, 254);
            lbTotal.BackColor = Color.FromArgb(27, 133, 254);
            lbTotal.Text = "R$0,00";
            lbTotalText.BackColor = Color.FromArgb(27, 133, 254);
            var horario = DateTime.Now;
            hora.Text = horario.ToString("hh:mm:ss");
            data.Text = horario.ToString("dd-MM-yyyy");
        }
        //distribui os botões da barra de navegação (e os textos abaixo deles) igualmente ao longo da largura ATUAL
        //do pnlTopo, em vez de posições fixas em pixel — assim nenhum botão fica cortado/fora da tela em monitores
        //menores, nem sobra um vão gigante em monitores maiores. Chamada no load e sempre que o pnlTopo mudar de tamanho.
        private void PosicionaBotoesTopo()
        {
            //monta a lista na ordem da barra, incluindo o painel de senhas só se ele estiver ligado.
            //(não dá pra usar a propriedade Visible aqui: durante o Load ela ainda retorna false para todos, porque o form não foi exibido)
            List<Button> botoes = new List<Button> { btnPedido, btnPagamento, btnProdutos, btnCozinha };
            List<Label> labels = new List<Label> { label1, label2, label3, label4 };
            if (MOSTRAR_PAINEL_SENHAS) { botoes.Add(btnFila); labels.Add(label5); }
            botoes.Add(btnRelatorio); labels.Add(label6);
            botoes.Add(btnCaixa); labels.Add(label7Caixa);
            int margemEsquerda = 250; //reserva espaço pro relógio/data à esquerda
            int margemDireita = MOSTRAR_BOTAO_SOM_SENHA ? 110 : 20; //reserva espaço pro botão de som só quando ele aparece
            int larguraMinima = botoes.Count * botoes[0].Width;
            int larguraUtil = Math.Max(pnlTopo.Width - margemEsquerda - margemDireita, larguraMinima);
            int espacamento = larguraUtil / botoes.Count;
            for (int i = 0; i < botoes.Count; i++)
            {
                int centroSlot = margemEsquerda + espacamento * i + espacamento / 2;
                botoes[i].Location = new Point(centroSlot - botoes[i].Width / 2, 7);
                labels[i].Location = new Point(centroSlot - labels[i].Width / 2, 86);
            }
            if (MOSTRAR_BOTAO_SOM_SENHA)
            {
                tbnMusic.Location = new Point(Math.Max(pnlTopo.Width - tbnMusic.Width - 10, margemEsquerda + larguraUtil + 10), 50);
            }
        }
        //timer para atualizar o horário no canto superior
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var horario = DateTime.Now;
                hora.Text = horario.ToString("hh:mm:ss");
                data.Text = horario.ToString("dd-MM-yyyy");
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //timer que atualiza os valores de total, quantidade de pedidos pendentes e de pedidos entregues
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                lbPag.Text = sql.QtdPagPend();
                lbPreparando.Text = sql.QtdPreparando();
                lbProntos.Text = sql.QtdProntos();
                lbTotal.Text = $"R${sql.TotalGeral()}";
                //alerta de estoque baixo acompanha o ritmo das vendas (consulta leve e indexada)
                AtualizaAlertaEstoque(sql);
            }
            catch (Exception er)
            {
                timer2.Stop();
                bool sucess_log = ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                if (sucess_log)
                {
                    timer2.Start();
                }
            }
        }
        //função do botão de iniciar novo pedido
        private void btnPedido_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(abrirPedido);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função do botão de confirmar pagamento
        private void btnPagamento_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>(sql.FilaCadPed(true));
                if (filaPedidos.Count > 0)
                {
                    try
                    {
                        t1 = new Thread(abrirPagamento);
                        t1.SetApartmentState(ApartmentState.STA);
                        t1.Start();
                    }
                    catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                }
                else { MessageBox.Show("Sem pedidos com pagamento pendentes!!"); }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        //função do botão de gerenciar os produtos
        private void btnProdutos_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(abrirProdutos);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função para abrir a tela da cozinha
        private void btnCozinha_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(abrirCozinha);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função para abrir a fila de senhas
        private void btnFila_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(abrirFila);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função para abrir o relatório final
        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(abrirRelatorio);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função para abrir o controle de caixa
        private void btnCaixa_Click(object sender, EventArgs e)
        {
            try
            {
                t1 = new Thread(abrirCaixa);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função que abre a pagina de pedidos
        private void abrirPedido(object obj)
        {
            try { Application.Run(new FazerPedido()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função que abre a pagina de pagamentos pendentes
        private void abrirPagamento(object obj)
        {
            try { Application.Run(new RealizarPagamento()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função que abre a pagina de produtos
        private void abrirProdutos(object obj)
        {
            try { Application.Run(new Produtos()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }

        }
        //função que abre a cozinha
        private void abrirCozinha(object obj)
        {    
            try { Application.Run(new Cozinha()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função que abre a fila desenhas
        private void abrirFila(object obj)
        {
            ConectarSqlClasse sql = new ConectarSqlClasse();
            if (sql.QtdProntos() != "0")
            {
                try { Application.Run(new Fila()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            else { MessageBox.Show("Sem pedidos registrados!", "Atenção"); }
        }
        //função que abre a pagina de relatório
        private void abrirRelatorio(object obj)
        {
            try { Application.Run(new Relatorio()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }

        }
        //função que abre o controle de caixa
        private void abrirCaixa(object obj)
        {
            try { Application.Run(new Caixa()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //botão que abre a seleção de audio que toca ao trocar a senha
        private void tbnMusic_Click(object sender, EventArgs e)
        {
            try
            { 
                MessageBox.Show("Apenas são aceitos arquivos de audio com extensão '.mp3' ");
                openFileDialog1.ShowDialog();
                try
                {
                    File.Copy(openFileDialog1.FileName, Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\somTrocaSenha.mp3");
                } catch
                {
                    if (File.Exists(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\somTrocaSenha.mp3"))
                    {
                        File.Delete(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\somTrocaSenha.mp3");
                        File.Copy(openFileDialog1.FileName, Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + @"\somTrocaSenha.mp3");
                    }
                }
                MessageBox.Show("Audio para troca de senha adicionado com sucesso!");
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }

        }
        //botão que chama uma messagebox com o endereço da pasta raiz do programa
        private void button1_Click(object sender, EventArgs e)
        {
            try { MessageBox.Show(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString())); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }   
        }

    }
}