using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify();
    }
}
