using System;
using NetMQ.ReactiveExtensions;
namespace leoRXSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            // Application 1 (subscriber)
            using (var subscriber1 = new SubscriberNetMq<int>("tcp://127.0.0.1:56001"))
            {
                subscriber1.Subscribe(onNext: Console.WriteLine);
            }

            // // Application 2 (subscriber)
            // using (var subscriber2 = new SubscriberNetMq<int>("tcp://127.0.0.1:56001"))
            // {
            //     subscriber2.Subscribe(Console.WriteLine);
            // }

            Console.ReadLine();
        }
    }
}
