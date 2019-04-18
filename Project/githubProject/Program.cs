using System;
using System.Reactive.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.PlatformServices;
using System.Reactive.Subjects;

namespace githubProject
{
    class Program
    {
        static void Main(string[] args)
        {
            IObservable<int> source = null;
            IObserver<int> handler = null;
            IDisposable subscription = source.Subscribe(handler);
            //Observable.Range(1,5);//creates an observable sequence of 5 integers, starting from 1
            IDisposable subscribe = source.Subscribe(x => Console.WriteLine("onNext: {0}", x),
                                                    ex => Console.WriteLine("onError: {0}", ex),
                                                    () => Console.WriteLine("onCompleted"));

            Console.WriteLine("Press ENTER to unsubscribe...");
            Console.ReadLine();
            subscription.Dispose();
        }
    }

    public interface IObservable<out T>
    {
        IDisposable Subscribe(IObserver<T> observer);

    }

    public interface IObserver<in T>
    {
        void OnCompleted();// Notifies the observer that the source has finished sending messages
        void OnError(Exception error);// Notifies the observer about any exception or error.
        void OnNext(T Value);// Pushes the next data value from the source to the observer.

    }
}
