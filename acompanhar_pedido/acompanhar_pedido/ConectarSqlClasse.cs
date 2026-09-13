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
        //insere produto na tabela de produtos (cadastro novo) ou altera um existente (nomeOriginal preenchido).
        //valorAtacado/qtdMinAtacado vazios ou "0" = produto sem preço de atacado.
        //qtd_inicial só é usado no cadastro de um produto NOVO (gera a 1ª movimentação de ENTRADA); ao editar, o estoque não é tocado aqui.
        public string InsertProduto(string nome, string valor, string caminho, string nomeOriginal, string qtd_inicial, string categoria, string valorCusto, string valorAtacado, string qtdMinAtacado, string estoqueMinimo)
        {
            string retorno = "Ocorreu um erro, nada foi realizado";
            decimal custo = ParseDecimalOuZero(valorCusto);
            int qtdMinAtacadoInt = ParseIntOuZero(qtdMinAtacado);
            int estoqueMinimoInt = ParseIntOuZero(estoqueMinimo);
            decimal atacado = ParseDecimalOuZero(valorAtacado);
            bool temAtacado = qtdMinAtacadoInt > 0 && atacado > 0;
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    if (nomeOriginal != null && nomeOriginal != "")
                    {
                        string comando_atualiza = "UPDATE produtos SET nome=@nome, valor=@valor, categoria=@categoria, valor_custo=@custo, " +
                            "valor_atacado=@atacado, qtd_min_atacado=@qtdMin, estoque_minimo=@estoqueMin WHERE nome=@nomeOriginal AND usuario=@usuario;";
                        MySqlCommand atualiza = new MySqlCommand(comando_atualiza, conexao, transaction);
                        atualiza.Parameters.AddWithValue("@nome", nome.Trim());
                        atualiza.Parameters.AddWithValue("@valor", valor);
                        atualiza.Parameters.AddWithValue("@categoria", categoria);
                        atualiza.Parameters.AddWithValue("@custo", custo);
                        atualiza.Parameters.AddWithValue("@atacado", temAtacado ? (object)atacado : DBNull.Value);
                        atualiza.Parameters.AddWithValue("@qtdMin", temAtacado ? (object)qtdMinAtacadoInt : DBNull.Value);
                        atualiza.Parameters.AddWithValue("@estoqueMin", estoqueMinimoInt);
                        atualiza.Parameters.AddWithValue("@nomeOriginal", nomeOriginal);
                        atualiza.Parameters.AddWithValue("@usuario", usuario_logado);
                        atualiza.ExecuteNonQuery();
                        retorno = $"Produto '{nomeOriginal}' alterado com sucesso!!";
                        transaction.Commit();
                    }
                    else
                    {
                        try
                        {
                            int qtdInicialInt = ParseIntOuZero(qtd_inicial);
                            string comando_insere = "INSERT INTO produtos(nome, valor, valor_custo, valor_atacado, qtd_min_atacado, estoque_minimo, qtd_vendido, qtd_estoque, caminho_foto, usuario, categoria) " +
                                "VALUES(@nome, @valor, @custo, @atacado, @qtdMin, @estoqueMin, 0, @qtdInicial, @caminho, @usuario, @categoria);";
                            MySqlCommand insere = new MySqlCommand(comando_insere, conexao, transaction);
                            insere.Parameters.AddWithValue("@nome", nome.Trim());
                            insere.Parameters.AddWithValue("@valor", valor);
                            insere.Parameters.AddWithValue("@custo", custo);
                            insere.Parameters.AddWithValue("@atacado", temAtacado ? (object)atacado : DBNull.Value);
                            insere.Parameters.AddWithValue("@qtdMin", temAtacado ? (object)qtdMinAtacadoInt : DBNull.Value);
                            insere.Parameters.AddWithValue("@estoqueMin", estoqueMinimoInt);
                            insere.Parameters.AddWithValue("@qtdInicial", qtdInicialInt);
                            insere.Parameters.AddWithValue("@caminho", caminho);
                            insere.Parameters.AddWithValue("@usuario", usuario_logado);
                            insere.Parameters.AddWithValue("@categoria", categoria);
                            insere.ExecuteNonQuery();
                            long novoIdProduto = insere.LastInsertedId;
                            if (qtdInicialInt > 0)
                            {
                                string comando_mov = "INSERT INTO movimentacoes_estoque(id_produto, usuario, tipo, quantidade, valor_unitario, motivo) " +
                                    "VALUES(@id, @usuario, 'ENTRADA', @qtd, @custo, 'Estoque inicial de cadastro');";
                                MySqlCommand mov = new MySqlCommand(comando_mov, conexao, transaction);
                                mov.Parameters.AddWithValue("@id", novoIdProduto);
                                mov.Parameters.AddWithValue("@usuario", usuario_logado);
                                mov.Parameters.AddWithValue("@qtd", qtdInicialInt);
                                mov.Parameters.AddWithValue("@custo", custo);
                                mov.ExecuteNonQuery();
                            }
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
        //Converte para decimal um valor monetário vindo do banco ou da tela, aceitando OS DOIS formatos que circulam no sistema:
        //  - "8,00"    -> colunas DECIMAL (valor_custo, valor_atacado...) lidas com a cultura local (pt-BR)
        //  - "10.00"   -> a coluna `valor` de produtos, que é varchar e é gravada com ponto
        //Usar InvariantCulture direto em "8,00" faria a vírgula ser lida como separador de milhar e o valor virar 800.
        public static decimal ValorDoBanco(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) { return 0; }
            string texto = valor.Trim();
            if (texto.Contains(","))
            {
                //formato local: ponto é separador de milhar e a vírgula é o decimal
                texto = texto.Replace(".", "").Replace(',', '.');
            }
            if (decimal.TryParse(texto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal resultado)) { return resultado; }
            return 0;
        }
        //helpers de parse tolerantes a string vazia/invalida, usados nos novos campos opcionais de custo/atacado
        private static decimal ParseDecimalOuZero(string valor)
        {
            return ValorDoBanco(valor);
        }
        private static int ParseIntOuZero(string valor)
        {
            if (int.TryParse((valor ?? "").Split('.')[0], out int resultado)) { return resultado; }
            return 0;
        }
        //gera uma lista com a fila dos pedidos ja feitos para aparecerem na cozinha
        public List<Dictionary<string, string>> FilaPedidos()
        {
            List<Dictionary<string, string>> filaPedidos = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando_sql = $"SELECT *, {SQL_ITENS_TEXTO} AS itens_texto FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = false;";
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
                            { "produtos_nome", Convert.ToString(reader["itens_texto"]) },
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
                string str_comando = $"SELECT *, {SQL_ITENS_TEXTO} AS itens_texto FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = true ORDER BY hora_ficou_pronto {ordem} ;";
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
                                    { "produtos_nome", Convert.ToString(reader["itens_texto"]) },
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
        //monta o texto "2X produto,1X outro" a partir da tabela pedido_itens, para as telas que exibem os itens do pedido em uma linha só.
        //Mantém o mesmo formato que antes ficava gravado em pedidos.produtos_nome, então as telas não precisaram mudar.
        private const string SQL_ITENS_TEXTO =
            "IFNULL((SELECT GROUP_CONCAT(CONCAT(pi.quantidade,'X ',pi.nome_produto) ORDER BY pi.id_item SEPARATOR ',') " +
            "FROM pedido_itens pi WHERE pi.id_pedido = pedidos.numero_pedido), '')";

        //realiza o cadastro de um pedido novo. itensVenda: lista de dicionarios com "id_produto","quantidade","valor_unitario" (preço realmente
        //cobrado, varejo ou atacado) e "atacado" ("1" quando o preço de atacado foi aplicado) por item. Tudo roda em uma unica transacao:
        //valida estoque disponivel de cada item (com FOR UPDATE pra evitar corrida entre estacoes), so entao grava o pedido e seus itens na
        //tabela pedido_itens, debita o estoque, registra a movimentacao de VENDA e (se pago) lanca a entrada no caixa.
        //Se faltar estoque de algum item, nada e gravado e a mensagem de erro e retornada.
        public string CadPedido(string nome_cliente,string endereco, string obs, string valor,string formaPag,bool delivery,bool pagamento_efetuado, bool produto_entregue, List<Dictionary<string, string>> itensVenda, string desconto)
        {
            //quantidade total pedida por produto (se o mesmo produto vier em mais de uma linha, soma tudo antes de validar)
            Dictionary<string, int> requeridoPorProduto = new Dictionary<string, int>();
            foreach (Dictionary<string, string> item in itensVenda)
            {
                int q = Convert.ToInt32(item["quantidade"]);
                if (requeridoPorProduto.ContainsKey(item["id_produto"])) { requeridoPorProduto[item["id_produto"]] += q; }
                else { requeridoPorProduto[item["id_produto"]] = q; }
            }
            if (requeridoPorProduto.Count == 0) { return "Nenhum item no pedido."; }

            //tenta até 2 vezes: se der deadlock/lock timeout entre duas máquinas cadastrando ao mesmo tempo, refaz a transação
            for (int tentativa = 1; tentativa <= 2; tentativa++)
            {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    //trava (FOR UPDATE) TODAS as linhas de produto do pedido de uma vez e SEMPRE na mesma ordem (id_produto):
                    //assim duas máquinas cadastrando ao mesmo tempo travam na mesma sequência e não formam deadlock. A segunda
                    //máquina fica esperando a primeira commitar/desfazer e só então lê o estoque já atualizado.
                    List<string> ids = new List<string>(requeridoPorProduto.Keys);
                    List<string> placeholders = new List<string>();
                    MySqlCommand trava = new MySqlCommand();
                    trava.Connection = conexao;
                    trava.Transaction = transaction;
                    trava.Parameters.AddWithValue("@usuario", usuario_logado);
                    for (int i = 0; i < ids.Count; i++)
                    {
                        placeholders.Add("@p" + i);
                        trava.Parameters.AddWithValue("@p" + i, ids[i]);
                    }
                    trava.CommandText = $"SELECT id_produto, nome, qtd_estoque FROM produtos WHERE usuario=@usuario AND id_produto IN ({string.Join(",", placeholders)}) ORDER BY id_produto FOR UPDATE;";

                    Dictionary<string, int> estoquePorProduto = new Dictionary<string, int>();
                    Dictionary<string, string> nomePorProduto = new Dictionary<string, string>();
                    using (var reader = trava.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string idp = reader["id_produto"].ToString();
                            estoquePorProduto[idp] = Convert.ToInt32(reader["qtd_estoque"]);
                            nomePorProduto[idp] = reader["nome"].ToString();
                        }
                    }

                    //valida item por item; junta todas as faltas numa mensagem só
                    List<string> faltas = new List<string>();
                    foreach (KeyValuePair<string, int> req in requeridoPorProduto)
                    {
                        if (!estoquePorProduto.ContainsKey(req.Key))
                        {
                            faltas.Add($"produto não encontrado (id {req.Key})");
                            continue;
                        }
                        if (estoquePorProduto[req.Key] < req.Value)
                        {
                            faltas.Add($"'{nomePorProduto[req.Key]}': pedido {req.Value}, disponível {estoquePorProduto[req.Key]}");
                        }
                    }
                    if (faltas.Count > 0)
                    {
                        transaction.Rollback();
                        return "Estoque insuficiente. Nenhuma venda foi registrada:\n- " + string.Join("\n- ", faltas);
                    }
                    //guarda o nome do produto no momento da venda, gravado depois em pedido_itens
                    foreach (Dictionary<string, string> item in itensVenda)
                    {
                        item["nome_produto"] = nomePorProduto.ContainsKey(item["id_produto"]) ? nomePorProduto[item["id_produto"]] : "";
                    }
                    //grava o pedido (os itens vão para a tabela pedido_itens logo abaixo)
                    string str_cadPedido_sql = "INSERT INTO pedidos(nome_cliente, endereco, observacoes, valor_total, desconto, forma_pag, pagamento_aprovado, usuario, delivery, pedido_pronto) " +
                        "VALUES(@nome_cliente, @endereco, @obs, @valor, @desconto, @formaPag, @pagamento_efetuado, @usuario, @delivery, @produto_entregue);";
                    MySqlCommand cadPedido = new MySqlCommand(str_cadPedido_sql, conexao, transaction);
                    cadPedido.Parameters.AddWithValue("@nome_cliente", nome_cliente);
                    cadPedido.Parameters.AddWithValue("@endereco", endereco);
                    cadPedido.Parameters.AddWithValue("@obs", obs);
                    cadPedido.Parameters.AddWithValue("@valor", valor);
                    cadPedido.Parameters.AddWithValue("@desconto", ParseDecimalOuZero(desconto));
                    cadPedido.Parameters.AddWithValue("@formaPag", formaPag);
                    cadPedido.Parameters.AddWithValue("@pagamento_efetuado", pagamento_efetuado);
                    cadPedido.Parameters.AddWithValue("@usuario", usuario_logado);
                    cadPedido.Parameters.AddWithValue("@delivery", delivery);
                    cadPedido.Parameters.AddWithValue("@produto_entregue", produto_entregue);
                    cadPedido.ExecuteNonQuery();
                    long novonumero_gerado = cadPedido.LastInsertedId; // pega o numero gerado de senha do cadastro do pedido

                    //grava um registro por item vendido (estruturado, com o preço realmente praticado em cada linha)
                    foreach (Dictionary<string, string> item in itensVenda)
                    {
                        int qtdItem = Convert.ToInt32(item["quantidade"]);
                        decimal valorUnitItem = ParseDecimalOuZero(item.ContainsKey("valor_unitario") ? item["valor_unitario"] : "0");
                        string comboDoItem = item.ContainsKey("id_combo") ? item["id_combo"] : "";
                        string comando_item = "INSERT INTO pedido_itens(id_pedido, id_produto, usuario, nome_produto, quantidade, valor_unitario, valor_total, atacado_aplicado, id_combo) " +
                            "VALUES(@pedido, @produto, @usuario, @nome, @qtd, @unitario, @total, @atacado, @combo);";
                        MySqlCommand cadItem = new MySqlCommand(comando_item, conexao, transaction);
                        cadItem.Parameters.AddWithValue("@pedido", novonumero_gerado);
                        cadItem.Parameters.AddWithValue("@produto", item["id_produto"]);
                        cadItem.Parameters.AddWithValue("@usuario", usuario_logado);
                        cadItem.Parameters.AddWithValue("@nome", item.ContainsKey("nome_produto") ? item["nome_produto"] : "");
                        cadItem.Parameters.AddWithValue("@qtd", qtdItem);
                        cadItem.Parameters.AddWithValue("@unitario", valorUnitItem);
                        cadItem.Parameters.AddWithValue("@total", valorUnitItem * qtdItem);
                        cadItem.Parameters.AddWithValue("@atacado", item.ContainsKey("atacado") && item["atacado"] == "1" ? 1 : 0);
                        cadItem.Parameters.AddWithValue("@combo", string.IsNullOrEmpty(comboDoItem) ? (object)DBNull.Value : comboDoItem);
                        cadItem.ExecuteNonQuery();
                    }

                    //debita o estoque, soma no historico de vendido e registra a movimentacao de cada item
                    foreach (Dictionary<string, string> item in itensVenda)
                    {
                        //o "AND qtd_estoque >= @qtd" é uma trava a mais: depois do FOR UPDATE isso já é garantido,
                        //mas se por algum motivo não debitar nenhuma linha, aborta tudo em vez de deixar estoque negativo
                        string comando_debita = "UPDATE produtos SET qtd_estoque = qtd_estoque - @qtd, qtd_vendido = qtd_vendido + @qtd, " +
                            "ativo = CASE WHEN (qtd_estoque - @qtd) > 0 THEN 1 ELSE 0 END WHERE id_produto=@id AND usuario=@usuario AND qtd_estoque >= @qtd;";
                        MySqlCommand debita = new MySqlCommand(comando_debita, conexao, transaction);
                        debita.Parameters.AddWithValue("@qtd", Convert.ToInt32(item["quantidade"]));
                        debita.Parameters.AddWithValue("@id", item["id_produto"]);
                        debita.Parameters.AddWithValue("@usuario", usuario_logado);
                        if (debita.ExecuteNonQuery() == 0)
                        {
                            throw new Exception($"Falha ao debitar estoque do produto {item["id_produto"]} (concorrência).");
                        }

                        string comando_mov = "INSERT INTO movimentacoes_estoque(id_produto, usuario, tipo, quantidade, valor_unitario, documento) " +
                            "VALUES(@id, @usuario, 'VENDA', @qtd, @valorUnit, @doc);";
                        MySqlCommand mov = new MySqlCommand(comando_mov, conexao, transaction);
                        mov.Parameters.AddWithValue("@id", item["id_produto"]);
                        mov.Parameters.AddWithValue("@usuario", usuario_logado);
                        mov.Parameters.AddWithValue("@qtd", Convert.ToInt32(item["quantidade"]));
                        mov.Parameters.AddWithValue("@valorUnit", ParseDecimalOuZero(item.ContainsKey("valor_unitario") ? item["valor_unitario"] : "0"));
                        mov.Parameters.AddWithValue("@doc", novonumero_gerado.ToString());
                        mov.ExecuteNonQuery();
                    }

                    //se o pagamento ja foi efetuado no ato da venda, lanca a entrada correspondente no caixa
                    if (pagamento_efetuado)
                    {
                        string comando_caixa = "INSERT INTO caixa_movimentos(usuario, tipo, categoria, valor, forma_pagamento, documento) " +
                            "VALUES(@usuario, 'ENTRADA', 'Venda', @valor, @formaPag, @doc);";
                        MySqlCommand caixa = new MySqlCommand(comando_caixa, conexao, transaction);
                        caixa.Parameters.AddWithValue("@usuario", usuario_logado);
                        caixa.Parameters.AddWithValue("@valor", valor);
                        caixa.Parameters.AddWithValue("@formaPag", formaPag);
                        caixa.Parameters.AddWithValue("@doc", novonumero_gerado.ToString());
                        caixa.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return $"*********************************\nNumero do pedido/Senha: {novonumero_gerado}\n*********************************";
                }
                catch (MySqlException me) when ((me.Number == 1213 || me.Number == 1205) && tentativa < 2)
                {
                    //1213 = deadlock, 1205 = lock wait timeout: concorrência entre máquinas. Desfaz e tenta de novo.
                    try { transaction.Rollback(); } catch { }
                    EnviaLog(me.GetType().ToString(), me.StackTrace != null ? me.StackTrace.ToString() : "", me.Message);
                }
                catch (Exception er)
                {
                    try { transaction.Rollback(); } catch { }
                    EnviaLog(er.GetType().ToString(), er.StackTrace != null ? er.StackTrace.ToString() : "", er.Message);
                    return "Erro ao cadastrar pedido";
                }
            }
            }
            return "Não foi possível cadastrar o pedido por concorrência entre máquinas. Tente novamente.";
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
                    string limpaProdutosComand = $"Delete FROM produtos WHERE usuario = '{usuario_logado}';";
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
                    string limpaVendasComand = $"Delete FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = true; UPDATE produtos SET qtd_vendido = 0 WHERE usuario = '{usuario_logado}';";
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
                    string limpaPedidosComand = $"Delete FROM pedidos WHERE usuario = '{usuario_logado}' AND pedido_pronto = false;";
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
        //monta o trecho de filtro por período usado pelos relatórios. Datas vazias = sem filtro (relatório do histórico inteiro).
        //O período é aplicado sobre a hora do pedido; a data final entra por completo (o chamador manda o dia seguinte às 00:00).
        private static string FiltroPeriodo(string inicio, string fim, string apelidoTabela)
        {
            if (string.IsNullOrEmpty(inicio) || string.IsNullOrEmpty(fim)) { return ""; }
            string coluna = string.IsNullOrEmpty(apelidoTabela) ? "hora_pedido" : $"{apelidoTabela}.hora_pedido";
            return $" AND {coluna} >= @inicio AND {coluna} < @fim";
        }
        //adiciona os parâmetros do período no comando, quando o filtro estiver ativo
        private static void AplicaParametrosPeriodo(MySqlCommand comando, string inicio, string fim)
        {
            if (string.IsNullOrEmpty(inicio) || string.IsNullOrEmpty(fim)) { return; }
            comando.Parameters.AddWithValue("@inicio", inicio);
            comando.Parameters.AddWithValue("@fim", fim);
        }
        //tira um relatório baseado nas configs da tela janela de relatório. inicio/fim vazios = todo o histórico.
        public DataTable Relatorio(string ordem, string nome, string inicio, string fim)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                //colunas listadas explicitamente e NA MESMA ORDEM da tabela, porque a tela de relatório trabalha por índice de coluna.
                //A antiga coluna produtos_nome dá lugar ao texto dos itens montado a partir de pedido_itens, mantendo a mesma posição.
                var pesquisa = $"SELECT numero_pedido, nome_cliente, endereco, {SQL_ITENS_TEXTO} AS produtos_nome, observacoes, hora_pedido, " +
                    $"hora_ficou_pronto, valor_total, forma_pag, pagamento_aprovado, pedido_pronto, usuario, delivery " +
                    $"FROM pedidos WHERE usuario = '{usuario_logado}'" + FiltroPeriodo(inicio, fim, "");
                switch (ordem)
                {
                    case "az":
                        pesquisa += " ORDER BY nome_cliente ASC";
                        break;
                    case "za":
                        pesquisa += " ORDER BY nome_cliente DESC";
                        break;
                    case "nome":
                        pesquisa += $" AND nome_cliente LIKE '%{nome}%'";
                        break;
                    case "venda":
                        pesquisa += " ORDER BY valor_total ASC";
                        break;
                    default:
                        break;
                }
                pesquisa += ";";
                //etapa de verificação, caso tenha um registro sem produto ele será apagado
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                //apaga caso exista qualquer pedido/registro sem produto
                MySqlCommand pesquisa_verifica_campo_nulo = new MySqlCommand(pesquisa, conexao);
                AplicaParametrosPeriodo(pesquisa_verifica_campo_nulo, inicio, fim);
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
                    AplicaParametrosPeriodo(da.SelectCommand, inicio, fim);
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
        //traz a quantidade vendida de cada produto, calculada a partir dos itens realmente vendidos (pedido_itens).
        //inicio/fim vazios = todo o histórico. Antes isso vinha do contador acumulado produtos.qtd_vendido,
        //que não permitia recortar por data nem enxergar produtos já excluídos do cadastro.
        public List<Dictionary<string, string>> ListaProdVendidos(string inicio, string fim)
        {
            List<Dictionary<string,string>> listaProdutos = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando_sql = "SELECT pi.nome_produto AS nome, SUM(pi.quantidade) AS qtd_vendido " +
                    "FROM pedido_itens pi JOIN pedidos p ON p.numero_pedido = pi.id_pedido " +
                    "WHERE pi.usuario = @usuario" + FiltroPeriodo(inicio, fim, "p") +
                    " GROUP BY pi.nome_produto ORDER BY SUM(pi.quantidade) DESC;";
                MySqlCommand comando = new MySqlCommand(comando_sql, conexao);
                comando.Parameters.AddWithValue("@usuario", usuario_logado);
                AplicaParametrosPeriodo(comando, inicio, fim);
                try
                {
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaProdutos.Add(new Dictionary<string, string>
                            {
                                { "nome", Convert.ToString(reader["nome"]) },
                                { "qtd_vendido", Convert.ToString(reader["qtd_vendido"]) }
                            });
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
        //registra uma movimentação manual de estoque (ENTRADA ou SAIDA), usada pelos botões de estoque da tela de Produtos.
        //bloqueia SAIDA que deixaria o estoque negativo. Em ENTRADA, se um custo unitário for informado, atualiza também produtos.valor_custo.
        public string RegistraMovimentoEstoque(string idProduto, string tipo, string quantidade, string valorCustoUnitario, string fornecedor, string documento, string motivo)
        {
            string resultado = "Ocorreu um erro, nada foi realizado";
            int qtd = ParseIntOuZero(quantidade);
            if (qtd <= 0) { return "Informe uma quantidade maior que zero."; }
            decimal custo = ParseDecimalOuZero(valorCustoUnitario);
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    string comando_trava = "SELECT qtd_estoque FROM produtos WHERE id_produto=@id AND usuario=@usuario FOR UPDATE;";
                    MySqlCommand trava = new MySqlCommand(comando_trava, conexao, transaction);
                    trava.Parameters.AddWithValue("@id", idProduto);
                    trava.Parameters.AddWithValue("@usuario", usuario_logado);
                    int estoqueAtual = -1;
                    using (var reader = trava.ExecuteReader())
                    {
                        if (reader.Read()) { estoqueAtual = Convert.ToInt32(reader["qtd_estoque"]); }
                    }
                    if (estoqueAtual < 0)
                    {
                        transaction.Rollback();
                        return "Produto não encontrado.";
                    }
                    int novoEstoque = tipo == "ENTRADA" ? estoqueAtual + qtd : estoqueAtual - qtd;
                    if (novoEstoque < 0)
                    {
                        transaction.Rollback();
                        return $"Saída não realizada: estoque insuficiente (disponível: {estoqueAtual}).";
                    }
                    string comando_atualiza = tipo == "ENTRADA" && custo > 0
                        ? "UPDATE produtos SET qtd_estoque=@novoEstoque, valor_custo=@custo, ativo = CASE WHEN @novoEstoque > 0 THEN 1 ELSE 0 END WHERE id_produto=@id AND usuario=@usuario;"
                        : "UPDATE produtos SET qtd_estoque=@novoEstoque, ativo = CASE WHEN @novoEstoque > 0 THEN 1 ELSE 0 END WHERE id_produto=@id AND usuario=@usuario;";
                    MySqlCommand atualiza = new MySqlCommand(comando_atualiza, conexao, transaction);
                    atualiza.Parameters.AddWithValue("@novoEstoque", novoEstoque);
                    atualiza.Parameters.AddWithValue("@id", idProduto);
                    atualiza.Parameters.AddWithValue("@usuario", usuario_logado);
                    if (tipo == "ENTRADA" && custo > 0) { atualiza.Parameters.AddWithValue("@custo", custo); }
                    atualiza.ExecuteNonQuery();

                    string comando_mov = "INSERT INTO movimentacoes_estoque(id_produto, usuario, tipo, quantidade, valor_unitario, fornecedor, documento, motivo) " +
                        "VALUES(@id, @usuario, @tipo, @qtd, @custo, @fornecedor, @documento, @motivo);";
                    MySqlCommand mov = new MySqlCommand(comando_mov, conexao, transaction);
                    mov.Parameters.AddWithValue("@id", idProduto);
                    mov.Parameters.AddWithValue("@usuario", usuario_logado);
                    mov.Parameters.AddWithValue("@tipo", tipo);
                    mov.Parameters.AddWithValue("@qtd", qtd);
                    mov.Parameters.AddWithValue("@custo", custo > 0 ? (object)custo : DBNull.Value);
                    mov.Parameters.AddWithValue("@fornecedor", string.IsNullOrEmpty(fornecedor) ? (object)DBNull.Value : fornecedor);
                    mov.Parameters.AddWithValue("@documento", string.IsNullOrEmpty(documento) ? (object)DBNull.Value : documento);
                    mov.Parameters.AddWithValue("@motivo", string.IsNullOrEmpty(motivo) ? (object)DBNull.Value : motivo);
                    mov.ExecuteNonQuery();

                    transaction.Commit();
                    resultado = tipo == "ENTRADA"
                        ? $"Entrada de {qtd} unidade(s) registrada. Estoque atual: {novoEstoque}."
                        : $"Saída de {qtd} unidade(s) registrada. Estoque atual: {novoEstoque}.";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    resultado = $"Erro ao registrar movimentação de estoque \n ERRO: {er}";
                }
            }
            return resultado;
        }
        //traz o histórico de movimentações (entrada/saida/venda/ajuste) de um produto, mais recente primeiro
        public List<Dictionary<string, string>> ListaMovimentacoesProduto(string idProduto)
        {
            List<Dictionary<string, string>> lista = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = "SELECT tipo, quantidade, valor_unitario, fornecedor, documento, motivo, data_hora FROM movimentacoes_estoque " +
                    "WHERE id_produto=@id AND usuario=@usuario ORDER BY data_hora DESC;";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@id", idProduto);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Dictionary<string, string>
                            {
                                { "tipo", reader["tipo"].ToString() },
                                { "quantidade", reader["quantidade"].ToString() },
                                { "valor_unitario", reader["valor_unitario"] == DBNull.Value ? "" : reader["valor_unitario"].ToString() },
                                { "fornecedor", reader["fornecedor"] == DBNull.Value ? "" : reader["fornecedor"].ToString() },
                                { "documento", reader["documento"] == DBNull.Value ? "" : reader["documento"].ToString() },
                                { "motivo", reader["motivo"] == DBNull.Value ? "" : reader["motivo"].ToString() },
                                { "data_hora", reader["data_hora"].ToString() }
                            });
                        }
                    }
                }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            return lista;
        }
        //registra um lançamento manual de caixa (entrada ou saida, incluindo sangria/suprimento) com campos opcionais de detalhe
        public string RegistraMovimentoCaixa(string tipo, string categoria, string valor, string formaPagamento, string descricao, string documento)
        {
            string resultado = "Ocorreu um erro, nada foi realizado";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string comando = "INSERT INTO caixa_movimentos(usuario, tipo, categoria, valor, forma_pagamento, descricao, documento) " +
                    "VALUES(@usuario, @tipo, @categoria, @valor, @formaPagamento, @descricao, @documento);";
                MySqlCommand cmd = new MySqlCommand(comando, conexao, transaction);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                cmd.Parameters.AddWithValue("@tipo", tipo);
                cmd.Parameters.AddWithValue("@categoria", string.IsNullOrEmpty(categoria) ? (object)DBNull.Value : categoria);
                cmd.Parameters.AddWithValue("@valor", ParseDecimalOuZero(valor));
                cmd.Parameters.AddWithValue("@formaPagamento", string.IsNullOrEmpty(formaPagamento) ? (object)DBNull.Value : formaPagamento);
                cmd.Parameters.AddWithValue("@descricao", string.IsNullOrEmpty(descricao) ? (object)DBNull.Value : descricao);
                cmd.Parameters.AddWithValue("@documento", string.IsNullOrEmpty(documento) ? (object)DBNull.Value : documento);
                try
                {
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    resultado = tipo == "ENTRADA" ? "Entrada de caixa registrada com sucesso!" : "Saída de caixa registrada com sucesso!";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    resultado = $"Erro ao registrar movimento de caixa \n ERRO: {er}";
                }
            }
            return resultado;
        }
        //traz os lançamentos de caixa mais recentes do usuario logado
        public List<Dictionary<string, string>> ListaMovimentosCaixa(int limite = 300)
        {
            List<Dictionary<string, string>> lista = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = "SELECT tipo, categoria, valor, forma_pagamento, descricao, documento, data_hora FROM caixa_movimentos " +
                    "WHERE usuario=@usuario ORDER BY data_hora DESC LIMIT @limite;";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                cmd.Parameters.AddWithValue("@limite", limite);
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Dictionary<string, string>
                            {
                                { "tipo", reader["tipo"].ToString() },
                                { "categoria", reader["categoria"] == DBNull.Value ? "" : reader["categoria"].ToString() },
                                { "valor", reader["valor"].ToString() },
                                { "forma_pagamento", reader["forma_pagamento"] == DBNull.Value ? "" : reader["forma_pagamento"].ToString() },
                                { "descricao", reader["descricao"] == DBNull.Value ? "" : reader["descricao"].ToString() },
                                { "documento", reader["documento"] == DBNull.Value ? "" : reader["documento"].ToString() },
                                { "data_hora", reader["data_hora"].ToString() }
                            });
                        }
                    }
                }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            return lista;
        }
        //calcula o saldo atual de caixa (soma de entradas - soma de saidas) do usuario logado
        public string SaldoCaixa()
        {
            string saldo = "0,00";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = "SELECT SUM(CASE WHEN tipo='ENTRADA' THEN valor ELSE -valor END) AS saldo FROM caixa_movimentos WHERE usuario=@usuario;";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try
                {
                    object valor = cmd.ExecuteScalar();
                    if (valor != null && valor != DBNull.Value) { saldo = Convert.ToDecimal(valor).ToString("F2").Replace('.', ','); }
                }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            return saldo;
        }
        //relatório de lucro por produto no período: quantidade e receita vêm dos itens realmente vendidos (pedido_itens,
        //com o preço varejo/atacado/combo praticado em cada linha) e o custo usa o valor_custo ATUAL do produto
        //(aproximação — não guardamos o custo histórico por venda). inicio/fim vazios = todo o histórico.
        //Observação: itens de vendas anteriores à reestruturação não têm preço unitário gravado, então aparecem com
        //quantidade mas sem receita.
        public DataTable RelatorioLucroProdutos(string inicio, string fim)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = @"SELECT
                        pi.nome_produto AS Produto,
                        SUM(pi.quantidade) AS QtdVendida,
                        IFNULL(pr.valor_custo, 0) AS CustoUnitario,
                        (SUM(pi.quantidade) * IFNULL(pr.valor_custo, 0)) AS CustoTotal,
                        IFNULL(SUM(pi.quantidade * pi.valor_unitario), 0) AS ReceitaTotal,
                        (IFNULL(SUM(pi.quantidade * pi.valor_unitario), 0) - (SUM(pi.quantidade) * IFNULL(pr.valor_custo, 0))) AS Lucro,
                        ROUND(
                            (IFNULL(SUM(pi.quantidade * pi.valor_unitario), 0) - (SUM(pi.quantidade) * IFNULL(pr.valor_custo, 0)))
                            / NULLIF(SUM(pi.quantidade * pi.valor_unitario), 0) * 100, 1) AS MargemPorcento
                    FROM pedido_itens pi
                    JOIN pedidos p ON p.numero_pedido = pi.id_pedido
                    LEFT JOIN produtos pr ON pr.id_produto = pi.id_produto
                    WHERE pi.usuario = @usuario" + FiltroPeriodo(inicio, fim, "p") + @"
                    GROUP BY pi.nome_produto, pr.valor_custo
                    ORDER BY ReceitaTotal DESC, QtdVendida DESC;";
                using (MySqlDataAdapter da = new MySqlDataAdapter(comando, conexao))
                {
                    da.SelectCommand.Parameters.AddWithValue("@usuario", usuario_logado);
                    AplicaParametrosPeriodo(da.SelectCommand, inicio, fim);
                    using (DataTable dt = new DataTable())
                    {
                        try { da.Fill(dt); return dt; }
                        catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); return dt; }
                    }
                }
            }
        }
        //RESUMO do dashboard da tela inicial para o período: nº de pedidos, sacos (unidades) vendidos,
        //faturamento BRUTO (soma das vendas antes do desconto = valor_total + desconto) e LÍQUIDO
        //(valor_total já sem desconto, menos a taxa de 1,5% quando a forma de pagamento é débito/crédito),
        //além do produto líder em quantidade. inicio/fim vazios = todo o histórico.
        public Dictionary<string, string> ResumoDashboard(string inicio, string fim)
        {
            Dictionary<string, string> r = new Dictionary<string, string>
            {
                { "pedidos", "0" }, { "sacos", "0" }, { "bruto", "0" }, { "liquido", "0" }, { "produto_lider", "-" }
            };
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string qTot = "SELECT COUNT(*) AS pedidos, " +
                    "IFNULL(SUM(valor_total + desconto),0) AS bruto, " +
                    "IFNULL(SUM(valor_total - CASE WHEN forma_pag LIKE '%deb%' OR forma_pag LIKE '%déb%' " +
                    "OR forma_pag LIKE '%cred%' OR forma_pag LIKE '%créd%' THEN valor_total*0.015 ELSE 0 END),0) AS liquido " +
                    "FROM pedidos WHERE usuario=@usuario" + FiltroPeriodo(inicio, fim, "");
                MySqlCommand cTot = new MySqlCommand(qTot, conexao);
                cTot.Parameters.AddWithValue("@usuario", usuario_logado);
                AplicaParametrosPeriodo(cTot, inicio, fim);
                using (var rd = cTot.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        r["pedidos"] = Convert.ToString(rd["pedidos"]);
                        r["bruto"] = Convert.ToDecimal(rd["bruto"]).ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                        r["liquido"] = Convert.ToDecimal(rd["liquido"]).ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
                    }
                }
                string qItens = "SELECT pi.nome_produto AS nome, SUM(pi.quantidade) AS qtd " +
                    "FROM pedido_itens pi JOIN pedidos p ON p.numero_pedido = pi.id_pedido " +
                    "WHERE pi.usuario=@usuario" + FiltroPeriodo(inicio, fim, "p") +
                    " GROUP BY pi.nome_produto ORDER BY SUM(pi.quantidade) DESC;";
                MySqlCommand cItens = new MySqlCommand(qItens, conexao);
                cItens.Parameters.AddWithValue("@usuario", usuario_logado);
                AplicaParametrosPeriodo(cItens, inicio, fim);
                long sacos = 0; bool primeiro = true;
                using (var rd = cItens.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        sacos += Convert.ToInt64(rd["qtd"]);
                        if (primeiro) { r["produto_lider"] = Convert.ToString(rd["nome"]); primeiro = false; }
                    }
                }
                r["sacos"] = sacos.ToString();
            }
            return r;
        }
        //SAÍDA DE ESTOQUE do dashboard: para cada produto, o estoque atual, o mínimo configurado e quanto
        //saiu (foi vendido) no período. Ordenado do que mais saiu para o que menos saiu.
        public List<Dictionary<string, string>> SaidaEstoqueDashboard(string inicio, string fim)
        {
            List<Dictionary<string, string>> lista = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string q = "SELECT pr.nome, pr.qtd_estoque, pr.estoque_minimo, " +
                    "IFNULL((SELECT SUM(pi.quantidade) FROM pedido_itens pi JOIN pedidos p ON p.numero_pedido = pi.id_pedido " +
                    "        WHERE pi.id_produto = pr.id_produto AND pi.usuario = @usuario" + FiltroPeriodo(inicio, fim, "p") + "),0) AS vendido " +
                    "FROM produtos pr WHERE pr.usuario = @usuario ORDER BY vendido DESC, pr.nome;";
                MySqlCommand c = new MySqlCommand(q, conexao);
                c.Parameters.AddWithValue("@usuario", usuario_logado);
                AplicaParametrosPeriodo(c, inicio, fim);
                using (var rd = c.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        lista.Add(new Dictionary<string, string>
                        {
                            { "nome", Convert.ToString(rd["nome"]) },
                            { "estoque_atual", Convert.ToString(rd["qtd_estoque"]) },
                            { "estoque_minimo", Convert.ToString(rd["estoque_minimo"]) },
                            { "vendido", Convert.ToString(rd["vendido"]) }
                        });
                    }
                }
            }
            return lista;
        }
        //produtos que estão no ou abaixo do estoque mínimo configurado (>0). Alimenta o alerta da tela inicial.
        public List<Dictionary<string, string>> ProdutosEstoqueBaixo()
        {
            List<Dictionary<string, string>> lista = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string q = "SELECT nome, qtd_estoque, estoque_minimo FROM produtos " +
                    "WHERE usuario = @usuario AND estoque_minimo > 0 AND qtd_estoque <= estoque_minimo " +
                    "ORDER BY (qtd_estoque - estoque_minimo), nome;";
                MySqlCommand c = new MySqlCommand(q, conexao);
                c.Parameters.AddWithValue("@usuario", usuario_logado);
                using (var rd = c.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        lista.Add(new Dictionary<string, string>
                        {
                            { "nome", Convert.ToString(rd["nome"]) },
                            { "qtd_estoque", Convert.ToString(rd["qtd_estoque"]) },
                            { "estoque_minimo", Convert.ToString(rd["estoque_minimo"]) }
                        });
                    }
                }
            }
            return lista;
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
        //Total geral do menu: o dinheiro das vendas somado ao que já existe no caixa, SEM contar duas vezes.
        //Como toda venda paga já lança automaticamente uma entrada de "Venda" no caixa, somar "vendas + saldo do caixa"
        //contaria essas vendas duas vezes. Então o cálculo é: saldo atual do caixa (que já traz as vendas pagas,
        //os suprimentos e desconta as sangrias) MAIS as vendas pagas que ainda não possuem lançamento no caixa
        //— caso dos pedidos antigos, anteriores ao módulo de caixa.
        public string TotalGeral()
        {
            decimal total = 0;
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = @"SELECT
                        IFNULL((SELECT SUM(CASE WHEN tipo='ENTRADA' THEN valor ELSE -valor END)
                                FROM caixa_movimentos WHERE usuario=@usuario), 0)
                      + IFNULL((SELECT SUM(p.valor_total) FROM pedidos p
                                WHERE p.usuario=@usuario AND p.pagamento_aprovado = 1
                                  AND NOT EXISTS (SELECT 1 FROM caixa_movimentos c
                                                  WHERE c.usuario = p.usuario
                                                    AND c.categoria = 'Venda'
                                                    AND c.documento = CAST(p.numero_pedido AS CHAR))), 0) AS total;";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try
                {
                    object valor = cmd.ExecuteScalar();
                    if (valor != null && valor != DBNull.Value) { total = Convert.ToDecimal(valor); }
                }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            return total.ToString("F2").Replace('.', ',');
        }
        //traz o valor total que já entrou no caixa, somente dos pedidos ja prontos (mantido para relatórios/consultas pontuais)
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
                            { "disponivel", reader["qtd_estoque"].ToString()},
                            { "caminho_foto", reader["caminho_foto"].ToString() },
                            { "id_produto", reader["id_produto"].ToString() } ,
                            { "categoria", reader["categoria"].ToString() },
                            { "valor_atacado", reader["valor_atacado"] == DBNull.Value ? "" : reader["valor_atacado"].ToString() },
                            { "qtd_min_atacado", reader["qtd_min_atacado"] == DBNull.Value ? "" : reader["qtd_min_atacado"].ToString() }
                        };
                        dadosProd.Add(dicProds);
                    }
                }
            }

            return dadosProd;
        }
        //lê o estoque atual de UM produto direto do banco. Usado pela tela de venda para revalidar a quantidade
        //a cada ação (adicionar item / editar quantidade), assim o número nunca fica velho entre estações.
        public int EstoqueAtualProduto(string idProduto)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT qtd_estoque FROM produtos WHERE id_produto=@id AND usuario=@usuario;", conexao);
                cmd.Parameters.AddWithValue("@id", idProduto);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                object r = cmd.ExecuteScalar();
                if (r == null || r == DBNull.Value) { return 0; }
                return Convert.ToInt32(r);
            }
        }
        //traz TODOS os produtos do usuario (inclusive inativos/sem estoque) com os dados completos para a tela de cadastro/gestão de produtos
        public List<Dictionary<string, string>> ListaProdutosCadastro(string filtro)
        {
            List<Dictionary<string, string>> dadosProd = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = "SELECT * FROM produtos WHERE usuario=@usuario AND nome LIKE @filtro ORDER BY nome;";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                cmd.Parameters.AddWithValue("@filtro", $"%{filtro}%");
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dadosProd.Add(new Dictionary<string, string>
                        {
                            { "id_produto", reader["id_produto"].ToString() },
                            { "nome", reader["nome"].ToString() },
                            { "valor", reader["valor"].ToString() },
                            { "valor_custo", reader["valor_custo"] == DBNull.Value ? "0" : reader["valor_custo"].ToString() },
                            { "valor_atacado", reader["valor_atacado"] == DBNull.Value ? "" : reader["valor_atacado"].ToString() },
                            { "qtd_min_atacado", reader["qtd_min_atacado"] == DBNull.Value ? "" : reader["qtd_min_atacado"].ToString() },
                            { "qtd_estoque", reader["qtd_estoque"].ToString() },
                            { "qtd_vendido", reader["qtd_vendido"].ToString() },
                            { "estoque_minimo", reader["estoque_minimo"] == DBNull.Value ? "0" : reader["estoque_minimo"].ToString() },
                            { "caminho_foto", reader["caminho_foto"].ToString() },
                            { "categoria", reader["categoria"].ToString() },
                            { "ativo", reader["ativo"].ToString() }
                        });
                    }
                }
            }
            return dadosProd;
        }
        //traz os combos (promoções de atacado em conjunto) com seus produtos participantes.
        //Retorna uma linha por produto do combo; combos sem produtos vêm com id_produto vazio.
        //somenteAtivos = true traz só os combos ligados (usado na tela de venda).
        public List<Dictionary<string, string>> ListaCombos(bool somenteAtivos)
        {
            List<Dictionary<string, string>> lista = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = "SELECT c.id_combo, c.nome, c.qtd_min_total, c.ativo, ci.id_produto, p.nome AS nome_produto, ci.valor_unitario " +
                    "FROM combos c " +
                    "LEFT JOIN combo_itens ci ON ci.id_combo = c.id_combo " +
                    "LEFT JOIN produtos p ON p.id_produto = ci.id_produto " +
                    "WHERE c.usuario=@usuario " + (somenteAtivos ? "AND c.ativo = 1 " : "") +
                    "ORDER BY c.nome, p.nome;";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Dictionary<string, string>
                            {
                                { "id_combo", reader["id_combo"].ToString() },
                                { "nome", reader["nome"].ToString() },
                                { "qtd_min_total", reader["qtd_min_total"].ToString() },
                                { "ativo", Convert.ToBoolean(reader["ativo"]) ? "1" : "0" },
                                { "id_produto", reader["id_produto"] == DBNull.Value ? "" : reader["id_produto"].ToString() },
                                { "nome_produto", reader["nome_produto"] == DBNull.Value ? "" : reader["nome_produto"].ToString() },
                                { "valor_unitario", reader["valor_unitario"] == DBNull.Value ? "" : reader["valor_unitario"].ToString() }
                            });
                        }
                    }
                }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            return lista;
        }
        //cria ou atualiza um combo junto com seus produtos participantes (idCombo vazio = combo novo).
        //itens: lista de dicionários com "id_produto" e "valor_unitario". Tudo numa transação só.
        public string SalvarCombo(string idCombo, string nome, string qtdMinTotal, bool ativo, List<Dictionary<string, string>> itens)
        {
            if (string.IsNullOrWhiteSpace(nome)) { return "Dê um nome para o combo."; }
            int qtdMin = ParseIntOuZero(qtdMinTotal);
            if (qtdMin <= 0) { return "Informe a quantidade mínima total do combo (maior que zero)."; }
            if (itens == null || itens.Count < 2) { return "Selecione pelo menos 2 produtos para o combo."; }
            string resultado = "Ocorreu um erro, nada foi realizado";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    long id;
                    if (string.IsNullOrEmpty(idCombo))
                    {
                        MySqlCommand insere = new MySqlCommand("INSERT INTO combos(nome, qtd_min_total, ativo, usuario) VALUES(@nome,@qtd,@ativo,@usuario);", conexao, transaction);
                        insere.Parameters.AddWithValue("@nome", nome.Trim());
                        insere.Parameters.AddWithValue("@qtd", qtdMin);
                        insere.Parameters.AddWithValue("@ativo", ativo ? 1 : 0);
                        insere.Parameters.AddWithValue("@usuario", usuario_logado);
                        insere.ExecuteNonQuery();
                        id = insere.LastInsertedId;
                        resultado = $"Combo '{nome.Trim()}' criado com sucesso!";
                    }
                    else
                    {
                        id = Convert.ToInt64(idCombo);
                        MySqlCommand atualiza = new MySqlCommand("UPDATE combos SET nome=@nome, qtd_min_total=@qtd, ativo=@ativo WHERE id_combo=@id AND usuario=@usuario;", conexao, transaction);
                        atualiza.Parameters.AddWithValue("@nome", nome.Trim());
                        atualiza.Parameters.AddWithValue("@qtd", qtdMin);
                        atualiza.Parameters.AddWithValue("@ativo", ativo ? 1 : 0);
                        atualiza.Parameters.AddWithValue("@id", id);
                        atualiza.Parameters.AddWithValue("@usuario", usuario_logado);
                        atualiza.ExecuteNonQuery();
                        //regrava a lista de participantes do zero
                        MySqlCommand limpa = new MySqlCommand("DELETE FROM combo_itens WHERE id_combo=@id;", conexao, transaction);
                        limpa.Parameters.AddWithValue("@id", id);
                        limpa.ExecuteNonQuery();
                        resultado = $"Combo '{nome.Trim()}' alterado com sucesso!";
                    }
                    foreach (Dictionary<string, string> item in itens)
                    {
                        MySqlCommand cadItem = new MySqlCommand("INSERT INTO combo_itens(id_combo, id_produto, valor_unitario) VALUES(@combo,@produto,@valor);", conexao, transaction);
                        cadItem.Parameters.AddWithValue("@combo", id);
                        cadItem.Parameters.AddWithValue("@produto", item["id_produto"]);
                        cadItem.Parameters.AddWithValue("@valor", ParseDecimalOuZero(item["valor_unitario"]));
                        cadItem.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    resultado = $"Erro ao salvar o combo \n ERRO: {er.Message}";
                }
            }
            return resultado;
        }
        //apaga um combo (os produtos participantes saem junto, por cascata)
        public string ExcluirCombo(string idCombo)
        {
            string resultado = "Não foi possível excluir o combo";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                MySqlCommand cmd = new MySqlCommand("DELETE FROM combos WHERE id_combo=@id AND usuario=@usuario;", conexao, transaction);
                cmd.Parameters.AddWithValue("@id", idCombo);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try
                {
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    resultado = "Combo excluído com sucesso!";
                }
                catch (Exception er)
                {
                    transaction.Rollback();
                    EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                }
            }
            return resultado;
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
                string pegaCadPed_comando = $"SELECT *, {SQL_ITENS_TEXTO} AS itens_texto FROM pedidos WHERE usuario = '{usuario_logado}' ORDER BY numero_pedido DESC;";
                if (somente_pagamento_pendente) { pegaCadPed_comando = $"SELECT *, {SQL_ITENS_TEXTO} AS itens_texto FROM pedidos WHERE usuario = '{usuario_logado}' AND pagamento_aprovado = false ORDER BY numero_pedido DESC;"; }
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
                                    { "produtos_nome", reader["itens_texto"].ToString() },
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
        //lê as taxas (%) de débito/crédito fixadas para o usuário logado na tela de Relatórios.
        //string vazia = nunca foi configurada (a tela usa o padrão de 1,5% nesse caso).
        public Dictionary<string, string> PegaTaxasCartao()
        {
            Dictionary<string, string> resultado = new Dictionary<string, string> { { "taxa_debito", "" }, { "taxa_credito", "" } };
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT taxa_debito, taxa_credito FROM usuarios WHERE senha = @usuario;", conexao);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader["taxa_debito"] != DBNull.Value) { resultado["taxa_debito"] = Convert.ToDecimal(reader["taxa_debito"]).ToString(System.Globalization.CultureInfo.InvariantCulture); }
                            if (reader["taxa_credito"] != DBNull.Value) { resultado["taxa_credito"] = Convert.ToDecimal(reader["taxa_credito"]).ToString(System.Globalization.CultureInfo.InvariantCulture); }
                        }
                    }
                }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            return resultado;
        }
        //fixa a taxa (%) de débito ou crédito do usuário logado; fica valendo (mesmo em outra sessão/estação)
        //até o usuário digitar outra na tela de Relatórios. coluna: "taxa_debito" ou "taxa_credito" (nunca vem de
        //entrada do usuário — é sempre um literal fixo passado pelo código, por isso pode entrar direto no SQL).
        public void SalvarTaxaCartao(string coluna, decimal valorPercentual)
        {
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlCommand cmd = new MySqlCommand($"UPDATE usuarios SET {coluna} = @valor WHERE senha = @usuario;", conexao);
                cmd.Parameters.AddWithValue("@valor", valorPercentual);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try { cmd.ExecuteNonQuery(); }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
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
        //Aprova o pagamento (atualiza a coluna de pagamento para true no pedido determinado) e lança a entrada correspondente no caixa
        public void AprovaPagamento(string numero_pedido)
        {
            string resultado = "Não foi possivel aprovar o pagamento do pedido";
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                MySqlTransaction transaction = conexao.BeginTransaction(IsolationLevel.Serializable);
                string aprova_pagamento_comando = "UPDATE pedidos SET pagamento_aprovado = True WHERE usuario=@usuario AND numero_pedido=@numero;";
                MySqlCommand aprova_pagamento = new MySqlCommand(aprova_pagamento_comando, conexao, transaction);
                aprova_pagamento.Parameters.AddWithValue("@usuario", usuario_logado);
                aprova_pagamento.Parameters.AddWithValue("@numero", numero_pedido);
                try
                {
                    aprova_pagamento.ExecuteNonQuery();
                    string comando_valores = "SELECT valor_total, forma_pag FROM pedidos WHERE usuario=@usuario AND numero_pedido=@numero;";
                    MySqlCommand pega_valores = new MySqlCommand(comando_valores, conexao, transaction);
                    pega_valores.Parameters.AddWithValue("@usuario", usuario_logado);
                    pega_valores.Parameters.AddWithValue("@numero", numero_pedido);
                    string valorTotal = "0"; string formaPag = "";
                    using (var reader = pega_valores.ExecuteReader())
                    {
                        if (reader.Read()) { valorTotal = reader["valor_total"].ToString(); formaPag = reader["forma_pag"].ToString(); }
                    }
                    string comando_caixa = "INSERT INTO caixa_movimentos(usuario, tipo, categoria, valor, forma_pagamento, documento) " +
                        "VALUES(@usuario, 'ENTRADA', 'Venda', @valor, @formaPag, @doc);";
                    MySqlCommand caixa = new MySqlCommand(comando_caixa, conexao, transaction);
                    caixa.Parameters.AddWithValue("@usuario", usuario_logado);
                    caixa.Parameters.AddWithValue("@valor", valorTotal);
                    caixa.Parameters.AddWithValue("@formaPag", formaPag);
                    caixa.Parameters.AddWithValue("@doc", numero_pedido);
                    caixa.ExecuteNonQuery();
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
        //traz os itens de um pedido (um registro por produto vendido, com o preço praticado)
        public List<Dictionary<string, string>> ItensDoPedido(string numero_pedido)
        {
            List<Dictionary<string, string>> itens = new List<Dictionary<string, string>>();
            using (MySqlConnection conexao = new MySqlConnection($"server={res["server"]};uid={res["uid"]};pwd={res["pwd"]};database={res["database"]}"))
            {
                conexao.Open();
                string comando = "SELECT id_produto, nome_produto, quantidade, valor_unitario, valor_total, atacado_aplicado, id_combo " +
                    "FROM pedido_itens WHERE id_pedido=@pedido AND usuario=@usuario ORDER BY id_item;";
                MySqlCommand cmd = new MySqlCommand(comando, conexao);
                cmd.Parameters.AddWithValue("@pedido", numero_pedido);
                cmd.Parameters.AddWithValue("@usuario", usuario_logado);
                try
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itens.Add(new Dictionary<string, string>
                            {
                                { "id_produto", reader["id_produto"] == DBNull.Value ? "" : reader["id_produto"].ToString() },
                                { "nome_produto", reader["nome_produto"].ToString() },
                                { "quantidade", reader["quantidade"].ToString() },
                                { "valor_unitario", reader["valor_unitario"] == DBNull.Value ? "" : reader["valor_unitario"].ToString() },
                                { "valor_total", reader["valor_total"] == DBNull.Value ? "" : reader["valor_total"].ToString() },
                                //tinyint(1) volta como bool no conector; normaliza para "1"/"0"
                                { "atacado_aplicado", Convert.ToBoolean(reader["atacado_aplicado"]) ? "1" : "0" },
                                { "id_combo", reader["id_combo"] == DBNull.Value ? "" : reader["id_combo"].ToString() }
                            });
                        }
                    }
                }
                catch (Exception er) { EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
            return itens;
        }
        //imprime o pedido pronto do historico de pedidos prontos
        public string imprimePedido(string numero_pedido)
        {
            //busca os itens antes de abrir o reader do pedido (não dá pra ter dois readers na mesma conexão)
            List<Dictionary<string, string>> itensDoPedido = ItensDoPedido(numero_pedido);
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
                            //os itens agora vêm da tabela pedido_itens (nada de quebrar texto), o que permite mostrar o preço de cada item.
                            //o nome passa pela quebra de 30 caracteres; a linha de quantidade/preço é montada já curta, pra não quebrar no meio do valor.
                            foreach (Dictionary<string, string> item in itensDoPedido)
                            {
                                nf += AddLineBreaksEveryNChars($"\nITEM: {item["nome_produto"]}", 30);
                                if (!string.IsNullOrEmpty(item["valor_unitario"]))
                                {
                                    string unitario = ParseDecimalOuZero(item["valor_unitario"]).ToString("F2").Replace('.', ',');
                                    string totalItem = ParseDecimalOuZero(item["valor_total"]).ToString("F2").Replace('.', ',');
                                    nf += $"\n  {item["quantidade"]} x R${unitario} = R${totalItem}";
                                    if (item["atacado_aplicado"] == "1") { nf += " (atacado)"; }
                                }
                                else
                                {
                                    nf += $"\n  QTD: {item["quantidade"]}";
                                }
                            }
                            string obs_bruto = reader["observacoes"].ToString();
                            nf += $"\nOBS: {AddLineBreaksEveryNChars(obs_bruto, 30)}";
                            nf += $"\n\n ----- {reader["forma_pag"]} ----- ";
                            if (Convert.ToBoolean(reader["pagamento_aprovado"])) { nf += "\nPEDIDO PAGO"; } else { nf += "\nPAGAMENTO PENDENTE"; }
                            nf += $"\n---------------------------------\nVALOR TOTAL: R${Convert.ToDecimal(reader["valor_total"]).ToString("F2").Replace('.', ',')}\n---------------------------------";
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