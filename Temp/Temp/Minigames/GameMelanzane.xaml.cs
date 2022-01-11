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
    public partial class GameMelanzane : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Minimappa mappa;
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        bool puoiGiocare;
        int posizione;
        int secondiLocale;
        int secondiEsterno;
        public GameMelanzane()
        {
            InitializeComponent();
        }
        public GameMelanzane(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            puoiGiocare = false;
            Ulocale = a;
            Uesterno = b;
            c = cond;
            mappa = ma;
            posizione = 0;
            secondiLocale = 0;
            secondiEsterno = 0;
            //inizio minigioco
            content.Content = "Il minigioco inizia tra ";
            Thread tempo = new Thread(Timer);
            tempo.Start();
            //carica immagini
            caricaImmagini();
            //inizio il gioco
            content.Content = "Corri!";
            tempo = new Thread(Timer2);
            tempo.Start();
            Thread giocatore1 = new Thread(giocatoreLocale);
            Thread giocatore2 = new Thread(giocatoreEsterno);
            giocatore1.Start();
            giocatore2.Start();
            giocatore1.Join();
            giocatore2.Join();
            if (tempo.IsAlive)
                tempo.Abort();
            //raccolta punti
            conteggioPunti();
            mappa.Show();
            this.Hide();
        }
        private void conteggioPunti()
        {
            if (secondiLocale < secondiEsterno)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                Uesterno.numMonete -= 10;
            }
            else if (secondiLocale > secondiEsterno)
            {
                MessageBox.Show("Hai perso!");
                Ulocale.numMonete -= 10;
                Uesterno.numMonete += 10;
            }
            else
            {
                MessageBox.Show("Avete pareggiato!");
            }
        }
        private void giocatoreLocale()
        {
            Thickness a = new Thickness();
            a.Left = 113;
            a.Top = 90;
            a.Bottom = 0;
            a.Right = 0;
            puoiGiocare = true;
            while (true)
            {
                if (posizione == 100)
                    break;
                if (posizione != (a.Left - 113) / 10/4)
                {
                    c.BufferInviare.Add("M;avanti");
                    a.Left += 10/4;
                    locale.Margin = a;
                }
            }
            puoiGiocare = false;
            secondiLocale = Convert.ToInt32(secondi.Content);
        }
        private void giocatoreEsterno()
        {
            Thickness a = new Thickness();
            a.Left = 113;
            a.Top = 220;
            a.Bottom = 0;
            a.Right = 0;
            for (int i = 0; i < 100; i++)
            {
                string[] s = c.prendi().Split(';');
                if (s[0] == "M")
                    if (s[1] == "avanti")
                    {
                        a.Left += 10/4;
                        esterno.Margin = a;
                    }
            }
            secondiEsterno = Convert.ToInt32(secondi.Content);
        }
        private void caricaImmagini()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + Ulocale.skin + ".png");
            bitmap.EndInit();
            locale.Source = bitmap;
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + Uesterno.skin + ".png");
            bitmap.EndInit();
            esterno.Source = bitmap;
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
        private void Timer2()
        {
            while (stopwatch.ElapsedMilliseconds < 100000)
            {
                stampa2();
                Thread.Sleep(1000);
            }
        }
        private void stampa2()
        {
            if (!CheckAccess())
                Dispatcher.Invoke(() => { stampa2(); });
            else
                secondi.Content = stopwatch.ElapsedMilliseconds / 1000;
        }

        private void btnAvanti_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                posizione++;
        }
    }
}
