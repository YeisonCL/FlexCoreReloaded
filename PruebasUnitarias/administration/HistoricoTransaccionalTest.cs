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
    public class HistoricoTransaccionalTest
    {
        HistoricoTransaccionalQueriesDAO htq;

        [TestFixtureSetUp]
        public void setUp()
        {
            htq = new HistoricoTransaccionalQueriesDAO();
        }

        [Test]
        public void insertHistoricoTransaccionalTest()
        {
            String pDescripcion = "Hola";
            DateTime pFechaHoraEntrada = new DateTime(2014, 12, 12);
            DateTime pFechaHoraSalida = new DateTime(2015, 12, 12);
            string pEstado = "DERP";
            int pVersionAplicacion = 1;
            int idCuenta = 1;
            int tipoTransaccion = 1;
            try
            {
                htq.insertHistoricoTransaccional(pDescripcion, pFechaHoraEntrada, pFechaHoraSalida, pEstado, pVersionAplicacion,
                idCuenta, tipoTransaccion);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getHistoricoTransaccionalTest()
        {
            try
            {
                List<HistoricoTransaccionalDTO> l = htq.getHistoricoTransaccional();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getHistoricoTransaccionalIdTransaccionTest()
        {
            try
            {
                List<HistoricoTransaccionalDTO> l = htq.getHistoricoTransaccional(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getHistoricoTransaccionalDescripcionTest()
        {
            try
            {
                List<HistoricoTransaccionalDTO> l = htq.getHistoricoTransaccional("Hola");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getIdHistoricoTransaccionalTest()
        {
            try
            {
                int l = htq.getIdHistoricoTransaccional("Hola");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void updateDescripcionTransaccionVueloTest()
        {
            try
            {
                htq.updateDescripcionHistoricoTransaccional(1, "Holis");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void deleteHistoricoTransaccionalTest()
        {
            try
            {
                htq.deleteHistoricoTransaccional(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
