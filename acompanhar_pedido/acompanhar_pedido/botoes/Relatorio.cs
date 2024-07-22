using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace acompanhar_pedido.botoes
{
    public partial class Relatorio : Form
    {
        string fotopadrao = "C:\\Users\\AP20.APD\\source\\repos\\acompanhar_pedido\\fotos\\semFoto.png";
        string foto_caminho;
        string exten = "png";
        double entradaTotal = 0;
        DataTable dt = new DataTable();
        List<Dictionary<string, string>> listaBruta = new List<Dictionary<string, string>>();


        public Relatorio()
        {
            InitializeComponent();
            Estetica();
            this.escolherLocal.ShowNewFolderButton = true;
        }
        public void Estetica()
        {
            pcholdPesquisa.PlaceHolderText = "Digite o nome";
            flowLayoutPanel2.Size = new Size(1140, 650);
            flowLayoutPanel2.Location = new Point(200, 20);
            button1.Width = flowLayoutPanel2.Width - 10;
            tabelaVendas.Width = flowLayoutPanel2.Width - 10;
            tabelaVendas.Height = flowLayoutPanel2.Height - 150;
        }
        private void apagaBD_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("TEM CERTEZA DE QUE DESEJA APAGAR OS ITENS SELECIONADOS?", "ATENÇÃO", MessageBoxButtons.YesNo);
            if (resposta == DialogResult.Yes)
            {
                try
                {
                    if (delProduto.Checked|| delVendas.Checked|| delPendentes.Checked)
                    {
                        ConectarSqlClasse sql = new ConectarSqlClasse();
                        string produto = "*";
                        string vendas = "*";
                        string pendentes = "*";
                        if (delProduto.Checked)
                        {
                            produto = "produtos";
                        }
                        if (delVendas.Checked)
                        {
                            vendas = "pedidos_prontos";
                        }
                        if (delPendentes.Checked)
                        {
                            pendentes = "pedidos";
                        }
                        MessageBox.Show(sql.LimparBD(produto, vendas,pendentes));
                        delProduto.Checked = false;
                        delVendas.Checked = false;
                        delPendentes.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma das opções foram marcadas, nada será apagado!");
                    }
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
        private void GeraRelatorio_Click(object sender, EventArgs e)
        {
            //ele cria tudo em um datatable chamado 'dt'. após alterar todo esse dt ele atribui os dados dele ao TabelaVendas
            entradaTotal = 0;
            entradas.Text = string.Empty;
            saidas.Text = string.Empty;
            dt.Clear();
            ConectarSqlClasse sql = new ConectarSqlClasse();
            try
            {
                tabelaVendas.Columns.Clear();
                bool filtro = false;
                string ordem = "";
                string nome = "";
                if (btnNomeAZ.Checked)
                {
                    filtro = true;
                    ordem = "az";
                    dt = sql.Relatorio(filtro, ordem, nome);
                }
                else if (btnNomeZA.Checked)
                {
                    filtro = true;
                    ordem = "za";
                    dt = sql.Relatorio(filtro, ordem, nome);
                }
                else if (btnUmNome.Checked)
                {
                    if (pcholdPesquisa.Text != "")
                    {
                        filtro = true;
                        ordem = "nome";
                        nome = pcholdPesquisa.Text;
                        dt = sql.Relatorio(filtro, ordem, nome);
                    }
                    else
                    {
                        MessageBox.Show("Preencha como o nome a ser pesquisado");
                    }
                }
                if (btnValor.Checked)
                {
                    filtro = true;
                    ordem = "venda";
                    dt = sql.Relatorio(filtro, ordem, nome);
                }
                else
                {
                    dt = sql.Relatorio(filtro, ordem, nome);
                }
                //tabela que pega a lista dos produtos vendidos
                listaBruta = sql.ListaVendidos();
                AdicionaMedia();
                exportExc.Enabled = true;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        public void AdicionaMedia()
        {
            try
            {
                //add media de tempo de espera e valor liquido
                int qtdLinha = dt.Rows.Count;
                double deb = 0.015; // 1,5 / 100 = 0.015
                double cred = 0.015; // 1,5 / 100 = 0.015
                if (txDeb.Text != "") { deb = double.Parse(txDeb.Text.Replace("%", "")) / 100; }
                if (txCred.Text != "") { cred = double.Parse(txCred.Text.Replace("%", "")) / 100; }
                dt.Columns.Add("Tempo de espera");
                dt.Columns.Add("Produto");
                dt.Columns.Add("QTD vendida");
                for (int cont = 0; cont < qtdLinha; cont++) { for (int i = 0; i < 10; i++) { if (dt.Rows[cont][i] == "") { dt.Rows.Remove(dt.Rows[cont]);continue; } } }
                //calcula os valores liquidos com taxas de maquinas
                for (int c = 0; c < qtdLinha; c++)
                {
                    dt.Rows[c][11] = $"{int.Parse(dt.Rows[c][6].ToString().Split(':')[1]) - int.Parse(dt.Rows[c][5].ToString().Split(':')[1])} minuto(s)";
                    if (dt.Rows[c][8].ToString() == "debito")
                    {
                        dt.Rows[c][9] = (double.Parse(dt.Rows[c][7].ToString()) - (double.Parse(dt.Rows[c][7].ToString()) * deb)).ToString("F");
                    }
                    if (dt.Rows[c][8].ToString() == "credito")
                    {
                        dt.Rows[c][9] = (double.Parse(dt.Rows[c][7].ToString()) - (double.Parse(dt.Rows[c][7].ToString()) * cred)).ToString("F");
                    }
                }
                //calcula a entrada total baseada na tabela/relatorio gerado
                for (int c = 0; c <qtdLinha; c++) { entradaTotal += Convert.ToDouble(dt.Rows[c][9]); }
                //coloca no relatório a qtd de cada produto vendido
                for (int c = 0; c < listaBruta.Count(); c++) 
                {
                    if (c > dt.Rows.Count - 1)
                    {
                        dt.Rows.Add();
                        dt.Rows[c][12] = listaBruta[c]["produto"]; dt.Rows[c][13] = listaBruta[c]["qtd"];
                    }
                    dt.Rows[c][12] = listaBruta[c]["produto"]; dt.Rows[c][13] = listaBruta[c]["qtd"]; 
                }
                //altera o nome das colunas
                dt.Columns[0].ColumnName = "Senha";
                dt.Columns[1].ColumnName = "Nome";
                dt.Columns[2].ColumnName = "Endereço";
                dt.Columns[3].ColumnName = "Itens";
                dt.Columns[4].ColumnName = "Observações";
                dt.Columns[5].ColumnName = "hora do pedido";
                dt.Columns[6].ColumnName = "hora de conclusão";
                dt.Columns[7].ColumnName = "Valor Bruto";
                dt.Columns[8].ColumnName = "Forma de pagamento";
                dt.Columns[9].ColumnName = "Valor líquido";
                dt.Columns[10].ColumnName = "Operador";
                //atribui os dados do dt ao tabelaVendas
                tabelaVendas.DataSource = dt;
                tabelaVendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                button1.Width = flowLayoutPanel2.Width - 10;
                tabelaVendas.Width = flowLayoutPanel2.Width - 10;
                tabelaVendas.Height = flowLayoutPanel2.Height - 150;

                ConectarSqlClasse sql = new ConectarSqlClasse();
                entradas.Text = entradaTotal.ToString("F");
                if (saidas.Text != "")
                {
                    try
                    {
                        double ent = double.Parse(entradas.Text);
                        double sai = double.Parse(saidas.Text);
                        resFinal.Text = (ent - sai).ToString("C");
                    }
                    catch { }

                }
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };


        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAddpic.ShowDialog() == DialogResult.OK)
                {
                    string caminho_foto = btnAddpic.FileName.Replace(@"\", @"\\");
                    List<string> list = new List<string>(caminho_foto.Split('.'));
                    foto_caminho = caminho_foto;
                    exten = list[list.Count - 1];
                }
                else
                {
                    foto_caminho = fotopadrao;
                }
                Image imagem = new Bitmap(foto_caminho);
                pictureBox2.Image = imagem;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                try
                {
                    if (!Directory.Exists(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + $@"\fotos_clube"))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + $@"\fotos_clube");
                    }
                    File.Copy(foto_caminho, Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()) + $@"\fotos_clube\{nomeClube.Text}.{exten}");
                    foto_caminho = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()).Replace(@"\", @"\\") + $@"\\fotos_clube\\{nomeClube.Text}.{exten}";
                }
                catch { }
                sql.CriaUsuario(nomeClube.Text.ToLower().Trim(), foto_caminho);
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void exportExc_Click(object sender, EventArgs e)
        {
            DialogResult result = escolherLocal.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    string fl = escolherLocal.SelectedPath;
                    try
                    {
                        // a ordem para as celulas é primeiro numero vertical e segundo horizontal, ou primeiro numero e depois letra, sempre começando do 0
                        Workbook wb = new Workbook();
                        Worksheet ws = wb.Worksheets[0];
                        ws.Cells[0, 0].PutValue("Senha");
                        ws.Cells[0, 1].PutValue("Nome");
                        ws.Cells[0, 2].PutValue("Endereço");
                        ws.Cells[0, 3].PutValue("Itens");
                        ws.Cells[0, 4].PutValue("observações");
                        ws.Cells[0, 5].PutValue("Hora do pedido");
                        ws.Cells[0, 6].PutValue("Hora de conclusão");
                        ws.Cells[0, 7].PutValue("Valor Bruto");
                        ws.Cells[0, 8].PutValue("Forma de pagamento");
                        ws.Cells[0, 9].PutValue("Valor líquido");
                        ws.Cells[0, 10].PutValue("Operador");
                        ws.Cells[0, 11].PutValue("Tempo de espera");
                        ws.Cells[0, 12].PutValue("ITEM");
                        ws.Cells[0, 13].PutValue("Total Vendido");
                        string l = "A";
                        for (int c = 0; c < tabelaVendas.RowCount; c++)
                        {
                            for (int i = 0; i < tabelaVendas.ColumnCount; i++)
                            {
                                switch (i)
                                {
                                    case 0:
                                        l = "A";
                                        break;
                                    case 1:
                                        l = "B";
                                        break;
                                    case 2:
                                        l = "C";
                                        break;
                                    case 3:
                                        l = "D";
                                        break;
                                    case 4:
                                        l = "E";
                                        break;
                                    case 5:
                                        l = "F";
                                        break;
                                    case 6:
                                        l = "G";
                                        break;
                                    case 7:
                                        l = "H";
                                        break;
                                    case 8:
                                        l = "I";
                                        break;
                                    case 9:
                                        l = "J";
                                        break;
                                    case 10:
                                        l = "K";
                                        break;
                                    case 11:
                                        l = "L";
                                        break;
                                    case 12:
                                        l = "N";
                                        break;
                                    case 13:
                                        l = "O";
                                        break;
                                    case 14:
                                        l = "P";
                                        break;
                                    case 15:
                                        l = "Q";
                                        break;
                                    case 16:
                                        l = "R";
                                        break;
                                    default:
                                        return;
                                }
                                ws.Cells[$"{l}{c + 2}"].PutValue(tabelaVendas.Rows[c].Cells[i].Value);
                            }
                        }
                        wb.Save(fl + @"\Relatório de vendas.xlsx", SaveFormat.Xlsx);
                        //altera para centralizar texto
                        try
                        {
                            Workbook wa = new Workbook(fl + @"\Relatório de vendas.xlsx");
                            Worksheet wc = wa.Worksheets[0];
                            Style st = wa.CreateStyle();
                            st.HorizontalAlignment = TextAlignmentType.Center;
                            st.VerticalAlignment = TextAlignmentType.Center;
                            st.IsTextWrapped = true;
                            StyleFlag flag = new StyleFlag();
                            flag.Alignments = true;
                            wc.Cells.ApplyStyle(st, flag);
                            for (int i = 0; i < 10; i++)
                            {
                                wc.AutoFitColumn(i);
                            }
                            wa.Save(fl + @"\Relatório de vendas.xlsx", SaveFormat.Xlsx);
                        }
                        catch { }
                        MessageBox.Show("Planilha criada com sucesso no caminho: " + fl);
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Erro ao criar a planilha com o relatório.");
                        ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                    }
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
            }
        }
        private void saidas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                saidas.Text = "0";
            }
        }
        private void pcholdPesquisa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GeraRelatorio_Click(sender, e);
            }
        }
    }
}