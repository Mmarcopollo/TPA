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
        public override Guid Guid { get; set; }
        [Required, StringLength(100)]
        public override string FieldName { get; set; }
        public override bool IsReadOnly { get; set; }
        public new TypeMetadataDatabaseDTO FieldType { get; set; }
        public override Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }

        public FieldMetadataDatabaseDTO(BaseFieldMetadata baseFields)
        {
            FieldName = "";
            Guid = baseFields.Guid;
            FieldName = baseFields.FieldName;
            IsReadOnly = baseFields.IsReadOnly;
            FieldType = TypeMetadataDatabaseDTO.EmitReferenceDatabase(baseFields.FieldType);
            Modifiers = baseFields.Modifiers;

            if (!Mapper.DatabaseDTOFieldDictionary.ContainsKey(FieldName))
            {
                Mapper.DatabaseDTOFieldDictionary.Add(FieldName, this);
            }
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