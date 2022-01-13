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
        }
    }
}
