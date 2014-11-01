using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override void insert(PhysicalPersonDTO pPerson)
        {
            SqlConnection con = SQLServerManager.newConnection();
            SqlCommand command = new SqlCommand();
            SqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                insert(pPerson, command);
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

        public override void insert(PhysicalPersonDTO pPerson, SqlCommand pCommand)
        {
            try
            {
                PersonDAO perDao = PersonDAO.getInstance();
                PhysicalPersonDAO phyDao = PhysicalPersonDAO.getInstance();
                perDao.insert(pPerson, pCommand);
                System.Windows.Forms.MessageBox.Show("insertó persona");
                pPerson.setPersonID(perDao.search(pPerson, pCommand)[0].getPersonID());
                System.Windows.Forms.MessageBox.Show("obtuvo el id:"+pPerson.getPersonID());
                phyDao.insert(pPerson, pCommand);
                System.Windows.Forms.MessageBox.Show("TERMINÓ");
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
