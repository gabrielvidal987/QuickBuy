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
        //função contendo detalhes de estética de cor, elementos, formatos e posições
        public void Estetica()
        {
            pnlCadProd.BackColor = Color.FromArgb(198, 213, 239);
            lbCadProd.BackColor = Color.FromArgb(141, 172, 222);
            fotoProd.BackColor = Color.FromArgb(255, 255, 255);
            btnCadProd.BackColor = Color.FromArgb(0, 185, 34);
            pcholdNomeProd.PlaceHolderText = "ex: Açaí 400ml...";
            pnlGeral.BackColor = Color.FromArgb(192, 213, 239);
            pnlGeral.Location = new Point(30, 50);
            pnlGeral.Size = new Size(this.Width - 350, this.Height - 150);
            pnlCadProd.Location = new Point(pnlGeral.Width + 40, 60);
            pnlCadProd.Size = new Size(240, this.Height - 230);
            btnCadProd.Location = new Point(12, pnlCadProd.Height - 65);
            fotoProd.Location = new Point(12, pnlCadProd.Height - 235);
            impListProd.Location = new Point(pnlCadProd.Location.X + 12, 590);
            impListProd.BackColor = Color.FromArgb(141, 172, 222);
            categoria_box.SelectedIndex = 0;
        }
        //cria os botões para cada produto cadastrado
        public void CriaBtns()
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                string filtro = "";
                List<Dictionary<string, string>> listaProd = new List<Dictionary<string, string>>(sql.DadosProd(filtro));
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
                    PictureBox fotoProd = new PictureBox();
                    PictureBox remProd = new PictureBox();
                    btn.Size = new Size(210, 174 + Convert.ToInt32(altura));
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
                    btn.Controls.Add(nomeProd);
                    btn.Controls.Add(valorProd);
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
        //função para cadastrar um produto novo com as informações escritas
        private void btnCadProd_Click(object sender, EventArgs e)
        {
            if (pcholdNomeProd.Text != null && pcholdNomeProd.Text != "" && valorNumerico.Value != 0 && !produtos_nome.Contains(pcholdNomeProd.Text) )
            {
                try
                {
                    ConectarSqlClasse sql = new ConectarSqlClasse();
                    string nome = pcholdNomeProd.Text;
                    //é preciso converter em string e tirar a virgula para enviar o comando ao sql
                    string valor = valorNumerico.Value.ToString().Replace(',', '.');
                    string categoria = categoria_box.Text;
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
                    MessageBox.Show(sql.InsertProduto(nome, valor, nome_foto_produto, nomeOriginal, categoria));
                    pcholdNomeProd.Text = string.Empty;
                    valorNumerico.Value = 0;
                    try { Image imagemPadrao = new Bitmap(fotopadrao); fotoProd.BackgroundImage = imagemPadrao; } catch { }
                    nomeOriginal = null;
                    foto_caminho = null;
                    categoria_box.SelectedIndex = 0;
                    pcholdNomeProd.Focus();
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
                CriaBtns();
            }
            else { MessageBox.Show("Não são aceitos produtos sem nome, nomes iguais ou valor zerado"); }
        }
        //função chamada ao clicar em um produto, os dados dele são puxados para edição
        private void produtobtn_Click(object sender, EventArgs e)
        {
            try
            {
                nomeOriginal = null;
                foto_caminho = null;
                Control control = (Control)sender;
                pcholdNomeProd.Focus();
                pcholdNomeProd.Text = control.Controls[0].Text.ToString();
                nomeOriginal = control.Controls[0].Text.ToString();
                valorNumerico.Focus();
                valorNumerico.Value = decimal.Parse(control.Controls[1].Text.Replace("R$", "").Replace('.',','));
                fotoProd.BackgroundImage = control.Controls[2].BackgroundImage;
            }
            catch (Exception er) 
            { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
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
                        if (pnlGeral.Controls[i].Controls[3].Name == control.Name)
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