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
        //inicializador da classe
        public Menu()
        {
            InitializeComponent();
        }
        //itens carregados ao exibir a tela, carrega a função de estética e a logo do usuario
        private void Menu_Load(object sender, EventArgs e)
        {
            Estetica();
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
            lbTotal.Text = "R$00,00";
            lbTotalText.BackColor = Color.FromArgb(27, 133, 254);
            var horario = DateTime.Now;
            hora.Text = horario.ToString("hh:mm:ss");
            data.Text = horario.ToString("dd-MM-yyyy");
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
                lbTotal.Text = $"R${sql.ValorTotal()}";
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