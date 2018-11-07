using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ParameterMetadata : Metadata
    {
        public static Dictionary<string, ParameterMetadata> TypeDictionary = new Dictionary<string, ParameterMetadata>();

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;

            if (!TypeDictionary.ContainsKey(this.m_Name))
            {
                TypeDictionary.Add(this.m_Name, this);
            }
            else
            {
                return;
            }
        }

        //private vars
        public string m_Name;
        private TypeMetadata m_TypeMetadata;

        public override string Name { get => m_Name; set => m_Name = value; }

        public override IEnumerable<NamespaceMetadata> GetAllNamespaces()
        {
            return null;
        }

        public override IEnumerable<TypeMetadata> GetAllTypes()
        {
            if(m_TypeMetadata != null) return new[] { m_TypeMetadata };
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
