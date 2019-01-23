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
        public static Dictionary<string, TypeMetadataDatabaseDTO> DatabaseDTOTypeDictionary = new Dictionary<string, TypeMetadataDatabaseDTO>();
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
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

        public TypeMetadataDatabaseDTO(BaseTypeMetadata typeMetadataDTO)
        {
            TypeName = typeMetadataDTO.TypeName;
            NamespaceName = typeMetadataDTO.NamespaceName;

            if (typeMetadataDTO.BaseType != null)
            {
                if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(typeMetadataDTO.BaseType.TypeName)) BaseType = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[typeMetadataDTO.BaseType.TypeName];
                else BaseType = new TypeMetadataDatabaseDTO(typeMetadataDTO.BaseType);
            }

            if (typeMetadataDTO.GenericArguments != null)
            {
                List<TypeMetadataDatabaseDTO> arguments = new List<TypeMetadataDatabaseDTO>();
                foreach (BaseTypeMetadata DTO in typeMetadataDTO.GenericArguments)
                {
                    TypeMetadataDatabaseDTO metadata;
                    if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDatabaseDTO(DTO);
                    arguments.Add(metadata);
                }
                GenericArguments = arguments;
            }

            AccessLevel = typeMetadataDTO.AccessLevel;
            AbstractEnum = typeMetadataDTO.AbstractEnum;
            SealedEnum = typeMetadataDTO.SealedEnum;
            TypeKind = typeMetadataDTO.TypeKind;


            if (typeMetadataDTO.ImplementedInterfaces != null)
            {
                List<TypeMetadataDatabaseDTO> interfaces = new List<TypeMetadataDatabaseDTO>();
                foreach (BaseTypeMetadata DTO in typeMetadataDTO.ImplementedInterfaces)
                {
                    TypeMetadataDatabaseDTO metadata;
                    if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDatabaseDTO(DTO);
                    interfaces.Add(metadata);
                }
                ImplementedInterfaces = interfaces;
            }

            if (typeMetadataDTO.NestedTypes != null)
            {
                List<TypeMetadataDatabaseDTO> nested = new List<TypeMetadataDatabaseDTO>();
                foreach (BaseTypeMetadata DTO in typeMetadataDTO.NestedTypes)
                {
                    TypeMetadataDatabaseDTO metadata;
                    if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDatabaseDTO(DTO);
                    nested.Add(metadata);
                }
                NestedTypes = nested;
            }

            if (typeMetadataDTO.Properties != null)
            {
                List<PropertyMetadataDatabaseDTO> properties = new List<PropertyMetadataDatabaseDTO>();
                foreach (BasePropertyMetadata DTO in typeMetadataDTO.Properties)
                {
                    PropertyMetadataDatabaseDTO propertyMetadata = new PropertyMetadataDatabaseDTO(DTO);
                    properties.Add(propertyMetadata);
                }
                Properties = properties;
            }

            if (typeMetadataDTO.DeclaringType != null)
            {
                if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(typeMetadataDTO.DeclaringType.TypeName)) DeclaringType = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[typeMetadataDTO.DeclaringType.TypeName];
                else DeclaringType = new TypeMetadataDatabaseDTO(typeMetadataDTO.DeclaringType);
            }

            if (typeMetadataDTO.Methods != null)
            {
                List<MethodMetadataDatabaseDTO> methods = new List<MethodMetadataDatabaseDTO>();
                foreach (BaseMethodMetadata DTO in typeMetadataDTO.Methods)
                {
                    MethodMetadataDatabaseDTO methodMetadata = new MethodMetadataDatabaseDTO(DTO);
                    methods.Add(methodMetadata);
                }
                Methods = methods;
            }

            if (typeMetadataDTO.Constructors != null)
            {
                List<MethodMetadataDatabaseDTO> constructors = new List<MethodMetadataDatabaseDTO>();
                foreach (BaseMethodMetadata DTO in typeMetadataDTO.Constructors)
                {
                    MethodMetadataDatabaseDTO methodMetadata = new MethodMetadataDatabaseDTO(DTO);
                    constructors.Add(methodMetadata);
                }
                Constructors = constructors;
            }

            if (typeMetadataDTO.Fields != null)
            {
                List<FieldMetadataDatabaseDTO> fields = new List<FieldMetadataDatabaseDTO>();
                foreach (BaseFieldMetadata DTO in typeMetadataDTO.Fields)
                {
                    FieldMetadataDatabaseDTO fieldMetadata = new FieldMetadataDatabaseDTO(DTO);
                    fields.Add(fieldMetadata);
                }
                Fields = fields;
            }

            if (!DatabaseDTOTypeDictionary.ContainsKey(this.TypeName))
            {
                DatabaseDTOTypeDictionary.Add(TypeName, this);
            }
        }
    }
}