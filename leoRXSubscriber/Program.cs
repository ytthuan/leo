using System;
using NetMQ.ReactiveExtensions;
using ProtoBuf;
using System.Reactive.Linq;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace leoRXSubscriber
{
    class Program
    {
        private static string connection1;        
        private static string connection3;        

        static void Main(string[] args)
        {
            InitSetting();
            StartMyThread(RunSubscriber1).Start();
            StartMyThread(RunSubscriber2).Start();
            StartMyThread(RunSubscriber3).Start();
            Console.ReadLine();

        }

        public static Thread StartMyThread(Action method)
        {
            ThreadStart threadStart1 = new ThreadStart(method);
            Thread thread = new Thread(threadStart1);
            return thread;
        }

        //get connection information
        static void InitSetting()
        {
            string env = System.AppContext.BaseDirectory;
            string path = env + "config/appsetting.json";
            var jobject = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<Configuration>(jobject);
            connection1 = config.Connections.connection1;
            connection3 = config.Connections.connection3;
          
        }
        static void RunSubscriber1()
        {
            var subscriber1 = new SubscriberNetMq<LeoHVAC1>(connection1);
            //try where            
            subscriber1.Where(x => x.Temperature > 30 && x.DeviceId == "HVAC1").Subscribe(_message =>
                {
                    Console.WriteLine
                        ("Subscriber1 received data from HVAC1 with temp above 30 celsius -  device id: {0}; temp: {1}; humidity {2} ",
                         _message.DeviceId, _message.Temperature, _message.Humidity);
                });
        }
        static void RunSubscriber2()
        {
            var subscriber1 = new SubscriberNetMq<LeoHVAC2>(connection1);
            //try where            
            subscriber1.Where(x => x.DeviceId == "HVAC2").Subscribe(_message =>
                  {
                      Console.WriteLine
                          ("Subscriber2 received data from HVAC2-  device id: {0}; output1: {1} ",
                           _message.DeviceId, _message.Output1);
                  });
        }
        static void RunSubscriber3()
        {
            var subscriber1 = new SubscriberNetMq<LeoHVAC3>(connection3);
            //try where            
            subscriber1.Where(x => x.DeviceId == "HVAC3").Subscribe(_message =>
                {
                    Console.WriteLine
                        ("Subscriber3 received data of HVAC3 -  device id: {0}; output1: {1}, output2: {2}, output3: {3}",
                         _message.DeviceId, _message.Output1, _message.Output2, _message.Output3);
                });
        }

    }



    ///input a method and start that method as a separately thread



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
