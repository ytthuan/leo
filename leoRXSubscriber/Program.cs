using System;
using NetMQ.ReactiveExtensions;
namespace leoRXSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
           // Application 1 (subscriber)
            var subscriber1 = new SubscriberNetMq<string>("tcp://127.0.0.1:56000");
            subscriber1.Subscribe(message =>
            {
                Console.WriteLine("I am subscriber and I received message from publisher: {0}",message); // Prints "42".
            });            

            Console.ReadLine();
        }
    }
}
