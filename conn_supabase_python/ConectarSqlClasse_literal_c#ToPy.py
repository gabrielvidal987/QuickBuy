import mysql.connector
from mysql.connector import Error
from typing import Dict
import datetime
from tkinter import messagebox
import pandas as pd
import sys


# bloco exemplo
# try:
#     ...
# except Exception as er:
#     EnviaLog(type(er).__name__, traceback.format_exc(), str(er))

class ConectarSqlClasse:
    # STATIC dictionary, ele é atualizado pelo atualzirdicionario() que coloca as info do BD
    res: Dict[str, str] = {}
    # STATIC string, ela possui o nome do usuario armazenado para acessar ao longo da classe toda
    usuario_logado: str = ""

    # atualiza o dict private "res" que contem a conexão com o banco. essa função pega o dicionario de conexão enviado
    # e atualiza a "res" com essa conexão que é utilizada em todas as funções dessa classe
    @staticmethod
    def AtualizarDicionario(dadosConn: Dict[str, str]):
        ConectarSqlClasse.res = dadosConn

    # atualiza a variavel do nome de usuario logado, essa variavel é para diferenciar o usuario no BD
    @staticmethod
    def AtualizarUsuario(user: str):
        ConectarSqlClasse.usuario_logado = user

    # realiza um teste de conexão ao banco de dados com as config da tela de login, realiza o teste na tela de login
    def ConectDataBase(self) -> str:
        try:
            conexao = mysql.connector.connect(
                host=ConectarSqlClasse.res["server"],
                user=ConectarSqlClasse.res["uid"],
                password=ConectarSqlClasse.res["pwd"],
                database=ConectarSqlClasse.res["database"]
            )

            if conexao.is_connected():
                conexao.close()
                return "Conexão com o banco de dados realizada com sucesso!!!"
        except Exception as erro:
            return f"Erro ao se conectar com o banco de dados \n Verifique os dados informados... ERRO: {erro}"

    @staticmethod
    def EnviaLog(tipo: str, origem: str, erro: str) -> bool:
        hora = datetime.datetime.now().strftime("%Y/%m/%d-%H:%M:%S")
        try:
            conexao = mysql.connector.connect(
                host=ConectarSqlClasse.res["server"],
                user=ConectarSqlClasse.res["uid"],
                password=ConectarSqlClasse.res["pwd"],
                database=ConectarSqlClasse.res["database"]
            )
            conexao.start_transaction(isolation_level='SERIALIZABLE')
            cursor = conexao.cursor()

            comando_geraLog = (
                f"INSERT INTO log_erro (usuario,hora,tipo,origem,erro) "
                f"VALUES ('{ConectarSqlClasse.usuario_logado}','{hora}','{tipo}','{origem}','{erro.replace("'", "-")}');"
            )

            try:
                cursor.execute(comando_geraLog)
                conexao.commit()
            except:
                conexao.rollback()
            finally:
                cursor.close()
                conexao.close()

        except:
            acesso_retornado = messagebox.askquestion(
                "Confirmação",
                "Erro ao gerar log e enviar para BD. \n\n"
                "Contactar Administrador e verificar conexão com internet e BD\n\n"
                "O acesso ao Banco de Dados foi reestabelecido?\n"
                "Se for marcado 'NÃO' o programa será fechado"
            )
            if acesso_retornado == 'yes':
                return True
            else:
                sys.exit()  # Equivalente ao Application.Exit()
        return True

    def InsertProduto(self, nome, valor, caminho, nomeOriginal, qtd_inicial, qtd_vendida, categoria) -> str:
        retorno = "Ocorreu um erro, nada foi realizado"
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        try:
            if nomeOriginal is not None and nomeOriginal != "":
                comando_insere = (
                    f"UPDATE produtos SET nome = '{nome.strip()}', valor = {valor}, qtd_vendido = {qtd_vendida}, "
                    f"qtd_estoque = {qtd_inicial}, categoria = '{categoria}' "
                    f"WHERE nome = '{nomeOriginal}' AND usuario = '{ConectarSqlClasse.usuario_logado}';"
                )
                cursor.execute(comando_insere)
                conexao.commit()
                retorno = f"Produto '{nomeOriginal}' alterado com sucesso!!"
            else:
                try:
                    comando_insere = (
                        f"INSERT INTO produtos(nome, valor, qtd_vendido, qtd_estoque, caminho_foto, usuario, categoria) "
                        f"VALUES('{nome.strip()}', {valor}, {qtd_vendida}, {qtd_inicial}, "
                        f"'{caminho}', '{ConectarSqlClasse.usuario_logado}', '{categoria}');"
                    )
                    cursor.execute(comando_insere)
                    conexao.commit()
                    retorno = f"Adicionado {nome.strip()} com valor {valor} na tabela de produtos"
                except Exception as er:
                    conexao.rollback()
                    retorno = f"Ocorreu um erro ao adicionar produto {nome} \n ERRO: {er}"
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        except Exception as er:
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return retorno

    def FilaPedidos(self):
        filaPedidos = []
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        comando_sql = (
            f"SELECT * FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = false;"
        )
        cursor = conexao.cursor(dictionary=True)
        try:
            cursor.execute(comando_sql)
            for row in cursor:
                try:
                    pedido = {
                        "numero_pedido": str(row["numero_pedido"]),
                        "nome_cliente": str(row["nome_cliente"]),
                        "produtos_nome": str(row["produtos_nome"]),
                        "observacoes": str(row["observacoes"]),
                        "hora_pedido": str(row["hora_pedido"]),
                        "delivery": str(row["delivery"]),
                        "pagamento_aprovado": str(row["pagamento_aprovado"]),
                    }
                    filaPedidos.append(pedido)
                except Exception as er:
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                    break
        except Exception as er:
            messagebox.showerror("Erro", f"erro ao gerar lista de fila de pedidos: \n {er}")
        finally:
            cursor.close()
            conexao.close()

        return filaPedidos

    def FilaAnt(self, ordem: str):
        filaPedidos = []
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor(dictionary=True)
        str_comando = (
            f"SELECT * FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' "
            f"AND pedido_pronto = true ORDER BY hora_ficou_pronto {ordem};"
        )
        try:
            cursor.execute(str_comando)
            for row in cursor:
                try:
                    pedido = {
                        "numero_pedido": str(row["numero_pedido"]),
                        "nome_cliente": str(row["nome_cliente"]),
                        "produtos_nome": str(row["produtos_nome"]),
                        "observacoes": str(row["observacoes"]),
                        "hora_pedido": str(row["hora_pedido"]),
                        "hora_ficou_pronto": str(row["hora_ficou_pronto"]),
                        "delivery": str(row["delivery"]),
                        "pagamento_aprovado": str(row["pagamento_aprovado"]),
                    }
                    filaPedidos.append(pedido)
                except Exception as er:
                    from tkinter import messagebox
                    messagebox.showerror("Erro", "valor não encontrado")
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                    break
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return filaPedidos

    def CadPedido(
        self, nome_cliente, endereco, produtos, obs, valor, formaPag,
        delivery, pagamento_efetuado, produto_entregue, produtos_consumo
    ):
        resultado = "Erro, nada foi realizado"
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        novonumero_gerado = 0

        str_cadPedido_sql = (
            f"INSERT INTO pedidos(nome_cliente, endereco, produtos_nome, observacoes, valor_total, forma_pag, "
            f"pagamento_aprovado, usuario, delivery, pedido_pronto) "
            f"VALUES('{nome_cliente}', '{endereco}', '{produtos}', '{obs}', {valor},'{formaPag}',"
            f"{int(pagamento_efetuado)}, '{ConectarSqlClasse.usuario_logado}', {int(delivery)}, {int(produto_entregue)});"
        )

        try:
            cursor.execute(str_cadPedido_sql)
            novonumero_gerado = cursor.lastrowid
            conexao.commit()
            resultado = "Pedido cadastrado com sucesso!"
        except Exception as er:
            conexao.rollback()
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            resultado = "Erro ao cadastrar pedido"
        finally:
            cursor.close()
            conexao.close()

        if resultado == "Pedido cadastrado com sucesso!":
            resultado = (
                f"*********************************\n"
                f"Numero do pedido/Senha: {novonumero_gerado}\n"
                f"*********************************"
            )
            if produtos_consumo:
                for k, v in produtos_consumo.items():
                    self.AumentaQtdUtilizadaProduto(k, v)

        return resultado

    def LimparBD(self, tabelaProdutos, tabelaVendas, tabelaPedidos):
        resultado = ""
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()

        try:
            if tabelaProdutos != "*":
                limpaProdutosComand = f"DELETE FROM {tabelaProdutos} WHERE usuario = '{ConectarSqlClasse.usuario_logado}';"
                try:
                    cursor.execute(limpaProdutosComand)
                    resultado += "Sucesso ao apagar tabela de produtos"
                except Exception as er:
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                    resultado += "erro ao apagar tabela de produtos"

            if tabelaVendas != "*":
                limpaVendasComand = (
                    f"DELETE FROM {tabelaPedidos} WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = true; "
                    f"UPDATE produtos SET qtd_vendido = 0 WHERE usuario = '{ConectarSqlClasse.usuario_logado}';"
                )
                try:
                    for cmd in limpaVendasComand.split(";"):
                        if cmd.strip():
                            cursor.execute(cmd)
                    resultado += "\n\nSucesso ao apagar tabela de vendas\nQuantidade de produtos vendidos foi zerada"
                except Exception as er:
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                    resultado += "\n\nerro ao apagar tabela de vendas"

            if tabelaPedidos != "*":
                limpaPedidosComand = (
                    f"DELETE FROM {tabelaPedidos} WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = false;"
                )
                try:
                    cursor.execute(limpaPedidosComand)
                    resultado += "\n\nSucesso ao apagar tabela de pedido"
                except Exception as er:
                    ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                    resultado += "\n\nerro ao apagar tabela de pedidos"

            conexao.commit()
        except:
            conexao.rollback()
        finally:
            cursor.close()
            conexao.close()

        if resultado == "":
            resultado = "Nada foi realizado!"
        return resultado

    def Relatorio(self, ordem: str, nome: str):
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor(dictionary=True)

        pesquisa = f"SELECT * FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}'"

        if ordem == "az":
            pesquisa += " ORDER BY nome_cliente ASC"
        elif ordem == "za":
            pesquisa += " ORDER BY nome_cliente DESC"
        elif ordem == "nome":
            pesquisa += f" AND nome_cliente LIKE '%{nome}%'"
        elif ordem == "venda":
            pesquisa += " ORDER BY valor_total ASC"

        # Verifica campos nulos
        cursor.execute(pesquisa)
        lista_para_apagar = []
        for row in cursor:
            try:
                numero_pedido = str(row["numero_pedido"])
                produtos_nome = str(row["produtos_nome"])
                if not produtos_nome:
                    lista_para_apagar.append(numero_pedido)
            except Exception as er:
                from tkinter import messagebox
                messagebox.showerror("Erro", "valor não encontrado")
                ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
                break

        # Apaga os registros inválidos
        for numero_apagar in lista_para_apagar:
            try:
                apaga_comando = (
                    f"DELETE FROM prontos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' "
                    f"AND numero_pedido = {numero_apagar};"
                )
                conexao.cursor().execute(apaga_comando)
                conexao.commit()
            except Exception as er:
                conexao.rollback()
                ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))

        # Gera DataFrame (equivalente ao DataTable)
        try:
            df = pd.read_sql(pesquisa, conexao)
        except Exception as er:
            from tkinter import messagebox
            messagebox.showinfo("Erro", "Sem dados")
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            df = pd.DataFrame()

        cursor.close()
        conexao.close()
        return df

    def ListaProdVendidos(self):
        listaProdutos = []
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        cursor = conexao.cursor(dictionary=True)
        comando_sql = (
            f"SELECT * FROM produtos WHERE ativo = true AND usuario = '{ConectarSqlClasse.usuario_logado}';"
        )
        try:
            cursor.execute(comando_sql)
            for row in cursor:
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
        finally:
            cursor.close()
            conexao.close()
        return listaProdutos

    def MarcaPedidoPronto(self, id: int):
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        try:
            comando = (
                f"UPDATE pedidos SET pedido_pronto = true, hora_ficou_pronto = CURRENT_TIMESTAMP "
                f"WHERE numero_pedido = {id} AND usuario = '{ConectarSqlClasse.usuario_logado}';"
            )
            cursor.execute(comando)
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
            from tkinter import messagebox
            messagebox.showwarning("ATENÇÃO", "ERRO AO TENTAR MARCAR PEDIDO COMO PRONTO")
        finally:
            cursor.close()
            conexao.close()

    def AumentaQtdUtilizadaProduto(self, nome_produto: str, qtd_produto: str):
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        try:
            comando = (
                f"UPDATE produtos SET qtd_vendido = qtd_vendido + {qtd_produto} "
                f"WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND nome = '{nome_produto}';"
            )
            cursor.execute(comando)
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        self.AtivaDesativaProdDisponivel(nome_produto)

    def AtivaDesativaProdDisponivel(self, nome_produto: str):
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        try:
            comando = (
                f"UPDATE produtos SET ativo = IF(qtd_vendido = qtd_estoque, false, true) "
                f"WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND nome = '{nome_produto}';"
            )
            cursor.execute(comando)
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()

    def QtdPagPend(self) -> str:
        qtd = ""
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        cursor = conexao.cursor()
        comando = (
            f"SELECT COUNT(nome_cliente) FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pagamento_aprovado = false;"
        )
        cursor.execute(comando)
        for row in cursor:
            qtd = str(row[0])
        cursor.close()
        conexao.close()
        return qtd

    def QtdPreparando(self) -> str:
        qtd = ""
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        cursor = conexao.cursor()
        comando = (
            f"SELECT COUNT(nome_cliente) FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = false;"
        )
        cursor.execute(comando)
        for row in cursor:
            qtd = str(row[0])
        cursor.close()
        conexao.close()
        return qtd

    def QtdProntos(self) -> str:
        qtd = ""
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        cursor = conexao.cursor()
        comando = (
            f"SELECT COUNT(nome_cliente) FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = true;"
        )
        cursor.execute(comando)
        for row in cursor:
            qtd = str(row[0])
        cursor.close()
        conexao.close()
        return qtd

    def ValorTotal(self) -> str:
        valor_total = ""
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        cursor = conexao.cursor()
        comando = (
            f"SELECT SUM(valor_total) FROM pedidos "
            f"WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = true;"
        )
        cursor.execute(comando)
        row = cursor.fetchone()
        if row and row[0] is not None:
            valor_total = str(row[0])
        else:
            valor_total = "0,00"
        cursor.close()
        conexao.close()
        return valor_total

    def Senhas(self) -> list[tuple[str, str]]:
        senhas = []
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        cursor = conexao.cursor()
        comando = (
            f"SELECT numero_pedido, nome_cliente FROM pedidos "
            f"WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = true "
            f"ORDER BY hora_ficou_pronto ASC;"
        )
        cursor.execute(comando)
        for row in cursor:
            senhas.append((str(row[0]), str(row[1])))
        cursor.close()
        conexao.close()
        return senhas

    def DadosProd(self, filtro: str) -> list[dict[str, str]]:
        dadosProd = []
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        cursor = conexao.cursor(dictionary=True)
        comando = (
            f"SELECT * FROM produtos WHERE ativo = true AND usuario = '{ConectarSqlClasse.usuario_logado}' "
            f"AND nome LIKE '%{filtro}%';"
        )
        cursor.execute(comando)
        for row in cursor:
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
        cursor.close()
        conexao.close()
        return dadosProd

    def ListaHorarios(self) -> list[str]:
        horarios = []
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor(dictionary=True)
        try:
            comando = (
                f"SELECT * FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' AND pedido_pronto = true;"
            )
            cursor.execute(comando)
            for row in cursor:
                horarios.append(row["hora_pedido"])
                horarios.append(row["hora_ficou_pronto"])
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return horarios

    def FilaCadPed(self, somente_pagamento_pendente: bool) -> list[dict[str, str]]:
        filaCadPed = []
        conexao = mysql.connector.connect(
            host=ConectarSqlClasse.res["server"],
            user=ConectarSqlClasse.res["uid"],
            password=ConectarSqlClasse.res["pwd"],
            database=ConectarSqlClasse.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor(dictionary=True)
        try:
            if somente_pagamento_pendente:
                comando = (
                    f"SELECT * FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' "
                    f"AND pagamento_aprovado = false ORDER BY numero_pedido DESC;"
                )
            else:
                comando = (
                    f"SELECT * FROM pedidos WHERE usuario = '{ConectarSqlClasse.usuario_logado}' "
                    f"ORDER BY numero_pedido DESC;"
                )
            cursor.execute(comando)
            for row in cursor:
                filaCadPed.append({
                    "numero_pedido": str(row["numero_pedido"]),
                    "nome_cliente": row["nome_cliente"],
                    "endereco": row["endereco"],
                    "produtos_nome": row["produtos_nome"],
                    "observacoes": row["observacoes"],
                    "hora_pedido": row["hora_pedido"],
                    "valor_total": str(row["valor_total"]),
                    "forma_pag": row["forma_pag"],
                    "delivery": str(row["delivery"]),
                    "pagamento_aprovado": str(row["pagamento_aprovado"])
                })
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            ConectarSqlClasse.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return filaCadPed

    def CriaUsuario(self, nome: str, caminho_foto: str) -> None:
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        comando = (
            f"INSERT INTO usuarios (nome, senha, foto) VALUES ('{nome}', '{nome}', '{caminho_foto}');"
        )
        try:
            cursor.execute(comando)
            conexao.commit()
            messagebox.showinfo("Sucesso", "Usuário cadastrado com sucesso!!")
        except Exception as er:
            conexao.rollback()
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()

    def Usuario(self, senha: str) -> bool:
        result = False
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        cursor = conexao.cursor(dictionary=True)
        try:
            cursor.execute("SELECT senha FROM usuarios;")
            for row in cursor:
                if senha == row["senha"]:
                    result = True
                    break
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return result

    def FotoClube(self) -> str:
        caminho = ""
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        cursor = conexao.cursor()
        comando = f"SELECT foto FROM usuarios WHERE senha = '{self.usuario_logado}';"
        try:
            cursor.execute(comando)
            resultado = cursor.fetchone()
            if resultado:
                caminho = resultado[0]
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return caminho

    def RemoveProd(self, nome: str) -> None:
        resultado = "Não foi possível deletar o produto"
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        comando = (
            f"DELETE FROM produtos WHERE ativo = true AND usuario = '{self.usuario_logado}' AND nome = '{nome}';"
        )
        try:
            cursor.execute(comando)
            conexao.commit()
            resultado = "Produto removido com sucesso!"
        except Exception as er:
            conexao.rollback()
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
            messagebox.showinfo("Resultado", resultado)

    def RemovePedido(self, numero_pedido: str) -> None:
        resultado = "Não foi possível deletar o pedido"
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        comando = (
            f"DELETE FROM pedidos WHERE usuario = '{self.usuario_logado}' AND numero_pedido = {numero_pedido};"
        )
        try:
            cursor.execute(comando)
            conexao.commit()
            resultado = "Pedido removido com sucesso!"
        except Exception as er:
            conexao.rollback()
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
            messagebox.showinfo("Resultado", resultado)

    def AprovaPagamento(self, numero_pedido: str) -> None:
        resultado = "Não foi possível aprovar o pagamento do pedido"
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor()
        comando = (
            f"UPDATE pedidos SET pagamento_aprovado = true "
            f"WHERE usuario = '{self.usuario_logado}' AND numero_pedido = {numero_pedido};"
        )
        try:
            cursor.execute(comando)
            conexao.commit()
            resultado = "Pagamento do pedido aprovado com sucesso!"
        except Exception as er:
            conexao.rollback()
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
            messagebox.showinfo("Resultado", resultado)

    def imprimePedido(self, numero_pedido: str) -> str:
        nf = ""
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor(dictionary=True)
        comando = (
            f"SELECT * FROM pedidos WHERE usuario = '{self.usuario_logado}' AND numero_pedido = {numero_pedido};"
        )
        try:
            cursor.execute(comando)
            for row in cursor:
                nf += f"CLIENTE: {row['nome_cliente']}"
                endereco = self.AddLineBreaksEveryNChars(row["endereco"], 30)
                nf += f"\nENDEREÇO:\n{endereco}\n"

                produtos_comprados = row["produtos_nome"].split(",")
                for prod in produtos_comprados:
                    if not prod:
                        break
                    quantidade, nome_produto = prod.split("X")
                    texto_item = f"\nITEM: {nome_produto} QTD: {quantidade}"
                    nf += self.AddLineBreaksEveryNChars(texto_item, 30)

                obs = self.AddLineBreaksEveryNChars(row["observacoes"], 30)
                nf += f"\nOBS: {obs}"
                nf += f"\n\n ----- {row['forma_pag']} ----- "
                nf += "\nPEDIDO PAGO" if row["pagamento_aprovado"] else "\nPAGAMENTO PENDENTE"
                nf += f"\n---------------------------------\nVALOR TOTAL: R${row['valor_total']}\n---------------------------------"
                nf += "\n\nSAÍDA: ENTREGA" if row["delivery"] else "\n\nSAÍDA: BALCÃO"
                nf += f"\n\n*********************************\nNumero do pedido/Senha: {row['numero_pedido']}\n*********************************"
                nf += f"\nHorario do pedido\n{row['hora_pedido']}"
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return nf

    def imprimeListaProdutos(self) -> str:
        lista_pedidos = f"{self.usuario_logado}\n\n\n"
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        conexao.start_transaction(isolation_level='SERIALIZABLE')
        cursor = conexao.cursor(dictionary=True)
        comando = (
            f"SELECT * FROM produtos WHERE ativo = true AND usuario = '{self.usuario_logado}';"
        )
        try:
            cursor.execute(comando)
            for row in cursor:
                lista_pedidos += f"{row['nome']} -> R${row['valor']}\n\n"
            conexao.commit()
        except Exception as er:
            conexao.rollback()
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return lista_pedidos

    @staticmethod
    def AddLineBreaksEveryNChars(input_str: str, interval: int) -> str:
        if not input_str or interval <= 0:
            return input_str

        return '\n'.join(input_str[i:i+interval] for i in range(0, len(input_str), interval))

    def pegaListaDelivery(self) -> list[bool]:
        lista_tipo_delivery = []
        conexao = mysql.connector.connect(
            host=self.res["server"],
            user=self.res["uid"],
            password=self.res["pwd"],
            database=self.res["database"]
        )
        cursor = conexao.cursor(dictionary=True)
        comando = f"SELECT delivery FROM pedidos WHERE usuario = '{self.usuario_logado}';"
        try:
            cursor.execute(comando)
            for row in cursor:
                lista_tipo_delivery.append(bool(row["delivery"]))
        except Exception as er:
            self.EnviaLog(type(er).__name__, er.__traceback__, str(er))
        finally:
            cursor.close()
            conexao.close()
        return lista_tipo_delivery

