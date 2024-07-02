using acompanhar_pedido.botoes;
using Aspose.Cells.Charts;
using Aspose.Cells;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Transactions;
using System.Data.Common;
using IsolationLevel = System.Data.IsolationLevel;

namespace acompanhar_pedido
{
    public class ConectarSqlClasse
    {
        /* bloco exemplo
            catch (Exception er)
            {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        */

        //STATIC dictionary contendo as infos de acesso ao Banco de dados
        static Dictionary<string, string> res = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("BDfila.json"));
        //realiza um teste de conexão ao banco de dados com as config da tela de login, realiza o teste na tela de login
        public string ConectDataBase()
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                //conexão sql
                try
                {
                    conexao.Open();
                    conexao.Dispose();
                    return "Conexão com o banco de dados realizada com sucesso!!! \n";
                }
                catch (Exception erro)
                {
                    return $"Erro ao se conectar com o banco de dados \n Verifique os dados informados... ERRO: {erro}";
                }
                //conexão sql fim
            }
        }
        //envia um log com dados de tipo de erro, origem e mensagem de erro e hora e salva no banco de dados
        public static void EnviaLog(string tipo, string origem, string erro)
        {
            string hora = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss");
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand geraLog = new MySqlCommand($"INSERT INTO log_erro (usuario,hora,tipo,origem,erro) VALUES ('{VariaveisGlobais.Usuario}','{hora}','{tipo}','{origem}','{erro}');", conexao, transaction);
                try
                {
                    geraLog.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Erro ao gerar log: \n {er}");
                }
            }
        }
        //insere produto na tabela de produtos
        public string InsertProduto(string nome, string valor, string caminho, string nomeOriginal)
        {
            string retorno = "Ocorreu um erro, nada foi realizado";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                bool criar = true;
                List<string> lista = new List<string>();
                MySqlCommand pesquisa = new MySqlCommand($"SELECT * FROM produtos WHERE usuario = '{VariaveisGlobais.Usuario}';", conexao,transaction);
                try
                {
                    using (var leitura = pesquisa.ExecuteReader())
                    {
                        while (leitura.Read())
                        {
                            lista.Add(Convert.ToString(leitura["nome"]));
                        }
                    }
                    foreach (string produto in lista)
                    {
                        if (produto.Equals(nomeOriginal))
                        {
                            try
                            {
                                MySqlCommand insere = new MySqlCommand($"UPDATE produtos SET nome = '{nome}', valor = {valor} where nome = '{nomeOriginal}' AND usuario = '{VariaveisGlobais.Usuario}'", conexao, transaction);
                                insere.ExecuteNonQuery();
                                retorno = $"Produto {nomeOriginal} alterado com sucesso!!";
                                criar = false;
                                transaction.Commit();
                                break;
                            }
                            catch (Exception er)
                            {
                                transaction.Rollback();
                                EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                            }
                        }
                    }
                    if (criar == true)
                    {
                        try
                        {
                            MySqlCommand insere = new MySqlCommand($"INSERT INTO produtos(nome,valor, caminho_foto,usuario) VALUES('{nome}',{valor},'{caminho}','{VariaveisGlobais.Usuario}')", conexao,transaction);
                            insere.ExecuteNonQuery();
                            retorno = $"Adicionado {nome} com valor {valor} na tabela de produtos";
                            transaction.Commit();
                        }
                        catch (Exception er)
                        {
                            transaction.Rollback();
                            retorno = $"Ocorreu um erro ao adicionar produto {nome} \n ERRO: {er}";
                            EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                        }
                    }
                } 
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return retorno;


        }
        //gera uma lista com a fila dos pedidos
        public List<Dictionary<string, string>> FilaProdutos()
        {
            List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand comando = new MySqlCommand($"SELECT * FROM pedidos WHERE usuario = '{VariaveisGlobais.Usuario}' ;", conexao, transaction); 
                try
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Dictionary<string, string> pedido = new Dictionary<string, string>
                            {
                            { "numero_pedido", Convert.ToString(reader["numero_pedido"]) },
                            { "nome_cliente", Convert.ToString(reader["nome_cliente"]) },
                            { "produtos_nome", Convert.ToString(reader["produtos_nome"]) },
                            { "observacoes", Convert.ToString(reader["observacoes"]) },
                            { "hora_pedido", Convert.ToString(reader["hora_pedido"]) }
                            };
                                filaPedidos.Add(pedido);
                            }
                            catch (Exception er)
                            {
                                EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                                break;
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    MessageBox.Show($"erro ao gerar lista de fila de pedidos: \n {er}");
                }
            }
            return filaPedidos;

        }
        //gera a lista dos pedidos ja prontos
        public List<Dictionary<string, string>> FilaAnt(string ordem)
        {
            List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand comando = new MySqlCommand($"SELECT * FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}' ORDER BY hora_ficou_pronto {ordem} ;", conexao,transaction);
                try
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Dictionary<string, string> pedido = new Dictionary<string, string>
                                    {
                                    { "numero_pedido", Convert.ToString(reader["numero_pedido"]) },
                                    { "nome_cliente", Convert.ToString(reader["nome_cliente"]) },
                                    { "produtos_nome", Convert.ToString(reader["produtos_nome"]) },
                                    { "observacoes", Convert.ToString(reader["observacoes"]) },
                                    { "hora_pedido", Convert.ToString(reader["hora_pedido"]) },
                                    { "hora_ficou_pronto", Convert.ToString(reader["hora_ficou_pronto"]) },
                                    };
                                filaPedidos.Add(pedido);
                            }
                            catch (Exception erro)
                            {
                                MessageBox.Show("valor não encontrado, erro:" + erro);
                                break;
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return filaPedidos;

        }
        //realiza o cadastro de um pedido novo
        public string CadPedido(string nome_cliente,string endereco, string produtos, string obs, string data, string valor,string formaPag)
        {
            string resultado = "Erro, nada foi realizado";

            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand cadPedido = new MySqlCommand($"INSERT INTO pedidos(nome_cliente,endereco,produtos_nome,observacoes,hora_pedido,valorTotal,formaPag,valorLiq,usuario) VALUES('{nome_cliente}','{endereco}', '{produtos}', '{obs}', '{data}', {valor},'{formaPag}',{valor},'{VariaveisGlobais.Usuario}')", conexao,transaction);
                MySqlCommand numeroPedido = new MySqlCommand($"SELECT * FROM pedidos WHERE usuario = '{VariaveisGlobais.Usuario}'", conexao,transaction);
                string a = "";
                try
                {
                    cadPedido.ExecuteNonQuery();
                    transaction.Commit();
                    resultado = "Pedido cadastrado com sucesso!";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    resultado = $"Erro ao cadastrar pedido";
                }
                try
                {
                    using (var reader = numeroPedido.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            a = reader["numero_pedido"].ToString();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }

                if (resultado == "Pedido cadastrado com sucesso!") { resultado += $"\n*********************************\nNumero do pedido/Senha: {a}\n*********************************"; }
            }
            return resultado;
        }
        //limpa o banco de dados com as especificações da tela de relatórios
        public string LimparBD(string tabelaProdutos, string tabelaVendas)
        {
            string resultado = "Banco de dados apagado por completo!";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand limpaProdutos = new MySqlCommand($"Delete FROM {tabelaProdutos} WHERE usuario = '{VariaveisGlobais.Usuario}'", conexao,transaction);
                MySqlCommand limpaVendas = new MySqlCommand($"Delete FROM {tabelaVendas} WHERE usuario = '{VariaveisGlobais.Usuario}'", conexao,transaction);
                if (tabelaProdutos != "*")
                {
                    try
                    {
                        limpaProdutos.ExecuteNonQuery();
                        resultado = "Apagada tabela de produtos";
                        transaction.Commit();
                    }
                    catch (Exception er)
                    {
                        transaction.Rollback();
                        EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                        resultado = $"erro ao apagar tabela de produtos";
                    }
                }
                if (tabelaVendas != "*")
                {
                    try
                    {
                        limpaVendas.ExecuteNonQuery();
                        resultado = "Apagada tabela de vendas";
                        transaction.Commit();
                    }
                    catch (Exception er)
                    {
                        transaction.Rollback();
                        EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                        resultado = $"erro ao apagar tabela de vendas";
                    }
                }
            }
            return resultado;
        }
        //tira um relatório baseado nas configs da tela janela de relatório
        public DataTable Relatorio(bool filtro, string ordem,string nome)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                var pesquisa = $"SELECT * FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}'";
                if (filtro)
                {
                    switch (ordem)
                    {
                        case "az":
                            pesquisa = $"SELECT * FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}' ORDER BY nome_cliente ASC";
                            break;
                        case "za":
                            pesquisa = $"SELECT * FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}' ORDER BY nome_cliente DESC";
                            break;
                        case "nome":
                            pesquisa = $"SELECT * FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}' AND nome_cliente LIKE '%{nome}%'";
                            break;
                        case "venda":
                            pesquisa = $"SELECT * FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}' ORDER BY valorTotal ASC";
                            break;
                        default:
                            break;
                    }
                }
                using (MySqlDataAdapter da = new MySqlDataAdapter(pesquisa, conexao))
                {
                    using (DataTable dt = new DataTable())
                    {
                        try
                        {
                            da.Fill(dt);
                            return dt;
                        }
                        catch (Exception er)
                        { MessageBox.Show("Sem dados"); EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); ; return dt; }
                    }
                }
            }

        }
        //apaga o pedido da fila e joga na fila dos prontos
        public void ApagaPedidoFila(int id, string hora)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string nome_cliente = "";
                string endereco = "";
                string produtos_nome = "";
                string observacoes = "";
                string hora_pedido = "";
                string hora_ficou_pronto = hora;
                string valorTotal = "0";
                string formaPag = "";
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand pegaDados = new MySqlCommand($"SELECT * FROM pedidos WHERE numero_pedido = {id} AND usuario = '{VariaveisGlobais.Usuario}'", conexao,transaction);
                MySqlCommand apagaPedido = new MySqlCommand($"DELETE FROM pedidos WHERE numero_pedido = {id} AND usuario = '{VariaveisGlobais.Usuario}'", conexao,transaction);
                try
                {
                    using (var reader = pegaDados.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nome_cliente = reader["nome_cliente"].ToString();
                            endereco = reader["endereco"].ToString();
                            produtos_nome = reader["produtos_nome"].ToString();
                            observacoes = reader["observacoes"].ToString();
                            hora_pedido = reader["hora_pedido"].ToString();
                            valorTotal = reader["valorTotal"].ToString().Replace(',', '.');
                            formaPag = reader["formaPag"].ToString();
                        }
                    }
                    MySqlCommand filaPronto = new MySqlCommand($"INSERT INTO pedidos_prontos(numero_pedido,nome_cliente,endereco,produtos_nome,observacoes,hora_pedido,hora_ficou_pronto,valorTotal,formaPag,valorLiq,usuario) VALUES({id},'{nome_cliente}','{endereco}','{produtos_nome}','{observacoes}','{hora_pedido}','{hora_ficou_pronto}',{valorTotal},'{formaPag}',{valorTotal},'{VariaveisGlobais.Usuario}')", conexao, transaction);
                    try
                    {
                        filaPronto.ExecuteNonQuery();
                        apagaPedido.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (Exception er)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erro ao apagar pedido da lista ");
                        EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    }
                }
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
        }
        //traz a quantidade de pedidos preparando
        public string QtdPreparando()
        {
            string qtd = "";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand pegaQtd = new MySqlCommand($"SELECT COUNT(nome_cliente) FROM pedidos WHERE usuario = '{VariaveisGlobais.Usuario}'", conexao);
                using (var reader = pegaQtd.ExecuteReader())
                {
                    while (reader.Read()) { qtd = reader["COUNT(nome_cliente)"].ToString(); }
                }
            }
            return qtd;
        }
        //traz a quantidade de pedidos prontos
        public string QtdProntos()
        {
            string qtd = "";

            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand pegaQtd = new MySqlCommand($"SELECT COUNT(nome_cliente) FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}'", conexao);
                using (var reader = pegaQtd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        qtd = reader["COUNT(nome_cliente)"].ToString();
                    }
                }
            };
            return qtd;
        }
        //traz o valor total que já entrou no caixa, somente dos pedidos ja prontos
        public string ValorTotal()
        {
            string valorTotal = "";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand pegaValor = new MySqlCommand($"SELECT SUM(valorTotal) FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}'", conexao);
                using (var reader = pegaValor.ExecuteReader())
                {
                    while (reader.Read()) { valorTotal = reader["SUM(valorTotal)"].ToString(); }
                }
            }
            return valorTotal;
        }
        //traz uma lista de senhas por ordem da mais antiga para a mais nova
        public List<(string senha, string nome)> Senhas()
        {
            var senha = new List<(string senha, string nome)>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand pegaSenha = new MySqlCommand($"SELECT numero_pedido,nome_cliente FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}' ORDER BY hora_ficou_pronto ASC;", conexao);
                using (var reader = pegaSenha.ExecuteReader())
                {
                    while (reader.Read()) { senha.Add((reader["numero_pedido"].ToString(), reader["nome_cliente"].ToString())); }
                }
            }
            return senha;
        }
        //traz uma lista de dicionarios com os dados dos produtos para a janela de fazer pedido
        public List<Dictionary<string, string>> DadosProd()
        {
            List<Dictionary<string,string>> dadosProd = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand pegaDados = new MySqlCommand($"SELECT * FROM produtos WHERE usuario = '{VariaveisGlobais.Usuario}';", conexao);
                using (var reader = pegaDados.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> dicProds = new Dictionary<string, string>
                        {
                            { "nome", reader["nome"].ToString() },
                            { "valor", reader["valor"].ToString() },
                            { "caminho_foto", reader["caminho_foto"].ToString() },
                            { "id_produto", reader["id_produto"].ToString() }
                        };
                        dadosProd.Add(dicProds);
                    }
                }
            }

            return dadosProd;
        }
        //traz uma lista aonde a posição impar é o horario de pedido e a posição par é o horario que ficou pronto
        public List<string> ListaHorarios()
        {
            List<string> horarios = new List<string>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand pegaHorarios = new MySqlCommand($"SELECT * FROM pedidos_prontos WHERE usuario = '{VariaveisGlobais.Usuario}';", conexao,transaction);
                try
                {
                    using (var reader = pegaHorarios.ExecuteReader())
                    {
                        while (reader.Read()) { horarios.Add(reader["hora_pedido"].ToString()); horarios.Add(reader["hora_ficou_pronto"].ToString()); }
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return horarios;
        }
        //cria o usuario do sistema
        public List<Dictionary<string,string>> FilaCadPed()
        {
            List<Dictionary<string, string>> filaCadPed = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand pegaCadPed = new MySqlCommand($"SELECT * FROM pedidos WHERE usuario = '{VariaveisGlobais.Usuario}' ORDER BY numero_pedido DESC;", conexao, transaction);
                try
                {
                    using (var reader = pegaCadPed.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, string> dicProds = new Dictionary<string, string>
                                {
                                    { "numero_pedido", reader["numero_pedido"].ToString() },
                                    { "nome_cliente", reader["nome_cliente"].ToString() },
                                    { "endereco", reader["endereco"].ToString() },
                                    { "produtos_nome", reader["produtos_nome"].ToString() },
                                    { "observacoes", reader["observacoes"].ToString() },
                                    { "hora_pedido", reader["hora_pedido"].ToString() },
                                    { "valorTotal", reader["valorTotal"].ToString() },
                                    { "formaPag", reader["formaPag"].ToString() }
                                };
                            filaCadPed.Add(dicProds);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return filaCadPed;
        }
        //cria o usuario
        public void CriaUsuario(string nome, string caminho_foto)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand criaUsuario = new MySqlCommand($"INSERT INTO usuarios(senha, foto_clube) VALUES ('{nome}','{caminho_foto}');", conexao,transaction);
                try
                {
                    criaUsuario.ExecuteNonQuery();
                    MessageBox.Show("Usuário cadastrado com sucesso!!");
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
        }       
        //verifica se o usuario ja existe
        public bool Usuario(string nome)
        {
            bool result = false;
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand verificaUsuario = new MySqlCommand($"SELECT * FROM usuarios;", conexao);
                try
                {
                    using (var reader = verificaUsuario.ExecuteReader())
                    {
                        while (reader.Read()) { if (nome == reader["senha"].ToString()) { result = true; } }

                    }
                }
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return result;
        }
        //traz a foto do clube para a janela de relatorios
        public string FotoClube(string nome)
        {
            string caminho = "";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand fotoClube = new MySqlCommand($"SELECT foto_clube FROM usuarios WHERE senha = '{nome}' ;", conexao);
                try
                {
                    caminho = fotoClube.ExecuteScalar().ToString();
                }
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return caminho;
        }
        //remove um produto da lista de produtos
        public void RemoveProd(string nome)
        {
            string resultado = "Não foi possivel deletar o produto";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand apagaProduto = new MySqlCommand($"DELETE FROM produtos WHERE usuario = '{VariaveisGlobais.Usuario}' and nome = '{nome}'", conexao, transaction);
                try
                {
                    apagaProduto.ExecuteNonQuery();
                    transaction.Commit();
                    resultado = "Produto removido com sucesso!";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            MessageBox.Show(resultado);
        }
        public void RemovePedido(string numero_pedido)
        {
            string resultado = "Não foi possivel deletar o pedido";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand apagaProduto = new MySqlCommand($"DELETE FROM pedidos WHERE usuario = '{VariaveisGlobais.Usuario}' and numero_pedido = {numero_pedido}", conexao, transaction);
                try
                {
                    apagaProduto.ExecuteNonQuery();
                    transaction.Commit();
                    resultado = "Pedido removido com sucesso!";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            MessageBox.Show(resultado);
        }
    }
}