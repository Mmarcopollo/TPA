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
            //GUID
            Guid = baseFields.Guid;

            // Name
            FieldName = baseFields.FieldName;

            //Read Only
            IsReadOnly = baseFields.IsReadOnly;

            //FieldType
            if (baseFields.FieldType != null)
            {
                if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(baseFields.FieldType.TypeName))
                {
                    FieldType = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[baseFields.FieldType.TypeName];
                }
                else
                {
                    FieldType = new TypeMetadataDatabaseDTO(baseFields.FieldType);
                }
            }

            //Field Modifiers
            Modifiers = baseFields.Modifiers;

        }
    }
}