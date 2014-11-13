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
    public partial class PersonInfo : UserControl, IObserver<EventDTO>
    {

        private static readonly string BASIC_DATA = "Datos básicos";
        private static readonly string PHONES = "Teléfonos";
        private static readonly string ADDRESS = "Direcciones";
        private static readonly string DOCUMENTS = "Documentos";



        public PersonInfo()
        {
            InitializeComponent();
        }

        public PersonInfo(string pFullName, string pType)
            :this()
        {
            nameText.Text = pFullName;
            typeText.Text = pType;
            initializeMe();
        }

        private void initializeMe()
        {
            PersonInfoSpace basics = new PersonInfoSpace(BASIC_DATA, false, false);
            basics.Subscribe(this);
            basics.addInfo("123456789", "Cédula:");

            PersonInfoSpace phones = new PersonInfoSpace(PHONES);
            phones.Subscribe(this);

            PersonInfoSpace address = new PersonInfoSpace(ADDRESS);
            address.Subscribe(this);

            PersonInfoSpace docs = new PersonInfoSpace(DOCUMENTS, false, true, true);
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
