using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Temp.Minigames
{
    public partial class GamePesca : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        bool puoiGiocare;
        int scelta;
        string[] vettore;
        public GamePesca()
        {
            InitializeComponent();
        }
        public GamePesca(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            puoiGiocare = false;
            c = cond;
            mappa = ma;
            scelta = 0;
            if (Ulocale.turno)
            {
                c.BufferInviare.Add("P;" + creavettore());

            }
            else
            {
                string[] s = c.prendi().Split(';');
                if (s[0] == "P")
                {
                    vettore = s[1].Split(',');
                }
            }
            Thread tempo = new Thread(Timer);
            tempo.Start();

            gioca();
            mappa.Show();
            this.Hide();
        }


        private void Timer()
        {
            while (stopwatch.ElapsedMilliseconds < 10000)
            {
                stampa();
                Thread.Sleep(1000);
            }
        }
        private void stampa()
        {
            if (!CheckAccess())
                Dispatcher.Invoke(() => { stampa(); });
            else
                secondi.Content = 10 - (stopwatch.ElapsedMilliseconds / 1000);
        }
        private string creavettore()
        {
            string s = "";
            int numV = 0;
            for (int i = 0; i < 6; i++)
            {

                Random rnd = new Random();
                int estrazione = rnd.Next(0, 1);
                if (estrazione == 1 && numV < 2)
                {
                    vettore[i] = "v";
                    s += "v,";
                    numV++;
                }
                else
                {
                    s += "f,";
                    vettore[i] = "f";
                }
            }



            return s;
        }

        private void gioca()
        {
            for (int i = 0; i < 3; i++)
            {

            }
            for (int i = 0; i < 6; i++)
            {


                if (Ulocale.turno)
                {
                    content.Content = "Scegli la tua mossa! Entro ";
                    Thread tempo = new Thread(Timer);
                    tempo.Start();
                    puoiGiocare = true;
                    //potrebbe bloccare il thread corrente quindi non funzionano i bottoni
                    tempo.Join();
                    puoiGiocare = false;
                    //aspetto il messaggio e aggiorno i punti dell'avversario
                    c.BufferInviare.Add("E;" + scelta);
                    Ulocale.turno = false;
                }
                else
                {
                    string[] s = c.prendi().Split(';');


                    if (s[1] == "perso")
                    {
                        MessageBox.Show("Hai vinto! L'avversario non ha scelto");
                        Ulocale.numMonete += 10;
                        Uesterno.numMonete -= 10;
                        break;
                    }
                    else
                    {
                        scelta = Convert.ToInt32(s[1]);
                    }


                    Ulocale.turno = true;
                }

                if (scelta != 0)
                {
                    if (scelta == 1)
                        btn1.IsEnabled = false;
                    if (scelta == 2)
                        btn2.IsEnabled = false;
                    if (scelta == 3)
                        btn3.IsEnabled = false;
                    if (scelta == 4)
                        btn4.IsEnabled = false;
                    if (scelta == 5)
                        btn5.IsEnabled = false;
                    if (scelta == 6)
                        btn6.IsEnabled = false;

                    if (vettore[scelta - 1] == "f" && Ulocale.turno)
                    {
                        MessageBox.Show("Hai perso!");
                        Ulocale.numMonete -= 10;
                        Uesterno.numMonete += 10;
                        break;
                    }
                    if (vettore[scelta - 1] == "f" && !Ulocale.turno)
                    {
                        MessageBox.Show("Hai vinto!");
                        Ulocale.numMonete += 10;
                        Uesterno.numMonete -= 10;
                        break;
                    }
                }
                else
                {
                    c.BufferInviare.Add("e;" + "perso");
                    MessageBox.Show("Hai perso! Non hai scelto in tempo");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    break;
                }
            }

        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = 1;
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = 2;
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = 3;
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = 4;
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = 5;
            }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = 6;
            }
        }
    }
}
