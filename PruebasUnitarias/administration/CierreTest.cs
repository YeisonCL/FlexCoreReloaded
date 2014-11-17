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
    [TestFixture]
    public class CierreTest
    {
        CierreQueriesDAO cq;

        [TestFixtureSetUp]
        public void setUp()
        {
            cq = new CierreQueriesDAO();
        }


        [Test]
        public void insertCierreTest()
        {
            DateTime fechaHora = new DateTime(2015, 12, 12);
            bool estado = true;
            try
            {
                cq.insertCierre(fechaHora, estado);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getCierreTest()
        {

            try
            {
                List<CierreDTO> l = cq.getCierre();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getCierreIdCierreTest()
        {

            try
            {
                List<CierreDTO> l = cq.getCierre(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void updateCierreTest()
        {
            DateTime fechaHora = new DateTime(2015, 12, 12);
            bool estado = true;
            try
            {
                cq.updateCierre(1, fechaHora, estado);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void deleteCierreTest()
        {

            try
            {
                cq.deleteCierre(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
