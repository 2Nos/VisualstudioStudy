using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MultiThread
{
    class Program
    {
        static Thread t1;
        static void Main(string[] args)
        {
            /*ThreadStart threadStart = new ThreadStart(NewClient);
            t1 = new Thread(threadStart);
            t1.Start();
            int count = 0;
            while(count < 300)
            {
                count++;
                Console.WriteLine("Main함수 = " + count);
            }
            t1.Join();//NewClient 스레드 종료
            Console.WriteLine("Join");
            t1.Interrupt(); //스레드가 동작중일겨우를 피해서 WaitJoinSleep상태까지 기다리고 스레드 종료
            Console.WriteLine("Interrupt");*/
            Thread thread1 = new Thread(Test);
            thread1.Start();
            thread1.Join();
            Console.WriteLine("스레드 Thread1 이 종료되었습니다.");

            Thread thread2 = new Thread(Test);
            thread2.Start();
            Thread.Sleep(1000); //10초후 밑의 문구가 뜰거임
            Console.WriteLine("스레드 Thread2 이 종료되었습니다.");
        }

        private static void Test()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("XXX");
            }
        }
        /*static void NewClient()
        {
            int threadCount = 0;
            while(threadCount < 300)
            {
                threadCount++;
                Console.WriteLine("NewClient" + threadCount);
                Thread.Sleep(10); //지정된 밀리언초(0.1초)마다 일시 중단 무조건 작성
            }
        }*/
    }
}
