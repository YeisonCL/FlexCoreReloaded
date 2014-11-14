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
    public partial class Person : UserControl, IObservable<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;
        private int _itemID;
        private string _personType;

        public static readonly string PHYSICAL_PERSON = "Persona física";
        public static readonly string JURIDICAL_PERSON = "Persona jurídica";
        public static readonly string PHYSICAL_CLIENT = "Cliente físico";
        public static readonly string JURIDICAL_CLIENT = "Cliente jurídico";

        public Person()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public Person(string pName, string pType, string pId, int pItemID, byte[] pPhoto = null, string pCIF = "")
            :this()
        {
            nameText.Text = pName;
            typeText.Text = pType;
            idCardText.Text = pId;
            if (pCIF == "")
            {
                cifText.Visible = false;
                cifTtitle.Visible = false;
            }
            else
            {
                cifText.Text = pCIF;
            }
            _itemID = pItemID;
            _personType = pType;

            if (pPhoto != null && pPhoto.Length != 0)
            {
                Image img = Utils.byteArrayToImage(pPhoto);
                img = Utils.resizeImage(img, new Size(photo.Width, photo.Height));
                photo.Image = img;
            }

        }

        public string getPersonType() { return _personType; }

        public int getPersonID() { return _itemID; }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Person_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void Person_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void Person_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }

        private void Person_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.PERSON_CLICK);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
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

        private void Person_Load(object sender, EventArgs e)
        {

        }

    }
}
