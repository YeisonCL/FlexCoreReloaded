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

        public PersonInfo()
        {
            InitializeComponent();
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

            //BASICS
            PersonInfoSpace basics = new PersonInfoSpace(BASIC_DATA, person);
            basics.Subscribe(this);


            //PHONES
            PersonInfoSpace phones = new PersonInfoSpace(PHONES, person);
            phones.Subscribe(this);

            //ADDRESS
            PersonInfoSpace address = new PersonInfoSpace(ADDRESS, person);
            address.Subscribe(this);

            //DOCS
            PersonInfoSpace docs = new PersonInfoSpace(DOCUMENTS, person);
            docs.Subscribe(this);

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
