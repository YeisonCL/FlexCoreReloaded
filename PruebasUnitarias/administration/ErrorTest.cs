using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlexCoreDAOs.administration;
using FlexCoreDTOs.administration;

namespace PruebasUnitarias.administration
{
    class ErrorTest
    {
        ErrorQueriesDAO e;

        [TestFixtureSetUp]
        public void setUp()
        {
            e = new ErrorQueriesDAO();
        }

        [Test]
        public void insertErrorTest()
        {
            String metodo = "Hola";
            int linea = 5;
            DateTime fechaHora = new DateTime(2014, 12, 12);
            String descripcion = "DERP";
            try
            {
                e.insertError(metodo, linea, fechaHora, descripcion);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getErrorTest()
        {
            try
            {
                List<ErrorDTO> l = e.getError();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getErrorTest()
        {

            try
            {
                List<ErrorDTO> l = e.getError(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void updateErrorTest()
        {
            int idError = 1;
            String metodo = "Hola";
            int linea = 5;
            DateTime fechaHora = new DateTime(2014, 12, 12);
            String descripcion = "DERP";
            try
            {
                e.updateError(idError, metodo, linea, fechaHora, descripcion);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void deleteErrorlTest()
        {

            try
            {
                e.deleteError(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
