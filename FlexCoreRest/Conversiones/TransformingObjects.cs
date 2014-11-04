using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Xml.Serialization;

namespace FlexCoreRest.Conversiones
{
    public static class TransformingObjects
    {
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