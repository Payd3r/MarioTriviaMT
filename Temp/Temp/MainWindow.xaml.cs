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
        public MainWindow()
        {
            InitializeComponent();
            c = new Condivisa();                
            Thread t = new Thread(controlla);
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
            //trovare un modo per capire il proprio indirizzo
            c.BufferInviare.Add("a;101.58.47.192");
            c.indirizzo = txtIndirizzo.Text;
            c.startServer();
            ConnessioneDaInterno();
        }
        private void ConnessioneDaEsterno()
        {
            //chiedo ad utente se voglio iniziare a giocare
            //invio y; | n;
            //aspetto che mi rispoda
            //ricevo y; | n;
            //invio y; | n; di conferma
            //aspetto che mi risponda con le sue info nome;skin
            MessageBoxResult dialogResult = MessageBox.Show("Vuoi accettare la connessione?", "Nuova connessione", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                c.BufferInviare.Add("y;");
                string s = c.prendi();
                if (s.ElementAt(0) == 'y')
                {
                    c.BufferInviare.Add("y;");
                    MessageBox.Show("Connessione iniziata!");
                    MessageBox.Show("Attesa pick dell'avversario!");
                    //connessione creata con successo, via richiesta esterna
                }
                else if (s.ElementAt(0) == 'n')
                {
                    MessageBox.Show("Qualcosa e' andato storto!");
                    txtIndirizzo.Text = "";
                    c.indirizzo = "";
                }
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                c.BufferInviare.Add("n;");
                MessageBox.Show("Connessione rifiutata!");
                txtIndirizzo.Text = "";
                c.indirizzo = "";
            }
            FasePick a = new FasePick(false, c);
            a.Show();
            this.Close();
        }
        private void ConnessioneDaInterno()
        {
            //dopo aver mandato una richiesta di connessione attendo la risposta
            //ricevo y; | n;
            //invio y; | n; se no errori
            //ricevo y; | n;
            //inizio la connessione e apro la pick phase
            string s = c.prendi();
            if (s.ElementAt(0) == 'y')
            {
                c.BufferInviare.Add("y;");
                string s1 = c.prendi();
                if (s1.ElementAt(0) == 'y')
                    MessageBox.Show("Connessione iniziata!");
                else if (s1.ElementAt(0) == 'n')
                {
                    MessageBox.Show("Qualcosa e' andato storto!");
                    txtIndirizzo.Text = "";
                    c.indirizzo = "";
                }
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