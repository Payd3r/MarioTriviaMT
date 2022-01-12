using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using Temp.Minigames;

namespace Temp
{
    public partial class Minimappa : Window
    {
        Condivisa c;
        Utente Ulocale = new Utente();
        Utente Uesterno = new Utente();
        int estrazione;
        Random rnd;
        public Minimappa()
        {
            InitializeComponent();
        }

        public Minimappa(string nome1, string skin1, string nome2, string skin2, Condivisa cond)
        {
            //turno dell'avversario
            InitializeComponent();
            c = cond;
            estrazione = 0;
            rnd = new Random();
            c.startServer();
            //inserisco informazioni utente locale
            Ulocale = new Utente(nome1, skin1);
            //richiedi informazioni per crare l'altro utente(nick, skin, turno)
            Uesterno = new Utente(nome2, skin2);
            //imposto il turno
            Ulocale.turno = true;
            //inizializzazione parte visiva
            inizializeParteVisiva();

        }
        public Minimappa(string nome1, string skin1, Condivisa cond)
        {
            //e' il mio turno
            InitializeComponent();
            c = cond;
            //inserisco informazioni utente locale
            Ulocale = new Utente(nome1, skin1);
            //richiedi informazioni per crare l'altro utente(nick, skin, turno)
            do { } while (c.BufferRicevuti == "");
            string s = c.BufferRicevuti;
            c.BufferRicevuti = "";
            if (s.ElementAt(0) == 'p')
            {
                string[] vet = s.Split(';');
                Uesterno = new Utente(vet[1], vet[2]);
            }
            else
            {
                MessageBox.Show("Errore mancano le info!");
                Uesterno = new Utente("errore", "1");
            }
            //imposto il turno in base a chi ha richiesto la connessione
            Ulocale.turno = true;
        }
        private void inizializeParteVisiva()
        {
            nick1.Content = Ulocale.nick;
            monete1.Content = Ulocale.numMonete;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + Ulocale.skin + ".png");
            bitmap.EndInit();
            skin1.Source = bitmap;
            nick2.Content = Uesterno.nick;
            monete2.Content = Uesterno.numMonete;
            bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\File\\" + Uesterno.skin + ".png");
            bitmap.EndInit();
            skin2.Source = bitmap;
            Canvas.SetTop(skin1, 0);
            Canvas.SetLeft(skin1, Ulocale.posMappa * 30);
            Canvas.SetTop(skin2, 0);
            Canvas.SetLeft(skin2, (Uesterno.posMappa * 30) + 30);
        }

        private void btnDado_Click(object sender, RoutedEventArgs e)
        {
            //se tocca all'utente locale -> MessageBox con il num uscito
            //invio il num uscito ad esterno
            //sposto anche in locale
            if (Ulocale.turno)
            {
                Random rnd = new Random();
                int estrazione = rnd.Next(1, 7);
                MessageBox.Show("Il numero uscito e' " + estrazione + "!");
                //c.BufferInviare.Add("s;" + estrazione);
                int num = muoviPersonaggi(estrazione);
                controlloPos(num);
                Ulocale.turno = false;
            }
            else
                MessageBox.Show("Non e' il tuo turno!");
        }
        private int muoviPersonaggi(int num)
        {
            //controlla la posizione all'interno di locale ed esterno e riposiziona le skin nella minimappa
            if (num > 0)
            {
                bool check = false;
                for (int i = 0; i < num && !check; i++)
                {
                    Ulocale.posMappa++;
                    Uesterno.posMappa++;
                    if (Ulocale.posMappa == 53)
                        //se e' ad un bivio si ferma
                        return num - i;
                }
                if (!check)
                    estrazione = 0;
            }
            muovi();
            return 0;
        }
        private void muovi()
        {
            //sposta avanti di uno            
            Canvas.SetTop(skin1, 0);
            Canvas.SetLeft(skin1, Ulocale.posMappa * 50);
            Canvas.SetTop(skin2, 0);
            Canvas.SetLeft(skin2, (Uesterno.posMappa * 50) + 30);
        }
        private void controlloPos(int num)
        {
            if (Ulocale.posMappa == 1)
            {
                if (Ulocale.turno)
                    //se pos =  -> aggiungo monete a locale
                    Ulocale.numMonete += 10;
            }
            else if (Ulocale.posMappa == 2)
            {
                if (Ulocale.turno)
                {
                    //se pos =  -> aggiorno pos di locale ed invio a esterno
                    Ulocale.posMappa += 2;
                    muoviPersonaggi(2);
                }
                //  invio Ulocale.posMappa;
            }
            else if (Ulocale.posMappa == 3)
            {
                //se pos =  -> apro il form del minigame corrispondente
                //            1) Melanzane
                //            2) Pesca pesci
                //            3) Imbrocca il lato
                //            4) Immagine sgranata
                //            5) Carta forbice e sassi
                //            6) Tris
                switch (Ulocale.posMappa)
                {
                    case 4:
                        GameMelanzane a = new GameMelanzane(Ulocale, Uesterno, c, this);
                        a.Show();
                        break;
                    case 5:
                        GamePesca b = new GamePesca(Ulocale, Uesterno, c, this);
                        b.Show();
                        break;
                    case 6:
                        GameLato l = new GameLato(Ulocale, Uesterno, c, this);
                        l.Show();
                        break;
                    case 7:
                        GameImmagine d = new GameImmagine(Ulocale, Uesterno, c, this);
                        d.Show();
                        break;
                    case 8:
                        GamecartaForbiceSasso e = new GamecartaForbiceSasso(Ulocale, Uesterno, c, this);
                        e.Show();
                        break;
                    case 9:
                        GameTris f = new GameTris(Ulocale, Uesterno, c, this);
                        f.Show();
                        break;
                }
                this.Hide();
            }
            else if (Ulocale.posMappa == 10)
            {
                //se pos =  -> apro il form con una domanda random
                Domande a = new Domande(Ulocale, Uesterno);
                a.Show();
                this.Hide();
                //aspetto di sapere cos'ha fatto esterno
            }
            else if (Ulocale.posMappa == 11)
            {
                if (Ulocale.turno)
                    //se pos =  -> tolgo monete a locale
                    Ulocale.numMonete -= 10;
            }
            else if (Ulocale.posMappa == 12)
            {
                //se pos =  -> 1) aspetto che l'utente locale scelga dove andare
                //             2) aspetto che mi arrivi la scelta da esterno
                if (Ulocale.turno)
                {
                    //tocca a me
                    MessageBoxResult dialogResult = MessageBox.Show("Vuoi proseguire dritto?", "Scelta bivio", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                    {
                        //dritto
                        muoviPersonaggi(num);
                        controlloPos(0);
                    }
                    else if (dialogResult == MessageBoxResult.No)
                    {
                        //giro
                        muoviPersonaggi(num);
                        controlloPos(0);
                    }
                }
            }
        }
    }
}