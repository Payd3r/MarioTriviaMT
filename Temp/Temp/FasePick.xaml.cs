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
    public partial class FasePick : Window
    {
        Condivisa c;
        string nome2, skin2;
        bool turno;
        public FasePick(bool a, Condivisa cond)
        {
            InitializeComponent();
            c = cond;
            nome2 = "";
            skin2 = "";
            turno = a;
        }

        private void btnConferma_Click_1(object sender, RoutedEventArgs e)
        {
            int skin1 = 0;
            string nome1 = txtNome.Text;
            if (r1.IsChecked == true)
                skin1 = 1;
            if (r2.IsChecked == true)
                skin1 = 2;
            if (r3.IsChecked == true)
                skin1 = 3;
            if (r4.IsChecked == true)
                skin1 = 4;
            if (turno)
            {
                if (skin1 == 0)
                    MessageBox.Show("Devi scegliere una skin per iniziare");
                else if (nome1 == "")
                    MessageBox.Show("Devi inserire un nome per iniziare");
                else
                {
                    c.BufferInviare.Add("p;" + nome1 + ";" + skin1);
                    Minimappa a = new Minimappa(nome1, skin1.ToString(), c);
                    a.Show();
                    this.Close();
                }
            }
            else
            {
                string s1 = "s;andrea;2";
                if (s1.ElementAt(0) == 's')
                {
                    nome2 = s1.Split(';')[1];
                    skin2 = s1.Split(';')[2];
                }
                if (skin1 == 0)
                    MessageBox.Show("Devi scegliere una skin per iniziare");
                else if (nome1 == "")
                    MessageBox.Show("Devi inserire un nome per iniziare");
                else if (skin1 == Convert.ToInt32(skin2))
                {
                    MessageBox.Show("Skin gia' pikkata!");
                    skin1 = 0;
                }
                else if (nome1.Equals(nome2))
                {
                    MessageBox.Show("Nome gia' in uso!");
                    nome1 = "";
                }
                else
                {
                    Minimappa a = new Minimappa(nome1, skin1.ToString(), nome2, skin2.ToString(), c);
                    a.Show();
                    this.Close();
                }
            }
        }
    }
}