using System.Collections.Generic;
using FlexCoreDTOs.clients;
using FlexCoreDAOs.clients;
using FlexCoreLogic.exceptions;
using System.Data.SqlClient;

namespace FlexCoreLogic.clients
{

    class JuridicPersonLogic:AbstractPersonLogic<PersonDTO>
    {

        private static JuridicPersonLogic _instance = null;
        private static object _syncLock = new object();

        public static JuridicPersonLogic getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new JuridicPersonLogic();
                    }
                }
            }
            return _instance;
        }

        private JuridicPersonLogic() { }

        public override int insert(PersonDTO pPerson, SqlCommand pCommand)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.insert(pPerson, pCommand);
                return dao.search(pPerson, pCommand)[0].getPersonID();
            }
            catch (SqlException e)
            {
                throw new InsertException();
            }
        }

        public override void delete(PersonDTO pPerson, SqlCommand pCommand)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.delete(pPerson, pCommand);
            }
            catch (SqlException e)
            {
                throw new DeleteException();
            }
                
        }

        public override void update(PersonDTO pNewPerson, PersonDTO pPastPerson, SqlCommand pCommand)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.update(pNewPerson, pPastPerson, pCommand);
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
            
        }

        public override List<PersonDTO> search(PersonDTO pPerson, SqlCommand pCommand, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                return dao.searchJuridical(pPerson, pCommand, pPageNumber, pShowCount, pOrderBy);
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
        }

        public override List<PersonDTO> getAll(int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                return dao.getAllJuridical(pPageNumber, pShowCount, pOrderBy);
            }
            catch (SqlException e)
            {
                throw new SearchException("", e);
            }
        }
    }
}
