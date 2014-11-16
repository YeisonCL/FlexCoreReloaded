using FlexCoreDTOs.clients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexCore.persons
{
    public partial class NameEdit : Form
    {        

        private PersonDTO _person;
        private EditField _name;
        private EditField _lastName1;
        private EditField _lastName2;

        private Control _parent;

        public NameEdit()
        {
            InitializeComponent();
        }

        public NameEdit(Control pParent, PersonDTO pPerson)
            :this()
        {
            _person = pPerson;
            _name = new EditField("Nombre:", pPerson.getName(), false);
            _name.Name = "name";
            itemList.Controls.Add(_name);
            if (pPerson.getPersonType() == PersonDTO.PHYSICAL_PERSON)
            {
                string ln1 = ((PhysicalPersonDTO)pPerson).getFirstLastName();
                string ln2 = ((PhysicalPersonDTO)pPerson).getSecondLastName();
                initializePhysical(ln1, ln2);
            }
        }

        private void initializePhysical(string pLastName1, string pLastName2)
        {
            _lastName1 = new EditField("Primer apellido:", pLastName1, false);
            _lastName2 = new EditField("Segundo apellido:", pLastName2, false);
            _lastName1.Name = "firstLastName";
            _lastName2.Name = "secondLastName";
            itemList.Controls.Add(_lastName1);
            itemList.Controls.Add(_lastName2);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (_person.getPersonType() == PersonDTO.JURIDIC_PERSON)
            {
                PersonDTO newPerson = new PersonDTO();
                newPerson.setPersonID(_person.getPersonID());
                newPerson.setIDCard(_person.getIDCard());
                newPerson.setName(_name.getEditValue());
                PersonConnection.updateJuridicalPerson(_person, newPerson);
            }
            else if (_person.getPersonType() == PersonDTO.PHYSICAL_PERSON)
            {
                PhysicalPersonDTO newPerson = new PhysicalPersonDTO();
                newPerson.setPersonID(_person.getPersonID());
                newPerson.setIDCard(_person.getIDCard());
                newPerson.setName(_name.getEditValue());
                newPerson.setFirstLastName(_lastName1.getEditValue());
                newPerson.setSecondLastName(_lastName2.getEditValue());
                PersonConnection.updatePhysicalPerson((PhysicalPersonDTO)_person, newPerson);
            }
            _parent.Enabled = true;
            this.Dispose();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NameEdit_Load(object sender, EventArgs e)
        {

        }

    }
}
