using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace ThreadEchoServer //동기식
{

    class Program
    {
        static Socket listenSocket;
        static int port = 8082;
        static string strip = "192.168.0.5";
        static Thread t1;
        static List<User> userList;
        static void Main(string[] args)
        {
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse(strip), port);
            listenSocket.Bind(ip);
            Console.WriteLine("Bind");
            t1 = new Thread(new ThreadStart(NewClient));
            t1.Start();
            //byte[] receiveBuffer = new byte[128];
            //byte[] sendBuffer = new byte[128];
            userList = new List<User>();
            for (int i = 0; i < userList.Count; i++)
            {
                userList[i].userSock.Receive(userList[i].receiveBuffer); //동기함수 단점 : 유저한명당 스레드 하나가 필요함
                Array.Copy(userList[i].receiveBuffer, userList[i].sendBuffer, userList[i].receiveBuffer.Length);
                userList[i].userSock.Send(userList[i].sendBuffer);
                Array.Clear(userList[i].receiveBuffer, 0, userList[i].receiveBuffer.Length);
                Array.Clear(userList[i].sendBuffer, 0, userList[i].sendBuffer.Length);
            }
        }
        static void NewClient()
        {
            while(true)
            {
                listenSocket.Listen(1000);
                Console.WriteLine("Listen"); //실행시 여기까지만 처음에 나옴. 이후 Accept가 나온후 Listen한번 더 출력 루프문이기에
                Socket client = listenSocket.Accept(); //client를 빌드했을때 동작
                Console.WriteLine("Accept");
                Thread.Sleep(10);
                byte[] tmp = Encoding.Default.GetBytes("Game에 오신것을 환영합니다."); //클라이언트로 보냄
                client.Send(tmp);
            }
        }
    }
}
