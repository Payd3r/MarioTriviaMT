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
using System.Windows.Threading;

namespace Temp.Minigames
{
    public partial class GamecartaForbiceSasso : Window
    {
        Minimappa mappa;
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        string scelta;
        DispatcherTimer dispatcherTimer;
        Stopwatch stopWatch;
        TimeSpan ts;
        public GamecartaForbiceSasso()
        {
            InitializeComponent();
        }
        public GamecartaForbiceSasso(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            scelta = "";
            Ulocale = a;
            Uesterno = b;
            c = cond;
            mappa = ma;
            stopWatch = new Stopwatch();
            dispatcherTimer = new DispatcherTimer();
            stopWatch.Start();
            dispatcherTimer.Start();
            content.Content = "Il minigioco inizia a 10 sec ";
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            sasso.Visibility = Visibility.Hidden;
            carta.Visibility = Visibility.Hidden;
            forbice.Visibility = Visibility.Hidden;
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
            sasso.Visibility = Visibility.Visible;
            carta.Visibility = Visibility.Visible;
            forbice.Visibility = Visibility.Visible;
            btnInvio.Visibility = Visibility.Visible;
            content.Content = "Seleziona la tua mossa in 10 sec";
            secondi.Content = "";
        }
        private void risultato(int temp)
        {
            if (temp == 1)
            {
                Ulocale.numMonete += 10;
                Uesterno.numMonete -= 10;
            }
            else if (temp == 2)
            {
                Ulocale.numMonete -= 10;
                Uesterno.numMonete += 10;
            }
            mappa.Show();
            this.Hide();
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

        private void btnInvio_Click(object sender, RoutedEventArgs e)
        {
            int ris = 0;
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
                    ris = 1;
                }
                else if (s[1] == scelta)
                {
                    MessageBox.Show("Hai pareggiato!");
                    ris = 0;
                }
                else if (s[1] == "c" && scelta == "f")
                {
                    MessageBox.Show("Hai vinto!");
                    ris = 1;
                }
                else if (s[1] == "c" && scelta == "s")
                {
                    MessageBox.Show("Hai perso!");
                    ris = 2;
                }
                else if (s[1] == "f" && scelta == "c")
                {
                    MessageBox.Show("Hai perso!");
                    ris = 2;
                }
                else if (s[1] == "f" && scelta == "s")
                {
                    MessageBox.Show("Hai vinto!");
                    ris = 1;
                }
                else if (s[1] == "s" && scelta == "c")
                {
                    MessageBox.Show("Hai vinto!");
                    ris = 1;
                }
                else if (s[1] == "s" && scelta == "f")
                {
                    MessageBox.Show("Hai perso!");
                    ris = 2;
                }
            ris = 2;
            risultato(ris);
        }

        private void carta_Checked(object sender, RoutedEventArgs e)
        {
            scelta = "c";
            visualizzaImmagine("carta", true);
        }

        private void forbice_Checked(object sender, RoutedEventArgs e)
        {
            scelta = "f";
            visualizzaImmagine("forbice", true);
        }

        private void sasso_Checked(object sender, RoutedEventArgs e)
        {
            scelta = "s";
            visualizzaImmagine("sasso", true);
        }
    }
}
