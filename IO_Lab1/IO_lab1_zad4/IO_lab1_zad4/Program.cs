using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab_zad4
{
    class Program
    {
        static Object thisLock = new Object();
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(Server);

            ThreadPool.QueueUserWorkItem(Client, new object[] { "Klient 1" });
            Thread.Sleep(1000);
            ThreadPool.QueueUserWorkItem(Client, new object[] { "Klient 2" });
            Thread.Sleep(1000);
            Console.ReadKey();
        }
        static void Server(Object state)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
            while (true)
            {
                    TcpClient client = server.AcceptTcpClient();
                    byte[] buffer = new byte[1024];
                    client.GetStream().Read(buffer, 0, 1024);
                    client.GetStream().Write(buffer, 0, buffer.Length);
                    lock (thisLock)
                    {
                        writeConsoleMessage(System.Text.Encoding.UTF8.GetString(buffer), ConsoleColor.Red);
                    }
                    client.Close();
                
            }
        }
        static void Client(Object wiadomosc)
        {           
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
            var mes = ((object[])wiadomosc)[0];
            byte[] message = new ASCIIEncoding().GetBytes((string)mes);
            client.GetStream().Write(message, 0, message.Length);
            lock (thisLock)
            {
                writeConsoleMessage(System.Text.Encoding.UTF8.GetString(message), ConsoleColor.Green);
            }
        }
        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

    }
}
