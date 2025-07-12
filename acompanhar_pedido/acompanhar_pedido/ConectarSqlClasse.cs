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
using System.Net.Mail;
using System.Management;

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

        //STATIC dictionary, ele é atualizado pelo atualzirdicionario() que coloca as info do BD
        private static Dictionary<string, string> res = new Dictionary<string, string>();
        //STATIC string, ela possui o nome do usuario armazenado para acessar ao longo da classe toda
        private static string usuario_logado = "";
        //atualiza o dict private "res" que contem a conexão com o banco. essa função pega o dicionario de conexão enviado
        // e atualiza a "res" com essa conexão que é utilizada em todas as funções dessa classe
        public static void AtualizarDicionario(Dictionary<string,string> dadosConn)
        {
            res = dadosConn;
        }
        //atualiza a variavel do nome de usuario logado, essa variavel é para diferenciar o usuario no BD
        public static void AtualizarUsuario(string user)
        {
            usuario_logado = user;
        }
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
                    return "Conexão com o banco de dados realizada com sucesso!!!";
                }
                catch (Exception erro)
                {
                    return $"Erro ao se conectar com o banco de dados \n Verifique os dados informados... ERRO: {erro}";
                }
                //conexão sql fim
            }
        }
        //envia um log com dados de tipo de erro, origem e mensagem de erro e hora e salva no banco de dados
        public static bool EnviaLog(string tipo, string origem, string erro)
        {
            string hora = DateTime.Now.ToString("yyyy/MM/dd-HH:mm:ss");
            try
            {
                using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
                {
                    conexao.Open();
                    string comando_geraLog = $"INSERT INTO log_erro (usuario,hora,tipo,origem,erro) VALUES ('{usuario_logado}','{hora}','{tipo}','{origem}','{erro.Replace("'","-")}');";
                    MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                    MySqlCommand geraLog = new MySqlCommand(comando_geraLog, conexao, transaction);
                    try
                    {
                        geraLog.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch
            {
                DialogResult acesso_retornado = MessageBox.Show("Erro ao gerar log e enviar para BD. \n\nContactar Administrador e verificar conexão com internet e BD\n\nO acesso ao Banco de Dados foi reestabelecido?\nSe for marcado 'NÃO' o programa será fechado", "Confirmação",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (acesso_retornado == DialogResult.Yes)
                {
                    return true;
                }
                else
                {
                    Application.Exit();
                }
            }
            return true;
        }
        //insere produto na tabela de produtos
        public string InsertProduto(string nome, string valor, string caminho, string nomeOriginal, string qtd_inicial, string qtd_vendida, string categoria)
        {
            string retorno = "Ocorreu um erro, nada foi realizado";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    if (nomeOriginal != null && nomeOriginal != "") 
                    {
                        string comando_insere = $"UPDATE produtos SET nome = '{nome.Trim()}', valor = {valor}, qtd_vendido = {qtd_vendida}, qtd_estoque = {qtd_inicial}, categoria = '{categoria}' where nome = '{nomeOriginal}' AND usuario = '{usuario_logado}';";
                        MySqlCommand insere = new MySqlCommand(comando_insere, conexao, transaction);
                        insere.ExecuteNonQuery();
                        retorno = $"Produto '{nomeOriginal}' alterado com sucesso!!";
                        transaction.Commit();
                    }
                    else
                    {
                        try
                        {
                            string comando_insere = $"INSERT INTO produtos(nome, valor, qtd_vendido, qtd_estoque, caminho_foto, usuario, categoria) VALUES('{nome.Trim()}',{valor}, {qtd_vendida}, {qtd_inicial},'{caminho}','{usuario_logado}','{categoria}');";
                            MySqlCommand insere = new MySqlCommand(comando_insere, conexao, transaction);
                            insere.ExecuteNonQuery();
                            retorno = $"Adicionado {nome.Trim()} com valor {valor} na tabela de produtos";
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
        //gera uma lista com a fila dos pedidos ja feitos para aparecerem na cozinha
        public List<Dictionary<string, string>> FilaPedidos()
        {
            List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando_sql = $"SELECT * FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = false;";
                MySqlCommand comando = new MySqlCommand(comando_sql, conexao); 
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
                            { "delivery", Convert.ToString(reader["delivery"]) },
                            { "pagamento_aprovado", Convert.ToString(reader["pagamento_aprovado"]) }
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
                }
                catch (Exception er)
                {
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
                string str_comando = $"SELECT * FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = true ORDER BY hora_ficou_pronto {ordem} ;";
                MySqlCommand comando = new MySqlCommand(str_comando,conexao,transaction);
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
                                    { "delivery", Convert.ToString(reader["delivery"]) },
                                    { "pagamento_aprovado", Convert.ToString(reader["pagamento_aprovado"]) }
                                    };
                                filaPedidos.Add(pedido);
                            }
                            catch (Exception er)
                            {
                                MessageBox.Show("valor não encontrado");
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
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return filaPedidos;

        }
        //realiza o cadastro de um pedido novo
        public string CadPedido(string nome_cliente,string endereco, string produtos, string obs, string valor,string formaPag,bool delivery,bool pagamento_efetuado, bool produto_entregue, Dictionary<string, string> produtos_consumo)
        {
            string resultado = "Erro, nada foi realizado";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                long novonumero_gerado = 0;
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string str_cadPedido_sql = $"INSERT INTO pedidos(nome_cliente, endereco, produtos_nome, observacoes, valor_total, forma_pag, pagamento_aprovado, usuario, delivery, pedido_pronto) VALUES('{nome_cliente}', '{endereco}', '{produtos}', '{obs}', {valor},'{formaPag}',{pagamento_efetuado}, '{usuario_logado}', {delivery}, {produto_entregue});";
                MySqlCommand cadPedido = new MySqlCommand(str_cadPedido_sql, conexao,transaction);
                try
                {
                    cadPedido.ExecuteNonQuery();
                    novonumero_gerado = cadPedido.LastInsertedId; // pega o numero gerado de senha do cadastro do pedido
                    transaction.Commit();
                    resultado = "Pedido cadastrado com sucesso!";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    resultado = $"Erro ao cadastrar pedido";
                }
                if (resultado == "Pedido cadastrado com sucesso!") {
                    resultado = $"*********************************\nNumero do pedido/Senha: {novonumero_gerado}\n*********************************"; 
                    
                    if (produtos_consumo.Count > 0 )
                    {
                        foreach (var pair in produtos_consumo)
                        {
                            AumentaQtdUtilizadaProduto(pair.Key, pair.Value);
                        }
                    }

                }
            }
            return resultado;
        }
        //limpa o banco de dados com as especificações da tela de relatórios
        public string LimparBD(string tabelaProdutos, string tabelaVendas,string tabelaPedidos)
        {
            string resultado = "";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                if (tabelaProdutos != "*")
                {
                    string limpaProdutosComand = $"Delete FROM {tabelaProdutos} WHERE usuario = '{usuario_logado}';";
                    MySqlCommand limpaProdutos = new MySqlCommand(limpaProdutosComand, conexao,transaction);
                    try
                    {
                        limpaProdutos.ExecuteNonQuery();
                        resultado += "Sucesso ao apagar tabela de produtos";
                    }
                    catch (Exception er)
                    {
                        EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                        resultado += $"erro ao apagar tabela de produtos";
                    }
                }
                if (tabelaVendas != "*")
                {
                    string limpaVendasComand = $"Delete FROM {tabelaPedidos} WHERE usuario = '{usuario_logado}' AND pedido_pronto = true; UPDATE produtos SET qtd_vendido = 0 WHERE usuario = '{usuario_logado}';";
                    MySqlCommand limpaVendas = new MySqlCommand(limpaVendasComand, conexao, transaction);
                    try
                    {
                        limpaVendas.ExecuteNonQuery();
                        resultado += "\n\nSucesso ao apagar tabela de vendas\nQuantidade de produtos vendidos foi zerada";
                    }
                    catch (Exception er)
                    {
                        EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                        resultado += $"\n\nerro ao apagar tabela de vendas";
                    }
                }
                if (tabelaPedidos != "*")
                {
                    string limpaPedidosComand = $"Delete FROM {tabelaPedidos} WHERE usuario = '{usuario_logado}' AND pedido_pronto = false;";
                    MySqlCommand limpaPedidos = new MySqlCommand(limpaPedidosComand, conexao, transaction);
                    try
                    {
                        limpaPedidos.ExecuteNonQuery();
                        resultado += "\n\nSucesso ao apagar tabela de pedido";
                    }
                    catch (Exception er)
                    {
                        EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                        resultado += $"\n\nerro ao apagar tabela de vendas";
                    }
                }
                try { transaction.Commit(); } catch { transaction.Rollback(); }
            }
            if (resultado == "") { resultado = "Nada foi realizado!"; }
            return resultado;
        }
        //tira um relatório baseado nas configs da tela janela de relatório
        public DataTable Relatorio(string ordem,string nome)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                var pesquisa = $"SELECT * FROM pedidos WHERE usuario = '{usuario_logado}';";
                switch (ordem)
                {
                    case "az":
                        pesquisa += " ORDER BY nome_cliente ASC;";
                        break;
                    case "za":
                        pesquisa += " ORDER BY nome_cliente DESC;";
                        break;
                    case "nome":
                        pesquisa += " AND nome_cliente LIKE '%{nome}%';";
                        break;
                    case "venda":
                        pesquisa += " ORDER BY valor_total ASC;";
                        break;
                    default:
                        break;
                }
                //etapa de verificação, caso tenha um registro sem produto ele será apagado
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                //apaga caso exista qualquer pedido/registro sem produto
                MySqlCommand pesquisa_verifica_campo_nulo = new MySqlCommand(pesquisa, conexao);
                List<string> lista_para_apagar = new List<string>();
                using (var reader = pesquisa_verifica_campo_nulo.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        try
                        {
                            string numero_pedido = Convert.ToString(reader["numero_pedido"]);
                            string produtos_nome = Convert.ToString(reader["produtos_nome"]);
                            if (string.IsNullOrEmpty(produtos_nome)) 
                            {
                                lista_para_apagar.Add(numero_pedido);
                            }
                        }
                        catch (Exception er)
                        {
                            MessageBox.Show("valor não encontrado");
                            EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                            break;
                        }
                    }
                }
                if (lista_para_apagar.Count > 0)
                {
                    foreach(string numero_apagar in lista_para_apagar)
                    {
                        try
                        {
                            string apaga_valor_nulo_comand = $"DELETE FROM prontos WHERE usuario = '{usuario_logado}' AND numero_pedido = {numero_apagar};";
                            MySqlCommand apaga_valor_nulo = new MySqlCommand(apaga_valor_nulo_comand, conexao, transaction);
                            apaga_valor_nulo.ExecuteNonQuery();
                            transaction.Commit();
                        }
                        catch (Exception er)
                        {
                            transaction.Rollback();
                            EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                        }
                    }
                }
                //etapa de criação do datatable com os dados do select de relatório
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
        //traz uma fila com os produtos vendidos
        public List<Dictionary<string, string>> ListaProdVendidos()
        {
            List<Dictionary<string,string>> listaProdutos = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando_sql = $"SELECT * FROM produtos WHERE ativo = true AND usuario = '{usuario_logado}' ;";
                MySqlCommand comando = new MySqlCommand(comando_sql, conexao);
                try
                {
                    //traz a lista bruta dos produtos
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Dictionary<string, string> pedido = new Dictionary<string, string>
                                {
                                { "nome", Convert.ToString(reader["nome"]) },
                                { "qtd_vendido", Convert.ToString(reader["qtd_vendido"]) }
                                };
                                listaProdutos.Add(pedido);
                            }
                            catch (Exception er)
                            {
                                EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                                break;
                            }
                        }
                    }
                }
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return listaProdutos;
        }
        //apaga o pedido da fila e joga na fila dos prontos
        public void MarcaPedidoPronto(int id)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string marca_pedido_pronto = $"UPDATE pedidos SET pedido_pronto = true, hora_ficou_pronto = CURRENT_TIMESTAMP WHERE numero_pedido = {id} AND usuario = '{usuario_logado}';";
                MySqlCommand marca_pedido = new MySqlCommand(marca_pedido_pronto, conexao,transaction);
                try
                {
                    marca_pedido.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    MessageBox.Show("ERRO AO TENTAR MARCAR PEDIDO COMO PRONTO", "ATENÇÃO");
                }
            }
        }
        //atualiza a quantidade de tal produto utilizado
        public void AumentaQtdUtilizadaProduto(string nome_produto, string qtd_produto)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string atualizaQtd_comando = $"UPDATE produtos SET qtd_vendido = qtd_vendido + {qtd_produto} WHERE usuario = '{usuario_logado}' AND nome = '{nome_produto}';";
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand atualizaQtd = new MySqlCommand(atualizaQtd_comando, conexao, transaction);
                try
                {
                    atualizaQtd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            AtivaDesativaProdDisponivel(nome_produto);
        }
        //Desativa o produto caso a quantidade utilizada do produto seja igual a quantidade disponivel no estoque (significa que tudo que tem disponivel já foi consumido)
        public void AtivaDesativaProdDisponivel(string nome_produto)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string atualizaQtd_comando = $"UPDATE produtos SET ativo = IF(qtd_vendido = qtd_estoque, false, true) WHERE usuario = '{usuario_logado}' AND nome = '{nome_produto}';";
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand atualizaQtd = new MySqlCommand(atualizaQtd_comando, conexao, transaction);
                try
                {
                    atualizaQtd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
        }
        //traz a quantidade de pedidos com pagamento pendente
        public string QtdPagPend()
        {
            string qtd = "";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string pegaQtd_comando = $"SELECT COUNT(nome_cliente) FROM pedidos WHERE usuario = '{usuario_logado}' AND pagamento_aprovado = false;";
                MySqlCommand pegaQtd = new MySqlCommand(pegaQtd_comando, conexao);
                using (var reader = pegaQtd.ExecuteReader())
                {
                    while (reader.Read()) { qtd = reader["COUNT(nome_cliente)"].ToString(); }
                }
            }
            return qtd;
        }
        //traz a quantidade de pedidos preparando
        public string QtdPreparando()
        {
            string qtd = "";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string pegaQtd_comando = $"SELECT COUNT(nome_cliente) FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = false;";
                MySqlCommand pegaQtd = new MySqlCommand(pegaQtd_comando, conexao);
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
                string pegaQtd_comando = $"SELECT COUNT(nome_cliente) FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = true;";
                MySqlCommand pegaQtd = new MySqlCommand(pegaQtd_comando, conexao);
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
                string pegaValor_comando = $"SELECT SUM(valor_total) FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = true;";
                MySqlCommand pegaValor = new MySqlCommand(pegaValor_comando, conexao);
                using (var reader = pegaValor.ExecuteReader())
                {
                    while (reader.Read()) { valorTotal = reader["SUM(valor_total)"].ToString(); }
                }
            }
            if (valorTotal == "") { valorTotal = "0,00";  }
            return valorTotal;
        }
        //traz uma lista de senhas por ordem da mais antiga para a mais nova
        public List<(string senha, string nome)> Senhas()
        {
            var senha = new List<(string senha, string nome)>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string pegaSenha_comando = $"SELECT numero_pedido,nome_cliente FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = true ORDER BY hora_ficou_pronto ASC;";
                MySqlCommand pegaSenha = new MySqlCommand(pegaSenha_comando, conexao);
                using (var reader = pegaSenha.ExecuteReader())
                {
                    while (reader.Read()) { senha.Add((reader["numero_pedido"].ToString(), reader["nome_cliente"].ToString())); }
                }
            }
            return senha;
        }
        //traz uma lista de dicionarios com os dados dos produtos para a janela de fazer pedido
        public List<Dictionary<string, string>> DadosProd(string filtro)
        {
            List<Dictionary<string,string>> dadosProd = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string pegaDados_comando = $"SELECT * FROM produtos WHERE ativo = true AND usuario = '{usuario_logado}' AND nome LIKE '%{filtro}%';";
                MySqlCommand pegaDados = new MySqlCommand(pegaDados_comando, conexao);
                using (var reader = pegaDados.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> dicProds = new Dictionary<string, string>
                        {
                            { "nome", reader["nome"].ToString() },
                            { "valor", reader["valor"].ToString() },
                            { "disponivel", (Convert.ToInt64(reader["qtd_estoque"]) - Convert.ToInt64(reader["qtd_vendido"])).ToString()},
                            { "caminho_foto", reader["caminho_foto"].ToString() },
                            { "id_produto", reader["id_produto"].ToString() } ,
                            { "categoria", reader["categoria"].ToString() }
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
                string pegaHorarios_comando = $"SELECT * FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = true;";
                MySqlCommand pegaHorarios = new MySqlCommand(pegaHorarios_comando, conexao,transaction);
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
        //retorna lista de pedidos cadastrado para usar na tela de histórico de pedidos. *retorna mais infos do que o filaPedidos() *caso esteja com pagamento_efetuado false virá tudo,caso contrario virá apenas pagamento pendente
        public List<Dictionary<string,string>> FilaCadPed(bool somente_pagamento_pendente)
        {
            List<Dictionary<string, string>> filaCadPed = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string pegaCadPed_comando = $"SELECT * FROM pedidos WHERE usuario = '{usuario_logado}' ORDER BY numero_pedido DESC;";
                if (somente_pagamento_pendente) { pegaCadPed_comando = $"SELECT * FROM pedidos WHERE usuario = '{usuario_logado}' AND pagamento_aprovado = false ORDER BY numero_pedido DESC;"; }
                MySqlCommand pegaCadPed = new MySqlCommand(pegaCadPed_comando, conexao, transaction);
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
                                    { "valor_total", reader["valor_total"].ToString() },
                                    { "forma_pag", reader["forma_pag"].ToString() },
                                    { "delivery", reader["delivery"].ToString() },
                                    { "pagamento_aprovado", reader["pagamento_aprovado"].ToString() }
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
                string criaUsuario_comando = $"INSERT INTO usuarios(nome, senha, foto) VALUES ('{nome}','{nome}','{caminho_foto}');";
                MySqlCommand criaUsuario = new MySqlCommand(criaUsuario_comando, conexao,transaction);
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
        public bool Usuario(string senha)
        {
            bool result = false;
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string verificaUsuario_comando = $"SELECT * FROM usuarios;";
                MySqlCommand verificaUsuario = new MySqlCommand(verificaUsuario_comando, conexao);
                try
                {
                    using (var reader = verificaUsuario.ExecuteReader())
                    {
                        while (reader.Read()) { if (senha == reader["senha"].ToString()) { result = true; } }

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
        public string FotoClube()
        {
            string caminho = "";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string fotoClube_comando = $"SELECT foto FROM usuarios WHERE senha = '{usuario_logado}' ;";
                MySqlCommand fotoClube = new MySqlCommand(fotoClube_comando, conexao);
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
                string apagaProduto_comando = $"DELETE FROM produtos WHERE ativo = true AND usuario = '{usuario_logado}' and nome = '{nome}';";
                MySqlCommand apagaProduto = new MySqlCommand(apagaProduto_comando, conexao, transaction);
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
        //remove o pedido da lista de pedidos
        public void RemovePedido(string numero_pedido)
        {
            string resultado = "Não foi possivel deletar o pedido";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string apagaProduto_comando = $"DELETE FROM pedidos WHERE usuario = '{usuario_logado}' AND numero_pedido = {numero_pedido};";
                MySqlCommand apagaProduto = new MySqlCommand(apagaProduto_comando, conexao, transaction);
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
        //Aprova o pagamento (atualiza a coluna de pagamento para true no pedido determinado)
        public void AprovaPagamento(string numero_pedido)
        {
            string resultado = "Não foi possivel aprovar o pagamento do pedido";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string aprova_pagamento_comando = $"UPDATE pedidos SET pagamento_aprovado = True WHERE usuario = '{usuario_logado}' AND numero_pedido = {numero_pedido};";
                MySqlCommand aprova_pagamento = new MySqlCommand(aprova_pagamento_comando, conexao, transaction);
                try
                {
                    aprova_pagamento.ExecuteNonQuery();
                    transaction.Commit();
                    resultado = "Pagamento do pedido aprovado com sucesso!";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            MessageBox.Show(resultado);
        }
        //imprime o pedido pronto do historico de pedidos prontos
        public string imprimePedido(string numero_pedido)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string pega_dados_prod_comando = $"SELECT * FROM pedidos WHERE usuario = '{usuario_logado}' AND numero_pedido = {numero_pedido};";
                MySqlCommand pega_dados_prod = new MySqlCommand(pega_dados_prod_comando, conexao, transaction);
                string nf = "";
                try
                {
                    using (var reader = pega_dados_prod.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nf += $"CLIENTE: {reader["nome_cliente"]}";
                            string endereco_bruto = reader["endereco"].ToString();
                            string endereco = AddLineBreaksEveryNChars(endereco_bruto, 30);
                            nf += $"\nENDEREÇO:\n{endereco}\n";
                            string[] produtos_comprados = reader["produtos_nome"].ToString().Split(',');
                            foreach (string prod in produtos_comprados) 
                            { 
                                if (prod == "") { break; }
                                string nome_produto = prod.Split('X')[1];
                                string quantidade = prod.Split('X')[0];
                                string texto_item = $"\nITEM: {nome_produto} QTD: {quantidade}";
                                string texto_item_formatado = AddLineBreaksEveryNChars(texto_item, 30);
                                nf += texto_item_formatado;
                            }
                            string obs_bruto = reader["observacoes"].ToString();
                            nf += $"\nOBS: {AddLineBreaksEveryNChars(obs_bruto, 30)}";
                            nf += $"\n\n ----- {reader["forma_pag"]} ----- ";
                            if (Convert.ToBoolean(reader["pagamento_aprovado"])) { nf += "\nPEDIDO PAGO"; } else { nf += "\nPAGAMENTO PENDENTE"; }
                            nf += $"\n---------------------------------\nVALOR TOTAL: R${reader["valor_total"].ToString()}\n---------------------------------";
                            if (Convert.ToBoolean(reader["delivery"])) { nf += "\n\nSAÍDA: ENTREGA"; } else { nf += "\n\nSAÍDA: BALCÃO"; }
                            nf += $"\n\n*********************************\nNumero do pedido/Senha: {reader["numero_pedido"]}\n*********************************";
                            nf += $"\nHorario do pedido\n{reader["hora_pedido"]}";

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
                return nf;
            }
        }
        //imprime uma lista com os produtos e valores
        public string imprimeListaProdutos()
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string pega_dados_prod_comando = $"SELECT * FROM produtos WHERE ativo = true AND usuario = '{usuario_logado}';";
                MySqlCommand pega_dados_prod = new MySqlCommand(pega_dados_prod_comando, conexao, transaction);
                string lista_pedidos = $"{usuario_logado}\n\n\n";
                try
                {
                    using (var reader = pega_dados_prod.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_pedidos += $"{reader["nome"]} -> R${reader["valor"]}\n\n";
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
                return lista_pedidos;
            }
        }
        //quebra a linha a cada qtd de caracteres determinada pelo dev
        static string AddLineBreaksEveryNChars(string input, int interval)
        {
            if (string.IsNullOrEmpty(input) || interval <= 0)
            {
                return input;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                sb.Append(input[i]);
                if ((i + 1) % interval == 0)
                {
                    sb.Append("\n");
                }
            }

            return sb.ToString();
        }
        //retorna uma lista de booleano sobre os deliverys sendo true para entrega e false para balcão
        public List<bool> pegaListaDelivery()
        {
            List<bool> lista_tipo_delivery = new List<bool>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string pega_dados_delivery_comando = $"SELECT delivery FROM pedidos WHERE usuario = '{usuario_logado}';";
                MySqlCommand pega_dados_delivery = new MySqlCommand(pega_dados_delivery_comando, conexao);
                try
                {
                    using (var reader = pega_dados_delivery.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista_tipo_delivery.Add(Convert.ToBoolean(reader["delivery"]));
                        }
                    }
                }
                catch (Exception er)
                {
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
                return lista_tipo_delivery;
            }
        }
    }
}