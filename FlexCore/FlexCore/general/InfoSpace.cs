using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexCore.general
{
    public abstract partial class InfoSpace : UserControl, IObservable<EventDTO>, IObserver<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;

        public InfoSpace()
        {
            InitializeComponent();
        }

        public IDisposable Subscribe(IObserver<EventDTO> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<EventDTO>(_observers, observer);
        }

        public void OnCompleted()
        {
            throw new Exception("Not implemented yet.");
        }

        public void OnError(Exception error)
        {
            throw new Exception("Not implemented yet.");
        }

        public abstract void OnNext(EventDTO value);
        
    }
}
