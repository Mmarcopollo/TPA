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
            Name = assembly.ManifestModule.Name;
            //Namespaces = from Type _type in assembly.GetTypes()
            //               where _type.GetVisible()
            //               group _type by _type.GetNamespace() into _group
            //               orderby _group.Key
            //               select new NamespaceMetadata(_group.Key, _group);
            Type[] types = assembly.GetTypes();
            m_Namespaces = types.Where(t => t.IsVisible).GroupBy(t => t.Namespace).OrderBy(t => t.Key)
                .Select(t => new NamespaceMetadata(t.Key, t.ToList())).ToList();
        }
        [DataMember]
        public string m_Name;
        [DataMember]
        public List<NamespaceMetadata> m_Namespaces;

        public AssemblyMetadata() { }

        
        public string Name { get => m_Name; set => m_Name = value; }

        public List<NamespaceMetadata> Namespaces { get => m_Namespaces; set => m_Namespaces = value; }
    
    }
}
