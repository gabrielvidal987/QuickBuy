import pandas as pd
import sys

from supabase import create_client, Client
from supabase.client import ClientOptions

from datetime import datetime


# bloco exemplo para envair o log
# try:
#     ...
# except Exception as er:
#     EnviaLog(type(er).__name__, traceback.format_exc(), str(er))

class ConectarSqlClasse:
    # STATIC dictionary, ele é atualizado pelo atualzirdicionario() que coloca as info do BD
    schema = "public"
    url_api = "https://wajyykvigziquxydmmfh.supabase.co" # project quick
    key_api = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Indhanl5a3ZpZ3ppcXV4eWRtbWZoIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDg4NTYyODgsImV4cCI6MjA2NDQzMjI4OH0.ub0r8kpc1t758XAgKh_cFrrPb_2Khk83B5JiIKKgh-g" # project quick

    # STATIC string, ela possui o nome do usuario armazenado para acessar ao longo da classe toda
    usuario_logado: str = ""

    # Conecta no banco e retorna a conexão
    @staticmethod
    def connect_db():
        '''FUNÇÃO QUE CONECTA NO BANCO COLOCANDO A CONEXÃO NA VARIAVEL conn'''
        try:
            conn = create_client(
                ConectarSqlClasse.url_api, 
                ConectarSqlClasse.key_api,
                options=ClientOptions(
                    postgrest_client_timeout=10,
                    storage_client_timeout=10,
                    schema=ConectarSqlClasse.schema,
                )
            )
            return conn
        
        except Exception as er:
            print(f'Erro na conexão: {er}')
            return None


    # atualiza a variavel do nome de usuario logado, essa variavel é para diferenciar o usuario no BD
    @staticmethod
    def AtualizarUsuario(user: str):
        ConectarSqlClasse.usuario_logado = user


    @staticmethod
    def AddLineBreaksEveryNChars(input_str: str, interval: int) -> str:
        if not input_str or interval <= 0:
            return input_str

        return '\n'.join(input_str[i:i+interval] for i in range(0, len(input_str), interval))


    @staticmethod
    def EnviaLog(tipo: str, origem: str, erro: str) -> bool:
        hora = datetime.now()
        try:
            conn = ConectarSqlClasse.connect_db()

            dict_data = {"usuario": ConectarSqlClasse.usuario_logado, "hora": hora, "origem": origem, "erro": erro.replace("'", "-")}
            response = (
                conn.table("log_erro")
                .insert(dict_data,)
                .execute()
            )

        except:
            ###FAZER COMUNICACAO MESSAGEBOX
            pass

        del conn
        return True


    # realiza um teste de conexão ao banco de dados com as config da tela de login, realiza o teste na tela de login
    def ConectDataBase(self) -> str:
        try:
            conn = self.connect_db()
            
            if not conn:
                return f"Erro ao se conectar com o banco de dados \n Verifique os dados informados... ERRO: {erro}"
                
            return "Conexão com o banco de dados realizada com sucesso!!!"
        except Exception as erro:
            return f"Erro ao se conectar com o banco de dados \n Verifique os dados informados... ERRO: {erro}"


    def InsertProduto(self, nome, valor, caminho, nomeOriginal, qtd_inicial, qtd_vendida, categoria) -> str:
        retorno = "Ocorreu um erro, nada foi realizado"
        conn = ConectarSqlClasse.connect_db()

        try:
            if nomeOriginal:

                dict_data = {
                    "nome": nome.strip(),
                    "valor": valor,
                    "qtd_vendido": qtd_vendida,
                    "qtd_estoque": qtd_inicial,
                    "categoria": categoria,
                }
                
                response = (
                    conn.table("produtos")
                    .update(dict_data)
                    .eq("nome", nomeOriginal) #.eq é o WHERE
                    .eq("usuario", ConectarSqlClasse.usuario_logado) #.eq é o WHERE
                    .execute()
                )
                
                retorno = f"Produto '{nomeOriginal}' alterado com sucesso!!"
            else:
                try:
                    dict_data = {
                        "nome": nome.strip(),
                        "valor": valor,
                        "qtd_vendido": qtd_vendida,
                        "qtd_estoque": qtd_inicial,
                        "caminho_foto": caminho,
                        "usuario": ConectarSqlClasse.usuario_logado,
                        "categoria": categoria,
                    }
                    response = (
                        conn.table("produtos")
                        .insert(dict_data,)
                        .execute()
                    )
                    retorno = f"Adicionado {nome.strip()} com valor {valor} na tabela de produtos"
                except Exception as er:
                    retorno = f"Ocorreu um erro ao adicionar produto {nome}"
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                    
            
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        del conn
        return retorno


    def FilaPedidos(self):
        try:
            fila_data = []
            conn = ConectarSqlClasse.connect_db()

            response = (
                conn.table("pedidos")
                .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .eq("pedido_pronto", False)
                .order("numero_pedido", desc=True)
                .execute()
            )
            
            fila_data = response.data
        except Exception as er:
            ###FAZER COMUNICACAO MESSAGEBOX
            pass

        del conn
        return fila_data


    def FilaAnt(self, ordem: str):
        filaPedidos = []
        try:
            conn = ConectarSqlClasse.connect_db()

            desc = ordem.lower() == "desc"

            response = (
                conn.table("pedidos")
                .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .order("numero_pedido", desc=desc)
                .execute()
            )
            
            filaPedidos = response.data
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            
        return filaPedidos


    def _RemoveItem_prod(self, id_item):
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("pedido_itens")
                .delete()
                .eq("id_item", id_item)
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .execute()
            )
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        ###FAZER COMUNICACAO MESSAGEBOX

    
    def _CadPedido_prod(self, numero_pedido, id_produto, qtd_utilizada, preco_unitario):
        conn = ConectarSqlClasse.connect_db()

        dict_data = {
            "numero_pedido": numero_pedido,
            "id_produto": id_produto,
            "quantidade": qtd_utilizada,
            "preco_unitario": preco_unitario,
            "usuario": ConectarSqlClasse.usuario_logado
        }
        try:
            response = (
                conn.table("pedido_itens")
                .insert(dict_data,)
                .execute()
            )
            return True
            
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            return False
            
    
    def CadPedido(self, nome_cliente: str, endereco: str, produtos, obs, valor, formaPag, delivery, pagamento_efetuado, produto_entregue, produtos_consumo):
        resultado = "Erro, nada foi realizado"
        conn = ConectarSqlClasse.connect_db()

        novo_numero_gerado = 0

        try:
            
            dict_data = {
                "nome_cliente": nome_cliente,
                "endereco": endereco,
                "observacoes": obs,
                "valor_total": valor,
                "forma_pag": formaPag,
                "pagamento_aprovado": bool(pagamento_efetuado),
                "usuario": ConectarSqlClasse.usuario_logado,
                "delivery": bool(delivery),
                "pedido_pronto": bool(produto_entregue)
            }
            response = (
                conn.table("pedidos")
                .insert(dict_data,)
                .select("numero_pedido") # ← Retorna apenas o campo "id"
                .execute()
            )
            
            novo_numero_gerado = response.data[0]["numero_pedido"]
            
            resultado = "Pedido cadastrado com sucesso!"
            
            list_item_id = []
            
            for prod in produtos:
                sucesso, id_item = self._CadPedido_prod(numero_pedido=novo_numero_gerado, id_produto=prod["id"], qtd_utilizada=prod["qtd"], preco_unitario=prod["preco"])
                if not sucesso:
                    ConectarSqlClasse.EnviaLog("Erro no insert", "ConectarSqlClasse.CadPedido", "Erro ao inserir um dos produtos, cancelando pedido")
                    self.RemovePedido(novo_numero_gerado)
                    for id_item in list_item_id:
                        self._RemoveItem_prod(id_item=id_item)
                        
                    resultado = "Erro ao cadastrar pedido"
                    break
                    
                    
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            resultado = "Erro ao cadastrar pedido"

        if resultado == "Pedido cadastrado com sucesso!":
            resultado = (
                f"*********************************\n"
                f"Numero do pedido/Senha: {novo_numero_gerado}\n"
                f"*********************************"
            )
            for prod in produtos:
                self.AumentaQtdUtilizadaProduto(prod["nome"], prod["qtd"])


        return resultado


    def LimparBD(self, tabelaProdutos, tabelaVendas, tabelaPedidos):
        resultado = "Nada foi realizado!"
        conn = ConectarSqlClasse.connect_db()

        if tabelaProdutos != "*":
            try:
                response = (
                    conn.table(tabelaProdutos)
                    .delete()
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .execute()
                )
                resultado += "Sucesso ao apagar tabela de produtos"
            except Exception as er:
                ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                resultado += "erro ao apagar tabela de produtos"

        if tabelaVendas != "*":
            try:
                response = (
                    conn.table(tabelaPedidos)
                    .delete()
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .eq("pedido_pronto", True)
                    .execute()
                )
                
                response = (
                    conn.table("produtos")
                    .update({"qtd_vendido": 0})
                    .eq("usuario", ConectarSqlClasse.usuario_logado) #.eq é o WHERE
                    .execute()
                )
                
                resultado += "\n\nSucesso ao apagar tabela de vendas\nQuantidade de produtos vendidos foi zerada"
            except Exception as er:
                ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                resultado += "\n\nerro ao apagar tabela de vendas"

        if tabelaPedidos != "*":
            try:
                response = (
                    conn.table(tabelaPedidos)
                    .delete()
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .eq("pedido_pronto", False)
                    .execute()
                )
                
                resultado += "\n\nSucesso ao apagar tabela de pedido"
            except Exception as er:
                ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                resultado += "\n\nerro ao apagar tabela de pedidos"
        
        return resultado


    def Relatorio(self, ordem: str, nome: str):
        conn = ConectarSqlClasse.connect_db()
        
        try:
            
            if ordem == "az":
                response = (
                    conn.table("pedidos")
                    .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .order("nome_cliente", desc=False)
                    .execute()
                )
            
            elif ordem == "za":
                response = (
                    conn.table("pedidos")
                    .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .order("nome_cliente", desc=True)
                    .execute()
                )
            
            elif ordem == "nome":
                response = (
                    conn.table("pedidos")
                    .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .ilike("nome_cliente", f"%{nome}%")
                    .execute()
                )
            
            elif ordem == "venda":
                response = (
                    conn.table("pedidos")
                    .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .order("valor_total", desc=False)
                    .execute()
                )

            # Gera DataFrame (equivalente ao DataTable)
            df = pd.DataFrame(response.data)
                
        except Exception as er:
            ###FAZER COMUNICACAO MESSAGEBOX
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            df = pd.DataFrame()

        return df


    def ListaProdVendidos(self):
        listaProdutos = []
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("produtos")
                .select("*") # count (Optional) The property to use to get the count of rows returned.
                .eq("ativo", True)
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .execute()
            )
            
            for row in response.data:
                try:
                    pedido = {
                        "nome": str(row["nome"]),
                        "qtd_vendido": str(row["qtd_vendido"]),
                    }
                    listaProdutos.append(pedido)
                except Exception as er:
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                    break
                
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        return listaProdutos


    def MarcaPedidoPronto(self, id: int):
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("pedidos")
                .update({"pedido_pronto": True, "hora_ficou_pronto": datetime.now()})
                .eq("numero_pedido", id) #.eq é o WHERE
                .eq("usuario", ConectarSqlClasse.usuario_logado) #.eq é o WHERE
                .execute()
            )
            
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            ###FAZER COMUNICACAO MESSAGEBOX


    def AumentaQtdUtilizadaProduto(self, nome_produto: str, qtd_produto: str):
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("produtos")
                .select("qtd_vendido") # count (Optional) The property to use to get the count of rows returned.
                .eq("nome", nome_produto) #.eq é o WHERE
                .eq("usuario", ConectarSqlClasse.usuario_logado) #.eq é o WHERE
                .execute()
            )
            qtd_atual = response.data[0]["qtd_vendido"]
            qtd_final = qtd_atual + qtd_produto
            response = (
                conn.table("produtos")
                .update({"qtd_vendido": qtd_final})
                .eq("nome", nome_produto) #.eq é o WHERE
                .eq("usuario", ConectarSqlClasse.usuario_logado) #.eq é o WHERE
                .execute()
            )
            
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            
        self.AtivaDesativaProdDisponivel(nome_produto)


    def AtivaDesativaProdDisponivel(self, nome_produto: str):
        conn = ConectarSqlClasse.connect_db()

        try:
            comando = (
                f"UPDATE produtos SET ativo = IF(qtd_vendido = qtd_estoque, false, true) "
                f"WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND nome = '{nome_produto}';"
            )
            response = (
                conn.table("produtos")
                .select("qtd_vendido, qtd_estoque") # count (Optional) The property to use to get the count of rows returned.
                .eq("nome", nome_produto) #.eq é o WHERE
                .eq("usuario", ConectarSqlClasse.usuario_logado) #.eq é o WHERE
                .execute()
            )
            qtd_vendido = response.data[0]["qtd_vendido"]
            qtd_estoque = response.data[0]["qtd_estoque"]
            
            if qtd_vendido == qtd_estoque:
                response = (
                    conn.table("produtos")
                    .update({"ativo": False})
                    .eq("nome", nome_produto) #.eq é o WHERE
                    .eq("usuario", ConectarSqlClasse.usuario_logado) #.eq é o WHERE
                    .execute()
                )
                
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))


    def QtdPagPend(self) -> str:
        qtd = "0"
        conn = ConectarSqlClasse.connect_db()

        response = (
            conn.table("pedidos")
            .select("COUNT(nome_cliente)")
            .eq("usuario", ConectarSqlClasse.usuario_logado)
            .eq("pagamento_aprovado", False)
            .execute()
        )
        qtd = response.data[0]["count"]
        
        return qtd


    def QtdPreparando(self) -> str:
        qtd = "0"
        conn = ConectarSqlClasse.connect_db()

        response = (
            conn.table("pedidos")
            .select("COUNT(nome_cliente)")
            .eq("usuario", ConectarSqlClasse.usuario_logado)
            .eq("pedido_pronto", False)
            .execute()
        )
        qtd = response.data[0]["count"]
            
        return qtd


    def QtdProntos(self) -> str:
        qtd = "0"
        conn = ConectarSqlClasse.connect_db()

        response = (
            conn.table("pedidos")
            .select("COUNT(nome_cliente)")
            .eq("usuario", ConectarSqlClasse.usuario_logado)
            .eq("pedido_pronto", True)
            .execute()
        )
        qtd = response.data[0]["count"]

        return qtd


    def ValorTotal(self) -> str:
        valor_total = "0"
        conn = ConectarSqlClasse.connect_db()


        response = (
            conn.table("pedidos")
            .select("SUM(valor_total)")
            .eq("usuario", ConectarSqlClasse.usuario_logado)
            .eq("pedido_pronto", True)
            .execute()
        )
        
        valor_total = response.data[0]["sum"]
        return valor_total


    def Senhas(self) -> list[tuple[str, str]]:
        senhas = []
        conn = ConectarSqlClasse.connect_db()

        response = (
            conn.table("pedidos")
            .select("numero_pedido, nome_cliente")
            .eq("usuario", ConectarSqlClasse.usuario_logado)
            .eq("pedido_pronto", True)
            .order("hora_ficou_pronto", desc=False)
            .execute()
        )
        for row in response.data:
            senhas.append((row["numero_pedido"], row["nome_cliente"]))
            
        return senhas


    def DadosProd(self, filtro: str) -> list[dict[str, str]]:
        dadosProd = []
        conn = ConectarSqlClasse.connect_db()

        response = (
            conn.table("produtos")
            .select("*")
            .eq("usuario", ConectarSqlClasse.usuario_logado)
            .eq("ativo", True)
            .ilike("nome", f"%{filtro}%")
            .execute()
        )
        
        for row in response.data:
            try:
                disponivel = int(row["qtd_estoque"]) - int(row["qtd_vendido"])
                dadosProd.append({
                    "nome": row["nome"],
                    "valor": str(row["valor"]),
                    "disponivel": str(disponivel),
                    "caminho_foto": row["caminho_foto"],
                    "id_produto": str(row["id_produto"]),
                    "categoria": row["categoria"]
                })
            except Exception as er:
                ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        return dadosProd


    def ListaHorarios(self) -> list[str]:
        horarios = []
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("pedidos")
                .select("*")
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .eq("pedido_pronto", True)
                .execute()
            )
            for row in response.data:
                horarios.append(row["hora_pedido"])
                horarios.append(row["hora_ficou_pronto"])
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        return horarios


    def FilaCadPed(self, somente_pagamento_pendente: bool) -> list[dict[str, str]]:
        filaCadPed = []
        conn = ConectarSqlClasse.connect_db()

        try:
            if somente_pagamento_pendente:
                    
                response = (
                    conn.table("pedidos")
                    .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .eq("pagamento_aprovado", True)
                    .order("numero_pedido", desc=True)
                    .execute()
                )
            else:
                response = (
                    conn.table("pedidos")
                    .select("numero_pedido, nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                    .eq("usuario", ConectarSqlClasse.usuario_logado)
                    .order("numero_pedido", desc=True)
                    .execute()
                )

            for row in response.data:
                filaCadPed.append({
                    "numero_pedido": str(row["numero_pedido"]),
                    "nome_cliente": row["nome_cliente"],
                    "endereco": row["endereco"],
                    "produtos": row["pedido_itens"],
                    "observacoes": row["observacoes"],
                    "hora_pedido": row["hora_pedido"],
                    "valor_total": str(row["valor_total"]),
                    "forma_pag": row["forma_pag"],
                    "delivery": str(row["delivery"]),
                    "pagamento_aprovado": str(row["pagamento_aprovado"])
                })
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        
        return filaCadPed


    def CriaUsuario(self, nome: str, caminho_foto: str) -> None:
        conn = ConectarSqlClasse.connect_db()


        dict_data = {"nome": nome, "senha": nome, "foto": caminho_foto}
        try:
            response = (
                conn.table("usuarios")
                .insert(dict_data,)
                .execute()
            )
        except Exception as er:
            ###FAZER COMUNICACAO MESSAGEBOX
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))


    def Usuario(self, senha: str) -> bool:
        result = False
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("usuarios")
                .select("senha")
                .execute()
            )
            for row in response.data:
                if senha == row["senha"]:
                    result = True
                    break
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        return result


    def FotoClube(self) -> str:
        caminho = ""
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("usuarios")
                .select("foto")
                .eq("senha", self.usuario_logado)
                .execute()
            )
            caminho = response.data[0]["foto"]

        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        
        return caminho


    def RemoveProd(self, nome: str) -> None:
        resultado = "Não foi possível deletar o produto"
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("produtos")
                .delete()
                .eq("nome", nome)
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .execute()
            )
            resultado = "Produto removido com sucesso!"
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        ###FAZER COMUNICACAO MESSAGEBOX


    def RemovePedido(self, numero_pedido: str) -> None:
        resultado = "Não foi possível deletar o pedido"
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("pedidos")
                .delete()
                .eq("numero_pedido", numero_pedido)
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .execute()
            )
            resultado = "Pedido removido com sucesso!"
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        ###FAZER COMUNICACAO MESSAGEBOX


    def AprovaPagamento(self, numero_pedido: str) -> None:
        resultado = "Não foi possível aprovar o pagamento do pedido"
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("pedidos")
                .update({"pagamento_aprovado": True})
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .eq("numero_pedido", numero_pedido)
                .execute()
            )
            resultado = "Pagamento do pedido aprovado com sucesso!"
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        ###FAZER COMUNICACAO MESSAGEBOX


    def imprimePedido(self, numero_pedido: str) -> str:
        nf = ""
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("pedidos")
                .select("nome_cliente, endereco, observacoes, hora_pedido, valor_total, forma_pag, delivery, pagamento_aprovado, pedido_itens (id_produto, quantidade, preco_unitario, subtotal, usuario, produtos (nome))")
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .eq("numero_pedido", numero_pedido)
                .execute()
            )

            for row in response.data:
                nf += f"CLIENTE: {row['nome_cliente']}"
                endereco = self.AddLineBreaksEveryNChars(row["endereco"], 30)
                nf += f"\nENDEREÇO:\n{endereco}\n"

                for prod in row["pedido_itens"]:
                    quantidade = prod["qtd"]
                    nome = prod["nome"]
                    texto_item = f"\nITEM: {nome} QTD: {quantidade}"
                    nf += self.AddLineBreaksEveryNChars(texto_item, 30)

                obs = self.AddLineBreaksEveryNChars(row["observacoes"], 30)
                nf += f"\nOBS: {obs}"
                nf += f"\n\n ----- {row['forma_pag']} ----- "
                nf += "\nPEDIDO PAGO" if bool(row["pagamento_aprovado"]) else "\nPAGAMENTO PENDENTE"
                nf += f"\n---------------------------------\nVALOR TOTAL: R${row['valor_total']}\n---------------------------------"
                nf += "\n\nSAÍDA: ENTREGA" if row["delivery"] else "\n\nSAÍDA: BALCÃO"
                nf += f"\n\n*********************************\nNumero do pedido/Senha: {row['numero_pedido']}\n*********************************"
                nf += f"\nHorario do pedido\n{row['hora_pedido']}"
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        
        return nf


    def imprimeListaProdutos(self) -> str:
        lista_pedidos = f"{self.usuario_logado}\n\n\n"
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("produtos")
                .select("*")
                .eq("ativo", True)
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .execute()
            )
            for row in response.data:
                lista_pedidos += f"{row['nome']} -> R${row['valor']}\n\n"
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        return lista_pedidos


    def pegaListaDelivery(self) -> list[bool]:
        lista_tipo_delivery = []
        conn = ConectarSqlClasse.connect_db()

        try:
            response = (
                conn.table("pedidos")
                .select("delivery")
                .eq("usuario", ConectarSqlClasse.usuario_logado)
                .execute()
            )
            for row in response.data:
                lista_tipo_delivery.append(bool(row["delivery"]))
                
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        return lista_tipo_delivery

