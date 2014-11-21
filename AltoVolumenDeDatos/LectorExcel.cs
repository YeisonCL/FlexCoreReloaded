using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace AltoVolumenDeDatos
{
    public static class LectorExcel
    {
        private static string _documentConfigurationDirection = "C:/Configuracion.xml";
        /*
         * Metodo que lee el millón de clientes del archivo excel
         */
        public static DataTable leerClientesArchivo()
        {
            List<string> _valores = Generales.extraerDatos(_documentConfigurationDirection);
            string rutaExcel = _valores[5];

            //Creamos la cadena de conexión con el fichero excel

            OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder();
            builder.DataSource = rutaExcel;
            if (Path.GetExtension(rutaExcel).ToUpper() == ".XLS")
            {
                builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                builder.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
            }
            else if (Path.GetExtension(rutaExcel).ToUpper() == ".XLSX")
            {
                builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                builder.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
            } 

            //Estructura que almacena los datos traidos del archivo de excel.

            DataTable tablaDePersonas = new DataTable("Personas");
            using (OleDbConnection conexion = new OleDbConnection(builder.ConnectionString))
            {
                //Abrimos la conexión
                conexion.Open();
                using (OleDbCommand comando = conexion.CreateCommand())
                {
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "SELECT * FROM [Hoja2$]";
                    //Guardamos los datos en el DataTable
                    OleDbDataAdapter da = new OleDbDataAdapter(comando);
                    da.Fill(tablaDePersonas);
                }
                //Cerramos la conexión
                conexion.Close();
            }
            return tablaDePersonas;
        }
    }
}
