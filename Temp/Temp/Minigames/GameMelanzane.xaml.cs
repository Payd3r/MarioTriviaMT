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
    public partial class GameMelanzane : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Minimappa mappa;
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        int posizione;
        int secondiLocale;
        int secondiEsterno;
        DispatcherTimer dispatcherTimer;
        Stopwatch stopWatch;
        TimeSpan ts;
        public GameMelanzane()
        {
            InitializeComponent();
        }
        public GameMelanzane(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            c = cond;
            mappa = ma;
            posizione = 0;
            secondiLocale = 0;
            secondiEsterno = 0;
            //inizio minigioco
            stopWatch = new Stopwatch();
            dispatcherTimer = new DispatcherTimer();
            stopWatch.Start();
            dispatcherTimer.Start();
            content.Content = "Il minigioco inizia a 10 sec ";
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            //nascondi
            linea1.Visibility = Visibility.Hidden;
            linea2.Visibility = Visibility.Hidden;
            start.Visibility = Visibility.Hidden;
            finish.Visibility = Visibility.Hidden;
            btnAvanti.Visibility = Visibility.Hidden;
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
                linea1.Visibility = Visibility.Visible;
                linea2.Visibility = Visibility.Visible;
                start.Visibility = Visibility.Visible;
                finish.Visibility = Visibility.Visible;
                btnAvanti.Visibility = Visibility.Visible;
                caricaImmagini();
            }
        }
        private void conteggioPunti()
        {
            string s = "";
            int pos2 = 0;
            while (s.ElementAt(0) == 'M')
            {
                s = c.prendi();
                pos2++;
            }
            secondiEsterno = Convert.ToInt32(secondi.Content.ToString());
            if (pos2 < 100)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                Uesterno.numMonete -= 10;
            }
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
        }
        private void caricaImmagini()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + Ulocale.skin + ".png");
            bitmap.EndInit();
            locale.Source = bitmap;
        }
        private void spostaSkin()
        {
            Canvas.SetLeft(locale, posizione * 5);
            Canvas.SetTop(locale, 20);
            Canvas.SetLeft(locale, posizione * 5);
            Canvas.SetTop(locale, 20);
        }
        private void btnAvanti_Click(object sender, RoutedEventArgs e)
        {
            posizione++;
            c.BufferInviare.Add("M;avanti");
            secondiLocale = Convert.ToInt32(secondi.Content);
            spostaSkin();
            if (posizione == 100)
            {
                conteggioPunti();
                mappa.Show();
                this.Hide();
            }
        }
    }
}
