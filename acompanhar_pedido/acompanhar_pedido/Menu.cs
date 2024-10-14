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
        double valor = 0.00;
        int quant_preparando = 0;
        int quant_prontos = 0;

        public Menu()
        {
            InitializeComponent();
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            Estetica();
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                string nomeClube = File.ReadAllText("Usuario.TXT");
                string imagem_clube = sql.FotoClube(nomeClube);
                if (imagem_clube != "")
                {
                    fotoClube.ImageLocation = $@"{Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString())}\fotos_usuario\{imagem_clube}";
                }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }

        }
        public void Estetica()
        {
            this.BackColor = Color.FromArgb(239, 239, 239);
            //painel topo
            pnlTopo.BackColor = Color.FromArgb(198, 213, 239);
            btnProdutos.BackColor = Color.FromArgb(141, 172, 222);
            btnPedido.BackColor = Color.FromArgb(141, 172, 222);
            btnCozinha.BackColor = Color.FromArgb(141, 172, 222);
            btnRelatorio.BackColor = Color.FromArgb(141, 172, 222);
            btnFila.BackColor = Color.FromArgb(141, 172, 222);
            tbnMusic.BackColor = Color.FromArgb(141, 172, 222);
            data.BackColor = Color.FromArgb(198, 213, 239);
            hora.BackColor = Color.FromArgb(198, 213, 239);
            //botões layout e cores
            lbPreparando.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            lbProntos.Padding = new System.Windows.Forms.Padding(5, 5, 7, 5);
            lbPreparando.BackColor = Color.FromArgb(27, 133, 254);
            lbPrepText.BackColor = Color.FromArgb(27, 133, 254);
            lbPreparando.Text = quant_preparando.ToString();
            lbProntos.BackColor = Color.FromArgb(27, 133, 254);
            lbProntos.Text = quant_prontos.ToString();
            lbProntosText.BackColor = Color.FromArgb(27, 133, 254);
            lbTotal.BackColor = Color.FromArgb(27, 133, 254);
            lbTotal.Text = "R$" + valor;
            lbTotalText.BackColor = Color.FromArgb(27, 133, 254);
            var horario = DateTime.Now;
            hora.Text = horario.ToString("hh:mm:ss");
            data.Text = horario.ToString("dd-MM-yyyy");
        }
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
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                lbPreparando.Text = sql.QtdPreparando();
                lbProntos.Text = sql.QtdProntos();
                lbTotal.Text = $"R${sql.ValorTotal()}";
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            
        }
        private void abrirCozinha(object obj)
        {    
            try { Application.Run(new Cozinha()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        private void abrirPedido(object obj)
        {
            try { Application.Run(new FazerPedido()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            

        }
        private void abrirProdutos(object obj)
        {
            try { Application.Run(new Produtos()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            
        }
        private void abrirRelatorio(object obj)
        {
            try { Application.Run(new Relatorio()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            
        }
        private void abrirFila(object obj)
        {
            try { Application.Run(new Fila()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            
        }
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
        private void button1_Click(object sender, EventArgs e)
        {
            try { MessageBox.Show(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString())); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }   
        }
    }
}