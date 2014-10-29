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
    public partial class PersonTitle : UserControl, IObservable<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;

        public PersonTitle()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public PersonTitle(string pTitle, bool pAllowAdd = true)
            :this()
        {
            titleItem.Text = pTitle;
            if (!pAllowAdd)
            {
                editOption1.Visible = false;
            }
        }

        public IDisposable Subscribe(IObserver<EventDTO> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<EventDTO>(_observers, observer);
        }

        public string getTitle() { return titleItem.Text; }

        private void newButton_Click(Object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.NEW_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }
        
    }
}
