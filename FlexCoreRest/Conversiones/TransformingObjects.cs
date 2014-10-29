using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace FlexCoreRest.Conversiones
{
    public static class TransformingObjects
    {
        public static byte[] ConvertHexToBytes(string pInput)
        {
            var result = new byte[(pInput.Length + 1) / 2];
            var offset = 0;
            if (pInput.Length % 2 == 1)
            {
                result[0] = (byte)Convert.ToUInt32(pInput[0] + "", 16);
                offset = 1;
            }
            for (int i = 0; i < pInput.Length / 2; i++)
            {
                result[i + offset] = (byte)Convert.ToUInt32(pInput.Substring(i * 2 + offset, 2), 16);
            }
            return result;
        }

        public static Object ByteArrayToObject(byte[] pArray)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(pArray, 0, pArray.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object objectAux = (Object)binForm.Deserialize(memStream);
            return objectAux;
        }
    }
}