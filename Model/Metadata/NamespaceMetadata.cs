using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadata
    {
        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            Guid = Guid.NewGuid();
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetadata(type);

        }

        [DataMember]
        public string m_NamespaceName;
        [DataMember]
        public IEnumerable<TypeMetadata> m_Types;
        [DataMember]
        public Guid Guid;
        public string Name { get => m_NamespaceName; set => m_NamespaceName = value; }


        public override bool Equals(object obj)
        {
            var metadata = obj as NamespaceMetadata;
            return metadata != null &&
                   m_NamespaceName == metadata.m_NamespaceName &&
                   m_Types.SequenceEqual(metadata.m_Types);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
