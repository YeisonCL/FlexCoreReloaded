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

namespace FlexCore.closures
{
    public partial class ClosureList: UserControl, IObservable<EventDTO>, IObserver<EventDTO>
    {
        protected List<IObserver<EventDTO>> _observers;
        private int _currentPage;
        private int _maxPage;

        public ClosureList()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            itemNumber.SelectedItem = itemNumber.Items[0];
            addClosure("ola", "k", "ase");
        }

        public int getItemCount()
        {
            return Convert.ToInt32(itemNumber.Items[itemNumber.SelectedIndex].ToString());
        }

        public void clearList()
        {
            listItems.Controls.Clear();
        }

        public void addClosure(string pDate, string pTime, string pSate)
        {
            Closure closure = new Closure(pDate, pTime, pSate);
            listItems.Controls.Add(closure);
        }

        public IDisposable Subscribe(IObserver<EventDTO> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<EventDTO>(_observers, observer);
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
            if (value.getEventCode() == EventDTO.PERSON_CLICK)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNext(value);
                }
            }
            else if (value.getEventCode() == EventDTO.NEW_ELEMENT)
            {
                //PersonDTO person = (PersonDTO)value.getValue()
            }
        }

        private void sortCategories_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.SORT_CATEGORY_CHANGE);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void itemNumber_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.ITEM_COUNT_CHANGE);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void previousPage_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.PREVIOUS_PAGE);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void nextPage_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.NEXT_PAGE);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }

        private void currentPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (currentPage.Text != "" && currentPage.Text.All(Char.IsDigit)){
                    int newPage = Convert.ToInt32(currentPage.Text);
                    if (newPage <= _maxPage)
                    {
                        EventDTO dto = new EventDTO(this, EventDTO.PAGE_CHANGE);
                        foreach (var observer in _observers)
                        {
                            observer.OnNext(dto);
                        }
                    }
                    else
                    {
                        currentPage.Text = _currentPage.ToString();
                    }
                }
                else
                {
                    currentPage.Text = _currentPage.ToString();
                }
            }
        }

        public void dispose(IObserver<EventDTO> pObserver)
        {
            if (pObserver != null && _observers.Contains(pObserver))
                _observers.Remove(pObserver);
        }

        public int getCurrentPage() { return _currentPage; }

        public int getNewPage() { return Convert.ToInt32(currentPage.Text); }

        private void currentPage_TextChanged(object sender, EventArgs e)
        {

        }

        public void setCurrentPage(int pPage) { 
            _currentPage = pPage;
            currentPage.Text = pPage.ToString();
            checkOptions();
        }

        public void setMaxPage(int pMaxPage)
        {
            _maxPage = pMaxPage;
            string max = "/" + pMaxPage;
            pagesTotal.Text = max;
            checkOptions();
        }

        private void checkOptions()
        {
            if (_currentPage == 1)
            {
                previousPage.Visible = false;
            }
            else
            {
                previousPage.Visible = true;
            }
            if (_currentPage == _maxPage)
            {
                nextPage.Visible = false;
            }
            else
            {
                nextPage.Visible = true;
            }
        }
    }
}
