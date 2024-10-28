using acompanhar_pedido.botoes;
using Google.Protobuf.WellKnownTypes;
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
using static Org.BouncyCastle.Math.EC.ECCurve;

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

        }
        private void Login()
        {
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
                string caminho_usuario = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "Usuario.TXT");
                File.WriteAllText(caminho_usuario, txtSenha.Text);
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
            btnLogin.BackColor = Color.FromArgb(0, 54, 209);
            btnLogin.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 58, 225);
            btnLogin.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 47, 184);
            txtSenha.Location = new Point(panel2.Location.X + 3, panel2.Location.Y - 100);
            btnLogin.Location = new Point(txtSenha.Location.X + 57, txtSenha.Location.Y + 50);
            txtSenha.Enabled = false;
            btnLogin.Enabled = false;
            testConexaobtn_Click(sender, e);
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
            if (e.KeyCode == Keys.Enter)
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
            resultConexao.Text = "";
            try
            {
                Dictionary<string, string> config = new Dictionary<string, string>()
                {
                    { "server", "localhost" },
                    { "uid", "root"},
                    { "pwd", "Vid@l9871"},
                    { "database", "acompanha_pedidosschema" }
                };
                Dictionary<string, string> conn = new Dictionary<string, string>()
                {
                    { "server", string.IsNullOrEmpty(server.Text) ? config["server"] : server.Text },
                    { "uid", string.IsNullOrEmpty(uid.Text) ? config["uid"] : uid.Text },
                    { "pwd", string.IsNullOrEmpty(password.Text) ? config["pwd"] : password.Text },
                    { "database", string.IsNullOrEmpty(database.Text) ? config["database"] : database.Text }
                };
                try
                {
                    string caminho_json = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "last_conn_json.json");
                    string json = File.ReadAllText(caminho_json);
                    config = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    conn = new Dictionary<string, string>()
                    {
                        { "server", string.IsNullOrEmpty(server.Text) ? config["server"] : server.Text },
                        { "uid", string.IsNullOrEmpty(uid.Text) ? config["uid"] : uid.Text },
                        { "pwd", string.IsNullOrEmpty(password.Text) ? config["pwd"] : password.Text },
                        { "database", string.IsNullOrEmpty(database.Text) ? config["database"] : database.Text }
                    };
                }
                catch {}
                ConectarSqlClasse.AtualizarDicionario(conn);
                ConectarSqlClasse sql = new ConectarSqlClasse();
                string res_conn = $"{sql.ConectDataBase()}";
                if (res_conn == "Conexão com o banco de dados realizada com sucesso!!!")
                {
                    // Reescreve o JSON a partir do dicionário
                    foreach(var key_value_pair in conn)
                    {
                        config[key_value_pair.Key] = key_value_pair.Value;
                    }
                    string newJson = JsonConvert.SerializeObject(config, Formatting.Indented);
                    string caminho_json = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "last_conn_json.json");
                    File.WriteAllText(caminho_json, newJson);

                    resultConexao.Text += res_conn;
                    txtSenha.Enabled = true;
                    btnLogin.Enabled = true;
                    testConexaobtn.Enabled = false;
                    server.Enabled = false;
                    uid.Enabled = false;
                    password.Enabled = false;
                    database.Enabled = false;
                    txtSenha.Focus();
                }
                else
                {
                    resultConexao.Text += res_conn;
                }
            }
            catch (Exception ex)
            {
                resultConexao.Text += $"Conexão sem sucesso.... {ex}";
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