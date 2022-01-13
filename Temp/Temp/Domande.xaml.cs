using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Temp
{
    public partial class Domande : Window
    {
        Utente Ulocale;
        Utente Uesterno;
        Condivisa c;
        Minimappa mappa;
        List<CDomanda> domande;
        int estrazione;
        int scelta;
        Random rnd;
        public Domande()
        {
            InitializeComponent();
        }
        public Domande(Utente a, Utente b, Condivisa cond, Minimappa map)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
            c = cond;
            mappa = map;
            rnd = new Random();
            estrazione = rnd.Next(0, 11);
            domande = new List<CDomanda>();
            caricaLista(AppDomain.CurrentDomain.BaseDirectory + "\\File\\Domande.txt");
            if (Ulocale.turno)
            {
                txtdomanda.Content = domande[estrazione].domanda;
                txt1.Content = domande[estrazione].risposte[0];
                txt2.Content = domande[estrazione].risposte[1];
                txt3.Content = domande[estrazione].risposte[2];
                txt4.Content = domande[estrazione].risposte[3];
                c.BufferInviare.Add("D;" + estrazione);
            }
            else
            {
                string s = c.prendi();
                if (s.ElementAt(0) == 'D')
                    if (s.Split(';')[1] != "")
                    {
                        string dom = s.Split(';')[1];
                        int numdom = Convert.ToInt32(dom);
                        estrazione = numdom;
                        txtdomanda.Content = domande[numdom].domanda;
                        txt1.Content = domande[numdom].risposte[0];
                        txt2.Content = domande[numdom].risposte[1];
                        txt3.Content = domande[numdom].risposte[2];
                        txt4.Content = domande[numdom].risposte[3];
                    }
            }
        }
        public void caricaLista(string file)
        {
            string lettura = System.IO.File.ReadAllText(file);
            from_CSV(lettura);
        }
        public void from_CSV(string contenuto)
        {
            string[] linee = contenuto.Split('\n');
            for (int i = 0; i < linee.Length; i++)
            {
                CDomanda d = new CDomanda();
                if (linee[i] != "")
                {
                    string[] s = linee[i].Split(';');
                    d.domanda = s[0];
                    d.rispostaGiusta = s[1];
                    for (int j = 2; j < 6; j++)
                        d.risposte.Add(s[j]);
                }
                domande.Add(d);
            }
        }
        private void btnconferma_Click(object sender, RoutedEventArgs e)
        {
            if (r1.IsChecked == true)
                scelta = 1;
            if (r2.IsChecked == true)
                scelta = 2;
            if (r3.IsChecked == true)
                scelta = 3;
            if (r4.IsChecked == true)
                scelta = 4;
            if (scelta == 0)
                scelta = rnd.Next(1, 5);
            indovina();
            string s = c.prendi();//aspetto di sapere cos'ha fatto esterno
            if (s.ElementAt(0) == 'R')
                if (s.Split(';')[1] == "v")
                    //anche l'altro ha azzeccato
                    Uesterno.numMonete += 10;
            mappa.Show();
            this.Close();
        }
        private void indovina()
        {
            if (domande[estrazione].rispostaGiusta.Equals(domande[estrazione].risposte[scelta - 1]))
            {
                MessageBox.Show("Hai indovinato");
                c.BufferInviare.Add("R;" + "v");
                Ulocale.numMonete += 10;
            }
            else
            {
                MessageBox.Show("Hai sbagliato...");
                c.BufferInviare.Add("R;" + "f");
            }
        }
    }
}
