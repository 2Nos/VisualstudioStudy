using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[4];
            try
            {
                array[0] = 100;
                array[4] = 200; //예외 발생 (허용한 배열범위 초과)
            }
            catch(IndexOutOfRangeException e)
            {     //배열의 인덱스에 대한 예외
                Console.WriteLine(e.Message);
            }

            try
            {
                array[5] = 200;
            }
            catch(Exception e) //모든 예외에 대한 처리
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
