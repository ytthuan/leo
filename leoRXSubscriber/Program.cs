using System;
using NetMQ.ReactiveExtensions;
using ProtoBuf;
namespace leoRXSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var subscriber1 = new SubscriberNetMq<LeoTelemetryData>("tcp://127.0.0.1:56000");


            subscriber1.Subscribe(_message =>
            {
                if (_message.Temperature >= 30.00)
                {
                    Console.WriteLine
                    ("Subscriber received data with temp above 30 celsius -  device id: {0}; temp: {1}; humidity {2} ",
                    _message.DeviceId,_message.Temperature,_message.Humidity);
                }
                else
                {
                    Console.WriteLine
                         ("Subscriber received data with temp below 30 celsius - device id: {0}; temp: {1}; humidity {2} ",
                    _message.DeviceId,_message.Temperature,_message.Humidity);
                }
            });

            Console.ReadLine();

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
