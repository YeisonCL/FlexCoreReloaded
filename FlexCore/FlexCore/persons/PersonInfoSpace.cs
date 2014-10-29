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
    public partial class PersonInfoSpace : UserControl, IObservable<EventDTO>, IObserver<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;
        private List<EditField> _editList;
        PersonTitle _title;
        bool _forEdit;

        public PersonInfoSpace()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            _editList = new List<EditField>();
        }

        public PersonInfoSpace(string pTitle, bool pForEdit = false, bool pAllowAdding = true)
            :this()
        {
            _title = new PersonTitle(pTitle, pAllowAdding);
            titlePanel.Controls.Add(_title);
            _forEdit = pForEdit;
            _title.Subscribe(this);
        }

        public void addEditable(string pTitle="")
        {
            if (_forEdit)
            {
                EditField field = new EditField(pTitle, "");
                _editList.Add(field);
                itemList.Controls.Add(field);
            }
        }

        public void addInfo(string pValue, string pTitle="")
        {
            PersonData data = new PersonData(pValue, pTitle);
            itemList.Controls.Add(data);
            data.Subscribe(this);
        }

        public void addNewSpace()
        {
            if (!_forEdit)
            {
                PersonData data = new PersonData("", "");
                data.changeToEdit();
                itemList.Controls.Add(data);
                data.Subscribe(this);
            }
            else
            {
                EditField field = new EditField("", "");
                _editList.Add(field);
                itemList.Controls.Add(field);
            }
        }

        public string getTitle()
        {
            return _title.getTitle();
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

        public void OnNext(EventDTO value)
        {
            if (value.getEventCode() == EventDTO.NEW_BUTTON)
            {
                addNewSpace();
            }
            else
            {
                EventDTO dto = new EventDTO(this, EventDTO.PERSON_INFO_SPACE_EVENT, value);
                foreach (var observer in _observers)
                {
                    observer.OnNext(dto);
                }
            }
        }
        
    }
}
