using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Temp
{
    public class Condivisa
    {
        public List<string> BufferInviare { get; set; }
        public List<string> BufferRicevuti { get; set; }
        public string indirizzo { get; set; }

        public Condivisa()
        {
            BufferInviare = new List<string>();
            BufferRicevuti = new List<string>();
            Thread t1 = new Thread(client);
            t1.Start();
        }
        public void startServer()
        {
            Thread t = new Thread(server);
            t.Start();
        }
        private void server()
        {
            UdpClient server = new UdpClient();
            while (true)
            {
                if (BufferInviare.Count > 0)
                {
                    byte[] data = Encoding.ASCII.GetBytes(BufferInviare[0]);
                    server.Send(data, data.Length, indirizzo, 12346);
                    BufferInviare.RemoveAt(0);
                }
                Thread.Sleep(1000);
            }
        }

        private void client()
        {
            UdpClient client = new UdpClient(12345);
            IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);
            while (true)
            {
                byte[] dataReceived = client.Receive(ref riceveEP);
                String s = Encoding.ASCII.GetString(dataReceived);
                if (s != null)
                    BufferRicevuti.Add(s);
                Thread.Sleep(1000);
            }
        }
        public string prendi()
        {
            string s = "";
            if (BufferRicevuti.Count() > 0)
            {
                s = BufferRicevuti[0];
                BufferRicevuti.RemoveAt(0);
            }
            return s;
        }

    }
}
