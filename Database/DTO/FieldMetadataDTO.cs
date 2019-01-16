using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("FieldMetadata")]
    public class FieldMetadataDTO : BaseFieldMetadata
    {
        public int Id { get; set; }
        public override Guid Guid { get; set; }
        [Required, StringLength(100)]
        public override string FieldName { get; set; }
        public override bool IsReadOnly { get; set; }
        public new TypeMetadataDTO FieldType { get; set; }
        public override Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public new List<TypeMetadataDTO> Attributes { get; set; }

        public FieldMetadataDTO()
        {
            Attributes = new List<TypeMetadataDTO>();
        }
    }
}