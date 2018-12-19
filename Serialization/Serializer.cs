using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using log4net;
using System.ComponentModel.Composition;

namespace Serialization
{
    [Export(typeof(ISerializer))]
    public class Serializer : ISerializer
    {

        public void Write<T>(T obj, string filePath)
        {
            var lista = new List<Type>();
            lista.Add(typeof(System.FlagsAttribute));
            lista.Add(typeof(System.Reflection.DefaultMemberAttribute));
            lista.Add(typeof(System.AttributeUsageAttribute));
            lista.Add(typeof(System.ObsoleteAttribute));
            lista.Add(typeof(System.SerializableAttribute));
            lista.Add(typeof(System.Runtime.Serialization.KnownTypeAttribute));
            lista.Add(typeof(log4net.Util.TypeConverters.TypeConverterAttribute));


            DataContractSerializerSettings DCSsettings = new DataContractSerializerSettings { PreserveObjectReferences = true };


            var XmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            DataContractSerializer serializer = new DataContractSerializer(obj.GetType(), lista);

            using (FileStream stream = File.Create(filePath))
            {
                serializer.WriteObject(stream, obj);
            }

        }

        public T Read<T>(string filePath)
        {
            var lista = new List<Type>();
            lista.Add(typeof(System.FlagsAttribute));
            lista.Add(typeof(System.Reflection.DefaultMemberAttribute));
            lista.Add(typeof(System.AttributeUsageAttribute));
            lista.Add(typeof(System.ObsoleteAttribute));
            lista.Add(typeof(System.SerializableAttribute));
            lista.Add(typeof(System.Runtime.Serialization.KnownTypeAttribute));
            lista.Add(typeof(log4net.Util.TypeConverters.TypeConverterAttribute));


            T result = default(T);
            DataContractSerializer deserializer = new DataContractSerializer(typeof(T),lista);
            using (FileStream stream = File.OpenRead(filePath))
            {
                result = (T)deserializer.ReadObject(stream);
            }

            return result;
        }
    }
}
