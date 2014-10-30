using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FlexCore.general;

namespace FlexCore.persons
{
    public partial class DocumentField : UserControl, IObservable<EventDTO>
    {

        protected List<IObserver<EventDTO>> _observers;

        public DocumentField()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public DocumentField(string pName, string pDescription = "", bool pEraseable = true)
            : this()
        {
            nameText.Text = pName;
            descripText.Text = pDescription;
            eraseOption.Visible = pEraseable;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
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

        public string getFileName() { return nameText.Text; }
        public string getFileDir() { return fileText.Text; }
        public string getDescription() { return descripText.Text; }


        public void changeToEdit()
        {
            nameText.Visible = false;

            searchButton.Visible = true;
            fileValue.Visible = true;
            fileText.Visible = false;

            descripValue.Visible = true;
            descripText.Visible = false;

            editButton.Visible = false;
        }

        public void changeToView()
        {
            nameText.Visible = true;

            searchButton.Visible = false;
            fileValue.Visible = false;
            fileText.Visible = true;

            descripValue.Visible = false;
            descripText.Visible = true;

            editButton.Visible = false;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                string fullPath = openFileDialog1.FileName;
                string fileName = Path.GetFileName(openFileDialog1.FileName);
                if (fileName.Equals(nameText.Text))
                {
                    fileValue.Text = fullPath;
                }
                else
                {
                    MessageBox.Show("El archivo seleccionado no corresponde a una versión del archivo actual", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void editOption1_Click(object sender, EventArgs e)
        {
            changeToEdit();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            changeToView();
        }

        private void eraseOption_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.ERASE_EXISTING_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }
    }
}
