using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using acompanhar_pedido;
using Google.Protobuf.WellKnownTypes;
using Org.BouncyCastle.Asn1.X509;
using System.Threading;
using System.IO;
using System.Reflection;
using System.ComponentModel.Design;
using System.Numerics;
using Aspose.Cells;
using Font = System.Drawing.Font;

namespace acompanhar_pedido.botoes
{
    public partial class Produtos : Form
    {
        string dados_prods;
        string nomeOriginal;
        string idProdutoEmEdicao;
        string fotopadrao = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_produtos","semFoto.png");
        string foto_caminho;
        string nome_foto_produto;
        string exten = "png";
        List<string> produtos_nome = new List<string>();
        Bitmap apaga_ico = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"delete.png"));
        public Produtos()
        {
            InitializeComponent();
        }
        //função chamada ao carregar a pagina, chama a função de estética() e depois a criaBtns()
        private void Produtos_Load(object sender, EventArgs e)
        {
            Estetica();
            CriaBtns();
        }
        //função contendo detalhes de estética (cores e estado inicial dos campos).
        //ATENÇÃO: posição e tamanho dos painéis ficam por conta do Designer + âncoras. Antes esta função redimensionava
        //o pnlCadProd para 240px de largura no Load, o que espremia o painel e deixava as colunas de valor fora da área visível.
        public void Estetica()
        {
            pnlCadProd.BackColor = Color.FromArgb(198, 213, 239);
            lbCadProd.BackColor = Color.FromArgb(141, 172, 222);
            fotoProd.BackColor = Color.FromArgb(255, 255, 255);
            btnCadProd.BackColor = Color.FromArgb(0, 185, 34);
            pcholdNomeProd.PlaceHolderText = "ex: Açaí 400ml...";
            pnlGeral.BackColor = Color.FromArgb(192, 213, 239);
            impListProd.BackColor = Color.FromArgb(141, 172, 222);
            categoria_box.SelectedIndex = 0;
            //campo de categoria removido do cadastro (negócio de item único).
            //Para reativar, troque MOSTRAR_CATEGORIA para true (o combo e o label continuam no código e a categoria
            //segue sendo gravada com o valor padrão enquanto estiver oculto).
            categoria_box.Visible = MOSTRAR_CATEGORIA;
            label3.Visible = MOSTRAR_CATEGORIA;
            btnEntradaEstoque.Enabled = false;
            btnSaidaEstoque.Enabled = false;
            btnHistoricoEstoque.Enabled = false;
        }
        //liga/desliga o campo "CATEGORIA:" no cadastro de produto
        private static readonly bool MOSTRAR_CATEGORIA = true;
        //cria os botões para cada produto cadastrado
        public void CriaBtns()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                string filtro = "";
                List<Dictionary<string, string>> listaProd = new List<Dictionary<string, string>>(sql.ListaProdutosCadastro(filtro));
                pnlGeral.Controls.Clear();
                int ind_btn = 0;
                foreach (Dictionary<string, string> item in listaProd)
                {
                    produtos_nome.Add(item["nome"]);
                    int quantLetras = item["nome"].ToList().Count;
                    if (quantLetras < 15) { quantLetras = 15; }
                    double altura = quantLetras / 10 * 20;
                    FlowLayoutPanel btn = new FlowLayoutPanel();
                    Label nomeProd = new Label();
                    Label valorProd = new Label();
                    Label qtd_disponivel = new Label();
                    PictureBox fotoProd = new PictureBox();
                    PictureBox remProd = new PictureBox();
                    btn.Size = new Size(210, 200 + Convert.ToInt32(altura));
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
                    nomeProd.Size = new Size(186, Convert.ToInt32(altura));
                    nomeProd.BorderStyle = BorderStyle.FixedSingle;
                    nomeProd.TextAlign = ContentAlignment.MiddleCenter;
                    nomeProd.Text = item["nome"];
                    nomeProd.Enabled = false;
                    valorProd.BackColor = Color.FromArgb(255, 255, 255);
                    valorProd.Font = new Font("Arial", 10);
                    valorProd.ForeColor = Color.Black;
                    valorProd.AutoSize = false;
                    valorProd.Size = new Size(186, 30);
                    valorProd.BorderStyle = BorderStyle.FixedSingle;
                    valorProd.TextAlign = ContentAlignment.MiddleCenter;
                    valorProd.Text = $"R${item["valor"].Replace('.', ',')}";
                    valorProd.Enabled = false;
                    qtd_disponivel.BackColor = Color.FromArgb(255, 255, 255);
                    qtd_disponivel.Font = new Font("Arial", 10);
                    qtd_disponivel.ForeColor = Color.Black;
                    qtd_disponivel.AutoSize = false;
                    qtd_disponivel.Size = new Size(186, 30);
                    qtd_disponivel.BorderStyle = BorderStyle.FixedSingle;
                    qtd_disponivel.TextAlign = ContentAlignment.MiddleCenter;
                    qtd_disponivel.Text = $"Estoque: {item["qtd_estoque"]}";
                    if (!string.IsNullOrEmpty(item["valor_atacado"])) { qtd_disponivel.Text += $" | Atacado {item["qtd_min_atacado"]}+: R${item["valor_atacado"].Replace('.', ',')}"; }
                    qtd_disponivel.Enabled = false;
                    fotoProd.BackColor = Color.FromArgb(255, 255, 255);
                    fotoProd.Size = new Size(186, 105);
                    fotoProd.BorderStyle = BorderStyle.FixedSingle;
                    fotoProd.SizeMode = PictureBoxSizeMode.CenterImage;
                    fotoProd.Enabled = false;
                    try
                    {
                        Image myimage = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos", item["caminho_foto"]));
                        fotoProd.BackgroundImage = myimage;
                    }
                    catch
                    {
                        Bitmap food_ico = new Bitmap(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos", $"{item["categoria"]}.png"));
                        food_ico = new Bitmap(food_ico, fotoProd.Width - 40, fotoProd.Height - 40);
                        fotoProd.Image = food_ico;
                    }
                    fotoProd.BackgroundImageLayout = ImageLayout.Stretch;
                    remProd.BackColor = Color.Transparent;
                    remProd.Name = ind_btn.ToString();
                    remProd.Size = new Size(20, 20);
                    remProd.SizeMode = PictureBoxSizeMode.StretchImage;
                    remProd.Cursor = Cursors.Hand;
                    remProd.Click += new EventHandler(RemProd_Click);
                    remProd.Margin = new Padding(170, 1, 0, 0);
                    remProd.Image = apaga_ico;
                    if (item["ativo"] == "0") { nomeProd.BackColor = Color.MistyRose; qtd_disponivel.Text += " (SEM ESTOQUE)"; }
                    btn.Tag = item;
                    btn.Controls.Add(nomeProd);
                    btn.Controls.Add(valorProd);
                    btn.Controls.Add(qtd_disponivel);
                    btn.Controls.Add(fotoProd);
                    btn.Controls.Add(remProd);
                    pnlGeral.Controls.Add(btn);
                    ind_btn++;
                }
            }
            catch (Exception er) { MessageBox.Show("Erro ao gerar icone dos produtos"); ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função chamada ao clicar na foto do produto a ser cadastrado, assim seleciona uma nova foto e ela é copiada para o codigo caso seja salvo o produto
        private void fotoProd_Click(object sender, EventArgs e)
        {
            if (btnAddpic.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    foto_caminho = null;
                    string caminho_foto = btnAddpic.FileName.Replace(@"\", @"\\");
                    foto_caminho = caminho_foto;
                    List<string> list = new List<string>(caminho_foto.Split('.'));
                    exten = list[list.Count - 1];
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
            else
            {
                foto_caminho = fotopadrao;
            }
            try
            {
                Image imagem = new Bitmap(foto_caminho);
                imagem = new Bitmap(imagem, fotoProd.Width - 40, fotoProd.Height - 40);
                fotoProd.Image = imagem;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        //função para cadastrar um produto novo ou salvar alterações de um produto existente
        private void btnCadProd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(pcholdNomeProd.Text))
            {
                try
                {
                    ConectarSqlClasse sql = new ConectarSqlClasse();
                    string nome = pcholdNomeProd.Text;
                    //é preciso converter em string e tirar a virgula para enviar o comando ao sql
                    string valor = valorNumerico.Value.ToString().Replace(',', '.');
                    string categoria = categoria_box.Text;
                    string qtd_inicial = qtd_inicial_numerico.Value.ToString().Replace(',', '.');
                    string valorCusto = valorCustoNumerico.Value.ToString().Replace(',', '.');
                    string valorAtacado = valorAtacadoNumerico.Value.ToString().Replace(',', '.');
                    string qtdMinAtacado = qtdMinAtacadoNumerico.Value.ToString().Replace(',', '.');
                    string estoqueMinimo = qtdMinEstoqueNumerico.Value.ToString().Replace(',', '.');
                    try
                    {
                        if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos")))
                        {
                            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos"));
                        }
                        if (foto_caminho != null && foto_caminho != "")
                        {
                            File.Copy(foto_caminho, Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "fotos_produtos", $"{nome}.{exten}"), overwrite: true);
                        }
                    }
                    catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
                    nome_foto_produto = $"{nome}.{exten}";
                    MessageBox.Show(sql.InsertProduto(nome, valor, nome_foto_produto, nomeOriginal, qtd_inicial, categoria, valorCusto, valorAtacado, qtdMinAtacado, estoqueMinimo));
                    LimpaCamposCadastro();
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
                CriaBtns();
            }
            else { MessageBox.Show("Não são aceitos produtos sem nome"); }
        }
        //limpa o formulário de cadastro e volta ao modo "produto novo" (usado após cadastrar e pelo botão "Novo Cadastro")
        private void LimpaCamposCadastro()
        {
            pcholdNomeProd.Text = string.Empty;
            valorNumerico.Value = 0;
            valorCustoNumerico.Value = 0;
            valorAtacadoNumerico.Value = 0;
            qtdMinAtacadoNumerico.Value = 0;
            qtdMinEstoqueNumerico.Value = 0;
            qtd_inicial_numerico.Value = 0;
            qtd_inicial_numerico.Enabled = true;
            qtd_disponivel_numerico.Value = 0;
            qtd_vendido_numerico.Value = 0;
            try { Image imagemPadrao = new Bitmap(fotopadrao); fotoProd.Image = imagemPadrao; fotoProd.BackgroundImage = null; } catch { }
            nomeOriginal = null;
            idProdutoEmEdicao = null;
            foto_caminho = null;
            categoria_box.SelectedIndex = 0;
            btnEntradaEstoque.Enabled = false;
            btnSaidaEstoque.Enabled = false;
            btnHistoricoEstoque.Enabled = false;
            btnCadProd.Text = "CADASTRAR";
            pcholdNomeProd.Focus();
        }
        //botão "Novo Cadastro": descarta o que está no formulário e prepara para cadastrar outro produto
        private void btnNovoCadastro_Click(object sender, EventArgs e)
        {
            LimpaCamposCadastro();
        }
        //abre o dialog de movimentação de estoque (ENTRADA ou SAIDA) para o produto em edição
        private void AbreMovimentoEstoque(string tipo)
        {
            if (string.IsNullOrEmpty(idProdutoEmEdicao)) { return; }
            using (MovimentoEstoqueDialog dialog = new MovimentoEstoqueDialog(tipo, idProdutoEmEdicao, nomeOriginal, valorCustoNumerico.Value))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    RecarregaProdutoEmEdicao();
                }
            }
        }
        //abre a tela de configuração das promoções de atacado em combo
        private void btnCombos_Click(object sender, EventArgs e)
        {
            using (PromocaoCombo tela = new PromocaoCombo())
            {
                tela.ShowDialog(this);
            }
            CriaBtns();
        }
        private void btnEntradaEstoque_Click(object sender, EventArgs e) { AbreMovimentoEstoque("ENTRADA"); }
        private void btnSaidaEstoque_Click(object sender, EventArgs e) { AbreMovimentoEstoque("SAIDA"); }
        //mostra o histórico de movimentações de estoque do produto em edição
        private void btnHistoricoEstoque_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idProdutoEmEdicao)) { return; }
            ConectarSqlClasse sql = new ConectarSqlClasse();
            List<Dictionary<string, string>> lista = sql.ListaMovimentacoesProduto(idProdutoEmEdicao);
            DataTable dt = new DataTable();
            dt.Columns.Add("Data/Hora");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Quantidade");
            dt.Columns.Add("Valor Unit.");
            dt.Columns.Add("Fornecedor");
            dt.Columns.Add("Documento");
            dt.Columns.Add("Motivo");
            foreach (Dictionary<string, string> mov in lista)
            {
                dt.Rows.Add(mov["data_hora"], mov["tipo"], mov["quantidade"], mov["valor_unitario"], mov["fornecedor"], mov["documento"], mov["motivo"]);
            }
            using (Form janela = new Form())
            {
                janela.Text = $"Histórico de Estoque - {nomeOriginal}";
                janela.Size = new Size(900, 500);
                janela.StartPosition = FormStartPosition.CenterParent;
                DataGridView grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = dt,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };
                janela.Controls.Add(grid);
                janela.ShowDialog(this);
            }
        }
        //função chamada ao clicar em um produto, os dados dele são puxados para edição (inclusive estoque/custo/atacado, guardados no Tag do botão)
        private void produtobtn_Click(object sender, EventArgs e)
        {
            try
            {
                nomeOriginal = null;
                foto_caminho = null;
                Control control = (Control)sender;
                Dictionary<string, string> item = control.Tag as Dictionary<string, string>;
                if (item == null) { return; }
                pcholdNomeProd.Focus();
                pcholdNomeProd.Text = item["nome"];
                nomeOriginal = item["nome"];
                idProdutoEmEdicao = item["id_produto"];
                valorNumerico.Focus();
                valorNumerico.Value = ConectarSqlClasse.ValorDoBanco(item["valor"]);
                valorCustoNumerico.Value = ConectarSqlClasse.ValorDoBanco(item["valor_custo"]);
                valorAtacadoNumerico.Value = ConectarSqlClasse.ValorDoBanco(item["valor_atacado"]);
                qtdMinAtacadoNumerico.Value = ConectarSqlClasse.ValorDoBanco(item["qtd_min_atacado"]);
                qtdMinEstoqueNumerico.Value = ConectarSqlClasse.ValorDoBanco(item.ContainsKey("estoque_minimo") ? item["estoque_minimo"] : "0");
                qtd_disponivel_numerico.Value = ConectarSqlClasse.ValorDoBanco(item["qtd_estoque"]);
                qtd_vendido_numerico.Value = ConectarSqlClasse.ValorDoBanco(item["qtd_vendido"]);
                qtd_inicial_numerico.Value = 0;
                qtd_inicial_numerico.Enabled = false;
                int indiceCategoria = categoria_box.FindStringExact(item["categoria"]);
                categoria_box.SelectedIndex = indiceCategoria >= 0 ? indiceCategoria : 0;
                PictureBox fotoDoTile = control.Controls[3] as PictureBox;
                if (fotoDoTile != null)
                {
                    fotoProd.BackgroundImage = fotoDoTile.BackgroundImage;
                    fotoProd.Image = fotoDoTile.Image;
                }
                btnEntradaEstoque.Enabled = true;
                btnSaidaEstoque.Enabled = true;
                btnHistoricoEstoque.Enabled = true;
                btnCadProd.Text = "SALVAR ALTERAÇÕES";
            }
            catch (Exception er)
            { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        //recarrega os dados do produto em edição a partir do banco (usado após registrar entrada/saída de estoque no dialog)
        private void RecarregaProdutoEmEdicao()
        {
            if (string.IsNullOrEmpty(idProdutoEmEdicao)) { return; }
            ConectarSqlClasse sql = new ConectarSqlClasse();
            List<Dictionary<string, string>> lista = sql.ListaProdutosCadastro(nomeOriginal);
            Dictionary<string, string> item = lista.Find(p => p["id_produto"] == idProdutoEmEdicao);
            if (item == null) { return; }
            valorCustoNumerico.Value = ConectarSqlClasse.ValorDoBanco(item["valor_custo"]);
            qtd_disponivel_numerico.Value = ConectarSqlClasse.ValorDoBanco(item["qtd_estoque"]);
            CriaBtns();
        }
        //função chamada para remover produto
        private void RemProd_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Certeza que deseja REMOVER o produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Control control = (Control)sender;
                    for (int i = 0; i < pnlGeral.Controls.Count; i++)
                    {
                        //indice 4 = remProd (0 nome, 1 valor, 2 qtd_disponivel, 3 fotoProd, 4 remProd); estava comparando com o fotoProd (indice 3),
                        //que nunca tem Name definido, então o card certo nunca era encontrado e o produto nunca era removido
                        if (pnlGeral.Controls[i].Controls[4].Name == control.Name)
                        {
                            string nome_produto = pnlGeral.Controls[i].Controls[0].Text.ToString();
                            ConectarSqlClasse sql = new ConectarSqlClasse();
                            sql.RemoveProd(nome_produto);
                            CriaBtns();
                        }
                    }
                }
                catch (Exception er)
                { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
        //função para mudar a cor ao apertar o botão do mouse
        private void mouseDown(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackColor = Color.FromArgb(249, 249, 249);
            control.Controls[0].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[1].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[2].BackColor = Color.FromArgb(234, 234, 234);
        }
        //função para mudar a cor ao soltar o botão do mouse
        private void mouseUp(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackColor = Color.FromArgb(240, 240, 240);
            control.Controls[0].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[1].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[2].BackColor = Color.FromArgb(255, 255, 255);
        }
        //função chamada quando aperta o botão de imprimir lista de produtos
        private void impListProd_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Certeza que deseja IMPRIMIR a lista de produtos?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Control control = (Control)sender;
                    ConectarSqlClasse sql = new ConectarSqlClasse();
                    dados_prods = sql.imprimeListaProdutos();
                    try
                    {
                        impressora.Print();
                        pnlCadProd.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao imprimir pedido");
                    }
                }
                catch (Exception er)
                { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
        //função chamada para imprimir a lista de produtos
        private void impressora_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                Font font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush cor = new SolidBrush(Color.Black);
                Point local = new Point(5, 10);
                e.Graphics.DrawString(dados_prods, font, cor, local);
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
    }
}