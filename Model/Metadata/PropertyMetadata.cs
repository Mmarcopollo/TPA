using BasicData;
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
    public class PropertyMetadata : BasePropertyMetadata
    {
        public override string Name { get => base.Name; set => base.Name = value; }
        public new TypeMetadata UsedTypeMetadata { get => (TypeMetadata)base.UsedTypeMetadata; set => base.UsedTypeMetadata = value; }

        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props
                   where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                   select new PropertyMetadata(prop.Name, TypeMetadata.EmitReference(prop.PropertyType));
        }

        #region private

        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
            Name = propertyName;
            UsedTypeMetadata = propertyType;
        }

        public PropertyMetadata(PropertyMetadataDTO propertyMetadataDTO)
        {
            Name = propertyMetadataDTO.Name;
            if (propertyMetadataDTO.UsedTypeMetadata != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(propertyMetadataDTO.UsedTypeMetadata.TypeName)) UsedTypeMetadata = TypeMetadata.TypeDictionary[propertyMetadataDTO.UsedTypeMetadata.TypeName];
                else UsedTypeMetadata = new TypeMetadata((TypeMetadataDTO)propertyMetadataDTO.UsedTypeMetadata);
            }
        }

        public PropertyMetadataDTO ConvertToDTO()
        {
            PropertyMetadataDTO result = new PropertyMetadataDTO
            {
                Name = Name
            };
            if (UsedTypeMetadata != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(UsedTypeMetadata.TypeName)) result.UsedTypeMetadata = TypeMetadataDTO.DTOTypeDictionary[UsedTypeMetadata.TypeName];
                else result.UsedTypeMetadata = ((TypeMetadata)UsedTypeMetadata).ConvertToDTO();
            }
            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
