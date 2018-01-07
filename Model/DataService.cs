using System;
using System.Collections.Generic;
using System.Net;
using RxDemo.Observables;

namespace RxDemo.Model
{
    public class DataService : IDataService
    {
        public WebWorker WebWorker { get; set; }=new WebWorker();
        private Action<WebObservable> _callback;
        public void GetData(IEnumerable<string> urls,
            Action<WebObservable> callback)
        {
            var observable = new WebObservable();
            _callback(observable);
            observable.GetResponses(urls);
        }

        public void SetCallback(Action<WebObservable>callback)
        {
            _callback = callback;
        }
    }

    public class Response
    {
        public string Content { get; }
        public HttpStatusCode Result { get; }

        public Response(string content, HttpStatusCode result)
        {
            Content = content;
            Result = result;
        }
    }
}