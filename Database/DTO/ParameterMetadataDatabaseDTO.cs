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
            Name = "";
            Name = parameterMetadataDTO.Name;
            UsedTypeMetadata = TypeMetadataDatabaseDTO.EmitReferenceDatabase(parameterMetadataDTO.UsedTypeMetadata);

            if (!Mapper.DatabaseDTOParameterDictionary.ContainsKey(Name))
            {
                Mapper.DatabaseDTOParameterDictionary.Add(Name, this);
            }
        }

        public ParameterMetadataDatabaseDTO() { }
    }
}