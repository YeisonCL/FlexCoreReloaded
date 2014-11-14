using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlexCoreDTOs.cuentas;
using FlexCoreDTOs.clients;
using FlexCoreLogic.cuentas.Generales;
using FlexCoreLogic.cuentas.Facade;

namespace PruebasUnitarias.cuentas
{
    [TestFixture]
    public class PruebasCuentasAhorroVista
    {
        CuentaAhorroVistaDTO _cuentaAhorroVistaDTO;
        CuentaAhorroVistaDTO _cuentaPrueba;
        List<CuentaAhorroVistaDTO> _cuentasAhorroVistaList;

        [TestFixtureSetUp]
        public void SetupCuentasAhorroVista()
        {
            //Creacion del cliente.
            ClientVDTO _cliente = new ClientVDTO();
            _cliente.setClientID(1);
            //Creacion de la persona fisica.
            PhysicalPersonDTO _physycal = new PhysicalPersonDTO();
            _physycal.setPersonID(1);
            //Creacion de algun beneficiario.
            List<PhysicalPersonDTO> _listaBeneficiarios = new List<PhysicalPersonDTO>();
            _listaBeneficiarios.Add(_physycal);
            //Creacion de la cuenta.
            _cuentaAhorroVistaDTO = new CuentaAhorroVistaDTO();
            _cuentaAhorroVistaDTO.setCliente(_cliente);
            _cuentaAhorroVistaDTO.setDescripcion("Prueba Unitaria");
            _cuentaAhorroVistaDTO.setEstado(true);
            _cuentaAhorroVistaDTO.setTipoMoneda(Constantes.COLONES);
            _cuentaAhorroVistaDTO.setListaBeneficiarios(_listaBeneficiarios);
            //Cuenta de prueba
            _cuentaPrueba = new CuentaAhorroVistaDTO();
            //Extrayendo todas las cuentas ahorro vista de la base
            _cuentasAhorroVistaList = FacadeCuentas.obtenerTodasCuentaAhorroVista();
        }

        [Test]
        public void agregarCuentaAhorroVista()
        {
            string respuesta = FacadeCuentas.agregarCuentaAhorroVista(_cuentaAhorroVistaDTO);
            Assert.AreEqual("Transaccion completada con exito", respuesta);
        }

        [Test]
        public void agregarDineroCuentaAhorroVista()
        {
            _cuentaPrueba = _cuentasAhorroVistaList[0];
            string respuesta = FacadeCuentas.agregarDineroCuentaAhorroVista(_cuentaPrueba, 1000);
            Assert.AreEqual("Transaccion completada con exito", respuesta);
        }

        [Test]
        public void eliminarCuentaAhorroVista()
        {
            _cuentaPrueba = _cuentasAhorroVistaList[0];
            string respuesta = FacadeCuentas.eliminarCuentaAhorroVista(_cuentaPrueba);
            Assert.AreEqual("Transaccion completada con exito", respuesta);
        }

        [Test]
        public void modificarCuentaAhorroVista()
        {
            _cuentaPrueba = _cuentasAhorroVistaList[0];
            _cuentaPrueba.setDescripcion("Cambio De Descripcion");
            string respuesta = FacadeCuentas.modificarCuentaAhorroVista(_cuentaPrueba);
            Assert.AreEqual("Transaccion completada con exito", respuesta);
        }
    }
}