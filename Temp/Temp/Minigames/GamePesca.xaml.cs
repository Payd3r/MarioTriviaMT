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
    public partial class GamePesca : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        string s = "";
        int turno;
        int estrazione;
        int scelta;
        string[] vettore = { "", "", "", "", "", "" };
        DispatcherTimer dispatcherTimer;
        Stopwatch stopWatch;
        TimeSpan ts;
        public GamePesca()
        {
            InitializeComponent();
        }
        public GamePesca(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            turno = 0;
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
           
            if (Ulocale.turno)
            {
                c.BufferInviare.Add("P;" + creavettore());
            }
            
            else
            {
                string[] s = c.prendi().Split(';');
                 if (s[0] == "P")
                   vettore = s[1].Split(',');
            }




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
            btn5.Visibility = Visibility.Visible;
            btn6.Visibility = Visibility.Visible;
            content.Content = "Seleziona la tua mossa in 10 sec";
            secondi.Content = "";
            if(turno==0)
            gioca();
        }
        private string creavettore()
        {
            int numV = 0;
            s = "P;";

            for (int i = 0; i < 6; i++)
            {

                Random rnd = new Random();
                estrazione = rnd.Next(0, 2);
                if (estrazione == 1 && numV < 2)
                {
                    vettore[i] = "f";
                    s += "f,";

                    numV++;
                }
                else
                {
                    s += "t,";
                    vettore[i] = "t";
                }
            }
            if (numV < 2)
            {
                string riprova = creavettore();
                return riprova;
            }else
            return s;
        }


        private void gioca()
        {


            if (Ulocale.turno)
            {
                content.Content = "Scegli la tua mossa! Entro ";


                //aspetto il messaggio e aggiorno i punti dell'avversario
                c.BufferInviare.Add("E;" + scelta);
                Ulocale.turno = false;
            }
            else
            {
                string[] mess = c.prendi().Split(';');
                if (mess[1] == "perso")
                {
                    MessageBox.Show("Hai vinto! L'avversario non ha scelto");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    turno = 5;
                }
                else
                    scelta = Convert.ToInt32(mess[1]);
                Ulocale.turno = true;
            }

            if (scelta != 0)
            {
                if (scelta == 1)
                {
                    btn1.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = new BitmapImage();
                    //principale

                    i1.Source = null;
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\pesce.png");
                    bitmap.EndInit();
                    i1.Source = bitmap;
                }

                if (scelta == 2)
                {
                    btn2.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = new BitmapImage();
                    //principale

                    i2.Source = null;
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\pesce.png");
                    bitmap.EndInit();
                    i2.Source = bitmap;
                }
                if (scelta == 3)
                {
                    btn3.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = new BitmapImage();
                    //principale

                    i3.Source = null;
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\pesce.png");
                    bitmap.EndInit();
                    i3.Source = bitmap;
                }
                if (scelta == 4)
                {
                    btn4.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = new BitmapImage();
                    //principale

                    i4.Source = null;
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\pesce.png");
                    bitmap.EndInit();
                    i4.Source = bitmap;
                }
                if (scelta == 5)
                {
                    btn5.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = new BitmapImage();
                    //principale

                    i5.Source = null;
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\pesce.png");
                    bitmap.EndInit();
                    i5.Source = bitmap;
                }
                if (scelta == 6)
                {
                    btn6.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = new BitmapImage();
                    //principale

                    i6.Source = null;
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\pesce.png");
                    bitmap.EndInit();
                    i6.Source = bitmap;
                }


            }
            else
            {
                 c.BufferInviare.Add("e;" + "perso");
                MessageBox.Show("Hai perso! Non hai scelto in tempo");
                Ulocale.numMonete -= 10;
                Uesterno.numMonete += 10;
                turno = 5;

            }
            turno++;
            if (turno > 5)
            {
                mappa.Show();
                this.Hide();
            }

        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (turno == 6)
            {

                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 1;
                if (vettore[scelta - 1] == "f" && Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai perso!");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
                if (vettore[scelta - 1] == "f" && !Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    mappa.Show();
                    this.Hide();
                }
                gioca();
            }

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {

            if (turno == 6)
            {

                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 2;
                if (vettore[scelta - 1] == "f" && Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai perso!");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
                if (vettore[scelta - 1] == "f" && !Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    mappa.Show();
                    this.Hide();
                }
                gioca();
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {

            if (turno == 6)
            {

                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 3;
                if (vettore[scelta - 1] == "f" && Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai perso!");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
                if (vettore[scelta - 1] == "f" && !Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    mappa.Show();
                    this.Hide();
                }
                gioca();
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {

            if (turno == 6)
            {

                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 4;
                if (vettore[scelta - 1] == "f" && Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai perso!");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
                if (vettore[scelta - 1] == "f" && !Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    mappa.Show();
                    this.Hide();
                }
                gioca();
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (turno == 6)
            {

                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 5;
                if (vettore[scelta - 1] == "f" && Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai perso!");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
                if (vettore[scelta - 1] == "f" && !Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    mappa.Show();
                    this.Hide();
                }
                gioca();
            }
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {

            if (turno == 6)
            {

                mappa.Show();
                this.Hide();
            }
            else
            {
                scelta = 6;
               if (vettore[scelta - 1] == "f" && Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai perso!");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
                if (vettore[scelta - 1] == "f" && !Ulocale.turno)
                {
                    turno = 5;
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    mappa.Show();
                    this.Hide();
                }
                gioca();
            }
        }
    }
}
