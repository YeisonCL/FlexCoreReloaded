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
    public partial class PersonInfo : UserControl, IObserver<EventDTO>
    {

        private static readonly string BASIC_DATA = "Datos básicos";
        private static readonly string PHONES = "Teléfonos";
        private static readonly string ADDRESS = "Direcciones";
        private static readonly string DOCUMENTS = "Documentos";
        private static readonly string ID_CARD = "Cédula";

        private int _personID;

        public PersonInfo()
        {
            InitializeComponent();
        }

        public PersonInfo(int pPersonID, string pType)
            :this()
        {
            _personID = pPersonID;
            initializeMe(pType);
        }

        private void initializeMe(string pType)
        {
            typeText.Text = pType;
            PersonDTO person;
            if (pType == Person.PHYSICAL_PERSON)
            {
                PhysicalPersonDTO phyPerson = new PhysicalPersonDTO(_personID);
                phyPerson = PersonConnection.getPhysicalPerson(phyPerson)[0];
                nameText.Text = String.Format("{0} {1} {2}", phyPerson.getName(), phyPerson.getFirstLastName(), phyPerson.getSecondLastName());
                person = phyPerson;
            }
            else
            {
                person = new PersonDTO(_personID);
                person = PersonConnection.getJuridicalPerson(person)[0];
                nameText.Text = person.getName();
            }
            
            //BASICS
            PersonInfoSpace basics = new PersonInfoSpace(BASIC_DATA, false);
            basics.Subscribe(this);
            basics.addInfo(person.getIDCard(), ID_CARD);
            

            //PHONES
            PersonInfoSpace phones = new PersonInfoSpace(PHONES, false);
            phones.Subscribe(this);

            List<PersonPhoneDTO> phoneList = PersonConnection.getPersonPhones(_personID);
            foreach (var phone in phoneList)
            {
                phones.addInfo(phone.getPhone());
            }

            //ADDRESS
            PersonInfoSpace address = new PersonInfoSpace(ADDRESS);
            address.Subscribe(this);

            List<PersonAddressDTO> addressList = PersonConnection.getPersonAddress(_personID);
            foreach (var addressItem in addressList)
            {
                address.addInfo(addressItem.getAddress());
            }

            //DOCS
            PersonInfoSpace docs = new PersonInfoSpace(DOCUMENTS, false);
            docs.Subscribe(this);

            List<PersonDocumentDTO> docList = PersonConnection.getPersonDocuments(_personID);
            foreach (var doc in docList)
            {
                docs.addDoc(doc.getName(), doc.getDescription(), false);
            }


            itemList.Controls.Add(basics);
            itemList.Controls.Add(phones);
            itemList.Controls.Add(address);
            itemList.Controls.Add(docs);
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
            
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            NameEdit edit = new NameEdit();
            edit.ShowDialog();
        }

        private void eraseButton_Click(object sender, EventArgs e)
        {

        }

    }
}
