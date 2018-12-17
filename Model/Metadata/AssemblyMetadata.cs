using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract]
    public class AssemblyMetadata
    {
        public AssemblyMetadata(Assembly assembly)
        {
            Guid = Guid.NewGuid();
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group);
        }
        [DataMember]
        public string m_Name;
        [DataMember]
        public IEnumerable<NamespaceMetadata> m_Namespaces;
        [DataMember]
        public Guid Guid;


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
