    using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.DTO
{
    [Table("FieldMetadata")]
    public class FieldMetadataDatabaseDTO : BaseFieldMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public override Guid Guid { get => base.Guid; set => base.Guid = value; }
        [Required, StringLength(100)]
        public override string FieldName { get => base.FieldName; set => base.FieldName = value; }
        public override bool IsReadOnly { get => base.IsReadOnly; set => base.IsReadOnly = value; }
        public new TypeMetadataDatabaseDTO FieldType { get => (TypeMetadataDatabaseDTO)base.FieldType; set => base.FieldType = value; }
        public override Tuple<AccessLevel, StaticEnum> Modifiers { get => base.Modifiers; set => base.Modifiers = value; }

        public FieldMetadataDatabaseDTO(BaseFieldMetadata baseFields)
        {
            FieldName = "";
            Guid = baseFields.Guid;
            FieldName = baseFields.FieldName;
            IsReadOnly = baseFields.IsReadOnly;
            FieldType = TypeMetadataDatabaseDTO.EmitReferenceDatabase(baseFields.FieldType);
            Modifiers = baseFields.Modifiers;
        }

        internal static IEnumerable<FieldMetadataDatabaseDTO> EmitFieldsDatabase(IEnumerable<BaseFieldMetadata> fields)
        {
            if (fields == null) return null;
            return from field in fields
                   select new FieldMetadataDatabaseDTO(field);
        }

        public FieldMetadataDatabaseDTO() { }
    }
}