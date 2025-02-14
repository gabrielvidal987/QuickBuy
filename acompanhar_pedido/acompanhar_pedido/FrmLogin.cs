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
        //função que realiza o login: verifica se a senha é válida e chama o menu
        private void Login()
        {
            //verifica se o campo de senha foi preenchido
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Preencha a senha");
                txtSenha.Focus();
                return;
            }
            //é realizado o teste da senha, se estiver certa é criado de novo o documento do usuario
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
                ConectarSqlClasse.AtualizarUsuario(txtSenha.Text);
                this.Close();
                t1 = new Thread(abrirMenu);
                t1.SetApartmentState(ApartmentState.STA);
                t1.Start();
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
        }
        //função que abre o menu inicial do programa
        private void abrirMenu(object obj)
        {
            try { Application.Run(new Menu()); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }

        }
        //função que altera algumas estéticas ao carregar a pagina e realiza um teste na conexão
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
        //realiza login caso seja pressionado o botão de enter
        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
        //função do botão de login, caso seja clicado chama a função login()
        private void button1_Click(object sender, EventArgs e)
        {
            Login();
        }
        //realiza o teste da conexão com os campos da conexão do bd
        private void testConexaobtn_Click(object sender, EventArgs e)
        {
            //zera o campo de resultado da conexão e desabilita os campos para não serem editados enquanto a conexão é tentada
            resultConexao.Text = "";
            server.Enabled = false;
            uid.Enabled = false;
            password.Enabled = false;
            database.Enabled = false;
            testConexaobtn.Enabled = false;
            // cria o dicionario de conexão config base e o novo com os campos que foram preenchidos.
            // ele utiliza os campos que ficaram preenchidos e os que não ficaram é utilizado um valor default
            // sintaxe 'condição' ? 'valor_se_verdadeiro' : 'valor_se_falso'
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
                //procura o json da ultima conexão, caso exista ele tenta criar a conexão baseada nesse json como default
                //o json de ultima conexão foi feito a partir da ultima conexão realizada com sucesso
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
                //chama o atualizar dicionario com o dicionario de conexão criado
                ConectarSqlClasse.AtualizarDicionario(conn);
                ConectarSqlClasse sql = new ConectarSqlClasse();
                //realiza o teste de conexão com o bd
                string res_conn = $"{sql.ConectDataBase()}";
                if (res_conn == "Conexão com o banco de dados realizada com sucesso!!!")
                {
                    // Reescreve o JSON de ultima conexão se baseando nessa nova conexão que teve sucesso
                    foreach(var key_value_pair in conn)
                    {
                        config[key_value_pair.Key] = key_value_pair.Value;
                    }
                    string newJson = JsonConvert.SerializeObject(config, Formatting.Indented);
                    string caminho_json = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()), "last_conn_json.json");
                    File.WriteAllText(caminho_json, newJson);

                    //reativa os campos para realizar o login
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
                server.Enabled = true;
                uid.Enabled = true;
                password.Enabled = true;
                database.Enabled = true;
                testConexaobtn.Enabled = true;
            }

        }
        //realiza a tentativa de conexão caso pressione enter em um dos campos da config da conexão
        private void server_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                testConexaobtn_Click(sender, e);
            }
        }
    }
}