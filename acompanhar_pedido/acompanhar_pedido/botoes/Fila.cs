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
        List<(string, string)> senhas = new List<(string, string)>();
        string mudaSenha = "";
        string curDir = Path.Combine(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory.ToString()),"somTrocaSenha.mp3");
        public Fila()
        {
            InitializeComponent();
            Estética();
        }
        //função para carregar a estética da pagina com cores, janelas e demais elementos
        public void Estética()
        {
            this.BackColor = Color.FromArgb(239, 239, 239);
            lbSenha2.BackColor = Color.FromArgb(0, 102, 204);
            lbSenha2.Text = "0";
            lbSenha3.BackColor = Color.FromArgb(0, 102, 204);
            lbSenha3.Text = "0";
            lbSenha4.BackColor = Color.FromArgb(0, 102, 204);
            lbSenha4.Text = "0";
            lbSenha1.BackColor = Color.FromArgb(0, 102, 204);
            lbSenha1.Text = "0";
            senhaAtual.BackColor = Color.FromArgb(240, 240, 240);
            senhasAnteriores.BackColor = Color.FromArgb(240, 240, 240);
            tMedEspera.BackColor = Color.FromArgb(240, 240, 240);
        }
        //caso exista o som é chamado a função para tocar ele caso a senha tenha mudado
        //ele verifica se a variavel de mudaSenha possui uma senha diferente da ultima da lista, logo seria uma senha nova
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
                        if (mudaSenha != senhas[-1].ToString())
                        {
                            waveOutDevice.Init(audioFileReader);
                            waveOutDevice.Play();
                            while (waveOutDevice.PlaybackState == PlaybackState.Playing)
                            {
                                Thread.Sleep(100); // Aguarda o som terminar
                            }
                            mudaSenha = senhas[-1].ToString();
                        }
                    }
                }
                catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
            }
        }
        //troca as senhas das labels e também recalcula o tempo medio de espera
        public void TrocaSenha()
        {
            try
            {
                if (senhas.Count > 0)
                {
                    //adiciona as ultimas senhas em seus devidos campos, no caso da ultima senha ele adiciona na variavel mudaSenha
                    for (int c = -1; c > -5; c--)
                    {
                        switch (c)
                        {
                            case -1:
                                try { lbSenha1.Text = $"{senhas[c].Item1}\n{senhas[c].Item2}"; } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                                break;
                            case -2:
                                try { lbSenha2.Text = $"{senhas[c].Item1} - {senhas[c].Item2}"; } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                                break;
                            case -3:
                                try { lbSenha3.Text = $"{senhas[c].Item1} - {senhas[c].Item2}"; } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                                break;
                            case -4:
                                try { lbSenha4.Text = $"{senhas[c].Item1} - {senhas[c].Item2}"; } catch (Exception er) { ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message); }
                                break;
                            default:
                                break;
                        }   
                    }

                    //adiciona o tempo de espera se baseando na media
                    ConectarSqlClasse sql = new ConectarSqlClasse();
                    List<string> horas = new List<string>(sql.ListaHorarios());
                    int media = 0;
                    for (int c = 0; c < horas.Count - 1; c += 2)
                    {
                        media += int.Parse(horas[c + 1].Split(':')[1]) - int.Parse(horas[c].Split(':')[1]);
                    }
                    tMedEspera.Text = media / (horas.Count / 2) <= 0 ? "TEMPO MÉDIO DE ESPERA: 2 MINUTOS" : $"TEMPO MÉDIO DE ESPERA: {media / (horas.Count / 2)} MINUTOS";
                

                }
            }
            catch (Exception er)
            {
                ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
            }
        }
        //timer para chamar a atualização e troca de senha
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ConectarSqlClasse sql = new ConectarSqlClasse();
                senhas = sql.Senhas();
                Thread thread = new Thread(new ThreadStart(TocaSom));
                thread.Start();
                TrocaSenha();
            }
            catch (Exception er)
            {
                timer1.Stop();
                bool sucess_log = ConectarSqlClasse.EnviaLog(er.GetType().ToString(), er.StackTrace.ToString(), er.Message);
                if (sucess_log)
                {
                    timer1.Start();
                }
            }
        }
    }
}
