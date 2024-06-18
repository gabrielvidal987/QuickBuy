using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using IsolationLevel = System.Data.IsolationLevel;


namespace acompanhar_pedido
{
    public static class VariaveisGlobais
    {
        public static string Usuario = "";
        public static void LerNomeArquivo()
        {
            try
            {
                string conteudo = File.ReadAllText("Usuario.TXT");
                try { Usuario = conteudo.Split('\n')[0]; } catch { Usuario = conteudo; }
            }
            catch (Exception er)
            {
                MessageBox.Show("Não encontrado TXT de usuarios. Informe ao desenvolvedor. Programa será fechado!");
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.TargetSite.ToString(), er.Message);
                Application.Exit();
            }
        }
        
    }
}
