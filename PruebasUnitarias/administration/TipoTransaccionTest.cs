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
    public class TipoTransaccionTest
    {
        TipoTransaccionQueriesDAO tt;

        [TestFixtureSetUp]
        public void setUp()
        {
            tt = new TipoTransaccionQueriesDAO();
        }

        [Test]
        public void insertHistoricoTransaccionalTest()
        {
            String pDescripcion = "Hola";
            try
            {
                tt.insertTipoTransaccion(pDescripcion);
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
                List<TipoTransaccionDTO> l = tt.getTipoTransaccion();
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
                List<TipoTransaccionDTO> l = tt.getTipoTransaccion(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getTipoTransaccionDescripcionTest()
        {
            try
            {
                List<TipoTransaccionDTO> l = tt.getTipoTransaccion("Hola");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void getIdTipoTransaccionTest()
        {
            try
            {
                int l = tt.getIdTipoTransaccion("Hola");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void updateTipoTransaccionTest()
        {
            try
            {
                tt.updateTipoTransaccion(1, "Holis");
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void deleteTipoTransaccionTest()
        {
            try
            {
                tt.deleteTipoTransaccion(1);
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }
    }
}
