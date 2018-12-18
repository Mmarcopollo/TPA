using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NamespaceMetadata
    {
        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetadata(type);
        }

        public NamespaceMetadata(NamespaceMetadataDTO namespaceMetadataDTO)
        {
            m_NamespaceName = namespaceMetadataDTO.m_NamespaceName;
            if (namespaceMetadataDTO.m_Types != null)
            {
                List<TypeMetadata> types = new List<TypeMetadata>();
                foreach (TypeMetadataDTO DTO in namespaceMetadataDTO.m_Types)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.m_typeName)) metadata = TypeMetadata.TypeDictionary[DTO.m_typeName];
                    else metadata = new TypeMetadata(DTO);
                    types.Add(metadata);
                }
                m_Types = types;
            }
        }

        public string m_NamespaceName;
        public IEnumerable<TypeMetadata> m_Types;

        public string Name { get => m_NamespaceName; set => m_NamespaceName = value; }


    }
}
