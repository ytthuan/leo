using System;
using System.Threading;
using NetMQ.ReactiveExtensions;

namespace leoRXPublisher
{
    class Program
    {
        private const string V = "tcp://127.0.0.1:56000";

        static void Main(string[] args)
        {

            // var publisher = new PublisherNetMq<int>("tcp://127.0.0.1:56001");
            // publisher.OnNext(48); // Sends 42.
            // Console.ReadLine();  

            var publisher = new PublisherNetMq<string>(V);
            while (true)
            {
                
                Console.WriteLine("generate and sending message");     
                string a = Guid.NewGuid().ToString();
                publisher.OnNext(a);
                Console.WriteLine(a);
                Thread.Sleep(2000); // Sends 42.    
            }
        }
    }
}
