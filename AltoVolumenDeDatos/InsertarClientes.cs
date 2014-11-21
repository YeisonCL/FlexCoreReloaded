using FlexCoreDTOs.clients;
using FlexCoreLogic.clients;
using System;
using System.Collections.Generic;
using System.Data;

namespace AltoVolumenDeDatos
{
    public static class InsertarClientes
    {
        /*
         * Metodo para ingresar los clientes en la base de datos.
         */
        public static void insertarClientes(DataTable pDataT)
        {
            foreach (DataRow row in pDataT.Rows)
            {
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
                string ced = System.Convert.ToString((int)row.Field<Double>(3));
                persona.setIDCard(ced);
                persona.setFirstLastName(row.Field<string>(1));
                persona.setSecondLastName(row.Field<string>(2));
                int idCliente = ClientsFacade.getInstance().newClientAndPerson(persona, Dirs, Telefonos, null, null);
                InsertarCuentaAhorroVista.insertarCuentaAhorroVistaBase(idCliente);
            }
        }
    }
}
