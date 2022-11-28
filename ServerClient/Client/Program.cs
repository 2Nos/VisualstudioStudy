using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EchoClient
{
    class Program
    {
        static Socket sock;
        static int port = 8082;
        static string strip = "192.168.0.5";

        static void Main(string[] args)
        {
            sock = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strip), port);
            sock.Connect(ip);
            byte[] receiveBuffer = new byte[128];
            byte[] sendBuffer = new byte[128];
            sock.Receive(receiveBuffer);
            string receiveData = Encoding.Default.GetString(receiveBuffer);
            Console.WriteLine(receiveData);
            string line = string.Empty;
            while((line = Console.ReadLine()) != null)
            {
                byte[] sendData = Encoding.Default.GetBytes(line);
                sock.Send(sendData);

                line = string.Empty;
            }
        }
    }
}
