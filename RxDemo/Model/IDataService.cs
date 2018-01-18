using System;
using System.Collections.Generic;
using RxDemo.Observables;

namespace RxDemo.Model
{
    public interface IDataService
    {
        void GetData(IEnumerable<string> urls, Action<WebObservable> callback);
    }
}
