using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOlab4_zad13
{
    class Program
    {
        public async static void Zadanie2()
        {
            bool Z2 = false;
            await Task.Run(
                () =>
                {
                    Z2 = true;
                });
            Console.WriteLine(Z2);
             
        }
        public static void Zadanie2a()
        {
            bool Z2 = false;
            Task.Run(
                () =>
                {
                    Z2 = true;
                });
            Console.WriteLine(Z2);

        }
        static void Main(string[] args)
        {
            Zadanie2();
            Zadanie2a();
            Console.ReadKey();
        }
    }
}
