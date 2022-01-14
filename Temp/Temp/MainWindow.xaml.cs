using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Temp.Minigames;

namespace Temp
{
    public partial class MainWindow : Window
    {
        Condivisa c;
        Thread t;
        public MainWindow()
        {
            InitializeComponent();
            c = new Condivisa();
            t = new Thread(controlla);
            t.Start();
        }
        private void controlla()
        {
            string s = c.prendi();
            if (s.ElementAt(0) == 'a')
            {
                //inizia procedura connessione
                //ricevo a;indirizzoMittente
                c.indirizzo = s.Split(';')[1];
                c.startServer();
                ConnessioneDaEsterno();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            c.BufferInviare.Add("a;127.0.0.1");
            c.indirizzo = txtIndirizzo.Text;
            c.startServer();
            t.Abort();
            ConnessioneDaInterno();
        }
        private void ConnessioneDaEsterno()
        {
            //chiedo ad utente se voglio iniziare a giocare
            //invio y; | n;
            //aspetto che mi rispoda
            //ricevo y; | n;
            //aspetto che mi risponda con le sue info nome;skin
            MessageBoxResult dialogResult = MessageBox.Show("Vuoi accettare la connessione?", "Nuova connessione", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                c.BufferInviare.Add("y;");
                MessageBox.Show("Connessione iniziata!");
                this.Dispatcher.Invoke(() =>
                {
                    FasePick a = new FasePick(false, c);
                    a.Show();
                    this.Close();
                });
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                c.BufferInviare.Add("n;");
                MessageBox.Show("Connessione rifiutata!");
                txtIndirizzo.Text = "";
                c.indirizzo = "";
            }
        }
        private void ConnessioneDaInterno()
        {
            //dopo aver mandato una richiesta di connessione attendo la risposta
            //invio a; 
            //ricevo y; | n;
            //invio y; | n; se no errori
            //inizio la connessione e apro la pick phase
            string s = c.prendi();
            if (s.ElementAt(0) == 'y')
            {
                c.BufferInviare.Add("y;");
                MessageBox.Show("Connessione iniziata!");
            }
            else if (s.ElementAt(0) == 'n')
            {
                MessageBox.Show("Connessione rifiutata!");
                txtIndirizzo.Text = "";
                c.indirizzo = "";
            }
            FasePick a = new FasePick(true, c);
            a.Show();
            this.Close();
        }
    }
}