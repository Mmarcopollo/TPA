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
            m_NamespaceName = name;
            //m_Types = from type in types orderby type.Name select new TypeMetadata(type);
            m_Types = types.OrderBy(t => t.Name).Select(t => new TypeMetadata(t)).ToList();
        }

        public NamespaceMetadata() { }

        [DataMember]
        public string m_NamespaceName;
        [DataMember]
        public List<TypeMetadata> m_Types;

        public string Name { get => m_NamespaceName; set => m_NamespaceName = value; }


    }
}
