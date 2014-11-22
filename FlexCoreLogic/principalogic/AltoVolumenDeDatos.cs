using AltoVolumenDeDatos;
using ConexionSQLServer.SQLServerConnectionManager;
using FlexCoreLogic.general;
using System;
using System.Data;
using System.Threading;

namespace FlexCoreLogic.principalogic
{
    public static class AltoVolumenDeDatos
    {
        public static void iniciarAltoVolumenDeDatos(DateTime pHoraInicio)
        {
            ThreadStart _delegado = new ThreadStart(() => iniciarAltoVolumenDeDatosAux(pHoraInicio));
            Thread _hiloReplica = new Thread(_delegado);
            _hiloReplica.Start();
        }
        public static void iniciarAltoVolumenDeDatosAux(DateTime pHoraInicio)
        {
            TiempoManager.pausarReloj();
            DataTable _datos = LectorExcel.leerClientesArchivo();
            Utils.iniciarBaseDeDatos();
            Generales.vaciarBaseDeDatos();
            InsertarClientes.setEstadoInsercion(0);
            InsertarClientes.setCantidadDeDatos(0);
            InsertarClientes.insertarClientes(_datos, pHoraInicio);
            SQLServerManager.iniciarOReiniciarListaDeConexiones();
            TiempoManager.reanudarReloj();
        }

        public static int consultarCantidadDeDatosAInsertar()
        {
            return InsertarClientes.getCantidadDeDatos();
        }

        public static void cancelarInsercion()
        {
            InsertarClientes.cancelarInsercion();
        }

        public static int consultarEstadoDeInsercion()
        {
            return InsertarClientes.getEstadoInsercion();
        }
    }
}
