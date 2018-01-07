using System;
using System.Collections.Generic;
using RxDemo.Model;
using RxDemo.Observables;

namespace RxDemo.Design
{
    public class DesignDataService : IDataService
    {
        public void GetData(IEnumerable<string> urls, Action<WebObservable> callback)
        {
            throw new NotImplementedException();
        }
    }
}