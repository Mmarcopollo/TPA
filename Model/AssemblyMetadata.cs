using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AssemblyMetadata : Metadata
    {
        public AssemblyMetadata(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                           where _type.GetVisible()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group);
        }

        private string m_Name;
        private IEnumerable<NamespaceMetadata> m_Namespaces;

        public override string Name { get => m_Name; set => m_Name = value; }
        public IEnumerable<NamespaceMetadata> Namespaces { get => m_Namespaces; set => m_Namespaces = value; }

        public override IEnumerable<NamespaceMetadata> GetAllNamespaces()
        {
            return Namespaces;
        }

        public override IEnumerable<TypeMetadata> GetAllTypes()
        {
            return null;
        }

        public override IEnumerable<PropertyMetadata> GetAllProperties()
        {
            return null;
        }

        public override IEnumerable<MethodMetadata> GetAllMethods()
        {
            return null;
        }

        public override IEnumerable<ParameterMetadata> GetAllParameters()
        {
            return null;
        }
    }
}
