using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlexCore.general;

namespace FlexCore.cuts
{
    public partial class CutsMain : UserControl, IObserver<EventDTO>, IObservable<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;

        public CutsMain()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
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
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(EventDTO value)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            
        }

        private void searchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

            }
        }

    }
}
