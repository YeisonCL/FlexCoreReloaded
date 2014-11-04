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

namespace FlexCore.persons
{
    public partial class PersonsMain : UserControl, IObserver<EventDTO>, IObservable<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;

        public PersonsMain()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            personsMenu.Subscribe(this);
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
            foreach (var observer in _observers)
            {
                observer.OnNext(value);
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.SEARCH, null, searchText.Text);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void searchText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {

            }
        }

        private void searchText_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
