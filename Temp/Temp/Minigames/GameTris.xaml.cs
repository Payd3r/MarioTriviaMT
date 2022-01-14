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
    public partial class GameTris : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        string[] vettore = { "", "", "", "", "", "" };
        DispatcherTimer dispatcherTimer;
        Stopwatch stopWatch;
        TimeSpan ts;
        int vittoria = 0;
        string[] arr;
        int scelta;
        public GameTris()
        {
            InitializeComponent();
        }
        public GameTris(Utente a, Utente b, Condivisa cond, Minimappa ma)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            stopWatch = new Stopwatch();
            dispatcherTimer = new DispatcherTimer();
            stopWatch.Start();
            dispatcherTimer.Start();
            content.Content = "Il minigioco inizia a 10 sec ";
            btn1.Visibility = Visibility.Hidden;
            btn2.Visibility = Visibility.Hidden;
            btn3.Visibility = Visibility.Hidden;
            btn4.Visibility = Visibility.Hidden;
            btn5.Visibility = Visibility.Hidden;
            btn6.Visibility = Visibility.Hidden;
            btn7.Visibility = Visibility.Hidden;
            btn8.Visibility = Visibility.Hidden;
            btn9.Visibility = Visibility.Hidden;
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            c = cond;
            mappa = ma;
            scelta = 0;
            arr = new string[] { "", "", "", "", "", "", "", "", "" };
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
            btn7.Visibility = Visibility.Visible;
            btn8.Visibility = Visibility.Visible;
            btn9.Visibility = Visibility.Visible;
            content.Content = "Seleziona la tua mossa in 10 sec";
            secondi.Content = "";
        }
        private void gioca()
        {
            vittoria = 0;
            if (Ulocale.turno)
            {
                content.Content = "Scegli la tua mossa! Entro ";
                //aspetto il messaggio e aggiorno i punti dell'avversario
                c.BufferInviare.Add("T;" + scelta);
                Ulocale.turno = false;
                arr[scelta - 1] = "o";
                if (scelta == 1)
                {
                    btn1.IsEnabled = false;
                    btn1.Content = "o";
                }
                if (scelta == 2)
                {
                    btn2.IsEnabled = false;
                    btn2.Content = "o";
                }
                if (scelta == 3)
                {
                    btn3.IsEnabled = false;
                    btn3.Content = "o";
                }
                if (scelta == 4)
                {
                    btn4.IsEnabled = false;
                    btn4.Content = "o";
                }
                if (scelta == 5)
                {
                    btn5.IsEnabled = false;
                    btn5.Content = "o";
                }
                if (scelta == 6)
                {
                    btn6.IsEnabled = false;
                    btn6.Content = "o";
                }
                if (scelta == 7)
                {
                    btn7.IsEnabled = false;
                    btn7.Content = "o";
                }
                if (scelta == 8)
                {
                    btn8.IsEnabled = false;
                    btn8.Content = "o";
                }
                if (scelta == 9)
                {
                    btn9.IsEnabled = false;
                    btn9.Content = "o";
                }
                gioca();
            }
            else
            {
                string[] s = c.prendi().Split(';');
                scelta = Convert.ToInt32(s[1]);
                Ulocale.turno = true;
                arr[scelta - 1] = "x";
                if (scelta == 1)
                {
                    btn1.IsEnabled = false;
                    btn1.Content = "x";
                }
                if (scelta == 2)
                {
                    btn2.IsEnabled = false;
                    btn2.Content = "x";
                }
                if (scelta == 3)
                {
                    btn3.IsEnabled = false;
                    btn3.Content = "x";
                }
                if (scelta == 4)
                {
                    btn4.IsEnabled = false;
                    btn4.Content = "x";
                }
                if (scelta == 5)
                {
                    btn5.IsEnabled = false;
                    btn5.Content = "x";
                }
                if (scelta == 6)
                {
                    btn6.IsEnabled = false;
                    btn6.Content = "x";
                }
                if (scelta == 7)
                {
                    btn7.IsEnabled = false;
                    btn7.Content = "x";
                }
                if (scelta == 8)
                {
                    btn8.IsEnabled = false;
                    btn8.Content = "x";
                }
                if (scelta == 9)
                {
                    btn9.IsEnabled = false;
                    btn9.Content = "x";
                }
            }
        }

        private int CheckWin()
        {
            if (arr[1 - 1] == arr[2 - 1] && arr[2 - 1] == arr[3 - 1] && arr[2 - 1] != "")
                if (arr[2 - 1] == "o")
                    return 1;
                else
                    return 2;
            //Winning Condition For Second Row
            else if (arr[4 - 1] == arr[5 - 1] && arr[5 - 1] == arr[6 - 1] && arr[5 - 1] != "")
                if (arr[5 - 1] == "o")
                    return 1;
                else
                    return 2;
            //Winning Condition For Third Row
            else if (arr[6 - 1] == arr[7 - 1] && arr[7 - 1] == arr[8 - 1] && arr[7 - 1] != "")
                if (arr[7 - 1] == "o")
                    return 1;
                else
                    return 2;
            else if (arr[1 - 1] == arr[4 - 1] && arr[4 - 1] == arr[7 - 1] && arr[4 - 1] != "")
                if (arr[4 - 1] == "o")
                    return 1;
                else
                    return 2;
            //Winning Condition For Second Column
            else if (arr[2 - 1] == arr[5 - 1] && arr[5 - 1] == arr[8 - 1] && arr[5 - 1] != "")
                if (arr[5 - 1] == "o")
                    return 1;
                else
                    return 2;
            //Winning Condition For Third Column
            else if (arr[3 - 1] == arr[6 - 1] && arr[6 - 1] == arr[9 - 1] && arr[6 - 1] != "")
                if (arr[6 - 1] == "o")
                    return 1;
                else
                    return 2;
            else if (arr[1 - 1] == arr[5 - 1] && arr[5 - 1] == arr[9 - 1] && arr[5 - 1] != "")
                if (arr[5 - 1] == "o")
                    return 1;
                else
                    return 2;
            else if (arr[3 - 1] == arr[5 - 1] && arr[5 - 1] == arr[7 - 1] && arr[5 - 1] != "")
                if (arr[5 - 1] == "o")
                    return 1;
                else
                    return 2;
            else
                return 0;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            scelta = 1;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                Ulocale.numMonete += 10;
                MessageBox.Show("Hai vinto!");
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            scelta = 2;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            scelta = 3;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            scelta = 4;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            scelta = 5;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }
        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            scelta = 6;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            scelta = 7;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            scelta = 8;
            gioca();
            vittoria = CheckWin();
            if (vittoria == 1)
            {
                MessageBox.Show("Hai vinto!");
                Ulocale.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
            else if (vittoria == 2)
            {
                MessageBox.Show("Hai perso!");
                Uesterno.numMonete += 10;
                mappa.Show();
                this.Hide();
            }
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            {
                scelta = 9;
                gioca();
                vittoria = CheckWin();
                if (vittoria == 1)
                {
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
                else if (vittoria == 2)
                {
                    MessageBox.Show("Hai perso!");
                    Uesterno.numMonete += 10;
                    mappa.Show();
                    this.Hide();
                }
            }
        }
    }
}
