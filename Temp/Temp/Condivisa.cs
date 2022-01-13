using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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
                Thread.Sleep(100);
            }
        }

        private void client()
        {
            try
            {
                UdpClient listener = new UdpClient(12345);
                IPEndPoint riceveEP = new IPEndPoint(IPAddress.Any, 0);
                while (BufferRicevuti.Last() == "")
                {
                    byte[] dataReceived = listener.Receive(ref riceveEP);
                    BufferRicevuti.Add(Encoding.ASCII.GetString(dataReceived));
                }
            }
            catch (Exception)
            {
            }
        }
        public string prendi()
        {
            string s = "";
            while (true)
            {
                try
                {
                    if (BufferRicevuti.Count > 0)
                    {
                        s = BufferRicevuti.First();
                        BufferRicevuti.RemoveAt(0);
                        return s;
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
