using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FlexCore.general
{
    static class Utils
    {

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
         MemoryStream ms = new MemoryStream();
         imageIn.Save(ms,System.Drawing.Imaging.ImageFormat.Gif);
         return  ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string serializeObejct<T>(T pObject)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            Stream stream = new MemoryStream();
            serializer.Serialize(stream, pObject);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            string msg = reader.ReadToEnd();
            reader.Close();
            stream.Close();
            return msg;
        }

        public static T deserializeObject<T>(string pXML)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            Stream stream = GenerateStreamFromString(pXML);
            return (T)deserializer.Deserialize(stream);
        }

    }
}
