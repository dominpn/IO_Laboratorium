using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab2_zad7
{
    class Program
    {
  
        static void Main(string[] args)
        {

            FileStream fs = File.OpenRead("c:\\folder\\plik.txt");
            byte[] buffer = new byte[1024];
            IAsyncResult result = fs.BeginRead(buffer, 0, buffer.Length, null, null);
            int len = fs.EndRead(result);

            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, len));


        }
    }
}
