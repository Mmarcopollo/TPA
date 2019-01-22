using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("TypeMetadata")]
    public class TypeMetadataDatabaseDTO : BaseTypeMetadata
    {
        public int Id { get; set; }
        [Key, StringLength(100)]
        public override string TypeName { get; set; }
        public override string NamespaceName { get; set; }
        public new TypeMetadataDatabaseDTO BaseType { get; set; }
        public new List<TypeMetadataDatabaseDTO> GenericArguments { get; set; }
        public override AccessLevel AccessLevel { get; set; }
        public override AbstractEnum AbstractEnum { get; set; }
        public override SealedEnum SealedEnum { get; set; }
        public override TypeKind TypeKind { get; set; }
        public new List<TypeMetadataDatabaseDTO> ImplementedInterfaces { get; set; }
        public new List<TypeMetadataDatabaseDTO> NestedTypes { get; set; }
        public new List<FieldMetadataDatabaseDTO> Fields { get; set; }
        public new List<PropertyMetadataDatabaseDTO> Properties { get; set; }
        public new TypeMetadataDatabaseDTO DeclaringType { get; set; }
        public new List<MethodMetadataDatabaseDTO> Methods { get; set; }
        public new List<MethodMetadataDatabaseDTO> Constructors { get; set; }
    }
}