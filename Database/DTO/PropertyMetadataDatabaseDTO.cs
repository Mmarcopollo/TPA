using BasicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("PropertyMetadata")]
    public class PropertyMetadataDatabaseDTO : BasePropertyMetadata
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string Name { get; set; }
        public new TypeMetadataDatabaseDTO UsedTypeMetadata { get; set; }

        public PropertyMetadataDatabaseDTO(BasePropertyMetadata propertyMetadataDTO)
        {
            Name = propertyMetadataDTO.Name;
            if (propertyMetadataDTO.UsedTypeMetadata != null)
            {
                if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(propertyMetadataDTO.UsedTypeMetadata.TypeName)) UsedTypeMetadata = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[propertyMetadataDTO.UsedTypeMetadata.TypeName];
                else UsedTypeMetadata = new TypeMetadataDatabaseDTO(propertyMetadataDTO.UsedTypeMetadata);
            }
        }
    }
}