using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;

namespace acompanhar_pedido.botoes
{
    public partial class Fila : Form
    {
        int totalSenhas = 0;

        int varSenhaAtual = 0;
        int varSenhaAnt1 = 0;
        int varSenhaAnt2 = 0;
        int varSenhaAnt3 = 0;
        string mudaSenha = "";
        string curDir = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"somTrocaSenha.mp3");
        public Fila()
        {
            InitializeComponent();
        }

        public void Estética()
        {
            this.BackColor = Color.FromArgb(239, 239, 239);
            lbSenhaAnt1.BackColor = Color.FromArgb(0, 102, 204);
            lbSenhaAnt1.Text = varSenhaAnt1.ToString();
            lbSenhaAnt2.BackColor = Color.FromArgb(0, 102, 204);
            lbSenhaAnt2.Text = varSenhaAnt2.ToString();
            lbSenhaAnt3.BackColor = Color.FromArgb(0, 102, 204);
            lbSenhaAnt3.Text = varSenhaAnt3.ToString();
            lbSenhaAtual.BackColor = Color.FromArgb(0, 102, 204);
            lbSenhaAtual.Text = varSenhaAtual.ToString();
            senhaAtual.BackColor = Color.FromArgb(240, 240, 240);
            senhasAnteriores.BackColor = Color.FromArgb(240, 240, 240);
            tMedEspera.BackColor = Color.FromArgb(240, 240, 240);
        }
        private void Fila_Load(object sender, EventArgs e)
        {
            Estética();
        }
        public void TocaSom()
        {
            if (File.Exists(curDir))
            {
                try
                {
                    using (var waveOutDevice = new WaveOutEvent())
                    using(var audioFileReader = new AudioFileReader(curDir))
                    {
                        ConectarSqlClasse sql = new ConectarSqlClasse();
                        if (mudaSenha != sql.Senhas()[totalSenhas - 1].ToString())
                        {
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
                            while (waveOutDevice.PlaybackState == PlaybackState.Playing)
                            {
                                Thread.Sleep(100); // Aguarda o som terminar
                            }
                        }
                    }
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
        }
        public void TrocaSenha()
        {
            ConectarSqlClasse sql = new ConectarSqlClasse();
            try
            {
                var senhas = new List<(string senha, string nome)>();
                senhas = sql.Senhas();
                if (totalSenhas > 0)
                {
                    List<string> horas = new List<string>(sql.ListaHorarios());
                    int media = 0;
                    try { lbSenhaAtual.Text = $"{senhas[totalSenhas - 1].Item1}\n{senhas[totalSenhas - 1].Item2}"; mudaSenha = sql.Senhas()[totalSenhas - 1].ToString(); } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                    try { lbSenhaAnt1.Text = $"{senhas[totalSenhas - 2].Item1} - {senhas[totalSenhas - 2].Item2}"; } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                    try { lbSenhaAnt2.Text = $"{senhas[totalSenhas - 3].Item1} - {senhas[totalSenhas - 3].Item2}"; } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                    try { lbSenhaAnt3.Text = $"{senhas[totalSenhas - 4].Item1} - {senhas[totalSenhas - 4].Item2}"; } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                    for (int c = 0; c < horas.Count - 1; c += 2)
                    {
                        media += int.Parse(horas[c + 1].Split(':')[1]) - int.Parse(horas[c].Split(':')[1]);
                    }
                    if (media / (horas.Count / 2) <= 0)
                    {
                        tMedEspera.Text = "TEMPO MÉDIO DE ESPERA: 2 MINUTOS";
                    }
                    else
                    {
                        tMedEspera.Text = $"TEMPO MÉDIO DE ESPERA: {media / (horas.Count / 2)} MINUTOS";
                    }
                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            ConectarSqlClasse sql = new ConectarSqlClasse();
            totalSenhas = sql.Senhas().Count();
            Thread thread = new Thread(new ThreadStart(TocaSom));
            thread.Start();
            TrocaSenha();
        }
    }
}
