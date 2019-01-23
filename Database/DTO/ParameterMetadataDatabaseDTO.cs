using BasicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("ParameterMetadata")]
    public class ParameterMetadataDatabaseDTO : BaseParameterMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string Name { get; set; }
        public new TypeMetadataDatabaseDTO UsedTypeMetadata { get; set; }

        public ParameterMetadataDatabaseDTO(BaseParameterMetadata parameterMetadataDTO)
        {
            Name = parameterMetadataDTO.Name;
            if (parameterMetadataDTO.UsedTypeMetadata != null)
            {
                if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(parameterMetadataDTO.UsedTypeMetadata.TypeName)) UsedTypeMetadata = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[parameterMetadataDTO.UsedTypeMetadata.TypeName];
                else UsedTypeMetadata = new TypeMetadataDatabaseDTO(parameterMetadataDTO.UsedTypeMetadata);
            }
        }
    }
}