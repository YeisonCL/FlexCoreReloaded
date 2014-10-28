using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConexionSQLServer.Xml
{
    internal static class XmlAcess
    {
        public static List<string> extraerDatos(string pDireccion)
        {
            var _datos = new List<string>();
            XmlDocument _documentoXML = new XmlDocument();
            _documentoXML.Load(pDireccion);
            XmlNodeList _configuracion = _documentoXML.SelectNodes("/Configuracion")[0].ChildNodes;
            for (var index = 0; index < _configuracion.Count; index++)
            {
                _datos.Add(_configuracion[index].InnerXml);
            }
            return _datos;
        }
    }
}
