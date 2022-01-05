﻿using System;
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
        Utente Ulocale= new Utente();
        Utente Uesterno=new Utente();
        int estrazione;
        UdpClient peer = new UdpClient();
        public MainWindow()
        {
            InitializeComponent();
            Menu m = new Menu();
            m.Show();
            this.Hide();
        }
      
        public MainWindow(string nome, int skin, string indirizzo)
        {
            var THRiceviInfo = new Thread(richiediInfo);
            InitializeComponent();
            //inserisco informazioni utente locale
            Ulocale.nick = nome;
            Ulocale.skin = skin;
            //invio messaggio con le mie info (nick,skin)
            byte[] data = Encoding.ASCII.GetBytes("p;"+nome+";"+skin+";");
            peer.Send(data, data.Length, indirizzo, 666);

            //richiedi informazioni per crare l'altro utente(nick, skin, turno)
            //Uesterno = new Utente(richiediInfo());

            //crea utente locale
            if (Uesterno.turno)
                Ulocale = new Utente(nome + ";" + skin + ";" + false);
            else
                Ulocale = new Utente(nome + ";" + skin + ";" + true);
            //start threadMuoviPersonaggi
            var THmuoviPersonaggi = new Thread(muoviPersonaggi);
            THmuoviPersonaggi.Start();
            //start threadControlloPos
            var THcontrolloPos = new Thread(controlloPos);
            THcontrolloPos.Start();
        }
        private void richiediInfo()
        {
           
            IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);

            byte[] dataReceived = peer.Receive(ref riceveEP);

            String risposta = Encoding.ASCII.GetString(dataReceived);

            string[] info = risposta.Split(';');
            if(info[0]=="p")
            {
                Uesterno.nick = info[1];
                Uesterno.skin = Convert.ToInt32(info[2]);
            }
           
        }
        private void btnDado_Click(object sender, RoutedEventArgs e)
        {
            //se tocca all'utente locale -> MessageBox con il num uscito
            //invio il num uscito ad esterno
            //sposto anche in locale
            if (Ulocale.turno)
            {
                Random rnd = new Random();
                estrazione = rnd.Next(0, 6);
                Thread.Sleep(100);
                MessageBox.Show("Il numero uscito e' " + estrazione + "!");
                //invio estrazione
            }
            else
                MessageBox.Show("Non e' il tuo turno!");
        }
        private void muoviPersonaggi()
        {
            //controlla la posizione all'interno di locale ed esterno e riposiziona le skin nella minimappa
            while (true)
                if (estrazione > 0)
                {
                    bool check = false;
                    for (int i = 0; i < estrazione && !check; i++)
                    {
                        muoviUno(Ulocale.posMappa++);
                        if (Ulocale.posMappa == 53)
                            //se e' ad un bivio si ferma
                            check = true;
                    }
                    if (!check)
                        estrazione = 0;
                }
        }
        private void muoviUno(int num)
        {
            //sposta le skin fino al num
            Ulocale.posMappa++;
            Uesterno.posMappa++;

        }
        private void controlloPos()
        {
            while (true)
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
                        //se pos =  -> aggiorno pos di locale ed invio a esterno
                        Ulocale.posMappa += 2;
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
                            //proseguo dritto
                            bool check = false;
                            for (int i = 0; i < estrazione && !check; i++)
                            {
                                muoviUno(Ulocale.posMappa++);
                                if (Ulocale.posMappa == 53)
                                    //se e' ad un bivio si ferma
                                    check = true;
                            }
                            if (!check)
                                estrazione = 0;
                        }
                        else if (dialogResult == MessageBoxResult.No)
                        {
                            //giro
                            for (int i = 0; i < estrazione; i++)
                                muoviUno(Ulocale.posMappa++);
                        }
                    }
                }
            }
        }
    }
}
