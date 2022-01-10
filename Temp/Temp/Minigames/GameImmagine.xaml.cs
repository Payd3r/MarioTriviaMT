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
    public partial class GameImmagine : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        bool puoiGiocare;
        int scelta;
        public GameImmagine()
        {
            InitializeComponent();
        }
        public GameImmagine(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            puoiGiocare = false;
            c = cond;
            mappa = ma;
            scelta = 0;
            //carica immagini
            caricaImmagini();
            //inizio minigioco
            content.Content = "Il minigioco inizia tra ";
            Thread tempo = new Thread(Timer);
            tempo.Start();
            //10 sec per premere un bottone
            gioca();
            mappa.Show();
            this.Hide();
        }
        private void caricaImmagini()
        {
            BitmapImage bitmap = new BitmapImage();
            //principale
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\imgPrincipale.png");
            bitmap.EndInit();
            imgPrincipale.Source = bitmap;
            //1
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img1.png");
            bitmap.EndInit();
            img1.Source = bitmap;
            //2
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img2.png");
            bitmap.EndInit();
            img2.Source = bitmap;
            //3
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img3.png");
            bitmap.EndInit();
            img3.Source = bitmap;
            //4
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img4.png");
            bitmap.EndInit();
            img4.Source = bitmap;
        }
        private void gioca()
        {
            content.Content = "Scegli la tua mossa! Entro ";
            Thread tempo = new Thread(Timer);
            tempo.Start();
            puoiGiocare = true;
            //potrebbe bloccare il thread corrente quindi non funzionano i bottoni
            tempo.Join();
            puoiGiocare = false;
            //aspetto il messaggio e aggiorno i punti dell'avversario
            c.BufferInviare.Add("I;" + scelta);
            string[] s = c.prendi().Split(';');
            if (s[0] == "I")
                if (Convert.ToInt32(s[1]) == 3)
                    Uesterno.numMonete += 10;
                else
                    Uesterno.numMonete -= 10;
            //controllo la mia scelta e aggiorno i miei punti
            if (scelta == 3)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
            }
            else
            {
                MessageBox.Show("Hai perso");
                Ulocale.numMonete -= 10;
            }
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
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 1;
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 2;
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 3;
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 4;
        }
    }
}
