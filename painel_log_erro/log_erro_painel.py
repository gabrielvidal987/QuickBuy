import flet as ft
import threading
import time
import copy
import mysql.connector

usuario = None
password = None
host = None
database = None
log_usuario = None
page = None
ultimos_resultados = []

def busca_log():
    # Configurações da conexão
    config = {
        'user': usuario,
        'password': password,
        'host': host,
        'database': database,
    }
    
    resultados = None
    try:
        connection = mysql.connector.connect(**config)
        if connection.is_connected():
            print("Conexão bem-sucedida ao MySQL")
            # Criar um cursor para executar consultas
            cursor = connection.cursor()
            # Executar um SELECT
            query = f"SELECT * FROM log_erro WHERE usuario = '{log_usuario}' ORDER BY hora DESC LIMIT 8"
            cursor.execute(query)
            # Recuperar os resultados
            resultados = cursor.fetchall()
    except mysql.connector.Error as err:
        print(f"Erro: {err}")
    finally:
        if connection.is_connected():
            cursor.close()
            connection.close()
            print("Conexão ao MySQL fechada.")
            
    return resultados

def atualiza_page():
    global page_flet
    try:
        def clear_page():
            page_flet.controls.clear()  # Remove todos os componentes da página
            page_flet.update()  # Atualiza a página para refletir as mudanças
        resultados = busca_log()
        novo_log = False
        for res in resultados:
            for ult_res in ultimos_resultados:
                if res != ult_res:
                    novo_log = True
                    break
                if novo_log:
                    break
        if novo_log:
            table = ft.DataTable(
                border=ft.border.all(5,"#fdbf07"),
                border_radius=10,
                columns=[
                    ft.DataColumn(label=ft.Text("HORA")),
                    ft.DataColumn(label=ft.Text("TIPO")),
                    ft.DataColumn(label=ft.Text("ORIGEM")),
                    ft.DataColumn(label=ft.Text("ERRO")),
                ],
                rows=[
                    ft.DataRow(cells=[
                        ft.DataCell(ft.Text(row[2],no_wrap=False)),
                        ft.DataCell(ft.Text(row[3],no_wrap=False)),
                        ft.DataCell(ft.Text(row[4],no_wrap=False)),
                        ft.DataCell(ft.Text(row[5],no_wrap=False)),
                    ]) for row in resultados
                ],
            )
            container_table = ft.Container(
                margin=30,
                padding=30,
                content=table
            )
            clear_page()
            page_flet.add(ft.Text('NOVO ERRO REGISTRADO NO LOG',size=20,color=ft.colors.AMBER,text_align=ft.TextAlign.CENTER))
            page_flet.add(ft.Text('Na tabela abaixo aparecem os logs de erro do mais novo ao mais antigo',size=15,color="#FFFFFF",text_align=ft.TextAlign.CENTER))
            page_flet.add(container_table)
            print('pagina atualizada...')
            time.sleep(5)
    except:
        pass
    
# Função principal para rodar o loop de eventos
def thread_atualiza_page():
    while True:
        thread = threading.Thread(target=atualiza_page)
        thread.start()
        time.sleep(5)
    
def dados_connect():
    global page_flet

    alert = ft.AlertDialog(
        title=ft.Text("Preencha todos os campos!")
    )

    def passa_dados_conn(e):
        global usuario,password,host,database,log_usuario
        if usuario_input.value and senha_input.value and host_input.value and database_input.value:
            usuario = usuario_input.value
            password = senha_input.value
            host = host_input.value
            database = database_input.value
            log_usuario = log_usuario_input.value
            thread_atualiza_page()
        else:
            page_flet.open(alert)
        
    usuario_input = ft.TextField(label="Usuário",text_align=ft.TextAlign.CENTER,color="#ffffff",label_style=ft.TextStyle(size=20,color="#7a7b7c"),text_size=20)
    senha_input = ft.TextField(label="Senha",text_align=ft.TextAlign.CENTER,color="#ffffff",label_style=ft.TextStyle(size=20,color="#7a7b7c"),text_size=20)
    host_input = ft.TextField(label="Host",text_align=ft.TextAlign.CENTER,color="#ffffff",label_style=ft.TextStyle(size=20,color="#7a7b7c"),text_size=20)
    database_input = ft.TextField(label="Database",text_align=ft.TextAlign.CENTER,color="#ffffff",label_style=ft.TextStyle(size=20,color="#7a7b7c"),text_size=20)
    log_usuario_input = ft.TextField(label="Usuário em que os logs são registrados",text_align=ft.TextAlign.CENTER,color="#ffffff",label_style=ft.TextStyle(size=20,color="#7a7b7c"),text_size=20)
    container_connect = ft.Container(
        bgcolor="#2f2f2f",
        margin=30,
        padding=50,
        width=500,
        border_radius=50,
        content=ft.Column(
            controls=[
                ft.Text("Insira os dados para conexão com o Banco de Dados",text_align=ft.TextAlign.CENTER,color=ft.colors.AMBER,size=25),
                host_input,
                usuario_input,
                senha_input,
                database_input,
                log_usuario_input,
                ft.ElevatedButton("CONECTAR!",bgcolor=ft.colors.AMBER,color=ft.colors.BLACK,on_click=passa_dados_conn,width=150,height=50)
            ],
            alignment=ft.MainAxisAlignment.CENTER,
            horizontal_alignment=ft.CrossAxisAlignment.CENTER 
        )
    )
    return container_connect

# Principal modulo, cria a pagina do flet com os dados gerais
def main(page: ft.Page) -> None:
    global page_flet
    page_flet = page
    page.title="LOG_QUICKBUY"
    page.bgcolor="#212121"
    page.window_width = 1366
    page.window_height = 768
    page.vertical_alignment = ft.MainAxisAlignment.CENTER
    page.horizontal_alignment = ft.CrossAxisAlignment.CENTER
    
    container_connect = dados_connect()
    page.add(container_connect)  
    
if __name__ == '__main__':
    # ft.app(target=main,view=ft.AppView.WEB_BROWSER,port=3000, assets_dir="assets")
    ft.app(target=main)