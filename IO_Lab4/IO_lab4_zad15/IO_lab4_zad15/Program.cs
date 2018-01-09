using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IO_lab4_zad15
{
    class Serwer
    {
        TcpListener serwer;
        CancellationTokenSource token = new CancellationTokenSource();
        Task serverTask;
        
        
        public async Task Uruchom(CancellationToken token)
        {
            serwer = new TcpListener(IPAddress.Any, 2048);
            serwer.Start();
            
            
            while (!token.IsCancellationRequested)
            {
                TcpClient client = await serwer.AcceptTcpClientAsync();
                byte[] buffer = new byte[1024];
                using (token.Register(() => client.GetStream().Close()))
                {
                    client.GetStream().ReadAsync(buffer, 0, buffer.Length, token).ContinueWith(
                    async (t) =>
                        {
                            int i = t.Result;
                            while (true)
                            {
                                client.GetStream().WriteAsync(buffer, 0, i, token);
                                i = await client.GetStream().ReadAsync(buffer, 0, buffer.Length, token);
                            }
                    });
                }
            }
        }

        public void Zatrzymaj()
        {
            token.Cancel();
            serwer.Stop();
        }
        
        public void Run()
        {
            serverTask = Uruchom(token.Token);
        }
        
    }
    class Klient
    {
        TcpClient klient;
        public void Polacz()
        {
            klient = new TcpClient();
            klient.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));
        }




        public async Task<string> Ping(string message)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes(message);
            klient.GetStream().WriteAsync(buffer, 0, buffer.Length);
            buffer = new byte[1024];
            var t = await klient.GetStream().ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, t);
        }

        public async Task<IEnumerable<string>> keepPinging(string message, CancellationToken token)
        {
            List<string> messages = new List<string>();
            bool done = false;
            while (!done)
            {
                if (token.IsCancellationRequested)
                    done = true;
                messages.Add(await Ping(message));
            }
            return messages;
        }
    }
    

    class Program
    {
        public static void Serwer_Log(Task<IEnumerable<string>> Zadania)
        {
            foreach (var wiadomosc in Zadania.Result)
            {
                Console.WriteLine(wiadomosc);
            }
        }

        static void Main(string[] args)
        {
            Serwer s = new Serwer();

            s.Run();

            Klient klient_1 = new Klient();
            Klient klient_2 = new Klient();
            Klient klient_3 = new Klient();

            klient_1.Polacz();
            klient_2.Polacz();
            klient_3.Polacz();

            CancellationTokenSource token_1 = new CancellationTokenSource();
            CancellationTokenSource token_2 = new CancellationTokenSource();
            CancellationTokenSource token_3 = new CancellationTokenSource();

            var client1T = klient_1.keepPinging("Wiadomosc od klienta nr 1", token_1.Token);
            var client2T = klient_2.keepPinging("Wiadomosc od klienta nr 2", token_2.Token);
            var client3T = klient_3.keepPinging("Wiadomosc od klienta nr 3", token_3.Token);

            token_1.CancelAfter(20);
            token_2.CancelAfter(30);
            token_3.CancelAfter(40);

            Serwer_Log(client1T);
            Serwer_Log(client2T);
            Serwer_Log(client3T);


            Task.WaitAll(new Task[] { client1T, client2T, client3T });

            s.Zatrzymaj();
            Console.ReadKey();

        }
    }
}
