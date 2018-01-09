using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zad12
{
    struct TResultDataStructure
    {
        public int i;
        public TResultDataStructure(int i) : this()
        {
            this.i = i;
        }
    }
    class Program
    {
        public static Task<TResultDataStructure> OperationTask(byte[] buffer)
        {
            TaskCompletionSource<TResultDataStructure> zadanie = new TaskCompletionSource<TResultDataStructure>();
            Task.Run(() =>
            {
                int x = 5;
                for (int i = 0; i < 10; i++)
                {
                    x += 5;
                    Console.WriteLine(x);
                }
                zadanie.SetResult(new TResultDataStructure(x));
            });
            return zadanie.Task;
        }
        static void Main(string[] args)
        {
            byte[] buffer = new byte[128];
            Task zadanie = OperationTask(buffer);
            Task.WaitAll(new Task[] { zadanie });
            Console.ReadKey();
        }
        
        
    }
    
}



