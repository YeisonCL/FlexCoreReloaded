using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlexDesktop.ArduinoManager
{
    public static class ArduinoConnect
    {
        static string _puertoFisico = "COM6";
        static int _baudRate = 9600;

        public static string iniciarConexionRedInalambrica(string pSsid, string pPassword)
        {
            string _respuesta = "";
            SerialPort puerto = new SerialPort(_puertoFisico, _baudRate);
            puerto.Open();
            puerto.WriteLine("1&" + pSsid + "&" + pPassword + "&");
            while (_respuesta == "")
            {
                _respuesta = puerto.ReadExisting();
                Thread.Sleep(1000);
            }
            puerto.Dispose();
            puerto.Close();
            return _respuesta;
        }

        public static string setearParametrosConexionServer(string pIp, int pPuerto)
        {
            string _respuesta = "";
            SerialPort puerto = new SerialPort(_puertoFisico, _baudRate);
            puerto.Open();
            puerto.WriteLine("2&" + pIp + "&" + pPuerto.ToString() + "&");
            while (_respuesta == "")
            {
                _respuesta = puerto.ReadExisting();
                Thread.Sleep(1000);
            }
            puerto.Dispose();
            puerto.Close();
            return _respuesta;
        }

        public static string iniciarPago(string pCuentaDestino, decimal pMonto)
        {
            string _respuesta = "";
            SerialPort puerto = new SerialPort(_puertoFisico, _baudRate);
            puerto.Open();
            puerto.WriteLine("3&" + pCuentaDestino + "&" + pMonto.ToString() + "&");
            while(_respuesta == "")
            {
                _respuesta = puerto.ReadExisting();
                Thread.Sleep(1000);
            }
            puerto.Dispose();
            puerto.Close();
            return _respuesta;
        }
    }
}
