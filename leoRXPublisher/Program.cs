using System;
using System.Threading;
using NetMQ.ReactiveExtensions;
using ProtoBuf;


namespace leoRXPublisher
{
    class Program
    {
        private const string V = "tcp://127.0.0.1:56000";

        static void Main(string[] args)
        {

            var publisher = new PublisherNetMq<LeoTelemetryData>("tcp://127.0.0.1:56000");                        Console.WriteLine("Begining to generate message");
            double minTemperature = 20;
            double minHumidity = 60;
            Random rand = new Random();
            while (true)
            {
                double currentTemperature = minTemperature + rand.NextDouble() * 15;
                double currentHumidity = minHumidity + rand.NextDouble() * 20;
                string a = Guid.NewGuid().ToString();
                publisher.OnNext(new LeoTelemetryData() { DeviceId = a, Temperature = currentTemperature,Humidity = currentHumidity }); // Sends msg.
                Console.WriteLine("Generated the message DeviceID :{0}; Temp: {1}, Humidity: {2}", a,currentTemperature,currentHumidity);                
                Thread.Sleep(2000);
            }

        }


        [ProtoContract]
        public class LeoTelemetryData
        {
            [ProtoMember(1)]
            public string DeviceId { get; set; }

            [ProtoMember(2)]
            public double Temperature { set; get; }

            [ProtoMember(3)]
            public double Humidity { set; get; }
        }
    }
}