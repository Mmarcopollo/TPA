using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PropertyMetadata
    {
        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
        }



        #region private
        public string m_Name;
        public TypeMetadata m_TypeMetadata;
        public string Name { get => m_Name; set => m_Name = value; }

        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
            m_Name = propertyName;
            m_TypeMetadata = propertyType;
        }

        public PropertyMetadata(PropertyMetadataDTO propertyMetadataDTO)
        {
            m_Name = propertyMetadataDTO.m_Name;
            if (propertyMetadataDTO.m_TypeMetadata != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(propertyMetadataDTO.m_TypeMetadata.m_typeName)) m_TypeMetadata = TypeMetadata.TypeDictionary[propertyMetadataDTO.m_TypeMetadata.m_typeName];
                else m_TypeMetadata = new TypeMetadata(propertyMetadataDTO.m_TypeMetadata);
            }
        }
        #endregion
    }
}
