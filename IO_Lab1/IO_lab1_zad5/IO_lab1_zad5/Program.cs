using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace IO_lab1_zad5
{
    class Program
    {
        static Object thisLock = new Object();
        const int len = 1000;
        static int sum = 0;
        public static List<int> tab = new List<int>();
        static void Main(string[] args)
        {
            
            System.Random x = new Random(System.DateTime.Now.Millisecond);
            for (int i = 0; i <= len; i++)
            {
                tab.Add(x.Next(1, 100));
            }
            Console.WriteLine("Z ilu watkow chcesz skorzystac?");
            int n = int.Parse(Console.ReadLine());
            WaitHandle[] wh = new WaitHandle[n];
            int num = len / n;
            int begin = 0;
            int end = num - 1;
            for (int i = 0; i < n; i++)
            {
                AutoResetEvent ars = new AutoResetEvent(false);
                //Console.WriteLine("Poczatek " + begin);
                //Console.WriteLine("Koniec " + end);
                ThreadPool.QueueUserWorkItem(new WaitCallback(Sumator), new object[] { begin, end, ars });
                wh[i] = ars;
                begin = begin + num;
                
                if (end+num<len)
                {
                    end = end + num;
                }
                else
                {
                    end = len - 1;
                }
                
            }
            WaitHandle.WaitAll(wh);
            //Thread.Sleep(1000);
            Console.WriteLine("Suma rowna sie " + sum);
            /*
            int suma_ogolna = 0;
            for (int i=0; i<len;i++)
            {
                suma_ogolna = suma_ogolna + tab[i];
            }
            Console.WriteLine(suma_ogolna);
            */
            Console.ReadKey();
        }
        static void Sumator(Object value)
        {
            var begin = ((object[])value)[0];
            var end = ((object[])value)[1];
            AutoResetEvent ars = (AutoResetEvent)((object[])value)[2];
            int part_sum = 0;
            for (int i=(int)begin; i<=(int)end; i ++)
            {
                part_sum = part_sum + tab[i];
            }
            lock (thisLock)
            {
                sum = sum + part_sum;
                ars.Set();
            }
        }
    }
}