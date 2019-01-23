﻿using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("TypeMetadata")]
    public class TypeMetadataDatabaseDTO : BaseTypeMetadata
    {
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
                if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(typeMetadataDTO.BaseType.TypeName)) BaseType = Mapper.DatabaseDTOTypeDictionary[typeMetadataDTO.BaseType.TypeName];
                else BaseType = new TypeMetadataDatabaseDTO(typeMetadataDTO.BaseType);
            }

            if (typeMetadataDTO.GenericArguments != null)
            {
                List<TypeMetadataDatabaseDTO> arguments = new List<TypeMetadataDatabaseDTO>();
                foreach (BaseTypeMetadata DTO in typeMetadataDTO.GenericArguments)
                {
                    TypeMetadataDatabaseDTO metadata;
                    if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = Mapper.DatabaseDTOTypeDictionary[DTO.TypeName];
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
                    if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = Mapper.DatabaseDTOTypeDictionary[DTO.TypeName];
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
                    if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = Mapper.DatabaseDTOTypeDictionary[DTO.TypeName];
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
                    PropertyMetadataDatabaseDTO propertyMetadata;
                    if (Mapper.DatabaseDTOPropertyDictionary.ContainsKey(DTO.Name)) propertyMetadata = Mapper.DatabaseDTOPropertyDictionary[DTO.Name];
                    else propertyMetadata = new PropertyMetadataDatabaseDTO(DTO);
                    properties.Add(propertyMetadata);
                }
                Properties = properties;
            }

            if (typeMetadataDTO.DeclaringType != null)
            {
                if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(typeMetadataDTO.DeclaringType.TypeName)) DeclaringType = Mapper.DatabaseDTOTypeDictionary[typeMetadataDTO.DeclaringType.TypeName];
                else DeclaringType = new TypeMetadataDatabaseDTO(typeMetadataDTO.DeclaringType);
            }

            if (typeMetadataDTO.Methods != null)
            {
                List<MethodMetadataDatabaseDTO> methods = new List<MethodMetadataDatabaseDTO>();
                foreach (BaseMethodMetadata DTO in typeMetadataDTO.Methods)
                {
                    if(DTO.Name != null)
                    {
                        MethodMetadataDatabaseDTO methodMetadata;
                        if (Mapper.DatabaseDTOMethodDictionary.ContainsKey(DTO.Name)) methodMetadata = Mapper.DatabaseDTOMethodDictionary[DTO.Name];
                        else methodMetadata = new MethodMetadataDatabaseDTO(DTO);
                        methods.Add(methodMetadata);
                    }
                }
                Methods = methods;
            }

            if (typeMetadataDTO.Constructors != null)
            {
                List<MethodMetadataDatabaseDTO> constructors = new List<MethodMetadataDatabaseDTO>();
                foreach (BaseMethodMetadata DTO in typeMetadataDTO.Constructors)
                {
                    if(DTO.Name != null)
                    {
                        MethodMetadataDatabaseDTO methodMetadata;
                        if (Mapper.DatabaseDTOMethodDictionary.ContainsKey(DTO.Name)) methodMetadata = Mapper.DatabaseDTOMethodDictionary[DTO.Name];
                        else methodMetadata = new MethodMetadataDatabaseDTO(DTO);
                        constructors.Add(methodMetadata);
                    }
                }
                Constructors = constructors;
            }

            if (typeMetadataDTO.Fields != null)
            {
                List<FieldMetadataDatabaseDTO> fields = new List<FieldMetadataDatabaseDTO>();
                foreach (BaseFieldMetadata DTO in typeMetadataDTO.Fields)
                {
                    FieldMetadataDatabaseDTO fieldMetadata;
                    if (Mapper.DatabaseDTOFieldDictionary.ContainsKey(DTO.FieldName)) fieldMetadata = Mapper.DatabaseDTOFieldDictionary[DTO.FieldName];
                    else fieldMetadata = new FieldMetadataDatabaseDTO(DTO);
                    fields.Add(fieldMetadata);
                }
                Fields = fields;
            }

            if (!Mapper.DatabaseDTOTypeDictionary.ContainsKey(TypeName))
            {
                Mapper.DatabaseDTOTypeDictionary.Add(TypeName, this);
            }
        }
    }
}