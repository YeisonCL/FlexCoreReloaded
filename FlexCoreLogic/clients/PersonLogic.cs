using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FlexCoreLogic.clients
{
    class PersonLogic:AbstractPersonLogic<PersonDTO>
    {
        private static PersonLogic _instance = null;
        private static object _syncLock = new object();

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

        public int searchCount(GenericPersonDTO pPerson)
        {
            return GenericPersonVDAO.getInstance().getSearchCount(pPerson);
        }

        public List<GenericPersonDTO> search(GenericPersonDTO pPerson, SqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            return GenericPersonVDAO.getInstance().search(pPerson, pCommand, pPageNumber, pShowCount, pOrderBy);
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

        public override List<PersonDTO> search(PersonDTO pPerson, SqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        
        public override List<PersonDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }
    }
}
