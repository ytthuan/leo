using System;
using NetMQ.ReactiveExtensions;

namespace leoRXPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriber1 = new SubscriberNetMq<int>("tcp://127.0.0.1:56001");
            subscriber1.Subscribe(message =>
            {
                Console.Write(message); // Prints "42".
            });
            var publisher = new PublisherNetMq<int>("tcp://127.0.0.1:56001");
            publisher.OnNext(44);
            
            Console.ReadLine();

        }
    }
}
