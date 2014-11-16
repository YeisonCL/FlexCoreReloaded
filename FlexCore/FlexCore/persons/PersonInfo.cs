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
    public partial class PersonInfo : UserControl, IObserver<EventDTO>, IObservable<EventDTO>
    {

        private static readonly string BASIC_DATA = "Datos básicos";
        private static readonly string PHONES = "Teléfonos";
        private static readonly string ADDRESS = "Direcciones";
        private static readonly string DOCUMENTS = "Documentos";
        private static readonly string ID_CARD = "Cédula";

        private PersonDTO _person;
        protected List<IObserver<EventDTO>> _observers;
        private PersonInfoSpace _general;

        public PersonInfo()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public PersonInfo(int pPersonID, string pType)
            :this()
        {
            typeText.Text = pType;
            PersonDTO person;
            if (pType == Person.PHYSICAL_PERSON)
            {
                PhysicalPersonDTO phyPerson = new PhysicalPersonDTO(pPersonID);
                phyPerson = PersonConnection.getPhysicalPerson(phyPerson)[0];
                nameText.Text = String.Format("{0} {1} {2}", phyPerson.getName(), phyPerson.getFirstLastName(), phyPerson.getSecondLastName());
                person = phyPerson;
            }
            else
            {
                person = new PersonDTO(pPersonID);
                person = PersonConnection.getJuridicalPerson(person)[0];
                nameText.Text = person.getName();
            }
            _person = person;

            //BASICS
            _general = new PersonInfoSpace(BASIC_DATA, person);
            _general.Subscribe(this);


            //PHONES
            PersonInfoSpace phones = new PersonInfoSpace(PHONES, person);
            phones.Subscribe(this);

            //ADDRESS
            PersonInfoSpace address = new PersonInfoSpace(ADDRESS, person);
            address.Subscribe(this);

            //DOCS
            PersonInfoSpace docs = new PersonInfoSpace(DOCUMENTS, person);
            docs.Subscribe(this);

            itemList.Controls.Add(_general);
            itemList.Controls.Add(phones);
            itemList.Controls.Add(address);
            itemList.Controls.Add(docs);
        }

        public void updatePersonDTO(PersonDTO pPerson)
        {
            if (pPerson.getPersonType() == PersonDTO.JURIDIC_PERSON)
            {
                nameText.Text = pPerson.getName();
            }
            else if (pPerson.getPersonType() == PersonDTO.PHYSICAL_PERSON)
            {
                nameText.Text = String.Format("{0} {1} {2}", pPerson.getName(), ((PhysicalPersonDTO)pPerson).getFirstLastName(), ((PhysicalPersonDTO)pPerson).getSecondLastName());
            }
            _general.setPerson(pPerson);
        }

        public IDisposable Subscribe(IObserver<EventDTO> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<EventDTO>(_observers, observer);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

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
            if (value.getEventCode() == EventDTO.SAVE_BUTTON && value.getOrigin().GetType() == typeof(PersonInfoSpace))
            {
                _person.setIDCard(((PersonInfoSpace)value.getOrigin()).getPersonDTO().getIDCard());
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            NameEdit edit = new NameEdit(this, _person);
            edit.ShowDialog();
        }

        private void eraseButton_Click(object sender, EventArgs e)
        {
            if (_person.getPersonType() == PersonDTO.JURIDIC_PERSON)
            {
                PersonConnection.deleteJuridicalPerson(_person);
            }
            else if (_person.getPersonType() == PersonDTO.PHYSICAL_PERSON)
            {
                PersonConnection.deleteJuridicalPerson((PhysicalPersonDTO)_person);
            }
            EventDTO evalue = new EventDTO(this, EventDTO.CANCEL);
            foreach (var observer in _observers)
            {
                observer.OnNext(evalue);
            }
        }

    }
}
