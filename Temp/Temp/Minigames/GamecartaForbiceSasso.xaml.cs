using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Threading;
using System.Diagnostics;

namespace Temp.Minigames
{
    public partial class GamecartaForbiceSasso : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Minimappa mappa;
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        string scelta;
        bool puoiGiocare;
        public GamecartaForbiceSasso()
        {
            InitializeComponent();
        }
        public GamecartaForbiceSasso(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            puoiGiocare = false;
            scelta = "";
            Ulocale = a;
            Uesterno = b;
            c = cond;
            mappa = ma;
            //inizio minigioco
            content.Content = "Il minigioco inizia tra ";
            Thread tempo = new Thread(Timer);
            tempo.Start();
            int temp = 0;
            for (int i = 0; i < 3 && temp == 0; i++)
                temp = gioca(); //se 0 = pareggio | 1 = vinto | 2 = perso
            risultato(temp);
        }
        private int gioca()
        {
            content.Content = "Scegli la tua mossa! Entro ";
            Thread tempo = new Thread(Timer);
            tempo.Start();
            puoiGiocare = true;
            //potrebbe bloccare il thread corrente quindi non funzionano i bottoni
            tempo.Join();
            puoiGiocare = false;
            if (scelta == "")
                MessageBox.Show("Non hai scelto niente! Hai perso");
            c.BufferInviare.Add("S;" + scelta);
            string[] s = c.prendi().Split(';');
            if (s[1] == "c")
                visualizzaImmagine("carta", false);
            else if (s[1] == "f")
                visualizzaImmagine("forbice", false);
            else if (s[1] == "s")
                visualizzaImmagine("sasso", false);
            if (s[0] == "S")
                if (s[1] == "")
                {
                    MessageBox.Show("Hai vinto! L'altro non ha scelto niente.");
                    return 1;
                }
                else if (s[1] == scelta)
                {
                    MessageBox.Show("Hai pareggiato!");
                    return 0;
                }
                else if (s[1] == "c" && scelta == "f")
                {
                    MessageBox.Show("Hai vinto!");
                    return 1;
                }
                else if (s[1] == "c" && scelta == "s")
                {
                    MessageBox.Show("Hai perso!");
                    return 2;
                }
                else if (s[1] == "f" && scelta == "c")
                {
                    MessageBox.Show("Hai perso!");
                    return 2;
                }
                else if (s[1] == "f" && scelta == "s")
                {
                    MessageBox.Show("Hai vinto!");
                    return 1;
                }
                else if (s[1] == "s" && scelta == "c")
                {
                    MessageBox.Show("Hai vinto!");
                    return 1;
                }
                else if (s[1] == "s" && scelta == "f")
                {
                    MessageBox.Show("Hai perso!");
                    return 2;
                }
            return 2;
        }
        private void risultato(int temp)
        {
            if (temp == 1)
                Ulocale.numMonete += 10;
            else if (temp == 2)
                Ulocale.numMonete -= 10;
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
        private void visualizzaImmagine(string k, bool a)
        {
            string s = AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + k + ".png";
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(s);
            bitmap.EndInit();
            if (a)
                immagineMia.Source = bitmap;
            else
                immagineAltro.Source = bitmap;
        }
        private void btnCarta_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = "c";
                visualizzaImmagine("carta", true);
            }
        }

        private void btnForbice_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = "f";
                visualizzaImmagine("forbice", true);
            }
        }

        private void btnSasso_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
            {
                scelta = "s";
                visualizzaImmagine("sasso", true);
            }
        }
    }
}
