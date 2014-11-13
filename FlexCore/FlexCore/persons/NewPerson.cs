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

        private bool _selection;

        protected List<IObserver<EventDTO>> _observers;

        private string _type;
        private string _photoPath;

        public NewPerson()
        {
            InitializeComponent();
            personType.Items.Add(PHYSICAL);
            personType.Items.Add(JURIDICAL);
            _observers = new List<IObserver<EventDTO>>();
            saveButton.Visible = false;
            _selection = false;
            openFileDialog1.Filter = "JPEG Files (*.jpeg)|*.jpeg| JPG Files (*.jpg)|*.jpg";
            _photoPath = "";
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
            _selection = true;
            _type = personType.Items[personType.SelectedIndex].ToString();
            initializeMe();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string name = DTOConstants.DEFAULT_STRING;
            string lastName1 = DTOConstants.DEFAULT_STRING;
            string lastName2 = DTOConstants.DEFAULT_STRING;
            string idCard = DTOConstants.DEFAULT_STRING;
            foreach (EditField field in _basicData.getEditList())
            {
                if (field.getTitle() == NAME)
                {
                    name = field.getEditValue();
                }
                else if (field.getTitle() == FIRST_LASTNAME)
                {
                    lastName1 = field.getEditValue();
                }
                else if (field.getTitle() == SECOND_LASTNAME)
                {
                    lastName2 = field.getEditValue();
                }
                else if (field.getTitle() == ID_CARD)
                {
                    idCard = field.getEditValue();
                }
            }
            
            int personID;
            try
            {
                if (personType.Text == PHYSICAL)
                {
                    PhysicalPersonDTO person = new PhysicalPersonDTO(name, lastName1, lastName2, idCard);
                    personID = PersonConnection.newPhysicalPerson(person);
                }
                else
                {
                    PersonDTO person = new PersonDTO(name, idCard, PersonDTO.JURIDIC_PERSON);
                    personID = PersonConnection.newJuridicalPerson(person);
                }

                MessageBox.Show("Persona");

                //ADDRESS
                List<Control> addressControls = _address.getEditList();
                foreach (var address in addressControls)
                {
                    EditField field = (EditField)address;
                    PersonAddressDTO dto = new PersonAddressDTO(personID, field.getEditValue());
                    PersonConnection.newAddress(dto);
                }

                MessageBox.Show("address");

                //PHONES
                List<Control> phonesControls = _phones.getEditList();
                foreach (var phone in phonesControls)
                {
                    EditField field = (EditField)phone;
                    PersonPhoneDTO dto = new PersonPhoneDTO(personID, field.getEditValue());
                    PersonConnection.newPhone(dto);
                }

                MessageBox.Show("phones");

                //DOCUMENTS
                List<Control> docControls = _documents.getEditList();
                foreach (var doc in docControls)
                {
                    EditDocument field = (EditDocument)doc;
                    if (File.Exists(field.getFileDir()))
                    {
                        byte[] byteArray = File.ReadAllBytes(field.getFileDir());
                        PersonDocumentDTO dto = new PersonDocumentDTO(personID, byteArray, field.getFileName(), field.getDescription());
                        PersonConnection.newDocument(dto);
                    }
                    else
                    {
                        MessageBox.Show(String.Format("No se ha encontrado el archivo {0}, este será omitido.", field.getFileName()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show("docs");

                //PHOTO
                if (_photoPath != "")
                {
                    Image img = Image.FromFile(_photoPath);
                    PersonPhotoDTO photo = new PersonPhotoDTO(personID, Utils.imageToByteArray(img));
                }

                MessageBox.Show("photo");

                EventDTO edto = new EventDTO(this, EventDTO.SAVE_BUTTON);
                foreach (var observer in _observers)
                {
                    observer.OnNext(edto);
                }

            } 
            catch
            {
                MessageBox.Show("Uppss! Ha ocurrido un error. Por favor intente de nuevo o contacte al administrador del sistema", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public IDisposable Subscribe(IObserver<EventDTO> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<EventDTO>(_observers, observer);
        }

        private void photo_Click(object sender, EventArgs e)
        {
            if (_selection)
            {
                DialogResult dialog = openFileDialog1.ShowDialog();
                if (dialog == DialogResult.OK)
                {
                    _photoPath = openFileDialog1.FileName;
                    Image img = Image.FromFile(_photoPath);
                    photo.Image = Utils.resizeImage(img, new Size(photo.Width, photo.Height));
                }

            }
        }

        private void personType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
