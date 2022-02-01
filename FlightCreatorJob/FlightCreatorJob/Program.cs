using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FlightCreatorJob
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            var jobThread = new Thread(() =>
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    while (true)
                    {
                        httpClient.GetAsync("http://localhost:5500/api/airport/land/"+i);
                        Thread.Sleep(3000);
                        i++;
                        httpClient.GetAsync("http://localhost:5500/api/airport/departure/" + i);
                        Thread.Sleep(1200);
                        i++;
                    }
                }
            });
            jobThread.Start();
        }

    }

}
