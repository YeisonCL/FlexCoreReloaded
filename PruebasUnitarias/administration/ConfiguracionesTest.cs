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
    public class ConfiguracionesTest
    {
        ConfiguracionesQueriesDAO c;

        [TestFixtureSetUp]
        public void setUp()
        {
            c = new ConfiguracionesQueriesDAO();
        }

        [Test]
        public void insertHistoricoTransaccionalTest()
        {
            Decimal compraDolar = 10;
            Decimal ventaDolar = 10;
            DateTime fechaHoraSistema = new DateTime(2014,12,12);
            Decimal tasaInteresAhorro = 10;
            try
            {
                c.insertConfiguracion(compraDolar, ventaDolar, fechaHoraSistema, tasaInteresAhorro);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getConfiguracionTest()
        {

            try
            {
                List<ConfiguracionesDTO> l = c.getConfiguracion();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getConfiguracionTest()
        {
            Decimal compraDolar = 10;
            Decimal ventaDolar = 10;
            DateTime fechaHoraSistema = new DateTime(2014, 12, 12);
            Decimal tasaInteresAhorro = 10;
            try
            {
                List<ConfiguracionesDTO> l = c.getConfiguracion(compraDolar, ventaDolar, fechaHoraSistema, tasaInteresAhorro);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void actualizarHoraBaseTest()
        {
            DateTime pHora = new DateTime(2014, 12, 12);
            try
            {
                c.actualizarHoraBase(pHora);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void deleteConfiguracionTest()
        {
            Decimal compraDolar = 10;
            Decimal ventaDolar = 10;
            DateTime fechaHoraSistema = new DateTime(2014, 12, 12);
            Decimal tasaInteresAhorro = 10;
            try
            {
                c.deleteConfiguracion(compraDolar, ventaDolar, fechaHoraSistema, tasaInteresAhorro);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
