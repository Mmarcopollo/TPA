using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract(IsReference = true)]
    public class PropertyMetadata
    {
        internal static List<PropertyMetadata> EmitProperties(Type type)
        {
            List<PropertyInfo> props = type
               .GetProperties(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                              BindingFlags.Static | BindingFlags.Instance).ToList();

            return props.Where(t => t.GetGetMethod().GetVisible() || t.GetSetMethod().GetVisible())
                .Select(t => new PropertyMetadata(t.Name, TypeMetadata.EmitReference(t.PropertyType))).ToList();
        }

        public PropertyMetadata() { }

        #region private
        [DataMember]
        public string m_Name;
        [DataMember]
        public TypeMetadata m_TypeMetadata;
        [DataMember]
        public string Name { get => m_Name; set => m_Name = value; }

        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
            m_Name = propertyName;
            m_TypeMetadata = propertyType;
        }
        #endregion
    }
}
