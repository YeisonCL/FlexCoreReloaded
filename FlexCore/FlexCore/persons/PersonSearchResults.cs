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
    public partial class PersonSearchResults : UserControl, IObserver<EventDTO>, IObservable<EventDTO>
    {

        private static readonly char   SEPARATOR        = ',';
        private static readonly string NAME             = "Nombre:";
        private static readonly string FIRST_LASTNAME   = "PrimerApellido:";
        private static readonly string SECOND_LASTNAME = "SegundoApellido:";
        private static readonly string ID_CARD          = "Cédula:";

        protected List<IObserver<EventDTO>> _observers;

        public PersonSearchResults()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
        }

        public PersonSearchResults(string pQuery)
            : this()
        {
            PersonList personList = new PersonList("Resultado de la búsqueda", pQuery);
            personList.Subscribe(this);
            this.Subscribe(personList);
            panel.Controls.Add(personList);

            bool cName = pQuery.Contains(NAME);
            bool cFirstLastname = pQuery.Contains(FIRST_LASTNAME);
            bool cSecondLastname = pQuery.Contains(SECOND_LASTNAME);
            bool cID = pQuery.Contains(ID_CARD);

            if (cName)
            {
                int startIndex = pQuery.IndexOf(NAME) + NAME.Length;
                int sepIndex = pQuery.IndexOf(SEPARATOR, startIndex);
                int length =  sepIndex!=-1?sepIndex:pQuery.Length  - startIndex;
                string name = pQuery.Substring(startIndex, length);
            }
            else if (cFirstLastname)
            {
                int startIndex = pQuery.IndexOf(FIRST_LASTNAME) + NAME.Length;
                int length = pQuery.IndexOf(FIRST_LASTNAME, startIndex) - startIndex;
                string firstLastName = pQuery.Substring(startIndex, length);
            }
            else if (cSecondLastname)
            {
                int startIndex = pQuery.IndexOf(SECOND_LASTNAME) + NAME.Length;
                int length = pQuery.IndexOf(SECOND_LASTNAME, startIndex) - startIndex;
                string secondLastName = pQuery.Substring(startIndex, length);
            }
            else if (cID)
            {
                int startIndex = pQuery.IndexOf(ID_CARD) + NAME.Length;
                int length = pQuery.IndexOf(ID_CARD, startIndex) - startIndex;
                string idCard = pQuery.Substring(startIndex, length);
            }
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
