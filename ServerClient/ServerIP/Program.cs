using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ServerIP
{
    class Program
    {
        static string serverIP = "192.168.0.5";
        static void Main(string[] args)
        {
            //문자열 IP를 다루기 위한 클래스 : IPAddress
            IPAddress ip1 = IPAddress.Parse(serverIP);
            //byte배열로 IP를 표현
            IPAddress ip2 = new IPAddress(new byte[] { 192, 168, 0, 5 });
            //byte배열을 long값으로 표현
            int ipAddress = BitConverter.ToInt32(new byte[] {192,168,0,5}, 0);
            Console.WriteLine(ipAddress);
            //IP를 바이트 배열로 다시 변환
            byte[] b = ip1.GetAddressBytes();
            Console.WriteLine("Address :" + b[0] + "." + b[1] + "." + b[2] + "." + b[3]);
        }
    }
}
