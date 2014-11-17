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
    public class DispositivoCuentaTest
    {
        DispositivoCuentaQueriesDAO dc;

        [TestFixtureSetUp]
        public void setUp()
        {
            dc = new DispositivoCuentaQueriesDAO();
        }

        [Test]
        public void insertDispositivoCuentaTest()
        {
            String idDispositivo = "DERP";
            bool activo = true;
            int idCuenta = 1;
            try
            {
                dc.insertDispositivoCuenta(idDispositivo, activo, idCuenta);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getDispositivoCuentaTest()
        {

            try
            {
                List<DispositivoCuentaDTO> l = dc.getDispositivoCuenta();
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getDispositivoCuentaTest()
        {

            try
            {
                List<DispositivoCuentaDTO> l = dc.getDispositivoCuenta(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void checkDispositivoCuentaTest()
        {
            String idDispositivo = "DERP";
            int idCuenta = 1;
            try
            {
                List<int> l = dc.checkDispositivoCuenta(idDispositivo, idCuenta);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void obtenerIdCuentaTest()
        {

            try
            {
                int l = dc.obtenerIdCuenta("DERP");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void existeDispositivoTest()
        {

            try
            {
                bool l = dc.existeDispositivo("DERP");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void dispositivoActivoTest()
        {

            try
            {
                bool l = dc.dispositivoActivo("DERP");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void updateDispositivoCuentaTest()
        {
            String idDispositivo = "DERP";
            bool activo = true;
            int idCuenta = 1;
            try
            {
                dc.updateDispositivoCuenta(1, idDispositivo, activo, idCuenta);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void deleteDispositivoCuentaTest()
        {
            try
            {
                dc.deleteDispositivoCuenta(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
