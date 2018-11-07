using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NamespaceMetadata : Metadata
    {
        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            m_NamespaceName = name;
            m_Types = from type in types orderby type.Name select new TypeMetadata(type);
        }

        public string m_NamespaceName;
        public IEnumerable<TypeMetadata> m_Types;

        public override string Name { get => m_NamespaceName; set => m_NamespaceName = value; }

        public override IEnumerable<NamespaceMetadata> GetAllNamespaces()
        {
            return null;
        }

        public override IEnumerable<TypeMetadata> GetAllTypes()
        {
            return m_Types;
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
