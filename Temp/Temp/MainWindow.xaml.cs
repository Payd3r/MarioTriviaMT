using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Temp.Minigames;

namespace Temp
{
    public partial class MainWindow : Window
    {
        Utente Ulocale;
        Utente Uesterno;

        public MainWindow()
        {
            InitializeComponent();
            //crea utente locale
            Ulocale = new Utente();
            //invio messaggio con le mie info (nick,skin)
            //richiedi informazioni per crare l'altro utente(nick, skin)
            Uesterno = new Utente(richiediInfo());
            //start threadMuoviPersonaggi
            var THmuoviPersonaggi = new Thread(muoviPersonaggi);
            THmuoviPersonaggi.Start();
            //start threadControlloPos
            var THcontrolloPos = new Thread(controlloPos);
            THcontrolloPos.Start();
        }
        private string richiediInfo()
        {
            string s = "";
            return s;
        }
        private void btnDado_Click(object sender, RoutedEventArgs e)
        {
            //se tocca all'utente locale -> MessageBox con il num uscito
            //invio il num uscito ad esterno
            //sposto anche in locale

        }
        private void muoviPersonaggi()
        {
            //controlla la posizione all'interno di locale ed esterno e riposiziona le skin nella minimappa

        }
        private void controlloPos()
        {
            if (Ulocale.posMappa == 1)
            {
                //se pos =  -> aggiungo monete a locale 
                Ulocale.numMonete += 10;
            }
            else if (Ulocale.posMappa == 2)
            {
                //se pos =  -> aggiorno pos di locale ed invio a esterno
                Ulocale.posMappa++;
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
                        GameMelanzane a = new GameMelanzane(Ulocale, Uesterno);
                        a.Show();
                        break;
                    case 5:
                        GamePesca b = new GamePesca(Ulocale, Uesterno);
                        b.Show();
                        break;
                    case 6:
                        GameLato c = new GameLato(Ulocale, Uesterno);
                        c.Show();
                        break;
                    case 7:
                        GameImmagine d = new GameImmagine(Ulocale, Uesterno);
                        d.Show();
                        break;
                    case 8:
                        GamecartaForbiceSasso e = new GamecartaForbiceSasso(Ulocale, Uesterno);
                        e.Show();
                        break;
                    case 9:
                        GameTris f = new GameTris(Ulocale, Uesterno);
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
            }
            else if (Ulocale.posMappa == 11)
            {
                //se pos =  -> tolgo monete a locale
                Ulocale.numMonete -= 10;
            }
            else if (Ulocale.posMappa == 12)
            {
                //se pos =  -> 1) aspetto che l'utente locale scelga dove andare
                //             2) aspetto che mi arrivi la scelta da esterno

            }
        }
    }
}
