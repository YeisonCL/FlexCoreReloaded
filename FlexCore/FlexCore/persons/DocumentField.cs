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
        private bool _new;
        private bool _onView;

        public DocumentField()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            _onView = true;
        }

        public DocumentField(string pName, string pDescription = "", bool pEraseable = true, bool pNew = false)
            : this()
        {
            nameText.Text = pName;
            descripValue.Text = descripText.Text = pDescription;
            eraseOption.Visible = pEraseable;
            _new = pNew;
            if (pNew)
            {
                nameText.Visible = false;
                nameTitle.Visible = false;
            }
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
        public string getFileDir() { return fileValue.Text; }
        public string getDescription() { return descripText.Text; }


        public void changeToEdit()
        {
            _onView = false;

            docTitle.Visible = true;
            searchButton.Visible = true;
            fileValue.Visible = true;

            descripValue.Visible = true;
            descripText.Visible = false;

            editButton.Visible = false;
            saveButton.Visible = true;
            eraseOption.Visible = true;
            cancelButton.Visible = true;
        }

        public void changeToView()
        {
            _onView = true;
            nameText.Visible = true;

            searchButton.Visible = false;
            fileValue.Visible = false;
            docTitle.Visible = false;

            descripValue.Visible = false;
            descripText.Visible = true;

            editButton.Visible = true;
            saveButton.Visible = false;
            eraseOption.Visible = false;
            cancelButton.Visible = false;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                string fullPath = openFileDialog1.FileName;
                string fileName = Path.GetFileName(openFileDialog1.FileName);

                if (_new)
                {
                    fileValue.Text = fullPath;
                    nameText.Text = fileName;
                    nameText.Visible = true;
                    nameTitle.Visible = true;
                    fileValue.Enabled = false;
                    _new = false;
                }
                else if (fileName.Equals(nameText.Text))
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
            if (fileValue.Text == "")
            {
                MessageBox.Show("Por favor seleccione un archivo antes de guardar", "Seleccione archivo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                changeToView();
            }
        }

        private void eraseOption_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.ERASE_EXISTING_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            changeToView();
        }

        private void DocumentField_MouseEnter(object sender, EventArgs e)
        {
            if (_onView)
            {
                this.BackColor = Color.FromArgb(200, 200, 200);
            }
        }

        private void DocumentField_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void DocumentField_Click(object sender, EventArgs e)
        {

        }

        private void nameText_Click(object sender, EventArgs e)
        {
            EventDTO edto = new EventDTO(this, EventDTO.OPEN_DOC);
            foreach (var observer in _observers)
            {
                observer.OnNext(edto);
            }
        }
    }
}
