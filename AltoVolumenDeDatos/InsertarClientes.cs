using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using System;
using System.Collections.Generic;
using System.Data;

namespace AltoVolumenDeDatos
{
    public static class InsertarClientes
    {
        public static int _estadoInsercion = 0;
        public static int _cantidadDeDatos = 0;
        public static bool _banderaDeInsercion = true;

        public static void insertarPrimerCliente()
        {
            PhysicalPersonDTO persona = new PhysicalPersonDTO();
            string d = "Direccion De Prueba";
            PersonAddressDTO direccion = new PersonAddressDTO(d);
            string t1 = "27101119";
            string t2 = "89414595";
            PersonPhoneDTO telefono1 = new PersonPhoneDTO(t1);
            PersonPhoneDTO telefono2 = new PersonPhoneDTO(t2);
            List<PersonAddressDTO> Dirs = new List<PersonAddressDTO>();
            List<PersonPhoneDTO> Telefonos = new List<PersonPhoneDTO>();
            Dirs.Add(direccion);
            Telefonos.Add(telefono1);
            Telefonos.Add(telefono2);
            persona.setName("Yeison");
            persona.setPersonType(PersonDTO.PHYSICAL_PERSON);
            string ced = "702300243";
            persona.setIDCard(ced);
            persona.setFirstLastName("Cruz");
            persona.setSecondLastName("León");
            int idCliente = ClientsFacade.getInstance().newClientAndPerson(persona, Dirs, Telefonos, null, null);
            InsertarCuentaAhorroVista.insertarCuentaAhorroVistaBase(idCliente);
        }

        /*
         * Metodo para ingresar los clientes en la base de datos.
         */
        public static void insertarClientes(DataTable pDataT, string pNumeroCuentaDeduccion, DateTime pHoraInicio)
        {
            _cantidadDeDatos = pDataT.Rows.Count;
            foreach (DataRow row in pDataT.Rows)
            {
                if(_banderaDeInsercion == true)
                {
                    setEstadoInsercion(_estadoInsercion + 1);
                    PhysicalPersonDTO persona = new PhysicalPersonDTO();
                    string d = Convert.ToString(row.Field<string>(4));
                    PersonAddressDTO direccion = new PersonAddressDTO(d);
                    string t1 = Convert.ToString(row.Field<Double>(5));
                    string t2 = Convert.ToString(row.Field<Double>(6));
                    PersonPhoneDTO telefono1 = new PersonPhoneDTO(t1);
                    PersonPhoneDTO telefono2 = new PersonPhoneDTO(t2);
                    List<PersonAddressDTO> Dirs = new List<PersonAddressDTO>();
                    List<PersonPhoneDTO> Telefonos = new List<PersonPhoneDTO>();
                    Dirs.Add(direccion);
                    Telefonos.Add(telefono1);
                    Telefonos.Add(telefono2);
                    persona.setName(row.Field<string>(0));
                    persona.setPersonType(PersonDTO.PHYSICAL_PERSON);
                    string ced = Convert.ToString((int)row.Field<Double>(3));
                    persona.setIDCard(ced);
                    persona.setFirstLastName(row.Field<string>(1));
                    persona.setSecondLastName(row.Field<string>(2));
                    int idCliente = ClientsFacade.getInstance().newClientAndPerson(persona, Dirs, Telefonos, null, null);
                    InsertarCuentaAhorroVista.insertarCuentaAhorroVistaBase(idCliente);
                    InsertarCuentaAhorroAutomatico.insertarCuentaAhorroAutomaticoBase(idCliente, pNumeroCuentaDeduccion, pHoraInicio);

                }
                else 
                {
                    break;
                }
            }
            _banderaDeInsercion = true;
        }

        public static void cancelarInsercion()
        {
            _banderaDeInsercion = false;
        }

        public static void setCantidadDeDatos(int pCantidadDeDatos)
        {
            _cantidadDeDatos = pCantidadDeDatos;
        }

        public static int getCantidadDeDatos()
        {
            return _cantidadDeDatos;
        }

        public static void setEstadoInsercion(int pEstadoInsercion)
        {
            _estadoInsercion = pEstadoInsercion;
        }

        public static int getEstadoInsercion()
        {
            return _estadoInsercion;
        }
    }
}
