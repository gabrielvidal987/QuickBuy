using acompanhar_pedido.botoes;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace acompanhar_pedido
{
    public partial class FrmLogin : Form
    {
        Thread t1;
        // dicionario com a conexão de bd padrão
        //Dictionary<string, string> json = new Dictionary<string, string>()
        //{
        //    {"server", "localhost" },
        //    {"uid", "root" },
        //    {"pwd", "Vid@l9871" },
        //    {"database", "acompanha_pedidosschema" }
        //};
        public FrmLogin()
        {
            InitializeComponent();
            pnlLogin.Visible = false;
        }
        private void Login()
        {
            if (txtEmail.Text != "desbravadores")
            {
                MessageBox.Show("Preencha o usuário");
                txtEmail.Focus();
                return;
            }
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Preencha a senha");
                txtSenha.Focus(); 
                return;
            }
            if (File.Exists("Usuario.TXT"))
            {
                File.Delete("Usuario.TXT");
            }
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                if (sql.Usuario(txtSenha.Text) == false)
                {
                    MessageBox.Show("Usuário desconhecido");
                    txtSenha.Text = "";
                    txtSenha.Focus();
                    return;
                }
                File.WriteAllText("Usuario.TXT", txtSenha.Text);
                VariaveisGlobais.LerNomeArquivo();
                this.Close();
                t1 = new Thread(abrirMenu);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        private void abrirMenu(object obj)
        {
            try { Application.Run(new Menu()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtEmail.Text = "desbravadores";
            pnlLogin.Location = new Point(this.Size.Width / 2 - 180, this.Size.Height / 2 - 196);
            pnlLogin.Visible = true;
            button1.BackColor = Color.FromArgb(0,54,209);
            button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 58, 225);
            button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 47, 184);
            txtEmail.Enabled = false;
            txtSenha.Enabled = false;
            button1.Enabled = false;
        }
        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
        private void testConexaobtn_Click(object sender, EventArgs e)
        {
            //if (server.Text != "" && uid.Text != "" && password.Text != "" && database.Text != "")
            //{
            //    Dictionary<string, string> novoJson = new Dictionary<string, string>()
            //    {
            //        { "server", server.Text },
            //        { "uid", uid.Text },
            //        { "pwd", password.Text },
            //        { "database", database.Text }
            //    };
            //    string jsonString = JsonConvert.SerializeObject(novoJson);
            //    File.WriteAllText("BDfila.json", jsonString);
            //}
            Dictionary<string, string> novoJson = new Dictionary<string, string>()
            {
                { "server", string.IsNullOrEmpty(server.Text) ? "localhost" : server.Text },
                { "uid", string.IsNullOrEmpty(uid.Text) ? "root" : uid.Text },
                { "pwd", string.IsNullOrEmpty(password.Text) ? "Vid@l9871" : password.Text },
                { "database", string.IsNullOrEmpty(database.Text) ? "acompanha_pedidosschema" : database.Text }
            };
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                resultConexao.Text = "";
                resultConexao.Text = "Realizando tentativa de conexão....\n";
                resultConexao.Text += $"{sql.ConectDataBase()}\n";
                if (sql.ConectDataBase() == "Conexão com o banco de dados realizada com sucesso!!! \n")
                {
                    string jsonString = JsonConvert.SerializeObject(novoJson);
                    File.WriteAllText("BDfila.json", jsonString);
                    txtEmail.Enabled = false;
                    txtSenha.Enabled = true;
                    button1.Enabled = true;
                    testConexaobtn.Enabled = false;
                    server.Enabled = false;
                    uid.Enabled = false;
                    password.Enabled = false;
                    database.Enabled = false;
                    txtSenha.Focus();
                }
            } 
            catch 
            {
                resultConexao.Text += "Conexão sem sucesso....\n";
            }

        }
        private void server_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                testConexaobtn_Click(sender, e);
            }
        }
        private void uid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                testConexaobtn_Click(sender, e);
            }
        }
        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                testConexaobtn_Click(sender, e);
            }
        }
        private void database_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                testConexaobtn_Click(sender, e);
            }
        }
    }
}
