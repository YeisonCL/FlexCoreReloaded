using System;
using System.Collections.Generic;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using FlexCoreLogic.exceptions;
using System.Data.SqlClient;
using ConexionSQLServer.SQLServerConnectionManager;

namespace FlexCoreLogic.clients
{
    class PhysicalPersonLogic:AbstractPersonLogic<PhysicalPersonDTO>
    {
        private static PhysicalPersonLogic _instance = null;
        private static object _syncLock = new object();

        private static readonly string FIRST_LASTNAME = "Primer apellido";
        private static readonly string SECOND_LASTNAME = "Segundo apellido";

        public static PhysicalPersonLogic  getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PhysicalPersonLogic();
                    }
                }
            }
            return _instance;
        }

        private PhysicalPersonLogic() { }

        public override int insert(PhysicalPersonDTO pPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            SqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                int id = insert(pPerson, command);
                tran.Commit();
                return id;
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public override int insert(PhysicalPersonDTO pPerson, SqlCommand pCommand)
        {
            try
            {
                PersonDAO perDao = PersonDAO.getInstance();
                PhysicalPersonDAO phyDao = PhysicalPersonDAO.getInstance();
                perDao.insert(pPerson, pCommand);
                pPerson.setPersonID(perDao.search(pPerson, pCommand)[0].getPersonID());
                phyDao.insert(pPerson, pCommand);
                return phyDao.search(pPerson, pCommand)[0].getPersonID();
            }
            catch (SqlException e)
            {
                throw new InsertException("", e);
            }
        }

        public override void delete(PhysicalPersonDTO  pPerson, SqlCommand pCommand)
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

        public override void update(PhysicalPersonDTO pNewPerson, PhysicalPersonDTO pPastPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            SqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                update(pNewPerson, pPastPerson, command);
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
            }
            finally
            {
                SQLServerManager.closeConnection(con);
            }
        }

        public override void update(PhysicalPersonDTO  pNewPerson, PhysicalPersonDTO  pPastPerson, SqlCommand pCommand)
        {
            try
            {
                PersonDAO perDao = PersonDAO.getInstance();
                perDao.update(pNewPerson, pPastPerson, pCommand);
                PhysicalPersonDAO phyDao = PhysicalPersonDAO.getInstance();
                phyDao.update(pNewPerson, pPastPerson, pCommand);
            }
            catch (SqlException e)
            {
                throw new UpdateException();
            }
            
        }

        public override List<PhysicalPersonDTO> search(PhysicalPersonDTO  pPerson, SqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
                return dao.search(pPerson, pCommand, pPageNumber, pShowCount, pOrderBy);
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
        
        }

        public override List<PhysicalPersonDTO > getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
                return dao.getAll(pPageNumber, pShowCount, pOrderBy);
            }
            catch (SqlException e)
            {
                throw new SearchException();
            }
            
        }

        protected override string getOrderBy(string pSort)
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
            else if (pSort == SECOND_LASTNAME){
                return PhysicalPersonDAO.SECOND_LSTNM;
            }
            else
            {
                return null;
            }
        }
    }
}
