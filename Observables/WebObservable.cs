using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using RxDemo.Model;

namespace RxDemo.Observables
{
    public class WebObservable:IObservable<Response>
    {
        private Dictionary<IDisposable, IObserver<Response>> _subscribers=
            new Dictionary<IDisposable, IObserver<Response>>();
        private WebWorker _webWorker=new WebWorker();

        public IDisposable Subscribe(IObserver<Response> observer)
        {
            var disp=new MyDisposable(_subscribers);
            _subscribers.Add(disp,observer);
            return disp;
        }

        public IDisposable Subscribe(Action<Response> onNext, Action<Exception> onException, Action onCompleted)
        {
            var disp = new MyDisposable(_subscribers);
            var observer = Observer.Create(onNext, onException, onCompleted);
            _subscribers.Add(disp, observer);
            return disp;
        }

        public  async Task GetResponses(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                try
                {
                    var response=await _webWorker.Get(url);
                    NotifySubscribers(subscriber=>subscriber.OnNext(response));
                }
                catch (Exception e)
                {
                    NotifySubscribers(subscriber=>subscriber.OnError(e));
                }
            }
            NotifySubscribers(subscriber=>subscriber.OnCompleted());
        }

        private void NotifySubscribers(Action<IObserver<Response>> action)
        {
            foreach (var subscriber in _subscribers)
            {
                action.Invoke(subscriber.Value);
            }
        }
    }

    class MyDisposable : IDisposable
    {
        private Dictionary<IDisposable, IObserver<Response>> _dict;

        public MyDisposable(Dictionary<IDisposable, IObserver<Response>> dict)
        {
            _dict = dict;
        }

        public void Dispose()
        {
            _dict.Remove(this);
        }
    }
}
