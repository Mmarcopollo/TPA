using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TypeMetadata
    {
        public static Dictionary<string, TypeMetadata> TypeDictionary = new Dictionary<string, TypeMetadata>();

        #region constructors
        public TypeMetadata(Type type)
        {
            m_typeName = type.Name;
            m_DeclaringType = EmitDeclaringType(type.DeclaringType);
            m_Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            m_Methods = MethodMetadata.EmitMethods(type.GetMethods());
            m_NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            m_ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            m_GenericArguments = !type.IsGenericTypeDefinition ? null : TypeMetadata.EmitGenericArguments(type.GetGenericArguments());
            EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            m_TypeKind = GetTypeKind(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();

            if (!TypeDictionary.ContainsKey(this.m_typeName))
            {
                TypeDictionary.Add(Name, this);
            }
        }

        public TypeMetadata(TypeMetadataDTO typeMetadataDTO)
        {
            m_typeName = typeMetadataDTO.m_typeName;
            m_NamespaceName = typeMetadataDTO.m_NamespaceName;

            if(typeMetadataDTO.m_BaseType != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(typeMetadataDTO.m_BaseType.m_typeName)) m_BaseType = TypeMetadata.TypeDictionary[typeMetadataDTO.m_BaseType.m_typeName];
                else m_BaseType = new TypeMetadata(typeMetadataDTO.m_BaseType);
            }
            
            if(typeMetadataDTO.m_GenericArguments != null)
            {
                List<TypeMetadata> arguments = new List<TypeMetadata>();
                foreach(TypeMetadataDTO DTO in typeMetadataDTO.m_GenericArguments)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.m_typeName)) metadata = TypeMetadata.TypeDictionary[DTO.m_typeName];
                    else metadata = new TypeMetadata(DTO);
                    arguments.Add(metadata);
                }
                m_GenericArguments = arguments;
            }

            //nie wiem czy to rzutowanie jak i każde kolejne będzie działać
            AccessLevel = (AccessLevel)typeMetadataDTO.AccessLevel;
            AbstractEnum = (AbstractEnum)typeMetadataDTO.AbstractEnum;
            SealedEnum = (SealedEnum)typeMetadataDTO.SealedEnum;
            m_TypeKind = (TypeKind)typeMetadataDTO.m_TypeKind;
            m_Attributes = typeMetadataDTO.m_Attributes;

            if(typeMetadataDTO.m_ImplementedInterfaces != null)
            {
                List<TypeMetadata> interfaces = new List<TypeMetadata>();
                foreach (TypeMetadataDTO DTO in typeMetadataDTO.m_ImplementedInterfaces)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.m_typeName)) metadata = TypeMetadata.TypeDictionary[DTO.m_typeName];
                    else metadata = new TypeMetadata(DTO);
                    interfaces.Add(metadata);
                }
                m_ImplementedInterfaces = interfaces;
            }
            
            if(typeMetadataDTO.m_NestedTypes != null)
            {
                List<TypeMetadata> nested = new List<TypeMetadata>();
                foreach (TypeMetadataDTO DTO in typeMetadataDTO.m_NestedTypes)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.m_typeName)) metadata = TypeMetadata.TypeDictionary[DTO.m_typeName];
                    else metadata = new TypeMetadata(DTO);
                    nested.Add(metadata);
                }
                m_NestedTypes = nested;
            }

            if(typeMetadataDTO.m_Properties != null)
            {
                List<PropertyMetadata> properties = new List<PropertyMetadata>();
                foreach(PropertyMetadataDTO DTO in typeMetadataDTO.m_Properties)
                {
                    PropertyMetadata propertyMetadata = new PropertyMetadata(DTO);
                    properties.Add(propertyMetadata);
                }
                m_Properties = properties;
            }

            if (typeMetadataDTO.m_BaseType != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(typeMetadataDTO.m_DeclaringType.m_typeName)) m_DeclaringType = TypeMetadata.TypeDictionary[typeMetadataDTO.m_DeclaringType.m_typeName];
                else m_DeclaringType = new TypeMetadata(typeMetadataDTO.m_DeclaringType);
            }

            if(typeMetadataDTO.m_Methods != null)
            {
                List<MethodMetadata> methods = new List<MethodMetadata>();
                foreach (MethodMetadataDTO DTO in typeMetadataDTO.m_Methods)
                {
                    MethodMetadata methodMetadata = new MethodMetadata(DTO);
                    methods.Add(methodMetadata);
                }
                m_Methods = methods;
            }

            if(typeMetadataDTO.m_Constructors != null)
            {
                List<MethodMetadata> constructors = new List<MethodMetadata>();
                foreach (MethodMetadataDTO DTO in typeMetadataDTO.m_Constructors)
                {
                    MethodMetadata methodMetadata = new MethodMetadata(DTO);
                    constructors.Add(methodMetadata);
                }
                m_Constructors = constructors;
            }

            if (!TypeDictionary.ContainsKey(this.m_typeName))
            {
                TypeDictionary.Add(Name, this);
            }
        }
        #endregion

        #region API
        public enum TypeKind
        {
            EnumType, StructType, InterfaceType, ClassType
        }
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
        #endregion

        #region private
        //vars
        public string m_typeName;
        public string m_NamespaceName;
        public TypeMetadata m_BaseType;
        public IEnumerable<TypeMetadata> m_GenericArguments;
        public AccessLevel AccessLevel { get; set; }
        public AbstractEnum AbstractEnum { get; set; }
        public SealedEnum SealedEnum { get; set; }
        public TypeKind m_TypeKind;
        public IEnumerable<Attribute> m_Attributes;
        public IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        public IEnumerable<TypeMetadata> m_NestedTypes;
        public IEnumerable<PropertyMetadata> m_Properties;
        public TypeMetadata m_DeclaringType;
        public IEnumerable<MethodMetadata> m_Methods;
        public IEnumerable<MethodMetadata> m_Constructors;

        public string Name { get => m_typeName; set => m_typeName = value; }

        //constructors
        private TypeMetadata(string typeName, string namespaceName)
        {
            m_typeName = typeName;
            m_NamespaceName = namespaceName;
        }
        private TypeMetadata(string typeName, string namespaceName, IEnumerable<TypeMetadata> genericArguments) : this(typeName, namespaceName)
        {
            m_GenericArguments = genericArguments;
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
