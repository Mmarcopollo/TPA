using BasicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("ParameterMetadata")]
    public class ParameterMetadataDTO : BaseParameterMetadata
    {
        public int Id { get; set; }
        [Required, StringLength(150)]
        public override string Name { get; set; }
        public new TypeMetadataDTO UsedTypeMetadata { get; set; }
    }
}