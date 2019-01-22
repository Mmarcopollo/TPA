using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel.Composition;
using Serialization.DTO;
using BasicData;

namespace Serialization
{
    [Export(typeof(ISerializer))]
    public class Serializer : ISerializer
    {

        public void Write(BaseAssemblyMetadata obj, string filePath)
        {
            List<Type> lista = new List<Type>
            {
                typeof(System.FlagsAttribute),
                typeof(System.Reflection.DefaultMemberAttribute),
                typeof(System.AttributeUsageAttribute),
                typeof(System.ObsoleteAttribute),
                typeof(System.SerializableAttribute),
                typeof(System.Runtime.Serialization.KnownTypeAttribute),
                typeof(AssemblyMetadataDTO),
                typeof(MethodMetadataDTO),
                typeof(NamespaceMetadataDTO),
                typeof(ParameterMetadataDTO),
                typeof(PropertyMetadataDTO),
                typeof(TypeMetadataDTO),
                typeof(FieldMetadataDTO)
            };


            DataContractSerializerSettings DCSsettings = new DataContractSerializerSettings { PreserveObjectReferences = true };


            XmlWriterSettings XmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyMetadataDTO), lista);

            using (FileStream stream = File.Create(filePath))
            {
                serializer.WriteObject(stream, new AssemblyMetadataDTO(obj));
            }

        }

        public BaseAssemblyMetadata Read(string filePath)
        {
            List<Type> lista = new List<Type>
            {
                typeof(System.FlagsAttribute),
                typeof(System.Reflection.DefaultMemberAttribute),
                typeof(System.AttributeUsageAttribute),
                typeof(System.ObsoleteAttribute),
                typeof(System.SerializableAttribute),
                typeof(System.Runtime.Serialization.KnownTypeAttribute),
                typeof(AssemblyMetadataDTO),
                typeof(MethodMetadataDTO),
                typeof(NamespaceMetadataDTO),
                typeof(ParameterMetadataDTO),
                typeof(PropertyMetadataDTO),
                typeof(TypeMetadataDTO),
                typeof(FieldMetadataDTO)
            };


            AssemblyMetadataDTO result = default(AssemblyMetadataDTO);
            DataContractSerializer deserializer = new DataContractSerializer(typeof(AssemblyMetadataDTO),lista);
            using (FileStream stream = File.OpenRead(filePath))
            {
                result = new AssemblyMetadataDTO((BaseAssemblyMetadata)deserializer.ReadObject(stream));
            }

            return result;
        }
    }
}
