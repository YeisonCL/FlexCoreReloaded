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
using FlexCoreDTOs.general;
using FlexCore.persons;

namespace FlexCore.clients
{

    public partial class ClientsMenu : UserControl, IObserver<EventDTO>, IObservable<EventDTO>
    {
        private IDisposable _mainPanelDis;
        protected List<IObserver<EventDTO>> _observers;

        public ClientsMenu()
        {
            InitializeComponent();
            _observers = new List<IObserver<EventDTO>>();
            _mainPanelDis = null;
            clientOption();
        }

        private void personsItem_Click(object sender, EventArgs e)
        {
            clientOption();
        }

        public void clientOption()
        {
            mainPanel.Controls.Clear();
            if (_mainPanelDis != null)
            {
                _mainPanelDis.Dispose();
            }
            PersonList personList = new PersonList("Todos los clientes", "Clientes físicos y jurídicos");
            mainPanel.Controls.Add(personList);
            _mainPanelDis = personList.Subscribe(this);

            //Añadir las categorias
            List<string> categories = ClientsConnection.getAllCategories();
            foreach (var cat in categories)
            {
                personList.addCategory(cat);
            }
            personList.setCategory(categories[0]);

            SearchResultDTO<ClientVDTO> persons = ClientsConnection.getAll(1, 10, categories[0]);

            //Numero máximo de paginas
            personList.setMaxPage(persons.getMaxPage());
            personList.setCurrentPage(1);

            setClients(persons.getResult(), personList);

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

        private void setClients(List<ClientVDTO> pResultList, PersonList pList)
        {
            try
            {
                foreach (var person in pResultList)
                {
                    string name = person.getName();
                    string personType;
                    if (person.getPersonType() == GenericPersonDTO.PHYSICAL_PERSON)
                    {
                        name += String.Format(" {0} {1}", person.getFirstLastName(), person.getSecondLastName());
                        personType = Person.PHYSICAL_PERSON;
                    }
                    else
                    {
                        personType = Person.JURIDICAL_PERSON;
                    }
                    pList.addPerson(name, personType, person.getIDCard(), person.getPersonID(), person.getPhotoBytes());
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Uppss! Ha ocurrido un error al inentar leer las personas. Por favor intentelo de nuevo o contacte al administrador del sistema", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void personsEventHandler(EventDTO pEvent)
        {
            PersonList personList = (PersonList)pEvent.getOrigin();
            string category = personList.getSortCategory();
            int itemCount = personList.getItemCount();

            if (pEvent.getEventCode() == EventDTO.PERSON_CLICK)
            {
                foreach (var observer in _observers)
                {
                    observer.OnNext(pEvent);
                }
            }
            else if (pEvent.getEventCode() == EventDTO.NEXT_PAGE)
            {
                int currentPage = personList.getCurrentPage();

                SearchResultDTO<GenericPersonDTO> result = PersonConnection.getAllPersons(currentPage+1, itemCount, category);
                personList.clearList();
                personList.setCurrentPage(currentPage + 1);
                setClients(result.getResult(), personList);
            }
            else if (pEvent.getEventCode() == EventDTO.PREVIOUS_PAGE)
            {
                int currentPage = personList.getCurrentPage();

                SearchResultDTO<GenericPersonDTO> result = PersonConnection.getAllPersons(currentPage - 1, itemCount, category);
                personList.clearList();
                personList.setCurrentPage(currentPage - 1);
                setClients(result.getResult(), personList);
            }
            else if (pEvent.getEventCode() == EventDTO.PAGE_CHANGE)
            {
                int newPage = personList.getNewPage();

                SearchResultDTO<GenericPersonDTO> result = PersonConnection.getAllPersons(newPage, itemCount, category);
                personList.clearList();
                personList.setCurrentPage(newPage);
                setClients(result.getResult(), personList);

            }
            else if (pEvent.getEventCode() == EventDTO.SORT_CATEGORY_CHANGE || pEvent.getEventCode() == EventDTO.ITEM_COUNT_CHANGE)
            {
                
                SearchResultDTO<GenericPersonDTO> result = PersonConnection.getAllPersons(1, itemCount, category);
                personList.setMaxPage(result.getMaxPage());
                personList.setCurrentPage(1);
                personList.clearList();
                setClients(result.getResult(), personList);
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
