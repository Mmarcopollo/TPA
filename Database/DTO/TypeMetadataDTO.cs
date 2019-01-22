using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("TypeMetadata")]
    public class TypeMetadataDTO : BaseTypeMetadata
    {
        [Key, StringLength(100)]
        public override string TypeName { get; set; }
        public override string NamespaceName { get; set; }
        public new TypeMetadataDTO BaseType { get; set; }
        public new List<TypeMetadataDTO> GenericArguments { get; set; }
        public override AccessLevel AccessLevel { get; set; }
        public override AbstractEnum AbstractEnum { get; set; }
        public override SealedEnum SealedEnum { get; set; }
        public override TypeKind TypeKind { get; set; }
        public new List<TypeMetadataDTO> ImplementedInterfaces { get; set; }
        public new List<TypeMetadataDTO> NestedTypes { get; set; }
        public new List<FieldMetadataDTO> Fields { get; set; }
        public new List<PropertyMetadataDTO> Properties { get; set; }
        public new TypeMetadataDTO DeclaringType { get; set; }
        public new List<MethodMetadataDTO> Methods { get; set; }
        public new List<MethodMetadataDTO> Constructors { get; set; }

        public TypeMetadataDTO()
        {
            GenericArguments = new List<TypeMetadataDTO>();
            //Attributes = new List<Attribute>();
            ImplementedInterfaces = new List<TypeMetadataDTO>();
            NestedTypes = new List<TypeMetadataDTO>();
            Fields = new List<FieldMetadataDTO>();
            Properties = new List<PropertyMetadataDTO>();
            Methods = new List<MethodMetadataDTO>();
            Constructors = new List<MethodMetadataDTO>();
        }
    }
}