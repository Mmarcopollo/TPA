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
    public class AssemblyMetadata
    {
        [Import(typeof(ISerializer))]
        public ISerializer Serialization
        {
            get; set;
        }

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
            m_Name = assemblyMetadataDTO.m_Name;
            if(assemblyMetadataDTO.m_Namespaces != null)
            {
                List<NamespaceMetadata> namespaces = new List<NamespaceMetadata>();
                foreach (NamespaceMetadataDTO DTO in assemblyMetadataDTO.m_Namespaces)
                {
                    NamespaceMetadata methodMetadata = new NamespaceMetadata(DTO);
                    namespaces.Add(methodMetadata);
                }
                m_Namespaces = namespaces;
            }
        }

        public AssemblyMetadataDTO ConvertToDTO()
        {
            AssemblyMetadataDTO result = new AssemblyMetadataDTO();
            result.m_Name = m_Name;
            if(m_Namespaces != null)
            {
                List<NamespaceMetadataDTO> namespaces = new List<NamespaceMetadataDTO>();
                foreach (NamespaceMetadata metadata in m_Namespaces)
                {
                    namespaces.Add(metadata.ConvertToDTO());
                }
                result.m_Namespaces = namespaces;
            }
            return result;
        }

        public void SerializeAssembly(string path)
        {
            AssemblyMetadataDTO dataToSerialize = this.ConvertToDTO();
            Serialization.Write<AssemblyMetadataDTO>(dataToSerialize, path);
        }

        public static AssemblyMetadata DeserializeAssembly(string path)
        {
            Serializer serializer = new Serializer();
            AssemblyMetadataDTO deserializedData = serializer.Read<AssemblyMetadataDTO>(path);
            return new AssemblyMetadata(deserializedData);
        }

        public string m_Name;
        public IEnumerable<NamespaceMetadata> m_Namespaces;

        public string Name { get => m_Name; set => m_Name = value; }
        public IEnumerable<NamespaceMetadata> Namespaces { get => m_Namespaces; set => m_Namespaces = value; }

        public override bool Equals(object obj)
        {
            var metadata = obj as AssemblyMetadata;
            return metadata != null &&
                   m_Name == metadata.m_Name &&
                   Namespaces.SequenceEqual(metadata.Namespaces);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
