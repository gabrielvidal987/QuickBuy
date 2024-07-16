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
            tabelaVendasProd.Width = flowLayoutPanel2.Width - 10;
            tabelaVendasProd.Height = flowLayoutPanel2.Height - 540;
            tabelaVendas.Width = flowLayoutPanel2.Width - 10;
            tabelaVendas.Height = flowLayoutPanel2.Height - 300;
        }
        private void apagaBD_Click(object sender, EventArgs e)
        {
            var resposta = MessageBox.Show("TEM CERTEZA DE QUE DESEJA APAGAR OS ITENS SELECIONADOS?", "ATENÇÃO", MessageBoxButtons.YesNo);
            if (resposta == DialogResult.Yes)
            {
                try
                {
                    if (delProduto.Checked != false || delVendas.Checked != false)
                    {
                        ConectarSqlClasse sql = new ConectarSqlClasse();
                        string produto = "*";
                        string vendas = "*";
                        if (delProduto.Checked)
                        {
                            produto = "produtos";
                        }
                        if (delVendas.Checked)
                        {
                            vendas = "pedidos_prontos";
                        }
                        MessageBox.Show(sql.LimparBD(produto, vendas));
                        delProduto.Checked = false;
                        delVendas.Checked = false;
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
            entradaTotal = 0;
            entradas.Text = string.Empty;
            saidas.Text = string.Empty;
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
                    tabelaVendas.DataSource = sql.Relatorio(filtro, ordem, nome);
                }
                else if (btnNomeZA.Checked)
                {
                    filtro = true;
                    ordem = "za";
                    tabelaVendas.DataSource = sql.Relatorio(filtro, ordem, nome);
                }
                else if (btnUmNome.Checked)
                {
                    if (pcholdPesquisa.Text != "")
                    {
                        filtro = true;
                        ordem = "nome";
                        nome = pcholdPesquisa.Text;
                        tabelaVendas.DataSource = sql.Relatorio(filtro, ordem, nome);
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
                    tabelaVendas.DataSource = sql.Relatorio(filtro, ordem, nome);
                }
                else
                {
                    tabelaVendas.DataSource = sql.Relatorio(filtro, ordem, nome);
                }
                tabelaVendas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                AdicionaMedia();
                exportExc.Enabled = true;
                exportTxt.Enabled = true;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };

            try
            {
                tabelaVendasProd.Columns.Clear();
                DataTable datatable = new DataTable();
                List<Dictionary<string,string>> listaBruta = new List<Dictionary<string,string>>(sql.ListaVendidos());
                datatable.Columns.Add("produto");
                datatable.Columns.Add("qtd");
                foreach (var dict in  listaBruta) { datatable.Rows.Add(dict["produto"], dict["qtd"]); }
                tabelaVendasProd.DataSource = datatable;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        public void AdicionaMedia()
        {
            try
            {
                //add media de tempo de espera e valor liquido
                int qtdLinha = tabelaVendas.RowCount;
                double deb = 0.015; // 1,5 / 100 = 0.015
                double cred = 0.015; // 1,5 / 100 = 0.015
                if (txDeb.Text != "") { deb = double.Parse(txDeb.Text.Replace("%", "")) / 100; }
                if (txCred.Text != "") { cred = double.Parse(txCred.Text.Replace("%", "")) / 100; }
                tabelaVendas.Columns.Add("Tempo de espera", "Tempo de espera");
                for (int c = 0; c < qtdLinha; c++)
                {
                    tabelaVendas.Rows[c].Cells[11].Value = $"{int.Parse(tabelaVendas.Rows[c].Cells[6].Value.ToString().Split(':')[1]) - int.Parse(tabelaVendas.Rows[c].Cells[5].Value.ToString().Split(':')[1])} minuto(s)";
                    if (tabelaVendas.Rows[c].Cells[8].Value.ToString() == "debito")
                    {
                        tabelaVendas.Rows[c].Cells[9].Value = (double.Parse(tabelaVendas.Rows[c].Cells[7].Value.ToString()) - (double.Parse(tabelaVendas.Rows[c].Cells[7].Value.ToString()) * deb)).ToString("F");
                    }
                    if (tabelaVendas.Rows[c].Cells[8].Value.ToString() == "credito")
                    {
                        tabelaVendas.Rows[c].Cells[9].Value = (double.Parse(tabelaVendas.Rows[c].Cells[7].Value.ToString()) - (double.Parse(tabelaVendas.Rows[c].Cells[7].Value.ToString()) * cred)).ToString("F");
                    }
                }
                for (int c = 0; c <qtdLinha; c++) { entradaTotal += Convert.ToDouble(tabelaVendas.Rows[c].Cells[9].Value); }
                tabelaVendas.Columns[0].HeaderText = "Senha";
                tabelaVendas.Columns[1].HeaderText = "Nome";
                tabelaVendas.Columns[2].HeaderText = "Endereço";
                tabelaVendas.Columns[3].HeaderText = "Itens";
                tabelaVendas.Columns[4].HeaderText = "Observações";
                tabelaVendas.Columns[5].HeaderText = "hora do pedido";
                tabelaVendas.Columns[6].HeaderText = "hora de conclusão";
                tabelaVendas.Columns[7].HeaderText = "Valor Bruto";
                tabelaVendas.Columns[8].HeaderText = "Forma de pagamento";
                tabelaVendas.Columns[9].HeaderText = "Valor líquido";
                tabelaVendas.Columns[10].HeaderText = "Operador";
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                button1.Width = flowLayoutPanel2.Width - 10;
                tabelaVendasProd.Width = flowLayoutPanel2.Width - 10;
                tabelaVendasProd.Height = flowLayoutPanel2.Height - 540;
                tabelaVendas.Width = flowLayoutPanel2.Width - 10;
                tabelaVendas.Height = flowLayoutPanel2.Height - 300;

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
                        ws.Cells[0, 2].PutValue("Itens");
                        ws.Cells[0, 3].PutValue("observações");
                        ws.Cells[0, 4].PutValue("Hora do pedido");
                        ws.Cells[0, 5].PutValue("Hora de conclusão");
                        ws.Cells[0, 6].PutValue("Valor Bruto");
                        ws.Cells[0, 7].PutValue("Forma de pagamento");
                        ws.Cells[0, 8].PutValue("Valor líquido");
                        ws.Cells[0, 9].PutValue("Tempo de espera");
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
        private void exportTxt_Click(object sender, EventArgs e)
        {
            DialogResult result = escolherLocal.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    List<string> list = new List<string>();
                    string fl = escolherLocal.SelectedPath;
                    try
                    {
                        File.AppendAllText(fl + @"\Relatório de vendas.txt", "Senha || Nome || Itens || observações || Hora do pedido || Hora de conclusão || Valor Bruto || Forma de pagamento || Valor líquido || Tempo de espera \n");
                        for (int c = 0; c < tabelaVendas.RowCount; c++)
                        {
                            for (int i = 0; i < tabelaVendas.ColumnCount; i++)
                            {
                                if (i == tabelaVendas.ColumnCount - 1) { list.Add($"{tabelaVendas.Rows[c].Cells[i].Value}"); continue; }
                                list.Add($"{tabelaVendas.Rows[c].Cells[i].Value} || ");
                            }
                            foreach (string s in list) { File.AppendAllText(fl + @"\Relatório de vendas.txt", s); }
                            if (c == tabelaVendas.RowCount - 1) { break; }
                            File.AppendAllText(fl + @"\Relatório de vendas.txt", "\n");
                            list.Clear();
                        }
                        MessageBox.Show("Relatório salvo com sucesso em TXT no caminho: " + fl);
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Erro ao exportar o relatório para TXT.");
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