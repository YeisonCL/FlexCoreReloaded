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
    class PagoInteresesTest
    {
        PagoInteresesQueriesDAO pi;

        [TestFixtureSetUp]
        public void setUp()
        {
            pi = new PagoInteresesQueriesDAO();
        }

        [Test]
        public void insertPagoInteresesTest()
        {
            Decimal monto = 10;
            int idCuenta = 1;
            int idCierre = 1;
            try
            {
                pi.insertPagoIntereses(monto, idCuenta, idCierre);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getPagoInteresesTest()
        {

            try
            {
                List<PagoInteresesDTO> l = pi.getPagoIntereses();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getPagoInteresesIdPagoTest()
        {

            try
            {
                List<PagoInteresesDTO> l = pi.getPagoIntereses(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void updatePagoInteresesTest()
        {
            Decimal monto = 10;
            int idCuenta = 1;
            int idCierre = 1;
            try
            {
                pi.updatePagoIntereses(1, monto, idCuenta, idCierre);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void deletePagoInteresesTest()
        {
            try
            {
                pi.deletePagoIntereses(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
