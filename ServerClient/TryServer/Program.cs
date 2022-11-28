using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace EchoServer
{
    class Program
    {
        static Socket listenSocket;
        static int port = 8082;
        static string strip = "192.168.0.5";

        static void Main(string[] args)
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strip), port);
            listenSocket.Bind(ip);
            Console.WriteLine("Bind");
            listenSocket.Listen(1000);
            Console.WriteLine("Listen");
            Socket client = listenSocket.Accept();
            Console.WriteLine("Accept");
            string data = "Game에 오신것을 환영합니다.";
            byte[] tmp = Encoding.Default.GetBytes(data);
            byte[] sendBuffer = new byte[128];
            byte[] receiveBuffer = new byte[128];
            client.Send(tmp);
            Console.WriteLine(client.RemoteEndPoint + "님께서 접속했습니다.");
            while (true)
            {
                try
                {   if(client.Connected)
                    {
                    client.Receive(receiveBuffer);
                    string strTmp = Encoding.Default.GetString(receiveBuffer);
                    Console.WriteLine("클라이언트에서 받은 내용 = " + strTmp);
                    Array.Copy(receiveBuffer, sendBuffer, receiveBuffer.Length);
                    client.Send(sendBuffer);
                    Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
                    Array.Clear(sendBuffer, 0, sendBuffer.Length);
                    }
                }
                catch(SocketException e)
                {
                    Console.WriteLine(e.Message);
                    client.Close(); //소켓을 닫았기에 오브젝트소멸이 발생된것과 같기에 밑의 ObjectDisposedException로도 메세지
                }
                catch(ObjectDisposedException e)
                {
                    Console.WriteLine(e.Message);
                }
            }


        }
    }
}
