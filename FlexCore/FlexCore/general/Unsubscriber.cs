using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCore.general
{
    class Unsubscriber<t> : IDisposable
    {
        private List<IObserver<t>> _observers;
        private IObserver<t> _observer;

        public Unsubscriber(List<IObserver<t>> observers, IObserver<t> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
