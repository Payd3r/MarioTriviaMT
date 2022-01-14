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
    public partial class GameLato : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Random rnd;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        DispatcherTimer dispatcherTimer;
        Stopwatch stopWatch;
        TimeSpan ts;
        int scelta;
        int estrazione;
        int i = 0;
        string[] vettore = { "", "", "", "" };

        string s = "";
        public GameLato()
        {
            InitializeComponent();
        }
        public GameLato(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;

            c = cond;
            mappa = ma;
            scelta = 0;
            stopWatch = new Stopwatch();
            dispatcherTimer = new DispatcherTimer();
            stopWatch.Start();
            dispatcherTimer.Start();
            content.Content = "Il minigioco inizia a 10 sec ";
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            btn1.Visibility = Visibility.Hidden;
            btn2.Visibility = Visibility.Hidden;
            btn3.Visibility = Visibility.Hidden;
            btn4.Visibility = Visibility.Hidden;
            Ulocale.turno = true;
            if (Ulocale.turno)
                 c.BufferInviare.Add("P;" + creavettore());
            else
            {
                string[] messa = c.prendi().Split(';');
                if (messa[0] == "P")
                    vettore = messa[1].Split(',');
            }
        }
        private string creavettore()
        {
            s = "P;";
            for (int i = 0; i < 4; i++)
            {
                Random rnd = new Random();
                estrazione = rnd.Next(1, 5);
                if (estrazione == 1)
                {
                    vettore[i] = "1";
                    s += "1,";
                }
                if (estrazione == 2)
                {
                    vettore[i] = "2";
                    s += "2,";
                }
                if (estrazione == 3)
                {
                    vettore[i] = "3";
                    s += "3,";
                }
                if (estrazione == 4)
                {
                    vettore[i] = "4";
                    s += "4,";
                }
            }
            return s;
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
            btn1.Visibility = Visibility.Visible;
            btn2.Visibility = Visibility.Visible;
            btn3.Visibility = Visibility.Visible;
            btn4.Visibility = Visibility.Visible;
            content.Content = "Seleziona la tua mossa in 10 sec";
            secondi.Content = "";
            if(i==0)
            gioca();
        }
        void gioca()
        {
            if (vettore[i] == "1")
            {
                BitmapImage bitmap = new BitmapImage();
                //principale
                img1.Source = null;
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia2.png");
                bitmap.EndInit();
                img2.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia3.png");
                bitmap.EndInit();
                img3.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia4.png");
                bitmap.EndInit();
                img4.Source = bitmap;
            }
            else if (vettore[i] == "2")
            {
                BitmapImage bitmap = new BitmapImage();
                //principale
                img2.Source = null;
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia1.png");
                bitmap.EndInit();
                img1.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia3.png");
                bitmap.EndInit();
                img3.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia4.png");
                bitmap.EndInit();
                img4.Source = bitmap;
            }
            else if (vettore[i] == "3")
            {
                BitmapImage bitmap = new BitmapImage();
                //principale
                img3.Source = null;
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia2.png");
                bitmap.EndInit();
                img2.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia1.png");
                bitmap.EndInit();
                img1.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia4.png");
                bitmap.EndInit();
                img4.Source = bitmap;
            }
            else if (vettore[i] == "4")
            {
                BitmapImage bitmap = new BitmapImage();
                //principale
                img4.Source = null;
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia2.png");
                bitmap.EndInit();
                img2.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia3.png");
                bitmap.EndInit();
                img3.Source = bitmap;
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia1.png");
                bitmap.EndInit();
                img1.Source = bitmap;
            }

            content.Content = "Scegli la tua mossa! Entro ";            
            i++;
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (i == 4)
            {
                 string[] s = c.prendi().Split(';');
                if (s[0] == "L")
                  Uesterno.numMonete += Convert.ToInt32(s[1]);
                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 3;
                if (scelta == Convert.ToInt32(vettore[i]))
                    Ulocale.numMonete += 2;
                else
                    Ulocale.numMonete -= 2;
                gioca();
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (i == 4) {
                string[] s = c.prendi().Split(';');
                if (s[0] == "L")
                    Uesterno.numMonete += Convert.ToInt32(s[1]);
                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 1;
                if (scelta == Convert.ToInt32(vettore[i]))
                    Ulocale.numMonete += 2;
                else
                    Ulocale.numMonete -= 2;
                gioca();
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (i == 4)
            {
                string[] s = c.prendi().Split(';');
                if (s[0] == "L")
                    Uesterno.numMonete += Convert.ToInt32(s[1]);
                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 4;
                if (scelta == Convert.ToInt32(vettore[i]))
                    Ulocale.numMonete += 2;
                else
                    Ulocale.numMonete -= 2;
                gioca();
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (i == 4)
            {
                string[] s = c.prendi().Split(';');
                if (s[0] == "L")
                    Uesterno.numMonete += Convert.ToInt32(s[1]);
                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 2;
                if (scelta == Convert.ToInt32(vettore[i]))
                    Ulocale.numMonete += 2;
                else
                    Ulocale.numMonete -= 2;
                gioca();
            }
        }
    }
}
