using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.DTO
{
    [Table("TypeMetadata")]
    public class TypeMetadataDatabaseDTO : BaseTypeMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(100)]
        public override string TypeName { get => base.TypeName; set => base.TypeName = value; }
        public override string NamespaceName { get => base.NamespaceName; set => base.NamespaceName = value; }
        public new TypeMetadataDatabaseDTO BaseType { get => (TypeMetadataDatabaseDTO)base.BaseType; set => base.BaseType = value; }
        [NotMapped]
        public new IEnumerable<TypeMetadataDatabaseDTO> GenericArguments { get => (IEnumerable<TypeMetadataDatabaseDTO>)base.GenericArguments; set => base.GenericArguments = value; }
        public List<TypeMetadataDatabaseDTO> GenericArgumentsEF { get; set; } = new List<TypeMetadataDatabaseDTO>();
        public override AccessLevel AccessLevel { get => base.AccessLevel; set => base.AccessLevel = value; }
        public override AbstractEnum AbstractEnum { get => base.AbstractEnum; set => base.AbstractEnum = value; }
        public override SealedEnum SealedEnum { get => base.SealedEnum; set => base.SealedEnum = value; }
        public override TypeKind TypeKind { get => base.TypeKind; set => base.TypeKind = value; }
        [NotMapped]
        public new IEnumerable<TypeMetadataDatabaseDTO> ImplementedInterfaces { get => (IEnumerable<TypeMetadataDatabaseDTO>)base.ImplementedInterfaces; set => base.ImplementedInterfaces = value; }
        public List<TypeMetadataDatabaseDTO> ImplementedInterfacesEF { get; set; } = new List<TypeMetadataDatabaseDTO>();
        [NotMapped]
        public new IEnumerable<TypeMetadataDatabaseDTO> NestedTypes { get => (IEnumerable<TypeMetadataDatabaseDTO>)base.NestedTypes; set => base.NestedTypes = value; }
        public List<TypeMetadataDatabaseDTO> NestedTypesEF { get; set; } = new List<TypeMetadataDatabaseDTO>();
        [NotMapped]
        public new IEnumerable<FieldMetadataDatabaseDTO> Fields { get => (IEnumerable<FieldMetadataDatabaseDTO>)base.Fields; set => base.Fields = value; }
        [NotMapped]
        public List<FieldMetadataDatabaseDTO> FieldsEF { get; set; } = new List<FieldMetadataDatabaseDTO>();
        [NotMapped]
        public new IEnumerable<PropertyMetadataDatabaseDTO> Properties { get => (IEnumerable<PropertyMetadataDatabaseDTO>)base.Properties; set => base.Properties = value; }
        [NotMapped]
        public List<PropertyMetadataDatabaseDTO> PropertiesEF { get; set; } = new List<PropertyMetadataDatabaseDTO>();
        public new TypeMetadataDatabaseDTO DeclaringType { get => (TypeMetadataDatabaseDTO)base.DeclaringType; set => base.DeclaringType = value; }
        [NotMapped]
        public new IEnumerable<MethodMetadataDatabaseDTO> Methods { get => (IEnumerable<MethodMetadataDatabaseDTO>)base.Methods; set => base.Methods = value; }
        public List<MethodMetadataDatabaseDTO> MethodsEF { get; set; } = new List<MethodMetadataDatabaseDTO>();
        [NotMapped]
        public new IEnumerable<MethodMetadataDatabaseDTO> Constructors { get => (IEnumerable<MethodMetadataDatabaseDTO>)base.Constructors; set => base.Constructors = value; }
        public List<MethodMetadataDatabaseDTO> ConstructorsEF { get; set; } = new List<MethodMetadataDatabaseDTO>();

        public TypeMetadataDatabaseDTO(BaseTypeMetadata typeMetadataDTO)
        {
            TypeName = "";
            TypeName = typeMetadataDTO.TypeName;
            NamespaceName = typeMetadataDTO.NamespaceName;
            DeclaringType = EmitDeclaringTypeDatabase(typeMetadataDTO.DeclaringType);
            Constructors = MethodMetadataDatabaseDTO.EmitMethodsDatabase(typeMetadataDTO.Constructors);
            Methods = MethodMetadataDatabaseDTO.EmitMethodsDatabase(typeMetadataDTO.Methods);
            NestedTypes = EmitNestedTypesDatabase(typeMetadataDTO.NestedTypes);
            ImplementedInterfaces = EmitImplementsDatabase(typeMetadataDTO.ImplementedInterfaces);
            GenericArguments = CheckGenericArgumentsDatabase(typeMetadataDTO);
            BaseType = EmitExtendsDatabase(typeMetadataDTO.BaseType);
            Properties = PropertyMetadataDatabaseDTO.EmitPropertiesDatabase(typeMetadataDTO.Properties);
            Fields = FieldMetadataDatabaseDTO.EmitFieldsDatabase(typeMetadataDTO.Fields);
            AccessLevel = typeMetadataDTO.AccessLevel;
            SealedEnum = typeMetadataDTO.SealedEnum;
            TypeKind = typeMetadataDTO.TypeKind;
            AbstractEnum = typeMetadataDTO.AbstractEnum;

            if (!Mapper.DatabaseDTOTypeDictionary.ContainsKey(TypeName))
            {
                Mapper.DatabaseDTOTypeDictionary.Add(TypeName, this);
            }
        }

        public static TypeMetadataDatabaseDTO FillType(TypeMetadataDatabaseDTO _BaseTypeMetadata, BaseTypeMetadata typeMetadata)
        {
            _BaseTypeMetadata.TypeName = typeMetadata.TypeName;
            _BaseTypeMetadata.DeclaringType = EmitDeclaringTypeDatabase(typeMetadata.DeclaringType);
            _BaseTypeMetadata.Constructors = MethodMetadataDatabaseDTO.EmitMethodsDatabase(typeMetadata.Constructors);
            _BaseTypeMetadata.Methods = MethodMetadataDatabaseDTO.EmitMethodsDatabase(typeMetadata.Methods);
            _BaseTypeMetadata.NestedTypes = EmitNestedTypesDatabase(typeMetadata.NestedTypes);
            _BaseTypeMetadata.ImplementedInterfaces = EmitImplementsDatabase(typeMetadata.ImplementedInterfaces);
            if (typeMetadata.GenericArguments != null)
                _BaseTypeMetadata.GenericArguments = EmitGenericArgumentsDatabase(typeMetadata.GenericArguments);
            else _BaseTypeMetadata.GenericArguments = null;
            _BaseTypeMetadata.BaseType = EmitExtendsDatabase(typeMetadata.BaseType);
            _BaseTypeMetadata.Properties = PropertyMetadataDatabaseDTO.EmitPropertiesDatabase(typeMetadata.Properties);
            _BaseTypeMetadata.AccessLevel = typeMetadata.AccessLevel;
            _BaseTypeMetadata.SealedEnum = typeMetadata.SealedEnum;
            _BaseTypeMetadata.TypeKind = typeMetadata.TypeKind;
            _BaseTypeMetadata.AbstractEnum = typeMetadata.AbstractEnum;
            return _BaseTypeMetadata;
        }

        public void ToEntityFramework()
        {
            if (GenericArguments == null)
            {
                GenericArgumentsEF = null;
            }
            else
            {
                GenericArgumentsEF = GenericArguments.ToList();
                foreach (TypeMetadataDatabaseDTO type in GenericArgumentsEF)
                {
                    type.ToEntityFramework();
                }
            }

            if (ImplementedInterfaces != null) ImplementedInterfacesEF = ImplementedInterfaces.ToList();

            foreach (TypeMetadataDatabaseDTO type in ImplementedInterfacesEF)
            {
                type.ToEntityFramework();
            }

            if (NestedTypes != null) NestedTypesEF = NestedTypes.ToList();

            foreach (TypeMetadataDatabaseDTO type in NestedTypesEF)
            {
                type.ToEntityFramework();
            }

            if (Properties != null) PropertiesEF = Properties.ToList();

            if (Fields != null) FieldsEF = Fields.ToList();

            if (Methods != null) MethodsEF = Methods.ToList();

            foreach (MethodMetadataDatabaseDTO method in MethodsEF)
            {
                method.ToEntityFramework();
            }

            if (Constructors != null) ConstructorsEF = Constructors.ToList();

            foreach (MethodMetadataDatabaseDTO method in ConstructorsEF)
            {
                method.ToEntityFramework();
            }
        }

        internal static TypeMetadataDatabaseDTO EmitReferenceDatabase(BaseTypeMetadata type)
        {
            if (type == null) return null;
            if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(type.TypeName)) return Mapper.DatabaseDTOTypeDictionary[type.TypeName];
            return new TypeMetadataDatabaseDTO(type);
        }

        private static TypeMetadataDatabaseDTO EmitDeclaringTypeDatabase(BaseTypeMetadata declaringType)
        {
            if (declaringType == null) return null;
            return EmitReferenceDatabase(declaringType);
        }

        private static IEnumerable<TypeMetadataDatabaseDTO> EmitNestedTypesDatabase(IEnumerable<BaseTypeMetadata> nestedTypes)
        {
            if (nestedTypes == null) return null;
            return from _type in nestedTypes
                   select new TypeMetadataDatabaseDTO(_type);
        }

        private static IEnumerable<TypeMetadataDatabaseDTO> EmitImplementsDatabase(IEnumerable<BaseTypeMetadata> interfaces)
        {
            if (interfaces == null) return null;
            return from currentInterface in interfaces
                   select EmitReferenceDatabase(currentInterface);
        }

        public static IEnumerable<TypeMetadataDatabaseDTO> CheckGenericArgumentsDatabase(BaseTypeMetadata typeMetadata)
        {
            if (typeMetadata.GenericArguments != null)
                return EmitGenericArgumentsDatabase(typeMetadata.GenericArguments);
            return null;
        }

        internal static IEnumerable<TypeMetadataDatabaseDTO> EmitGenericArgumentsDatabase(IEnumerable<BaseTypeMetadata> arguments)
        {
            if (arguments == null) return null;
            return from BaseTypeMetadata _argument in arguments select EmitReferenceDatabase(_argument);
        }

        private static TypeMetadataDatabaseDTO EmitExtendsDatabase(BaseTypeMetadata baseType)
        {
            if (baseType == null) return null;
            return EmitReferenceDatabase(baseType);
        }

        public void RepopulateType()
        {
            Mapper.RepopulatedTypesDictionary.Add(TypeName, this);
            GenericArguments = GenericArgumentsEF;
            if(GenericArguments != null)
            {
                foreach(TypeMetadataDatabaseDTO typeMetadata in GenericArguments)
                {
                    if (!Mapper.RepopulatedTypesDictionary.ContainsKey(typeMetadata.TypeName)) typeMetadata.RepopulateType();
                }
            }

            ImplementedInterfaces = ImplementedInterfacesEF;
            if (ImplementedInterfaces != null)
            {
                foreach (TypeMetadataDatabaseDTO typeMetadata in ImplementedInterfaces)
                {
                    if (!Mapper.RepopulatedTypesDictionary.ContainsKey(typeMetadata.TypeName)) typeMetadata.RepopulateType();
                }
            }

            NestedTypes = NestedTypesEF;
            if (NestedTypes != null)
            {
                foreach (TypeMetadataDatabaseDTO typeMetadata in NestedTypes)
                {
                    if (!Mapper.RepopulatedTypesDictionary.ContainsKey(typeMetadata.TypeName)) typeMetadata.RepopulateType();
                }
            }

            Fields = FieldsEF;
            Properties = PropertiesEF;

            Methods = MethodsEF;
            if (Methods != null)
            {
                foreach (MethodMetadataDatabaseDTO methodMetadata in Methods)
                {
                    methodMetadata.RepopulateMethod();
                }
            }

            Constructors = ConstructorsEF;
            if (Constructors != null)
            {
                foreach (MethodMetadataDatabaseDTO methodMetadata in Constructors)
                {
                    methodMetadata.RepopulateMethod();
                }
            }
        }

        public TypeMetadataDatabaseDTO() { }
    }
}