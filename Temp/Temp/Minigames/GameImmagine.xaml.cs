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
using System.Windows.Threading;

namespace Temp.Minigames
{
    public partial class GameImmagine : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        int scelta;
        DispatcherTimer dispatcherTimer;
        Stopwatch stopWatch;
        TimeSpan ts;
        public GameImmagine()
        {
            InitializeComponent();
        }
        public GameImmagine(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            c = cond;
            mappa = ma;
            scelta = 0;
            //inizio minigioco
            content.Content = "Il minigioco inizia tra ";
            stopWatch = new Stopwatch();
            dispatcherTimer = new DispatcherTimer();
            stopWatch.Start();
            dispatcherTimer.Start();
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            //nascondo
            scelta1.Visibility = Visibility.Hidden;
            scelta2.Visibility = Visibility.Hidden;
            scelta3.Visibility = Visibility.Hidden;
            scelta4.Visibility = Visibility.Hidden;
            btnInvio.Visibility = Visibility.Hidden;
        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                ts = stopWatch.Elapsed;
                secondi.Content = ts.Seconds;
            }
            if (ts.Seconds > 5)
            {
                stopWatch.Stop();
                avanti();
            }
        }
        private void avanti()
        {
            scelta1.Visibility = Visibility.Visible;
            scelta2.Visibility = Visibility.Visible;
            scelta3.Visibility = Visibility.Visible;
            scelta4.Visibility = Visibility.Visible;
            btnInvio.Visibility = Visibility.Visible;
            content.Content = "Seleziona la tua immagine in 10 sec";
            secondi.Content = "";
            caricaImmagini();
        }
        private void caricaImmagini()
        {
            BitmapImage bitmap = new BitmapImage();
            //principale
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\principale.png");
            bitmap.EndInit();
            imgPrincipale.Source = bitmap;
            //1
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img1.png");
            bitmap.EndInit();
            img1.Source = bitmap;
            //2
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img2.png");
            bitmap.EndInit();
            img2.Source = bitmap;
            //3
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img3.png");
            bitmap.EndInit();
            img3.Source = bitmap;
            //4
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\img4.png");
            bitmap.EndInit();
            img4.Source = bitmap;
        }

        private void btnInvio_Click(object sender, RoutedEventArgs e)
        {
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
            mappa.Show();
            this.Hide();
        }

        private void scelta1_Checked(object sender, RoutedEventArgs e)
        {
            scelta = 1;
        }

        private void scelta2_Checked(object sender, RoutedEventArgs e)
        {
            scelta = 2;
        }

        private void scelta3_Checked(object sender, RoutedEventArgs e)
        {
            scelta = 3;
        }

        private void scelta4_Checked(object sender, RoutedEventArgs e)
        {
            scelta = 4;
        }
    }
}
