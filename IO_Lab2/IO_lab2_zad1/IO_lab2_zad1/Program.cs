using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab2_zad1
{
    class Program
    {
        static void myAsyncCallback(IAsyncResult Resultstate)
        {
            object[] args = (object[])Resultstate.AsyncState;
            FileStream fs = args[0] as FileStream;
            byte[] buffer = args[1] as byte[];
            AutoResetEvent are = args[2] as AutoResetEvent;

            int len = fs.EndRead(Resultstate);
            Console.WriteLine(Encoding.ASCII.GetString(buffer, 0, len));
            fs.Close();
            are.Set();
        }
        static void Main(string[] args)
        {

            AutoResetEvent are = new AutoResetEvent(false);
            FileStream fs = File.OpenRead("c:\\folder\\plik.txt");
            byte[] buffer = new byte[1024];
            
            fs.BeginRead(buffer, 0, buffer.Length, myAsyncCallback, new object[]{ fs, buffer, are});
            are.WaitOne();


        }
    }
}
