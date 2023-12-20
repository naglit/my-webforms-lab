using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lab.Utility.MyXmlSerialization
{
    public class XmlDeserializationTest
    {
        public static T Deserialize<T>(string configFilePath)
        {
            try 
            {
                var serializer = new XmlSerializer(typeof(T));
                using (StreamReader reader = new StreamReader(configFilePath))
                {
                    var config = (T)serializer.Deserialize(reader);
                    return config;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
