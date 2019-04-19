using System;
using NetMQ.ReactiveExtensions;

namespace leoRXPublisher
{
    class Program
    {
        private const string V = "tcp://127.0.0.1:56000";

        static void Main(string[] args)
        {
            
            var publisher = new PublisherNetMq<int>(V);
            publisher.OnNext(49); // Sends 42.
            Console.ReadLine();    

        }
    }
}
