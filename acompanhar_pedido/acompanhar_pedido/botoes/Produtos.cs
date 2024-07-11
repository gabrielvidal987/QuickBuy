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

namespace acompanhar_pedido.botoes
{
    public partial class Produtos : Form
    {
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
            this.BackColor = Color.FromArgb(255, 216, 91);
            pnlCadProd.BackColor = Color.FromArgb(255, 249, 228);
            lbCadProd.BackColor = Color.FromArgb(255, 240, 191);
            fotoProd.BackColor = Color.FromArgb(255, 255, 255);
            btnCadProd.BackColor = Color.FromArgb(0, 185, 34);
            pcholdNomeProd.PlaceHolderText = "ex: Açaí 400ml...";
            pnlGeral.BackColor = Color.FromArgb(255, 226, 131);
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
                    FlowLayoutPanel btn = new FlowLayoutPanel();
                    Label nomeProd = new Label();
                    Label valorProd = new Label();
                    PictureBox fotoProd = new PictureBox();
                    PictureBox remProd = new PictureBox();
                    btn.Size = new Size(153, 180);
                    btn.BorderStyle = BorderStyle.Fixed3D;
                    btn.Padding = new Padding(8, 5, 0, 0);
                    btn.Margin = new Padding(5, 5, 5, 5);
                    btn.BackColor = Color.FromArgb(255, 246, 215);
                    btn.Cursor = Cursors.Hand;
                    btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouseDown);
                    btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseUp);
                    btn.Click += new System.EventHandler(this.produtobtn_Click);
                    nomeProd.BackColor = Color.FromArgb(255, 255, 255);
                    nomeProd.Font = new Font("Arial", 12);
                    nomeProd.AutoSize = false;
                    nomeProd.Size = new Size(129, 23);
                    nomeProd.BorderStyle = BorderStyle.FixedSingle;
                    nomeProd.TextAlign = ContentAlignment.MiddleCenter;
                    nomeProd.Text = item["nome"];
                    nomeProd.Enabled = false;
                    valorProd.BackColor = Color.FromArgb(255, 255, 255);
                    valorProd.Font = new Font("Arial", 12);
                    valorProd.AutoSize = false;
                    valorProd.Size = new Size(129, 23);
                    valorProd.BorderStyle = BorderStyle.FixedSingle;
                    valorProd.TextAlign = ContentAlignment.MiddleCenter;
                    valorProd.Text = $"R${item["valor"]}";
                    valorProd.Enabled = false;
                    fotoProd.BackColor = Color.FromArgb(255, 255, 255);
                    fotoProd.Size = new Size(129, 70);
                    fotoProd.BorderStyle = BorderStyle.FixedSingle;
                    fotoProd.SizeMode = PictureBoxSizeMode.StretchImage;
                    fotoProd.Enabled = false;
                    try { Image myimage = new Bitmap(item["caminho_foto"]); fotoProd.BackgroundImage = myimage; } catch { }
                    fotoProd.BackgroundImageLayout = ImageLayout.Stretch;
                    remProd.BackColor = Color.Transparent;
                    remProd.Name = ind_btn.ToString();
                    remProd.Size = new Size(20, 20);
                    remProd.SizeMode = PictureBoxSizeMode.StretchImage;
                    remProd.Cursor = Cursors.Hand;
                    remProd.Click += new EventHandler(RemProd_Click);
                    remProd.Margin = new Padding(112, 23, 0, 0);
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
    }
}