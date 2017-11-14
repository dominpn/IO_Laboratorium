using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(ThreadProc, new object[] { 500 });
            Thread.Sleep(1000);
        }
        static void ThreadProc(Object state)
        {
            var czas = ((object[])state)[0];
            Thread.Sleep((int)czas);
            Console.WriteLine(czas);
        }
    }
}
