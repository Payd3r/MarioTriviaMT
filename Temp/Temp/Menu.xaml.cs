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
    /// <summary>
    /// Logica di interazione per Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void btnConferma_Click(object sender, RoutedEventArgs e)
        {
            bool errore = false;
            int skin = 0;
            string name = txtNome.Text;
            string indirizzo = txtIndirizzo.Text;
            if (r1.IsChecked == true)
                skin = 1;
            if (r2.IsChecked == true)
                skin = 2;
            if (r3.IsChecked == true)
                skin = 3;
            if (r4.IsChecked == true)
                skin = 4;
            if (skin == 0)
            {
                MessageBox.Show("Devi scegliere una skin per iniziare");
                errore = true;
            }
            if (name == "")
            {
                MessageBox.Show("Devi inserire un nome per iniziare");
                errore = true;
            }
            if (indirizzo == "")
            {
                MessageBox.Show("Devi inserire un indirizzo per poter giocare con qualcuno");
                errore = true;
            }

            if(errore==false){
                MainWindow m = new MainWindow(name, skin, indirizzo);
                m.Show();
                this.Hide();
            }
           
        }
    }
}
