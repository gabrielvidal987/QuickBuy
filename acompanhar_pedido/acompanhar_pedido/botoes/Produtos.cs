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

namespace acompanhar_pedido.botoes
{
    public partial class Produtos : Form
    {
        string dados_prods;
        string nomeOriginal;
        string fotopadrao = "";
        string foto_caminho;
        string exten = "png";
        Bitmap apaga_ico = new Bitmap($@"{Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString())}\delete.png");
        public Produtos()
        {
            InitializeComponent();
        }
        private void Produtos_Load(object sender, EventArgs e)
        {
            EsteticaFundo();
            CriaBtns();
        }
        private void fotoProd_Click(object sender, EventArgs e)
        {
            if (btnAddpic.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string caminho_foto = btnAddpic.FileName.Replace(@"\", @"\\");
                    List<string> list = new List<string>(caminho_foto.Split('.'));
                    foto_caminho = caminho_foto;
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
                fotoProd.BackgroundImage = imagem;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void btnCadProd_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                string nome = pcholdNomeProd.Text;
                //é preciso converter em string e tirar a virgula para enviar o comando ao sql
                string valor = valorNumerico.Value.ToString().Replace(',', '.');
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + $@"\fotos_produtos"))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + $@"\fotos_produtos");
                    }
                    if (foto_caminho != null )
                    {
                        File.Copy(foto_caminho, Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + $@"\fotos_produtos\{nome}.{exten}");
                        foto_caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()).Replace(@"\", @"\\") + $@"\\fotos_produtos\\{nome}.{exten}";
                    }
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
                MessageBox.Show(sql.InsertProduto(nome, valor, foto_caminho, nomeOriginal));
                pcholdNomeProd.Text = string.Empty;
                valorNumerico.Value = 0;
                try { Image imagemPadrao = new Bitmap(fotopadrao); fotoProd.BackgroundImage = imagemPadrao; } catch { }
                pcholdNomeProd.Focus();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            CriaBtns();
        }
        public void EsteticaFundo()
        {
            pnlCadProd.BackColor = Color.FromArgb(198, 213, 239);
            lbCadProd.BackColor = Color.FromArgb(141, 172, 222);
            fotoProd.BackColor = Color.FromArgb(255, 255, 255);
            btnCadProd.BackColor = Color.FromArgb(0, 185, 34);
            pcholdNomeProd.PlaceHolderText = "ex: Açaí 400ml...";
            pnlGeral.BackColor = Color.FromArgb(192, 213, 239);
            pnlGeral.Location = new Point(30, 50);
            pnlGeral.Size = new Size(this.Width - 350, this.Height - 150);
            pnlCadProd.Location = new Point(pnlGeral.Width + 40, 80);
            pnlCadProd.Size = new Size(240, this.Height - 230);
            btnCadProd.Location = new Point(37, pnlCadProd.Height - 100);
            impListProd.Location = new Point(pnlCadProd.Location.X + 37, 600);
            impListProd.BackColor = Color.FromArgb(141, 172, 222);
            fotoProd.Location = new Point(37, pnlCadProd.Height - 250);

        }
        private void produtobtn_Click(object sender, EventArgs e)
        {
            try
            {
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
        private void RemProd_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                for(int i = 0; i < pnlGeral.Controls.Count;i++)
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
                    fotoProd.SizeMode = PictureBoxSizeMode.StretchImage;
                    fotoProd.Enabled = false;
                    try
                    {
                        Image myimage = new Bitmap(item["caminho_foto"]);
                        fotoProd.BackgroundImage = myimage;
                    }
                    catch { }
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
        private void mouseDown(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackColor = Color.FromArgb(232, 224, 199);
            control.Controls[0].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[1].BackColor = Color.FromArgb(234, 234, 234);
            control.Controls[2].BackColor = Color.FromArgb(234, 234, 234);
        }
        private void mouseUp(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackColor = Color.FromArgb(255, 246, 215);
            control.Controls[0].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[1].BackColor = Color.FromArgb(255, 255, 255);
            control.Controls[2].BackColor = Color.FromArgb(255, 255, 255);
        }
        private void impListProd_Click(object sender, EventArgs e)
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