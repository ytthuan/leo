using System;
using System.Threading;
using NetMQ.ReactiveExtensions;
using ProtoBuf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace leoRXPublisher
{
    class Program
    {
        private static string connection1;
        private static string connection2;
        private static string connection3;


        static void Main(string[] args)
        {
            InitSetting();
            StartMyThread(RunPublisher1).Start();
           StartMyThread(RunPublisher2).Start();
             StartMyThread(RunPublisher3).Start();

        }


        ///input a method and start that method as a separately thread
        public static Thread StartMyThread(Action method)
        {
            ThreadStart threadStart1 = new ThreadStart(method);
            Thread thread = new Thread(threadStart1);
            return thread;
        }
        static void InitSetting()
        {
            string env = System.AppContext.BaseDirectory;
            string path = env + "config/appsetting.json";
            var jobject = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<Configuration>(jobject);
            connection1 = config.Connections.connection1;
            connection2 = config.Connections.connection2;
            connection3 = config.Connections.connection3;
        }

        static void RunPublisher1()
        {
            var publisher = new PublisherNetMq<LeoHVAC1>(connection1);
            Console.WriteLine("Begining to generate message");
            double minTemperature = 20;
            double minHumidity = 60;
            Random rand = new Random();
            while (true)
            {
                double currentTemperature = minTemperature + rand.NextDouble() * 15;
                double currentHumidity = minHumidity + rand.NextDouble() * 20;
                var message = new LeoHVAC1() { DeviceId = "HVAC1", Temperature = currentTemperature, Humidity = currentHumidity };
                publisher.OnNext(message); // Sends msg.

                Console.WriteLine
                        ("Generated the message DeviceID: {0}; Temp: {1}, Humidity: {2}", message.DeviceId, message.Temperature, message.Humidity);
                Thread.Sleep(2000);
            }
        }


        static void RunPublisher2()
        {
            var publisher = new PublisherNetMq<LeoHVAC2>(connection1);
            Console.WriteLine("publisher 2 is begining to generate message");
            double minOutput1 = 20;

            Random rand = new Random();
            while (true)
            {
                double currentOutput1 = minOutput1 + rand.NextDouble() * 15;
                var message = new LeoHVAC2() { DeviceId = "HVAC2", Output1 = currentOutput1 };

                publisher.OnNext(message); // Sends msg.

                Console.WriteLine("Generated the message DeviceID: {0}; output1: {1}",
                                        message.DeviceId, message.Output1);
                Thread.Sleep(2000);
            }
        }


        static void RunPublisher3()
        {
            var publisher = new PublisherNetMq<LeoHVAC3>(connection1);
            Console.WriteLine("Begining to generate message");
            double minO1 = 20;
            double minO2 = 30;
            double minO3 = 40;
            Random rand = new Random();
            while (true)
            {
                double output1 = minO1 + rand.NextDouble() * 15;
                double output2 = minO2 + rand.NextDouble() * 20;
                double output3 = minO3 + rand.NextDouble() * 25;
                var message = new LeoHVAC3()
                {
                    DeviceId = "HVAC3",
                    Output1 = output1,
                    Output2 = output2,
                    Output3 = output3
                };
                publisher.OnNext(message); // Sends msg.

                Console.WriteLine("Generated the message DeviceID: {0}; output1: {1}, output2: {2}, output3: {3}", message.DeviceId,
                 message.Output1, message.Output2, message.Output3);
                Thread.Sleep(2000);
            }
        }


        //class temperature simmulator
        [ProtoContract]
        public class LeoHVAC1
        {
            [ProtoMember(1)]
            public string DeviceId { set; get; }

            [ProtoMember(2)]
            public double Temperature { set; get; }

            [ProtoMember(3)]
            public double Humidity { set; get; }
        }



        //class device hvac2 simmulator    
        [ProtoContract]
        public class LeoHVAC2
        {
            [ProtoMember(1)]
            public string DeviceId { set; get; }

            [ProtoMember(2)]
            public double Output1 { set; get; }

        }


        //class device hvac3 simmulator    

        [ProtoContract]
        public class LeoHVAC3
        {
            [ProtoMember(1)]
            public string DeviceId { set; get; }

            [ProtoMember(2)]
            public double Output1 { set; get; }

            [ProtoMember(3)]
            public double Output2 { set; get; }

            [ProtoMember(4)]
            public double Output3 { set; get; }
        }
    }

    public class Connections
    {
        public string connection1 { get; set; }
        public string connection2 { get; set; }
        public string connection3 { get; set; }
    }
    public class Configuration
    {
        public Connections Connections { get; set; }
    }
}