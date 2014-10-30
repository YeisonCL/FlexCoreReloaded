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
    public partial class NewPerson : UserControl, IObservable<EventDTO>
    {

        private static readonly string BASIC_DATA = "Datos básicos";
        private static readonly string PHONES = "Teléfonos";
        private static readonly string ADDRESS = "Direcciones";
        private static readonly string DOCUMENTS = "Documentos";
        private static readonly string PHYSICAL  = "Física";
        private static readonly string JURIDICAL = "Jurídica";
        private static readonly string NAME = "Nombre";
        private static readonly string FIRST_LASTNAME = "Primer apellido";
        private static readonly string SECOND_LASTNAME = "Segundo apellido";
        private static readonly string ID_CARD = "Cédula";

        private PersonInfoSpace _basicData;
        private PersonInfoSpace _phones;
        private PersonInfoSpace _address;
        private PersonInfoSpace _documents;

        protected List<IObserver<EventDTO>> _observers;

        private string _type;

        public NewPerson()
        {
            InitializeComponent();
            personType.Items.Add(PHYSICAL);
            personType.Items.Add(JURIDICAL);
            _observers = new List<IObserver<EventDTO>>();
            saveButton.Visible = false;
        }

        private void initializeMe()
        {
            saveButton.Visible = true;
            _basicData = new PersonInfoSpace(BASIC_DATA, true, false);
            _basicData.addEditable(NAME);
            if (_type == PHYSICAL)
            {
                _basicData.addEditable(FIRST_LASTNAME);
                _basicData.addEditable(SECOND_LASTNAME);
            }

            _basicData.addEditable(ID_CARD);

            _phones = new PersonInfoSpace(PHONES, true);

            _address = new PersonInfoSpace(ADDRESS, true);

            _documents = new PersonInfoSpace(DOCUMENTS, true, true, true);

            itemList.Controls.Add(_basicData);
            itemList.Controls.Add(_phones);
            itemList.Controls.Add(_address);
            itemList.Controls.Add(_documents);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.CANCEL);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void personType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            personType.Enabled = false;
            _type = personType.Items[personType.SelectedIndex].ToString();
            initializeMe();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        public IDisposable Subscribe(IObserver<EventDTO> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<EventDTO>(_observers, observer);
        }
    }
}
