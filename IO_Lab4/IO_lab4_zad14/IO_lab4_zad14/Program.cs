using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IO_lab4_zad14
{
    class Program
    {
        static async void Pobranie_informacji(string URL)
        {
            string dane = "";
            WebClient webClient = new WebClient();
            try
            {
                dane = await webClient.DownloadStringTaskAsync(URL);
            }
            catch
            {
                Console.WriteLine("Nie udalo sie pobrac danych");
            }
            Console.WriteLine(dane);
        }
        static void Main(string[] args)
        {
            Pobranie_informacji("http://www.feedforall.com/sample.xml");
            Console.ReadKey();
        }


    }
}
