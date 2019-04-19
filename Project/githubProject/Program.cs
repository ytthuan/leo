using System;
using System.Reactive.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;
using static Newtonsoft.Json.JsonConvert;


namespace githubProject
{
    class Program
    {
        static void Main(string[] args)
        {
            #region event handler
            // ///event handler    
            // AdditionDelegate demoDelegate = AddTwoNumbers;
            // //AdditionDelegate demoDelegate = AddTwoNumbers;           
            // demoDelegate += AddTwoMoreNumber;
            // demoDelegate += StillAddTwoMoreNumber;

            // //When this delegate is invoked it will invoke each of the method registered in it. 
            // //However order in execution is not guaranteed guaranteed by .NET. So do not depend on that !
            // Func<int, int, int> demoDelegateFunc = AddTwoNumbers;

            // //anonymous function
            // // Func<int, int , string> demoAnonymousFunc = (int x,int y)=> (x+y).ToString()+" Test Anonymous";
            // //lambda
            // //To put in simple terms, we have defined a lambda on the right side. 
            // //It is taking two parameters which are passed to the code body on the other side of => and the result is then assigned to the delegate. 
            // Func<int, int, string> demoAnonymousFunc = (x, y) => (x + y).ToString() + " Test Anonymous";

            // //Events handler declare
            // FileDownloader fileDownloader = new FileDownloader("How to win a lottery");
            // Reader clientA = new Reader(fileDownloader);
            // fileDownloader.StartFileDownload();

            // Console.WriteLine("Action Delegate: {0}", demoDelegate(1, 2));
            // Console.WriteLine("Func Delegate: {0}", demoDelegateFunc(1, 5));
            // Console.WriteLine("Func Delegate: {0}", demoAnonymousFunc(1, 5));

            #endregion

            #region  ReactiveX
            // var obserVABleInstance = new DemoObservable();
            // var obserVERInstance =  new DemoObserver();            
            // var subscription = obserVABleInstance.Subscribe(obserVERInstance);
            // var subscription1 = DemoObservableCreate.ObserveNumber(10);

            // IObservable<string> obj = Observable.Generate(
            //     0, _ => true,
            //     i => i + 1,
            //     i =>
            //     {
            //         return new string('#', i);
            //     },
            //     i => TimeSelector(i)
            //     );

            // using (obj.Subscribe(Console.WriteLine))
            // {
            //     Console.WriteLine("Press any key to exit!!!");
            //     Console.ReadLine();
            // }

            #endregion

            #region  ReactiveX Over Network

           

            #endregion


            Console.ReadLine();
        }
        //Returns TimeSelector    

        private static TimeSpan TimeSelector(int i)
        {
            return TimeSpan.FromSeconds(i);
        }

        #region Event Handler
        public delegate void EventHandler<TEventArgs>(object sender, TEventArgs e);




        private static int StillAddTwoMoreNumber(int a, int b)
        {
            return a - b;
        }

        private static int AddTwoMoreNumber(int a, int b)
        {
            return a * b;
        }

        static int AddTwoNumbers(int a, int b)
        {
            return a + b;
        }

        delegate int AdditionDelegate(int a, int b);

    }

    public class DownloadCompleteEventArgs : EventArgs
    {
        public DownloadCompleteEventArgs(string fileName)
        {
            FileName = fileName;
        }
        public string FileName { get; }
    }
    public class FileDownloader
    {
        private readonly string _fileName;


        //declare an instance of the event provided by Microsoft and call it FileDownloaded.
        public event EventHandler<DownloadCompleteEventArgs> FileDownloaded;

        public FileDownloader(string fileName)
        {
            _fileName = fileName;
        }


        //StartFileDownload function which will be called by the user of the program to download the file. 
        //Once completed it will call method RaiseFileDownloadedEvent to raise the event that downloading has finished
        public void StartFileDownload()
        {
            //Assume the file downloaded successfully
            //Raise event to subscribers notifying them
            //RaiseFileDownloadedEvent is called. Note how we are packaging information regarding the event using the DownloadCompleteEventArgs.
            RaiseFileDownloadedEvent(new DownloadCompleteEventArgs(_fileName));
        }



        //We check if someone has subscribed to FileDownloaded event in a thread safe manner (C# 6 syntax). 
        //If yes then we raise it via invoke. Done.
        protected virtual void RaiseFileDownloadedEvent(DownloadCompleteEventArgs eventArgs)
        {
            FileDownloaded?.Invoke(this, eventArgs);
        }
    }

    public class Reader
    {
        // public Reader(FileDownloader fileDownloader)
        // {
        //     fileDownloader.FileDownloaded += OnFileDownloaded;
        // }

        // private void OnFileDownloaded(object sender, DownloadCompleteEventArgs e)
        // {
        //     Console.WriteLine("\nThe book '" + e.FileName + "' has been read by Reader A");
        // }

        //become
        public Reader(FileDownloader fileDownloader)
        {
            fileDownloader.FileDownloaded += (sender, evenArgs) =>
            {
                Console.WriteLine("\nThe book '" + evenArgs.FileName + "' has been read by Reader A");
            };
        }

    }

    #endregion

}
