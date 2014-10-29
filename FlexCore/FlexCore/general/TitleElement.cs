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
    public abstract partial class TitleElement : UserControl, IObservable<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;

        public TitleElement()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public TitleElement(string pTitle, string pEditOption1, string pEditOption2)
            :this()
        {
            titleItem.Text = pTitle;
            editOption1.Text = pEditOption1;
            editOption2.Text = pEditOption2;
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



        protected abstract void editOption1_Click(Object sender, EventArgs e);

        protected abstract void editOption2_Click(Object sender, EventArgs e);
        
    }
}
