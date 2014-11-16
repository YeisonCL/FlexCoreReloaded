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
    public partial class PersonData : UserControl, IObservable<EventDTO>
    {

        protected List<IObserver<EventDTO>> _observers;
        private string _initValue;

        public PersonData()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public PersonData(string pValue, string pTitle = "", bool pEraseable=true)
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
            if (!pEraseable)
            {
                eraseOption.Visible = false;
            }
            itemText.Text = pValue;
            editValue.Text = pValue;
            _initValue = pValue;
            changeToView();
        }

        public string getInitiValue() { return _initValue; }

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
            editOption1.Visible = false;
            editOption2.Visible = true;
            eraseOption.Visible = true;
            cancelButton.Visible = true;
        }

        public void changeToView()
        {
            editValue.Visible = false;
            itemText.Visible = true;
            editOption1.Visible = true;
            editOption2.Visible = false;
            eraseOption.Visible = false;
            cancelButton.Visible = false;
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

        protected void editButton_Click(Object sender, EventArgs e)
        {
            changeToEdit();
        }

        protected void saveButton_Click(Object sender, EventArgs e)
        {
            itemText.Text = editValue.Text;
            changeToView();
            EventDTO dto = new EventDTO(this, EventDTO.SAVE_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void eraseOption_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.ERASE_EXISTING_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            changeToView();
        }
    }
}
