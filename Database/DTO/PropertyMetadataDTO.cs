using BasicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("PropertyMetadata")]
    public class PropertyMetadataDTO : BasePropertyMetadata
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string Name { get; set; }
        public new TypeMetadataDTO UsedTypeMetadata { get; set; }
    }
}