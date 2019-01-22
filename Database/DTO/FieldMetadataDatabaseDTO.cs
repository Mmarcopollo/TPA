    using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("FieldMetadata")]
    public class FieldMetadataDatabaseDTO : BaseFieldMetadata
    {
        public int Id { get; set; }
        public override Guid Guid { get; set; }
        [Required, StringLength(100)]
        public override string FieldName { get; set; }
        public override bool IsReadOnly { get; set; }
        public new TypeMetadataDatabaseDTO FieldType { get; set; }
        public override Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
    }
}