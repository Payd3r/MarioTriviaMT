﻿using System;
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

namespace Temp.Minigames
{
    public partial class GameTris : Window
    {
        Utente Ulocale;
        Utente Uesterno;
        public GameTris()
        {
            InitializeComponent();
        }
        public GameTris(Utente a, Utente b)
        {
            InitializeComponent();
            Ulocale = a;
            Uesterno = b;
        }
    }
}
