﻿using BasicData;
using Model.Metadata;
using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TypeMetadata : BaseTypeMetadata
    {
        public static Dictionary<string, TypeMetadata> TypeDictionary = new Dictionary<string, TypeMetadata>();

        public override string TypeName { get => base.TypeName; set => base.TypeName = value; }
        public override string NamespaceName { get => base.NamespaceName; set => base.NamespaceName = value; }
        public new TypeMetadata BaseType { get => (TypeMetadata)base.BaseType; set => base.BaseType = value; }
        public new IEnumerable<TypeMetadata> GenericArguments { get => (IEnumerable<TypeMetadata>)base.GenericArguments; set => base.GenericArguments = value; }
        public override AccessLevel AccessLevel { get => base.AccessLevel; set => base.AccessLevel = value; }
        public override AbstractEnum AbstractEnum { get => base.AbstractEnum; set => base.AbstractEnum = value; }
        public override SealedEnum SealedEnum { get => base.SealedEnum; set => base.SealedEnum = value; }
        public override TypeKind TypeKind { get => base.TypeKind; set => base.TypeKind = value; }
        public new IEnumerable<FieldMetadata> Fields { get => (IEnumerable<FieldMetadata>)base.Fields; set => base.Fields = value; }
        public override IEnumerable<Attribute> Attributes { get => base.Attributes; set => base.Attributes = value; }
        public new IEnumerable<TypeMetadata> ImplementedInterfaces { get => (IEnumerable<TypeMetadata>)base.ImplementedInterfaces; set => base.ImplementedInterfaces = value; }
        public new IEnumerable<TypeMetadata> NestedTypes { get => (IEnumerable<TypeMetadata>)base.NestedTypes; set => base.NestedTypes = value; }
        public new IEnumerable<PropertyMetadata> Properties { get => (IEnumerable<PropertyMetadata>)base.Properties; set => base.Properties = value; }
        public new TypeMetadata DeclaringType { get => (TypeMetadata)base.DeclaringType; set => base.DeclaringType = value; }
        public new IEnumerable<MethodMetadata> Methods { get => (IEnumerable<MethodMetadata>)base.Methods; set => base.Methods = value; }
        public new IEnumerable<MethodMetadata> Constructors { get => (IEnumerable<MethodMetadata>)base.Constructors; set => base.Constructors = value; }


        #region constructors
        public TypeMetadata(Type type)
        {
            TypeName = type.Name;
            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            Methods = MethodMetadata.EmitMethods(type.GetMethods());
            NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            EmitModifiers(type);
            BaseType = EmitExtends(type.BaseType);
            Properties = PropertyMetadata.EmitProperties(type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static));
            Fields = EmitFields(type);
            TypeKind = GetTypeKind(type);
            Attributes = type.GetCustomAttributes(false).Cast<Attribute>();


            if (!TypeDictionary.ContainsKey(this.TypeName))
            {
                TypeDictionary.Add(TypeName, this);
            }
        }

        public TypeMetadata(TypeMetadataDTO typeMetadataDTO)
        {
            TypeName = typeMetadataDTO.TypeName;
            NamespaceName = typeMetadataDTO.NamespaceName;

            if(typeMetadataDTO.BaseType != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(typeMetadataDTO.BaseType.TypeName)) BaseType = TypeMetadata.TypeDictionary[typeMetadataDTO.BaseType.TypeName];
                else BaseType = new TypeMetadata((TypeMetadataDTO)typeMetadataDTO.BaseType);
            }
            
            if(typeMetadataDTO.GenericArguments != null)
            {
                List<TypeMetadata> arguments = new List<TypeMetadata>();
                foreach(TypeMetadataDTO DTO in typeMetadataDTO.GenericArguments)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadata.TypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadata(DTO);
                    arguments.Add(metadata);
                }
                GenericArguments = arguments;
            }

            AccessLevel = typeMetadataDTO.AccessLevel;
            AbstractEnum = typeMetadataDTO.AbstractEnum;
            SealedEnum = typeMetadataDTO.SealedEnum;
            TypeKind = typeMetadataDTO.TypeKind;
            Attributes = typeMetadataDTO.Attributes;

            if(typeMetadataDTO.ImplementedInterfaces != null)
            {
                List<TypeMetadata> interfaces = new List<TypeMetadata>();
                foreach (TypeMetadataDTO DTO in typeMetadataDTO.ImplementedInterfaces)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadata.TypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadata(DTO);
                    interfaces.Add(metadata);
                }
                ImplementedInterfaces = interfaces;
            }
            
            if(typeMetadataDTO.NestedTypes != null)
            {
                List<TypeMetadata> nested = new List<TypeMetadata>();
                foreach (TypeMetadataDTO DTO in typeMetadataDTO.NestedTypes)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadata.TypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadata(DTO);
                    nested.Add(metadata);
                }
                NestedTypes = nested;
            }

            if(typeMetadataDTO.Properties != null)
            {
                List<PropertyMetadata> properties = new List<PropertyMetadata>();
                foreach(PropertyMetadataDTO DTO in typeMetadataDTO.Properties)
                {
                    PropertyMetadata propertyMetadata = new PropertyMetadata(DTO);
                    properties.Add(propertyMetadata);
                }
                Properties = properties;
            }

            if (typeMetadataDTO.DeclaringType != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(typeMetadataDTO.DeclaringType.TypeName)) DeclaringType = TypeMetadata.TypeDictionary[typeMetadataDTO.DeclaringType.TypeName];
                else DeclaringType = new TypeMetadata((TypeMetadataDTO)typeMetadataDTO.DeclaringType);
            }

            if(typeMetadataDTO.Methods != null)
            {
                List<MethodMetadata> methods = new List<MethodMetadata>();
                foreach (MethodMetadataDTO DTO in typeMetadataDTO.Methods)
                {
                    MethodMetadata methodMetadata = new MethodMetadata(DTO);
                    methods.Add(methodMetadata);
                }
                Methods = methods;
            }

            if(typeMetadataDTO.Constructors != null)
            {
                List<MethodMetadata> constructors = new List<MethodMetadata>();
                foreach (MethodMetadataDTO DTO in typeMetadataDTO.Constructors)
                {
                    MethodMetadata methodMetadata = new MethodMetadata(DTO);
                    constructors.Add(methodMetadata);
                }
                Constructors = constructors;
            }

            if (!TypeDictionary.ContainsKey(this.TypeName))
            {
                TypeDictionary.Add(TypeName, this);
            }
        }

        public TypeMetadataDTO ConvertToDTO()
        {
            TypeMetadataDTO result = new TypeMetadataDTO
            {
                TypeName = TypeName,
                NamespaceName = NamespaceName
            };

            if (BaseType != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(BaseType.TypeName)) result.BaseType = TypeMetadataDTO.DTOTypeDictionary[BaseType.TypeName];
                else result.BaseType = ((TypeMetadata)BaseType).ConvertToDTO();
            }

            if (GenericArguments != null)
            {
                List<TypeMetadataDTO> arguments = new List<TypeMetadataDTO>();
                foreach (TypeMetadata metadata in GenericArguments)
                {
                    TypeMetadataDTO DTO;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(metadata.TypeName)) DTO = TypeMetadataDTO.DTOTypeDictionary[metadata.TypeName];
                    else DTO = metadata.ConvertToDTO();
                    arguments.Add(DTO);
                }
                result.GenericArguments = arguments;
            }

            result.AccessLevel = AccessLevel;
            result.AbstractEnum = AbstractEnum;
            result.SealedEnum = SealedEnum;
            result.TypeKind = TypeKind;
            result.Attributes = Attributes;

            if (ImplementedInterfaces != null)
            {
                List<TypeMetadataDTO> interfaces = new List<TypeMetadataDTO>();
                foreach (TypeMetadata metadata in ImplementedInterfaces)
                {
                    TypeMetadataDTO DTO;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(metadata.TypeName)) DTO = TypeMetadataDTO.DTOTypeDictionary[metadata.TypeName];
                    else DTO = metadata.ConvertToDTO();
                    interfaces.Add(DTO);
                }
                result.ImplementedInterfaces = interfaces;
            }

            if (NestedTypes != null)
            {
                List<TypeMetadataDTO> nested = new List<TypeMetadataDTO>();
                foreach (TypeMetadata metadata in NestedTypes)
                {
                    TypeMetadataDTO DTO;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(metadata.TypeName)) DTO = TypeMetadataDTO.DTOTypeDictionary[metadata.TypeName];
                    else DTO = metadata.ConvertToDTO();
                    nested.Add(DTO);
                }
                result.NestedTypes = nested;
            }

            if (Properties != null)
            {
                List<PropertyMetadataDTO> properties = new List<PropertyMetadataDTO>();
                foreach (PropertyMetadata metadata in Properties)
                {
                    PropertyMetadataDTO propertyMetadataDTO = metadata.ConvertToDTO();
                    properties.Add(propertyMetadataDTO);
                }
                result.Properties = properties;
            }

            if (DeclaringType != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(DeclaringType.TypeName)) result.DeclaringType = TypeMetadataDTO.DTOTypeDictionary[DeclaringType.TypeName];
                else result.DeclaringType = ((TypeMetadata)DeclaringType).ConvertToDTO();
            }

            if (Methods != null)
            {
                List<MethodMetadataDTO> methods = new List<MethodMetadataDTO>();
                foreach (MethodMetadata metadata in Methods)
                {
                    MethodMetadataDTO methodMetadataDTO = metadata.ConvertToDTO();
                    methods.Add(methodMetadataDTO);
                }
                result.Methods = methods;
            }

            if (Constructors != null)
            {
                List<MethodMetadataDTO> constructors = new List<MethodMetadataDTO>();
                foreach (MethodMetadata metadata in Constructors)
                {
                    MethodMetadataDTO methodMetadataDTO = metadata.ConvertToDTO();
                    constructors.Add(methodMetadataDTO);
                }
                result.Constructors = constructors;
            }

            if (!TypeMetadataDTO.DTOTypeDictionary.ContainsKey(result.TypeName))
            {
                TypeMetadataDTO.DTOTypeDictionary.Add(result.TypeName, result);
            }
            return result;
        }
        #endregion

        #region API
        public static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
                return new TypeMetadata(type.Name, type.GetNamespace());
            else
                return new TypeMetadata(type.Name, type.GetNamespace(), EmitGenericArguments(type.GetGenericArguments()));
        }
        public static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type _argument in arguments select EmitReference(_argument);
        }
        private static IEnumerable<FieldMetadata> EmitFields(Type type)
        {
            List<FieldMetadata> fields = new List<FieldMetadata>();
            foreach (var field in type
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Where(f => f.GetCustomAttribute<CompilerGeneratedAttribute>() == null))
            {
                fields.Add(new FieldMetadata(field));
            }

            return fields;
        }
        #endregion

        #region private
        //constructors
        private TypeMetadata(string typeName, string namespaceName)
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }
        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            GenericArguments = genericArguments;
        }
        //methods
        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }
        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from _type in nestedTypes
                   where _type.GetVisible()
                   select new TypeMetadata(_type);
        }
        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                   select EmitReference(currentInterface);
        }
        private static TypeKind GetTypeKind(Type type) //#80 TPA: Reflection - Invalid return value of GetTypeKind() 
        {
            return type.IsEnum ? TypeKind.EnumType :
                   type.IsValueType ? TypeKind.StructType :
                   type.IsInterface ? TypeKind.InterfaceType :
                   TypeKind.ClassType;
        }
        static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevel _access = AccessLevel.IsPrivate;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            SealedEnum _sealed = SealedEnum.NotSealed;
            // check if not default 
            if (type.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (type.IsNestedPublic)
                _access = AccessLevel.IsPublic;
            else if (type.IsNestedFamily)
                _access = AccessLevel.IsProtected;
            else if (type.IsNestedFamANDAssem)
                _access = AccessLevel.IsProtectedInternal;
            if (type.IsSealed)
                _sealed = SealedEnum.Sealed;
            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(_access, _sealed, _abstract);
        }
        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }

        #endregion


    }
}
