using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexDesktop.ArduinoManager
{
    public static class ArduinoConnect
    {
        static string _puertoFisico = "COM6";
        static int _baudRate = 38400;

        public static void iniciarConexionRedInalambrica(string pSsid, string pPassword)
        {
            SerialPort puerto = new SerialPort(_puertoFisico, _baudRate);
            puerto.Open();
            puerto.Write("1&" + pSsid + "&" + pPassword + "&");
            puerto.Close();
        }

        public static void setearParametrosConexionServer(string pIp, int pPuerto)
        {
            SerialPort puerto = new SerialPort(_puertoFisico, _baudRate);
            puerto.Open();
            puerto.Write("2&" + pIp + "&" + pPuerto.ToString() + "&");
            puerto.Close();
        }

        public static void iniciarPago(string pCuentaDestino, decimal pMonto)
        {
            SerialPort puerto = new SerialPort(_puertoFisico, _baudRate);
            puerto.Open();
            puerto.Write("3&" + pCuentaDestino + "&" + pMonto.ToString() + "&");
            puerto.Close();
        }
    }
}
