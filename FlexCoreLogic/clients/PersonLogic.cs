using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using FlexCoreLogic.exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FlexCoreLogic.clients
{
    class PersonLogic:AbstractPersonLogic<PersonDTO>
    {
        private static PersonLogic _instance = null;
        private static object _syncLock = new object();

        private static readonly string FIRST_LASTNAME = "Primer apellido";
        private static readonly string SECOND_LASTNAME = "Segundo apellido";

        public static PersonLogic getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonLogic();
                    }
                }
            }
            return _instance;
        }

        private PersonLogic() { }

        public List<GenericPersonDTO> search(GenericPersonDTO pPerson, SqlCommand pCommand, int pPageNumber, int pShowCount, string pOrderBy)
        {
            
            return GenericPersonVDAO.getInstance().search(pPerson, pCommand, pPageNumber, pShowCount, pOrderBy);
        }

        public List<string> getAllOrderByList()
        {
            List<string> list = new List<string>();
            list.Add(ID_CARD);
            list.Add(NAME);
            list.Add(TYPE);
            list.Add(FIRST_LASTNAME);
            list.Add(SECOND_LASTNAME);
            return list;
        }

        protected string getAllOrderBy(string pSort)
        {
            if (pSort == ID_CARD)
            {
                return PersonDAO.ID_CARD;
            }
            else if (pSort == NAME)
            {
                return PersonDAO.NAME;
            }
            else if (pSort == TYPE)
            {
                return PersonDAO.TYPE;
            }
            else if (pSort == FIRST_LASTNAME)
            {
                return PhysicalPersonDAO.FIRST_LSTNM;
            }
            else if (pSort == SECOND_LASTNAME)
            {
                return PhysicalPersonDAO.SECOND_LSTNM;
            }
            else
            {
                return null;
            }
        }

        public override int insert(PersonDTO pPerson, SqlCommand pCommand)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        public override void delete(PersonDTO pPerson, SqlCommand pCommand)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        public override void update(PersonDTO pNewPerson, PersonDTO pPastPerson, SqlCommand pCommand)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        public override List<PersonDTO> search(PersonDTO pPerson, SqlCommand pCommand, int pPageNumber, int pShowCount, string pOrderBy)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        
        public override List<PersonDTO> getAll(int pPageNumber, int pShowCount, string pOrderBy)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }
    }
}
