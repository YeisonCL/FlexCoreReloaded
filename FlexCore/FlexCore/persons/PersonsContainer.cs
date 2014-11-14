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
    public partial class PersonsContainer : UserControl, IObserver<EventDTO>
    {
        public PersonsContainer()
        {
            InitializeComponent();
            PersonsMain persons = new PersonsMain();
            persons.Subscribe(this);
            setConentPanel(persons);
        }

        private void setConentPanel(UserControl pPanel)
        {
            panel.Controls.Clear();
            pPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            pPanel.Location = new System.Drawing.Point(0, 0);
            panel.Controls.Add(pPanel);
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void personsMain1_Load(object sender, EventArgs e)
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
            if (value.getEventCode() == EventDTO.NEW_BUTTON)
            {
                NewPerson newPerson = new NewPerson();
                newPerson.Subscribe(this);
                setConentPanel(newPerson);
            }
            else if (value.getEventCode() == EventDTO.PERSON_CLICK)
            {
                Person person = (Person)value.getOrigin();
                PersonInfo personInfo = new PersonInfo(person.getPersonID(), person.getPersonType());
                setConentPanel(personInfo);
            }
            else if (value.getEventCode() == EventDTO.CANCEL)
            {
                PersonsMain main = new PersonsMain();
                main.Subscribe(this);
                setConentPanel(main);
            }
            else if (value.getEventCode() == EventDTO.SAVE_BUTTON)
            {
                PersonsMain main = new PersonsMain();
                main.Subscribe(this);
                setConentPanel(main);
            }
            else if (value.getEventCode() == EventDTO.SEARCH)
            {
                PersonSearchResults search = new PersonSearchResults((string)value.getValue());
                search.Subscribe(this);
                setConentPanel(search);
            }
        }

    }
}
