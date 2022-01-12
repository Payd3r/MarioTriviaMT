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
    public partial class GameTris : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        bool puoiGiocare;
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
            puoiGiocare = false;
            c = cond;
            mappa = ma;
            scelta = 0;
            arr = new string[] { "", "", "", "", "", "", "", "", "" };
            Thread tempo = new Thread(Timer);
            tempo.Start();
            gioca();
            mappa.Show();
            this.Hide();
        }
        private void gioca()
        {
            int vittoria = 0;
            for (int j = 0; j < 2 || vittoria == 0; j++)
            {
                btn1.IsEnabled = true;
                btn2.IsEnabled = true;
                btn3.IsEnabled = true;
                btn4.IsEnabled = true;
                btn5.IsEnabled = true;
                btn6.IsEnabled = true;
                btn7.IsEnabled = true;
                btn8.IsEnabled = true;
                btn9.IsEnabled = true;
                btn1.Content = "";
                btn2.Content = "";
                btn3.Content = "";
                btn4.Content = "";
                btn5.Content = "";
                btn6.Content = "";
                btn7.Content = "";
                btn8.Content = "";
                btn9.Content = "";
                for (int i = 0; i < 8; i++)
                {
                    if (Ulocale.turno)
                    {
                        content.Content = "Scegli la tua mossa! Entro ";
                        Thread tempo = new Thread(Timer);
                        tempo.Start();
                        puoiGiocare = true;
                        //potrebbe bloccare il thread corrente quindi non funzionano i bottoni
                        tempo.Join();
                        puoiGiocare = false;
                        //aspetto il messaggio e aggiorno i punti dell'avversario
                        c.BufferInviare.Add("T;" + scelta);
                        Ulocale.turno = false;
                        arr[scelta] = "o";
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
                    }
                    else
                    {
                        string[] s = c.prendi().Split(';');
                        scelta = Convert.ToInt32(s[1]);
                        arr[scelta] = "x";
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
                    vittoria = CheckWin();
                    if (vittoria == 1)
                    {
                        Ulocale.numMonete += 10;
                        break;
                    }
                    else if (vittoria == 2)
                    {
                        Uesterno.numMonete += 10;
                        break;
                    }
                }
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
        private int CheckWin()
        {
            if (arr[1] == arr[2] && arr[2] == arr[3])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            //Winning Condition For Second Row
            else if (arr[4] == arr[5] && arr[5] == arr[6])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            //Winning Condition For Third Row
            else if (arr[6] == arr[7] && arr[7] == arr[8])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            else if (arr[1] == arr[4] && arr[4] == arr[7])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            //Winning Condition For Second Column
            else if (arr[2] == arr[5] && arr[5] == arr[8])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            //Winning Condition For Third Column
            else if (arr[3] == arr[6] && arr[6] == arr[9])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            else if (arr[1] == arr[5] && arr[5] == arr[9])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            else if (arr[3] == arr[5] && arr[5] == arr[7])
            {
                if (arr[1] == "o")
                    return 1;
                else
                    return 2;
            }
            else
                return 0;
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

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 5;
        }

        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 6;
        }

        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 7;
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 8;
        }

        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            if (puoiGiocare)
                scelta = 9;
        }
    }
}
