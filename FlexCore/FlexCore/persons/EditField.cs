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
    public partial class EditField : UserControl, IObservable<EventDTO>
    {

        private List<IObserver<EventDTO>> _observers;

        public EditField()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public EditField(string pTitle, string pValue, bool pEraseable=true, bool pSaveButton = false)
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
            editValue.Text = pValue;
            if (!pEraseable)
            {
                eraseOption.Visible = false;
            }
            if (!pSaveButton)
            {
                saveButton.Visible = false;
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

        public string getEditValue()
        {
            return editValue.Text;
        }

        public string getTitle()
        {
            return itemTitle.Text;
        }

        private void eraseOption_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.ERASE_EDIT_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.SAVE_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
