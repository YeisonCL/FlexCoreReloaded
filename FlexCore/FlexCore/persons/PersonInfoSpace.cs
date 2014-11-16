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
using System.IO;

namespace FlexCore.persons
{
    public partial class PersonInfoSpace : UserControl, IObservable<EventDTO>, IObserver<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;
        private List<Control> _editList;
        private PersonTitle _title;

        private bool _forEdit;
        private bool _forDocument;
        private bool _allowAdding;
        private PersonDTO _person;
        private string _type;

        private static readonly string BASIC_DATA = "Datos básicos";
        private static readonly string PHONES = "Teléfonos";
        private static readonly string ADDRESS = "Direcciones";
        private static readonly string DOCUMENTS = "Documentos";

        private static readonly string ID_CARD = "Cédula";
        private static readonly string CIF = "CIF";

        public PersonInfoSpace()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            _editList = new List<Control>();
        }

        public PersonInfoSpace(string pType, PersonDTO pPerson, string pCIF = "")
            :this()
        {
            _type = pType;
            _forEdit = false;
            _person = pPerson;
            if (pType == DOCUMENTS)
            {
                _title = new PersonTitle(DOCUMENTS, true);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = true;
                _allowAdding = true;

                List<PersonDocumentDTO> list = PersonConnection.getPersonDocuments(_person.getPersonID());
                foreach (var doc in list)
                {
                    string docName = doc.getName();
                    string docDescrip = doc.getDescription();
                    addDoc(docName, docDescrip, false);
                }

            }
            else if (pType == PHONES)
            {
                _title = new PersonTitle(PHONES, true);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = false;
                _allowAdding = true;

                List<PersonPhoneDTO> list = PersonConnection.getPersonPhones(_person.getPersonID());
                foreach (var phone in list)
                {
                    addInfo(phone.getPhone());
                }

            }
            else if (pType == ADDRESS)
            {
                _title = new PersonTitle(ADDRESS, true);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = false;
                _allowAdding = true;

                List<PersonAddressDTO> list = PersonConnection.getPersonAddress(_person.getPersonID());
                foreach (var address in list)
                {
                    addInfo(address.getAddress());
                }
            }
            else if (pType == BASIC_DATA)
            {
                _title = new PersonTitle(BASIC_DATA, false);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = false;
                _allowAdding = true;

                if (!_forEdit)
                {
                    addInfo(_person.getIDCard(), ID_CARD);
                    if (pCIF != "")
                    {
                        addInfo(pCIF, CIF);
                    }
                    else
                    {
                        //HACER CONSULTA SI ES CLIENTE
                    }
                }
            }
        }

        public PersonInfoSpace(string pType)
            :this()
        {
            _type = pType;
            _forEdit = true;
            if (pType == DOCUMENTS)
            {
                _title = new PersonTitle(DOCUMENTS, true);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = true;
                _allowAdding = true;
            }
            else if (pType == PHONES)
            {
                _title = new PersonTitle(PHONES, true);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = false;
                _allowAdding = true;
            }
            else if (pType == ADDRESS)
            {
                _title = new PersonTitle(ADDRESS, true);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = false;
                _allowAdding = true;
            }
            else if (pType == BASIC_DATA)
            {
                _title = new PersonTitle(BASIC_DATA, false);
                titlePanel.Controls.Add(_title);
                _title.Subscribe(this);
                _forDocument = false;
                _allowAdding = true;
            }
        }

        public void addEditable(string pTitle="")
        {
            EditField field = new EditField(pTitle, "", _allowAdding, !_forEdit);
            field.Subscribe(this);
            itemList.Controls.Add(field);
            if (_forEdit){
                _editList.Add(field);
            }
        }

        public void addEditableDoc()
        {
            EditDocument field = new EditDocument(_allowAdding, !_forEdit);
            field.Subscribe(this);
            itemList.Controls.Add(field);
            if (_forEdit)
            {
                _editList.Add(field);
            }
        }

        private void addDoc(string pName, string pDescription, bool pNew=false)
        {
            DocumentField field = new DocumentField(pName, pDescription, _allowAdding, pNew);
            field.changeToView();
            field.Subscribe(this);
            itemList.Controls.Add(field);
            if (pNew)
            {
                field.changeToEdit();
            }   
        }

        private void addInfo(string pValue, string pTitle = "", bool pNew = false)
        {
            PersonData data = new PersonData(pValue, pTitle, _allowAdding);
            data.Subscribe(this);
            itemList.Controls.Add(data);
            if (pNew)
            {
                data.changeToEdit();
            }
        }

        public List<Control> getEditList() { return _editList; }

        private void addNewSpace()
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
            //NEW BOTTON
            if (value.getEventCode() == EventDTO.NEW_BUTTON)
            {
                addNewSpace();
            }

            //ERASE BUTTON - FOR EDIT
            else if (value.getEventCode() == EventDTO.ERASE_EDIT_BUTTON)
            {
                itemList.Controls.Remove((Control)value.getOrigin());
                _editList.Remove((Control)value.getOrigin());
            }

            //ERASE BUTTON - EXISTING
            else if (value.getEventCode() == EventDTO.ERASE_EXISTING_BUTTON)
            {
                itemList.Controls.Remove((Control)value.getOrigin());
                Object origin = value.getOrigin();
                if (origin.GetType() == typeof(PersonData))
                {
                    string eraseValue = ((PersonData)origin).getInitiValue();
                    if (_type == ADDRESS)
                    {
                        PersonAddressDTO dto = new PersonAddressDTO(_person.getPersonID(), eraseValue);
                        PersonConnection.deletePersonAddress(dto);
                    }
                    else if (_type == PHONES)
                    {
                        PersonPhoneDTO dto = new PersonPhoneDTO(_person.getPersonID(), eraseValue);
                        PersonConnection.deletePersonPhone(dto);
                    }                    
                }
                else if (origin.GetType() == typeof(DocumentField))
                {
                    string docName = ((DocumentField)origin).getFileName();
                    PersonDocumentDTO doc = new PersonDocumentDTO(_person.getPersonID());
                    doc.setName(docName);
                    PersonConnection.deletePersondDoc(doc);
                }
            }

            //SAVE BUTTON
            else if (value.getEventCode() == EventDTO.SAVE_BUTTON && !_forEdit)
            {
                Object origin = value.getOrigin();

                if (origin.GetType() == typeof(PersonData))
                {
                    string oldValue = ((PersonData)origin).getInitiValue();
                    string newValue = ((PersonData)origin).getEditValue();
                    if (_type == ADDRESS)
                    {
                        PersonAddressDTO addOld = new PersonAddressDTO(_person.getPersonID(), oldValue);
                        PersonAddressDTO addNew = new PersonAddressDTO(_person.getPersonID(), newValue);
                        PersonConnection.updateAddress(addOld, addNew);
                    }
                    else if (_type == PHONES)
                    {
                        PersonPhoneDTO addOld = new PersonPhoneDTO(_person.getPersonID(), oldValue);
                        PersonPhoneDTO addNew = new PersonPhoneDTO(_person.getPersonID(), newValue);
                        PersonConnection.updatePhone(addOld, addNew);
                    }
                }

                else if (origin.GetType() == typeof(DocumentField))
                {
                    string docName = ((DocumentField)origin).getFileName();
                    string docDir = ((DocumentField)origin).getFileDir();
                    string docDescrip = ((DocumentField)origin).getDescription();
                    if (File.Exists(docDir))
                    {
                        byte[] byteArray = File.ReadAllBytes(docDir);
                        PersonDocumentDTO dto = new PersonDocumentDTO(_person.getPersonID(), byteArray, docName, docDescrip);
                        PersonConnection.updateDocument(dto);
                    }
                    else
                    {
                        MessageBox.Show(String.Format("No se ha encontrado el archivo {0}, este será omitido.", docName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //NEW FIELDS
                else if (origin.GetType() == typeof(EditField))
                {
                    if (_type == ADDRESS)
                    {
                        string address = ((EditField)origin).getEditValue();
                        PersonAddressDTO dto = new PersonAddressDTO(_person.getPersonID(), address);
                        PersonConnection.newAddress(dto);
                    }
                    else if (_type == PHONES)
                    {
                        string phone = ((EditField)origin).getEditValue();
                        PersonPhoneDTO dto = new PersonPhoneDTO(_person.getPersonID(), phone);
                        PersonConnection.newPhone(dto);
                    }
                }

                else if (origin.GetType() == typeof(EditDocument))
                {
                    string docName = ((EditDocument)value.getOrigin()).getFileName();
                    string docDir = ((EditDocument)value.getOrigin()).getFileDir();
                    string docDescrip = ((EditDocument)value.getOrigin()).getDescription();
                    if (File.Exists(docDir))
                    {
                        byte[] byteArray = File.ReadAllBytes(docDir);
                        PersonDocumentDTO dto = new PersonDocumentDTO(_person.getPersonID(), byteArray, docName, docDescrip);
                        PersonConnection.newDocument(dto);
                    }
                    else
                    {
                        MessageBox.Show(String.Format("No se ha encontrado el archivo {0}, este será omitido.", docName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            //ELSE
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
