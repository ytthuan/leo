using System;
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

            var publisher = new PublisherNetMq<string>("tcp://127.0.0.1:56000");
            while (true)
            {
                Console.WriteLine("enter the message to send via network");
                string a = Console.ReadLine();

                publisher.OnNext(a); // Sends 42.    
            }
        }
    }
}
