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
    public abstract partial class DataElement : UserControl, IObservable<EventDTO>
    {

        protected List<IObserver<EventDTO>> _observers;

        public DataElement()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public DataElement(string pValue, string pTitle="", string pEditOption1="", string pEditOption2="")
            :this()
        {
            if (pTitle == "")
            {
                itemTitle.Visible = false;
            }
            else 
            {
                itemTitle.Text = pTitle;
            }
            itemText.Text = pValue;
            editValue.Text = pValue;

            if (pEditOption1 == "")
            {
                editOption1.Visible = false;
            }
            else
            {
                editOption1.Text = pEditOption1;
            }

            if (pEditOption2 == "")
            {
                editOption2.Visible = false;
            }
            else
            {
                editOption2.Text = pEditOption2;
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

        public void changeToEdit()
        {
            editValue.Visible = true;
            itemText.Visible = false;
        }

        public void changeToView()
        {
            editValue.Visible = false;
            itemText.Visible = true;
        }

        public string getEditValue()
        {
            return editValue.Text;
        }

        public string getOriginalValue()
        {
            return itemText.Text;
        }

        public string getTitle()
        {
            return itemTitle.Text;
        }

        protected abstract void editOption1_Click(Object sender, EventArgs e);

        protected abstract void editOption2_Click(Object sender, EventArgs e);
    }
}
