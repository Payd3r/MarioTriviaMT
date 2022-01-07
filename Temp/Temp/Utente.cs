using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    public class Utente
    {
        public string nick { get; set; }
        public int numMonete { get; set; }
        public int posMappa { get; set; }
        public int skin { get; set; } // 1.2.3.4. ci possono essere duplicati
        public bool turno { get; set; } //true = turnoLocale | false = turnoEsterno
        public Utente()
        {
            nick = "";
            numMonete = 0;
            posMappa = 0;
            skin = 0;
            turno = false;
        }
        public Utente(string a,string b)
        {
            numMonete = 0;
            posMappa = 0;            
            nick = a;
            skin = Convert.ToInt32(b);
            turno = false;
        }
    }
}
