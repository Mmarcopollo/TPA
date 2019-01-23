using BasicData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.DTO
{
    [Table("PropertyMetadata")]
    public class PropertyMetadataDatabaseDTO : BasePropertyMetadata
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string Name { get => base.Name; set => base.Name = value; }
        public new TypeMetadataDatabaseDTO UsedTypeMetadata { get => (TypeMetadataDatabaseDTO)base.UsedTypeMetadata; set => base.UsedTypeMetadata = value; }

        public PropertyMetadataDatabaseDTO(BasePropertyMetadata propertyMetadataDTO)
        {
            Name = "";
            Name = propertyMetadataDTO.Name;
            UsedTypeMetadata = TypeMetadataDatabaseDTO.EmitReferenceDatabase(propertyMetadataDTO.UsedTypeMetadata);
        }

        internal static IEnumerable<PropertyMetadataDatabaseDTO> EmitPropertiesDatabase(IEnumerable<BasePropertyMetadata> props)
        {
            if (props == null) return null;
            return from prop in props
                   select new PropertyMetadataDatabaseDTO(prop);
        }

        public PropertyMetadataDatabaseDTO() { }
    }
}