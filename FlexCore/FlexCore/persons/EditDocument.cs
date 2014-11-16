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
    public partial class EditDocument : UserControl, IObservable<EventDTO>
    {

        protected List<IObserver<EventDTO>> _observers;

        public EditDocument(bool pEraseable=true, bool pSaveButton = false)
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            nameTitle.Visible = false;
            nameText.Visible = false;
            eraseOption.Visible = pEraseable;
            if (!pSaveButton)
            {
                saveButton.Visible = false;
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
        public string getDescription() { return descripValue.Text; }

        private void searchButton_Click(object sender, EventArgs e)
        {
            DialogResult dialog = openFileDialog1.ShowDialog();
            if (dialog == DialogResult.OK)
            {
                fileValue.Text = openFileDialog1.FileName;
                nameText.Text = Path.GetFileName(openFileDialog1.FileName);
                nameText.Visible = true;
                nameTitle.Visible = true;
                fileValue.Enabled = false;
            }
        }

        private void eraseOption_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.ERASE_EDIT_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.SAVE_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }
    }
}
