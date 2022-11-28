using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
namespace AsynchronousEchoServer //비동기란 요청 보낸 후 응답과는 상관없이 다음 방식이 동작 자원 효율적 활용이 가능
{
    class Program
    {
        static Thread t1;
        static Socket listenSock;
        static int port = 8082;
        static string strip = "192.168.0.5";
        static List<User> userList; //사용자마다 버퍼가 있어야하기에 클래스리스트를 만들어놓음
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
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket userSock = listenSock.EndAccept(ar); //EndAccept들어오는 연결시도를 비동기적으로 받아들이고 새로운 소켓 만듬
            User newUser = new User(userSock);
            userList.Add(newUser);
            byte[] tmp = Encoding.Default.GetBytes("Game에 오신것을 환영합니다.");
            userSock.Send(tmp); //동기로 전송
            userSock.BeginReceive(newUser.receiveBuffer, 0, newUser.receiveBuffer.Length, SocketFlags.None, ReceiveCallBack, newUser); //ReceiveCallBack에 newUser 매개변수를 전달, 수신대기상태
        }
        static void ReceiveCallBack(IAsyncResult ar) //스레드와 동일한 효과
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
                Console.WriteLine("Listen");
                User newUser = new User();
                listenSock.BeginAccept(AcceptCallBack, newUser); // 비동기는 앞에 Begin을 붙여주면된다.
                //Console.WriteLine("Accept");
                //byte[] tmp = Encoding.Default.GetBytes("Game에 오신것을 환영합니다.");
                //client.Send(tmp);
                Thread.Sleep(10);
            }
        }
    }
}
