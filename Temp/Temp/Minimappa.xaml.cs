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
            this.ResizeMode = ResizeMode.NoResize;
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
            string s = c.prendi();
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
            Canvas.SetTop(skin1, 15);
            Canvas.SetLeft(skin1, Ulocale.posMappa + 5);
            Canvas.SetTop(skin2, 15);
            Canvas.SetLeft(skin2, Uesterno.posMappa + 35);
        }

        private void btnDado_Click(object sender, RoutedEventArgs e)
        {
            //se tocca all'utente locale -> MessageBox con il num uscito
            //invio il num uscito ad esterno
            //sposto anche in locale
            if (Ulocale.turno)
            {
                Random rnd = new Random();
                estrazione = rnd.Next(1, 7);
                MessageBox.Show("Il numero uscito e' " + estrazione + "!");
                //c.BufferInviare.Add("s;" + estrazione);                
                int num = muoviPersonaggi(estrazione);
                if (num == -1)
                {
                    MessageBox.Show("Gioco finito!");
                    this.Close();
                }
                while (controlloPos(num)) ;
                //Ulocale.turno = false;
            }
            else
                MessageBox.Show("Non e' il tuo turno!");
        }
        private int muoviPersonaggi(int num)
        {
            //controlla la posizione all'interno di locale ed esterno e riposiziona le skin nella minimappa            
            if (Ulocale.posMappa > 40 && Ulocale.posMappa < 49)
                return -1;
            if (num > 0)
            {
                bool check = false;
                for (int i = 0; i < num && !check; i++)
                {
                    Ulocale.posMappa++;
                    Uesterno.posMappa++;
                    if (Ulocale.posMappa == 59)
                    {
                        Ulocale.posMappa = 7;
                        Uesterno.posMappa = 7;
                    }
                    else if (Ulocale.posMappa == 67)
                    {
                        Ulocale.posMappa = 28;
                        Uesterno.posMappa = 28;
                    }
                    else if (Ulocale.posMappa == 4)
                    {
                        return (num - i) - 1;
                    }
                    else if (Ulocale.posMappa == 21)
                    {
                        return (num - i) - 1;
                    }
                }
                if (!check)
                    estrazione = 0;
            }
            muovi();
            return 0;
        }
        private void muovi()
        {
            int posizione = Ulocale.posMappa;
            if (posizione < 18)
            {
                //prima riga
                Canvas.SetTop(skin1, 10);
                Canvas.SetLeft(skin1, (posizione * 63) - 50);
                Canvas.SetTop(skin2, 10);
                Canvas.SetLeft(skin2, (posizione * 63) - 20);
            }
            else if (posizione > 17 && posizione < 25)
            {
                //verticale
                Canvas.SetTop(skin1, (posizione * 63) - 1050);
                Canvas.SetLeft(skin1, 1030);
                Canvas.SetTop(skin2, (posizione * 63) - 1050);
                Canvas.SetLeft(skin2, 1050);
            }
            else if (posizione > 24 && posizione < 40)
            {
                //linea sotto
                Canvas.SetTop(skin1, 460);
                Canvas.SetLeft(skin1, 1030 - ((posizione - 24) * 63));
                Canvas.SetTop(skin2, 460);
                Canvas.SetLeft(skin2, 1050 - ((posizione - 24) * 63));
            }
            else if (posizione > 39 && posizione < 50)
            {
                //fine
                Canvas.SetTop(skin1, 460);
                Canvas.SetLeft(skin1, 5);
                Canvas.SetTop(skin2, 460);
                Canvas.SetLeft(skin2, 25);
            }
            else if (posizione == 51 || posizione == 52 || posizione == 53)
            {
                //51,52,53
                Canvas.SetTop(skin1, ((posizione - 50) * 63) + 20);
                Canvas.SetLeft(skin1, 200);
                Canvas.SetTop(skin2, ((posizione - 50) * 63) + 20);
                Canvas.SetLeft(skin2, 220);
            }
            else if (posizione == 54 || posizione == 55 || posizione == 56)
            {
                //54,55,56
                Canvas.SetTop(skin1, 200);
                Canvas.SetLeft(skin1, (posizione - 50) * 63);
                Canvas.SetTop(skin2, 200);
                Canvas.SetLeft(skin2, ((posizione - 50) * 63) + 30);
            }
            else if (posizione == 57 || posizione == 58)
            {
                //57,58
                Canvas.SetTop(skin1, (570 - (posizione - 50) * 63));
                Canvas.SetLeft(skin1, 390);
                Canvas.SetTop(skin2, (570 - (posizione - 50) * 63));
                Canvas.SetLeft(skin2, 410);
            }
            else if (posizione == 61 || posizione == 62 || posizione == 63 || posizione == 64)
            {
                //61,62,63,64
                Canvas.SetTop(skin1, 260);
                Canvas.SetLeft(skin1, 1030 - ((posizione - 60) * 63));
                Canvas.SetTop(skin2, 260);
                Canvas.SetLeft(skin2, 1050 - ((posizione - 60) * 63));
            }
            else if (posizione == 65 || posizione == 66)
            {
                //65,66
                Canvas.SetTop(skin1, ((posizione - 60) * 63) + 20);
                Canvas.SetLeft(skin1, 770);
                Canvas.SetTop(skin2, ((posizione - 60) * 63) + 20);
                Canvas.SetLeft(skin2, 790);
            }
            else if (posizione == 50)
            {
                Canvas.SetTop(skin1, 10);
                Canvas.SetLeft(skin1, 207);
                Canvas.SetTop(skin2, 10);
                Canvas.SetLeft(skin2, 227);
            }
            else if (posizione == 60)
            {
                Canvas.SetTop(skin1, 273);
                Canvas.SetLeft(skin1, 1030);
                Canvas.SetTop(skin2, 273);
                Canvas.SetLeft(skin2, 1050);
            }
        }
        private bool controlloPos(int num)
        {
            int posizione = Ulocale.posMappa;
            if (posizione == 5 || posizione == 10 || posizione == 16 || posizione == 19 || posizione == 25 || posizione == 33 || posizione == 37 || posizione == 57 || posizione == 65)
            {
                //se pos = 5,10,16,19,25,33,37,57,65  -> aggiungo monete a locale
                if (Ulocale.turno)
                {
                    Ulocale.numMonete += 10;
                    MessageBox.Show("Hai vinto 10 monete!");
                }
            }
            else if (posizione == 3 || posizione == 8 || posizione == 15 || posizione == 24 || posizione == 28 || posizione == 36 || posizione == 53 || posizione == 61)
            {
                //se pos = 3,8,15,24,28,36,53,61 -> non faccio niente
                MessageBox.Show("Salti questo turno!");
            }
            else if (posizione == 9 || posizione == 14 || posizione == 27 || posizione == 31 || posizione == 34 || posizione == 54 || posizione == 62)
            {
                //se pos =  9,14,27,31,34,54,62 -> apro il form del minigame corrispondente
                //            1) Melanzane
                //            2) Pesca pesci
                //            3) Imbrocca il lato
                //            4) Immagine sgranata
                //            5) Carta forbice e sassi
                //            6) Tris
                switch (posizione)
                {
                    case 9:
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
                    case 8:
                        GameImmagine d = new GameImmagine(Ulocale, Uesterno, c, this);
                        d.Show();
                        break;
                    case 4:
                        GamecartaForbiceSasso e = new GamecartaForbiceSasso(Ulocale, Uesterno, c, this);
                        e.Show();
                        break;
                    case 10:
                        GameTris f = new GameTris(Ulocale, Uesterno, c, this);
                        f.Show();
                        break;
                }
                this.Hide();
            }
            else if (posizione == 2 || posizione == 6 || posizione == 13 || posizione == 17 || posizione == 22 || posizione == 32 || posizione == 39 || posizione == 52 || posizione == 58 || posizione == 63 || posizione == 64)
            {
                //se pos =  2,6,13,17,22,32,39,52,58,63,64-> apro il form con una domanda random
                Domande a = new Domande(Ulocale, Uesterno, c, this);
                a.Show();
                this.Hide();


            }
            else if (posizione == 11 || posizione == 18 || posizione == 23 || posizione == 26 || posizione == 35 || posizione == 51 || posizione == 56)
            {
                //se pos = 11,18,23,26,35,51,56-> tolgo monete a locale                
                if (Ulocale.turno)
                {
                    MessageBox.Show("Hai perso 10 monete!");
                    Ulocale.numMonete -= 10;
                }
            }
            else if (posizione == 4 || posizione == 21)
            {
                //se pos =  4,21 -> 1) aspetto che l'utente locale scelga dove andare
                //                  2) aspetto che mi arrivi la scelta da esterno
                string s = "";
                if (Ulocale.turno)
                {
                    MessageBoxResult dialogResult = MessageBox.Show("Vuoi proseguire dritto?", "Scelta bivio", MessageBoxButton.YesNo);
                    if (dialogResult == MessageBoxResult.Yes)
                        //dritto
                        s = "B;1";
                    else if (dialogResult == MessageBoxResult.No)
                        //giro
                        s = "B;2";
                    //c.BufferInviare.Add(s);
                }
                else
                    s = c.prendi();
                //elaboro
                if (s.ElementAt(0) == 'B')
                    if (s.Split(';')[1] == "1")
                    {
                        //proseguo dritto
                        Ulocale.posMappa = Ulocale.posMappa;
                        Uesterno.posMappa = Uesterno.posMappa;
                    }
                    else
                    {
                        //faccio il bivio
                        if (posizione == 4)
                        {
                            //primo bivio
                            Ulocale.posMappa = 50;
                            Uesterno.posMappa = 50;
                        }
                        else
                        {
                            //secondo bivio
                            Ulocale.posMappa = 60;
                            Uesterno.posMappa = 60;
                        }
                    }
                muoviPersonaggi(num);
                return true;
            }
            return false;
        }
    }
}