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
        public override string Name { get => base.Name; set => base.Name = value; }
        public new TypeMetadataDatabaseDTO UsedTypeMetadata { get => (TypeMetadataDatabaseDTO)base.UsedTypeMetadata; set => base.UsedTypeMetadata = value; }

        public ParameterMetadataDatabaseDTO(BaseParameterMetadata parameterMetadataDTO)
        {
            Name = "";
            Name = parameterMetadataDTO.Name;
            UsedTypeMetadata = TypeMetadataDatabaseDTO.EmitReferenceDatabase(parameterMetadataDTO.UsedTypeMetadata);
        }

        public ParameterMetadataDatabaseDTO() { }
    }
}