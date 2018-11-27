using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Serialization
{
    public class XMLSerialization
    {
        private XmlSerializer xmlSerializer = new XmlSerializer(typeof(AssemblyMetadata));

        public void Serialize(AssemblyMetadata assembly, string path)
        {
            StreamWriter streamWriter = new StreamWriter(path);
            xmlSerializer.Serialize(streamWriter, assembly);
            streamWriter.Close();
        }

        public AssemblyMetadata Deserialize(string path)
        {
            AssemblyMetadata assembly;
            FileStream fileStream = new FileStream(path, FileMode.Open);
            assembly = (AssemblyMetadata)xmlSerializer.Deserialize(fileStream);
            fileStream.Close();

            return assembly;
        }
    }
}
