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
    public class TransaccionesVueloTest
    {
        TransaccionesVueloQueriesDAO tvq;

         [TestFixtureSetUp]
        public void setUp()
        {
            tvq = new TransaccionesVueloQueriesDAO();
        }

        [Test]
        public void insertTransaccionVueloTest()
        {
            String pDescripcion = "Hola";
            DateTime pFechaHoraEntrada = new DateTime(2014,12,12);
            DateTime pFechaHoraSalida = new DateTime(2015, 12, 12);
            string pEstado = "DERP";
            int pVersionAplicacion = 1;
            int idCuenta = 1;
            int tipoTransaccion = 1;
            try
            {
                tvq.insertTransaccionVuelo(pDescripcion, pFechaHoraEntrada, pFechaHoraSalida, pEstado, pVersionAplicacion,
                idCuenta, tipoTransaccion);
            }
            catch(Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
