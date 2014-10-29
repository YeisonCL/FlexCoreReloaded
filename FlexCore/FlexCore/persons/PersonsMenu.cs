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

    public partial class PersonsMenu : UserControl, IObserver<EventDTO>, IObservable<EventDTO>
    {
        private IDisposable _mainPanelDis;
        protected List<IObserver<EventDTO>> _observers;

        public PersonsMenu()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            _mainPanelDis = null;
            personOption();
        }

        private void personsItem_Click(object sender, EventArgs e)
        {
            personOption();
        }

        public void personOption()
        {
            mainPanel.Controls.Clear();
            if (_mainPanelDis != null)
            {
                _mainPanelDis.Dispose();
            }
            PersonList personList = new PersonList("Todas las personas", "Personas físicas y jurídicas");
            mainPanel.Controls.Add(personList);
            this.Subscribe(personList);

            //Añadir las categorias
            personList.addCategory("ola k ase");
            personList.addCategory("ola k tree");
            personList.setCategory("ola k ase");

            //Numero máxima de paginas
            personList.setMaxPage(10);
            personList.setCurrentPage(1);

            _mainPanelDis = personList.Subscribe(this);
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

        private void personsEventHandler(EventDTO pEvent)
        {
            if (pEvent.getEventCode() == EventDTO.PERSON_CLICK)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNext(pEvent);
                }
            }
            else if (pEvent.getEventCode() == EventDTO.NEXT_PAGE)
            {
                PersonList personList = (PersonList)pEvent.getOrigin();
                int currentPage = personList.getCurrentPage();
                int itemCount = personList.getItemCount();
                string categpry = personList.getSortCategory();
                int maxPages = 1; //HACER CONSULTA A LA BASE
                personList.setMaxPage(maxPages);
                personList.setCurrentPage(currentPage + 1);
                personList.clearList();
                //AÑADIR EL RESTO DE COSAS
                //.
                //..
                //...
            }
            else if (pEvent.getEventCode() == EventDTO.PREVIOUS_PAGE)
            {
                PersonList personList = (PersonList)pEvent.getOrigin();
                int currentPage = personList.getCurrentPage();
                int itemCount = personList.getItemCount();
                string categpry = personList.getSortCategory();
                int maxPages = 1; //HACER CONSULTA A LA BASE
                personList.setMaxPage(maxPages);
                personList.setCurrentPage(currentPage - 1);
                personList.clearList();
                //AÑADIR EL RESTO DE COSAS
                //.
                //..
                //...
            }
            else if (pEvent.getEventCode() == EventDTO.PAGE_CHANGE)
            {
                PersonList personList = (PersonList)pEvent.getOrigin();
                int currentPage = personList.getNewPage();
                int itemCount = personList.getItemCount();
                string category = personList.getSortCategory();
                int maxPages = 10; //HACER CONSULTA A LA BASE
                personList.setMaxPage(maxPages);
                personList.setCurrentPage(currentPage);
                personList.clearList();
                //AÑADIR EL RESTO DE COSAS
                //.
                //..
                //...
            }
            else if (pEvent.getEventCode() == EventDTO.SORT_CATEGORY_CHANGE)
            {
                PersonList personList = (PersonList)pEvent.getOrigin();
                int currentPage = personList.getCurrentPage();
                int itemCount = personList.getItemCount();
                string categpry = personList.getSortCategory();
                int maxPages = 1; //HACER CONSULTA A LA BASE
                personList.setMaxPage(maxPages);
                personList.setCurrentPage(currentPage - 1);
                personList.clearList();
                //AÑADIR EL RESTO DE COSAS
                //.
                //..
                //...
            }
            else if (pEvent.getEventCode() == EventDTO.ITEM_COUNT_CHANGE)
            {
                PersonList personList = (PersonList)pEvent.getOrigin();
                int currentPage = personList.getCurrentPage();
                int itemCount = personList.getItemCount();
                string categpry = personList.getSortCategory();
                int maxPages = 1; //HACER CONSULTA A LA BASE
                personList.setMaxPage(maxPages);
                personList.setCurrentPage(currentPage - 1);
                personList.clearList();
                //AÑADIR EL RESTO DE COSAS
                //.
                //..
                //...
            }
        }

        public void OnNext(EventDTO value)
        {
            personsEventHandler(value);
        }

        private void newPersonItem_Click(object sender, EventArgs e)
        {
            EventDTO dto = new EventDTO(this, EventDTO.NEW_BUTTON);
            foreach (var observer in _observers)
            {
                observer.OnNext(dto);
            }
        }
        
    }
}
