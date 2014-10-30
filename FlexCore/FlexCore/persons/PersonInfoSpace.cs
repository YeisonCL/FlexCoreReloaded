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
using FlexCoreDTOs.clients;

namespace FlexCore.persons
{
    public partial class PersonInfoSpace : UserControl, IObservable<EventDTO>, IObserver<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;
        private List<Control> _editList;
        PersonTitle _title;
        bool _forEdit;
        bool _forDocument;
        bool _allowAdding;

        public PersonInfoSpace()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            _editList = new List<Control>();
        }

        public PersonInfoSpace(string pTitle, bool pForEdit = false, bool pAllowAdding = true, bool pForDocument=false)
            :this()
        {
            _title = new PersonTitle(pTitle, pAllowAdding);
            titlePanel.Controls.Add(_title);
            _forEdit = pForEdit;
            _title.Subscribe(this);
            _forDocument = pForDocument;
            _allowAdding = pAllowAdding;
        }

        public void addEditable(string pTitle="")
        {
            if (_forEdit)
            {
                EditField field = new EditField(pTitle, "", _allowAdding);
                field.Subscribe(this);
                _editList.Add(field);
                itemList.Controls.Add(field);
            }
        }

        public void addEditableDoc()
        {
            if (_forEdit)
            {
                EditDocument field = new EditDocument(_allowAdding);
                field.Subscribe(this);
                _editList.Add(field);
                itemList.Controls.Add(field);
            }
        }

        public void addDoc(string pName, string pDescription, bool pNew=false)
        {
            DocumentField field = new DocumentField(pName, pDescription, _allowAdding);
            field.Subscribe(this);
            itemList.Controls.Add(field);
            if (pNew)
            {
                field.changeToEdit();
            }
        }

        public List<Control> getEditList() { return _editList; }

        public void addInfo(string pValue, string pTitle = "", bool pNew = false)
        {
            PersonData data = new PersonData(pValue, pTitle, _allowAdding);
            data.Subscribe(this);
            itemList.Controls.Add(data);
            if (pNew)
            {
                data.changeToEdit();
            }
        }

        public void addNewSpace()
        {
            if (!_forEdit)
            {
                if (_forDocument)
                {
                    addDoc("", "", true);
                }
                else
                {
                    addInfo("", "", true);
                }
            }
            else
            {
                if (_forDocument)
                {
                    addEditableDoc();
                }
                else
                {
                    addEditable();
                }
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
            else if (value.getEventCode() == EventDTO.ERASE_EDIT_BUTTON)
            {
                itemList.Controls.Remove((Control)value.getOrigin());
                _editList.Remove((Control)value.getOrigin());
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
