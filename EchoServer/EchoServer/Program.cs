using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
namespace EchoServer
{
    class Program
    {
        static Thread t1;
        static Socket listenSock;
        static int port = 8082;
        static string strip = "192.168.0.5";
        static List<User> userList;
        static void Main(string[] args)
        {
            listenSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strip), port);   // 종단점 설정
            t1 = new Thread(new ThreadStart(NewClient));
            listenSock.Bind(ip);
            Console.WriteLine("Bind");
            t1.Start();
            byte[] receiveBuffer = new byte[128];
            byte[] sendBuffer = new byte[128];
            userList = new List<User>();
        }
        static void AccpetCallBack(IAsyncResult ar)
        {
            Socket userSock = listenSock.EndAccept(ar);
            User newUser = new User(userSock);
            userList.Add(newUser);
            byte[] tmp = Encoding.Default.GetBytes("Game에 오신것을 환영합니다.");
            userSock.Send(tmp);
            userSock.BeginReceive(newUser.receiveBuffer, 0, newUser.receiveBuffer.Length, SocketFlags.None, ReceiveCallBack, newUser);
        }
        static void ReceiveCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            Array.Copy(user.receiveBuffer, user.sendBuffer, user.receiveBuffer.Length);
            Array.Clear(user.receiveBuffer, 0, user.receiveBuffer.Length);
            user.userSock.BeginSend(user.sendBuffer, 0, user.sendBuffer.Length, SocketFlags.None, SendCallBack, user);
        }
        static void SendCallBack(IAsyncResult ar)
        {
            User user = (User)ar.AsyncState;
            Array.Clear(user.sendBuffer, 0, user.sendBuffer.Length);
            user.userSock.BeginReceive(user.receiveBuffer, 0, user.receiveBuffer.Length, SocketFlags.None, ReceiveCallBack, user);
        }
        static void NewClient()
        {
            while (true)
            {
                listenSock.Listen(1000);
                //Console.WriteLine("Listen");
                listenSock.BeginAccept(AccpetCallBack, null);
                //Console.WriteLine("Accept");
                //byte[] tmp = Encoding.Default.GetBytes("Game에 오신것을 환영합니다.");
                //client.Send(tmp);
                Thread.Sleep(10);
            }
        }
    }
}
