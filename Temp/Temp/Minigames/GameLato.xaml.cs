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
    public partial class GameLato : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        bool puoiGiocare;
        int scelta;
        string[] vettore;
        public GameLato()
        {
            InitializeComponent();
        }
        public GameLato(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            puoiGiocare = false;
            c = cond;
            mappa = ma;
            scelta = 0;
            if (Ulocale.turno)
                c.BufferInviare.Add("P;" + creavettore());
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
        private string creavettore()
        {
            string s = "";

            for (int i = 0; i < 5; i++)
            {

                Random rnd = new Random();
                int estrazione = rnd.Next(0, 4);
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
        void gioca()
        {
            for (int i = 0; i < 4; i++)
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
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia3.png");
                    bitmap.EndInit();
                    img3.Source = bitmap;
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
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia3.png");
                    bitmap.EndInit();
                    img3.Source = bitmap;
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
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia1.png");
                    bitmap.EndInit();
                    img1.Source = bitmap;
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
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia3.png");
                    bitmap.EndInit();
                    img3.Source = bitmap;
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\freccia1.png");
                    bitmap.EndInit();
                    img1.Source = bitmap;
                }

                content.Content = "Scegli la tua mossa! Entro ";
                Thread tempo = new Thread(Timer);
                tempo.Start();
                puoiGiocare = true;
                //potrebbe bloccare il thread corrente quindi non funzionano i bottoni
                tempo.Join();
                puoiGiocare = false;
                if (scelta == Convert.ToInt32(vettore[i]))
                {
                    Ulocale.numMonete += 2;
                }
                else
                {
                    Ulocale.numMonete -= 2;
                }
            }
            string[] s = c.prendi().Split(';');
            if (s[0] == "L")
                Uesterno.numMonete += Convert.ToInt32(s[1]);
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 3;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 1;
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 4;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 2;
        }
    }
}
