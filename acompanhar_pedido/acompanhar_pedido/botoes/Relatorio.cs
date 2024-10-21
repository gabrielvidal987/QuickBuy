using Aspose.Cells;
using NAudio.CoreAudioApi;
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
        string foto_caminho = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"semFoto.png");
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
                dt.Columns.RemoveAt(0);
                dt.Columns.RemoveAt(12);
                //deixar a coluna de entrega em readonly
                dt.Columns[11].ReadOnly = true;
                //tabela que pega a lista dos produtos vendidos
                listaBruta = sql.ListaProdVendidos();
                //adiciona colunas de tempo, produto e qtd de produto
                dt.Columns.Add("Tempo de espera");
                dt.Columns.Add("Produto");
                dt.Columns.Add("QTD");
                FiltraDeliveryBalcao();
                ValorLiq_TEspera();
                FiltraRelatorio();
                exportExc.Enabled = true;
                exportExc.BackColor = Color.FromArgb(192, 255, 192);
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        public void FiltraDeliveryBalcao()
        {
            ConectarSqlClasse sql = new ConectarSqlClasse();
            List<bool> lista_delivery = new List<bool>(sql.pegaListaDelivery());
            //adiciona quantidade de entregas e quantidade de balcão
            int entrega = 0; int balcao = 0;
            foreach(bool delivery in lista_delivery)
            {
                if (delivery) { entrega++; } else { balcao++; }
            }
            List<DataRow> num_row_to_remove = new List<DataRow>();
            if (btnApenasBalcao.Checked)
            {
                num_row_to_remove.Clear();
                for (int l = 0; l < dt.Rows.Count; l++)
                {
                    if (dt.Rows[l][11].ToString() == "True") { num_row_to_remove.Add(dt.Rows[l]); }
                }
                foreach (DataRow row in num_row_to_remove)
                {
                    row.Delete();
                    dt.AcceptChanges();
                }
            }
            if (btnApenasDelivery.Checked)
            {
                num_row_to_remove.Clear();
                for (int l = 0; l < dt.Rows.Count; l++)
                {
                    if (dt.Rows[l][11].ToString() == "False") { num_row_to_remove.Add(dt.Rows[l]); }
                }
                foreach (DataRow row in num_row_to_remove)
                {
                    row.Delete();
                    dt.AcceptChanges();
                }
            }
            //adiciona info de qtd vendida de cada modalidade de entrega
            bool primeira_linha_livre = true;
            bool segunda_linha_livre = true;
            for (int l = 0; l < dt.Rows.Count; l++)
            {
                if (dt.Rows[l][13].ToString() == "" && primeira_linha_livre)
                {
                    primeira_linha_livre = false;
                    dt.Rows[l][13] = "Saída Entrega"; dt.Rows[l][14] = entrega.ToString();
                    continue;
                }
                if (dt.Rows[l][13].ToString() == "" && segunda_linha_livre)
                {
                    segunda_linha_livre = false;
                    dt.Rows[l][13] = "Saída Balcão"; dt.Rows[l][14] = balcao.ToString();
                    break;
                }
            }
            if (primeira_linha_livre && segunda_linha_livre)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1][13] = "Saída Entrega"; dt.Rows[dt.Rows.Count - 1][14] = entrega.ToString();
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1][13] = "Saída Balcão"; dt.Rows[dt.Rows.Count - 1][14] = balcao.ToString();
            }
            else if (segunda_linha_livre)
            {
                dt.Rows.Add();
                dt.Rows[dt.Rows.Count - 1][13] = "Saída Balcão"; dt.Rows[dt.Rows.Count - 1][14] = balcao.ToString();
            }
        }
        public void ValorLiq_TEspera()
        {
            //add media de tempo de espera e valor liquido
            double deb = 0.015; // 1,5 / 100 = 0.015
            double cred = 0.015; // 1,5 / 100 = 0.015
            if (txDeb.Text != "") { deb = double.Parse(txDeb.Text.Replace("%", "")) / 100; }
            if (txCred.Text != "") { cred = double.Parse(txCred.Text.Replace("%", "")) / 100; }
            //calcula os valores liquidos com taxas de maquinas
            for (int c = 0; c < dt.Rows.Count; c++)
            {
                if (dt.Rows[c][0].ToString() != "")
                {
                    dt.Rows[c][12] = $"{TimeSpan.Parse(dt.Rows[c][6].ToString()) - TimeSpan.Parse(dt.Rows[c][5].ToString())}";
                    if (dt.Rows[c][8].ToString() == "debito")
                    {
                        dt.Rows[c][9] = (double.Parse(dt.Rows[c][7].ToString()) - (double.Parse(dt.Rows[c][7].ToString()) * deb)).ToString("F");
                    }
                    if (dt.Rows[c][8].ToString() == "credito")
                    {
                        dt.Rows[c][9] = (double.Parse(dt.Rows[c][7].ToString()) - (double.Parse(dt.Rows[c][7].ToString()) * cred)).ToString("F");
                    }
                }
                else { break; }
            }
        }
        public void FiltraRelatorio()
        {
            try
            {
                //calcula a entrada total baseada na tabela/relatorio gerado
                for (int c = 0; c <dt.Rows.Count; c++) 
                {
                    if (dt.Rows[c][0].ToString() != "") { entradaTotal += Convert.ToDouble(dt.Rows[c][9]); }
                    else { break; }
                }
                //coloca no relatório a qtd de cada produto vendido; soma 2 na posição da linha pois as duas primeiras linhas tem a info de qtd de delivery e qtd balcão
                for (int c = 0; c < listaBruta.Count(); c++) 
                {
                    if (c > dt.Rows.Count - 1)
                    {
                        dt.Rows.Add();
                        dt.Rows[c][13] = listaBruta[c]["nome"]; dt.Rows[c][14] = listaBruta[c]["qtd_vendido"];
                        continue;
                    }
                    if (c + 2 > dt.Rows.Count - 1)
                    {
                        dt.Rows.Add();
                        dt.Rows[c + 2][13] = listaBruta[c]["nome"]; dt.Rows[c + 2][14] = listaBruta[c]["qtd_vendido"];
                        continue;
                    }
                    else
                    {
                        dt.Rows[c + 2][13] = listaBruta[c]["nome"]; dt.Rows[c + 2][14] = listaBruta[c]["qtd_vendido"];
                    }
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
                dt.Columns[11].ColumnName = "Saida";
                
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
                tabelaVendas.Height = flowLayoutPanel2.Height - 160;

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
                    foto_caminho = caminho_foto;
                    List<string> list = new List<string>(caminho_foto.Split('.'));
                    exten = list[list.Count - 1];
                }
                Image imagem = new Bitmap(foto_caminho);
                pictureBox2.Image = imagem;
            }
            catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); };
        }
        private void btn_Criar_Usuario_Click(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                try
                {
                    if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_usuario")))
                    {
                        Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_usuario"));
                    }
                    File.Copy(foto_caminho, Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"fotos_usuario",$"{nomeClube.Text}.{exten}"), overwrite: true);
                    foto_caminho = $"{nomeClube.Text}.{exten}";
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
                        ws.Cells[0, 11].PutValue("Saida");
                        ws.Cells[0, 12].PutValue("Tempo de espera");
                        ws.Cells[0, 13].PutValue("ITEM");
                        ws.Cells[0, 14].PutValue("Total Vendido");
                        for (int c = 0; c < tabelaVendas.RowCount; c++)
                        {
                            for(int i = 0; i < tabelaVendas.Columns.Count; i++)
                            {
                                string valor_celula = tabelaVendas.Rows[c].Cells[i].Value.ToString();
                                if (valor_celula != "")
                                {
                                    if (i == 11)
                                    {
                                        if (bool.Parse(valor_celula)) { valor_celula = "Entrega"; } else { valor_celula = "Balcão"; }
                                    }
                                    ws.Cells[c + 1, i].PutValue(valor_celula);
                                }
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