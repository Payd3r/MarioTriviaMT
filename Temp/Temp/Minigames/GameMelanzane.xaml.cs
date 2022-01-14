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
        Minimappa mappa;
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        int pos1;
        int pos2;
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
            pos1 = 0;
            pos2 = 0;
            //nascondi
            linea1.Visibility = Visibility.Hidden;
            linea2.Visibility = Visibility.Hidden;
            start.Visibility = Visibility.Hidden;
            finish.Visibility = Visibility.Hidden;
            btnAvanti.Visibility = Visibility.Hidden;
            //inizio minigioco
            content.Content = "Il minigioco inizia a 10 sec ";
            Thread t2 = new Thread(thread);
            t2.Start();
        }
        private void thread()
        {
            Thread.Sleep(10000);
            this.Dispatcher.Invoke(() =>
            {
                //visualizza
                linea1.Visibility = Visibility.Visible;
                linea2.Visibility = Visibility.Visible;
                start.Visibility = Visibility.Visible;
                finish.Visibility = Visibility.Visible;
                btnAvanti.Visibility = Visibility.Visible;
                caricaImmagini();
            });
            string s = "";
            while (true)
            {
                if (pos1 > 55)
                {
                    MessageBox.Show("Hai vinto!");
                    Ulocale.numMonete += 10;
                    Uesterno.numMonete -= 10;
                    break;
                }
                if (pos2 > 55)
                {
                    MessageBox.Show("Hai perso!");
                    Ulocale.numMonete -= 10;
                    Uesterno.numMonete += 10;
                    break;
                }
                if (c.BufferRicevuti.Count > 0)
                {
                    s = c.prendi();
                    if (s.ElementAt(0) == 'M')
                        pos2++;
                    this.Dispatcher.Invoke(() =>
                    {
                        txtPosizione.Content = pos1 + " | " + pos2;
                        spostaSkin();
                    });
                }
            }
            this.Dispatcher.Invoke(() =>
            {
                mappa.Show();
                this.Close();
            });
        }
        private void caricaImmagini()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + Ulocale.skin + ".png");
            bitmap.EndInit();
            locale.Source = bitmap;
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + Uesterno.skin + ".png");
            bitmap.EndInit();
            esterno.Source = bitmap;
            spostaSkin();
        }
        private void spostaSkin()
        {
            Canvas.SetLeft(locale, pos1 * 7);
            Canvas.SetTop(locale, 20);
            Canvas.SetLeft(esterno, pos2 * 7);
            Canvas.SetTop(esterno, 120);
        }
        private void btnAvanti_Click(object sender, RoutedEventArgs e)
        {
            pos1++;
            txtPosizione.Content = pos1 + " | " + pos2;
            c.BufferInviare.Add("M;avanti");
            spostaSkin();
        }
    }
}
