namespace githubProject
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive;
    using System.Reactive.Disposables;
    using System.Reactive.PlatformServices;
    using System.Reactive.Subjects;
    using static Newtonsoft.Json.JsonConvert;

    //Iobserver
    public class DemoObserver : IObserver<int>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Observable is done sending all data");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Observable failed with error: {error.Message}");
        }

        public void OnNext(int value)
        {
             Console.WriteLine($"Observable emitted value: {value}");
        }
    }


    public class DemoObservable : IObservable<int>
    {
        public IDisposable Subscribe(IObserver<int> observer)
        {
            observer.OnNext(1);
            observer.OnNext(2);
            observer.OnNext(3);
            observer.OnNext(4);
            observer.OnNext(5);
            observer.OnCompleted();
            return System.Reactive.Disposables.Disposable.Empty;

        }
    }   

    public class DemoObservableCreate
    {
        public static IObservable<int> ObserveNumber(int amount) => Observable.Create<int>(observer =>
        {
            observer.OnNext(1);
            observer.OnNext(2);
            observer.OnNext(3);
            observer.OnNext(4);
            observer.OnNext(5);
            observer.OnNext(6);
            observer.OnCompleted();
            return Disposable.Empty;
        });
    }

}