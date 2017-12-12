using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab2_zad8
{
    class Program
    {
        delegate int DelegateType(int n);
        static DelegateType silnia_it_del;
        static DelegateType silnia_rek_del;
        static DelegateType fib_rek_del;
        static DelegateType fib_it_del;
        static int sil_it(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        static int sil_rek(int n)
        {
            
            if (n < 1)
                return 1;
            else
                return n * sil_rek(n - 1);
        }
        static int fib_rek(int n)
        {
            if ((n == 1) || (n == 2))
                return 1;
            else
                return fib_rek(n - 1) + fib_rek(n - 2);
        }
        static int fib_it(int n)
        {
            if (n <= 2)
                return 1;
            else
            {
                int f1 = 1; 
                int f2 = 1; 
                int temp; 
                for (int i = 3; i <= n; i++)
                {
                    temp = f1 + f2;
                    f1 = f2;
                    f2 = temp;
                   
                }
                return f2;
            }
        }
        static void Main(string[] args)
        {
            int n = 10;
            silnia_it_del = new DelegateType(sil_it);
            silnia_rek_del = new DelegateType(sil_rek);
            fib_rek_del = new DelegateType(fib_rek);
            fib_it_del = new DelegateType(fib_it);
            IAsyncResult ar1 = silnia_it_del.BeginInvoke(n, null, "");
            IAsyncResult ar2 = silnia_rek_del.BeginInvoke(n, null, "");
            IAsyncResult ar3 = fib_rek_del.BeginInvoke(n, null, "");
            IAsyncResult ar4 = fib_it_del.BeginInvoke(n, null, "");
            WaitHandle.WaitAll(new WaitHandle[] { ar1.AsyncWaitHandle, ar2.AsyncWaitHandle, ar3.AsyncWaitHandle, ar4.AsyncWaitHandle });
            int result = silnia_it_del.EndInvoke(ar1);
            Console.WriteLine("Wynik silnii iteracyjnej = " + result);
            result = silnia_rek_del.EndInvoke(ar2);
            Console.WriteLine("Wynik silnii rekurencyjnej = " + result);
            result = fib_rek_del.EndInvoke(ar3);
            Console.WriteLine("Wynik ciagu Fibonnaciego rekurencyjnie = " + result);
            result = fib_it_del.EndInvoke(ar4);
            Console.WriteLine("Wynik ciagu Fibonnaciego iteracyjnie = " + result);
            Console.ReadKey();
        }
    }
}