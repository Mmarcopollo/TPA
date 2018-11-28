using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Serialization
{
    public class Serializer : ISerializer
    {
        public void Write(AssemblyMetadata obj, string filePath)
        {
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType());

            using (FileStream stream = File.Create(filePath))
            {
                serializer.WriteObject(stream, obj);
            }
        }

        public AssemblyMetadata Read(string filePath)
        {
            AssemblyMetadata result;
            DataContractSerializer deserializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (FileStream stream = File.OpenRead(filePath))
            {
                result = (AssemblyMetadata)deserializer.ReadObject(stream);
            }

            return result;
        }
    }
}
