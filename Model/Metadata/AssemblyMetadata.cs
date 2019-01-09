using BasicData;
using Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AssemblyMetadata : BaseAssemblyMetadata
    {
        [Import(typeof(ISerializer))]
        public ISerializer Serialization
        {
            get; set;
        }
        public override string Name { get => base.Name; set => base.Name = value; }
        public new IEnumerable<NamespaceMetadata> Namespaces { get => (IEnumerable<NamespaceMetadata>)base.Namespaces; set => base.Namespaces = value; }

        public AssemblyMetadata(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group);
        }

        public AssemblyMetadata(AssemblyMetadataDTO assemblyMetadataDTO)
        {
            Name = assemblyMetadataDTO.Name;
            if(assemblyMetadataDTO.Namespaces != null)
            {
                List<NamespaceMetadata> namespaces = new List<NamespaceMetadata>();
                foreach (NamespaceMetadataDTO DTO in assemblyMetadataDTO.Namespaces)
                {
                    NamespaceMetadata methodMetadata = new NamespaceMetadata(DTO);
                    namespaces.Add(methodMetadata);
                }
                Namespaces = namespaces;
            }
        }

        public AssemblyMetadataDTO ConvertToDTO()
        {
            AssemblyMetadataDTO result = new AssemblyMetadataDTO();
            result.Name = Name;
            if(Namespaces != null)
            {
                List<NamespaceMetadataDTO> namespaces = new List<NamespaceMetadataDTO>();
                foreach (NamespaceMetadata metadata in Namespaces)
                {
                    namespaces.Add(metadata.ConvertToDTO());
                }
                result.Namespaces = namespaces;
            }
            return result;
        }

        public void SerializeAssembly(string path)
        {
            AssemblyMetadataDTO dataToSerialize = this.ConvertToDTO();
            Serialization.Write(dataToSerialize, path);
        }

        public static AssemblyMetadata DeserializeAssembly(string path)
        {
            Serializer serializer = new Serializer();
            AssemblyMetadataDTO deserializedData = serializer.Read(path);
            return new AssemblyMetadata(deserializedData);
        }

        public override bool Equals(object obj)
        {
            AssemblyMetadata metadata = obj as AssemblyMetadata;
            return metadata != null &&
                   Name == metadata.Name &&
                   Namespaces.SequenceEqual(metadata.Namespaces);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
