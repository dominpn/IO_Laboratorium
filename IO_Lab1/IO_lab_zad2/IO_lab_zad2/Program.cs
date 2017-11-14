using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab_zad2
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem(Server);
            ThreadPool.QueueUserWorkItem(Client, new object[] { "Klient 1" });
            Thread.Sleep(1000);
            ThreadPool.QueueUserWorkItem(Client, new object[] { "Klient 2" });
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
                Console.WriteLine();
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

        }
    }
}
