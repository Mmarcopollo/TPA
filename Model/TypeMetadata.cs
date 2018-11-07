using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TypeMetadata : Metadata
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
            m_Modifiers = EmitModifiers(type);
            m_BaseType = EmitExtends(type.BaseType);
            m_Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            m_TypeKind = GetTypeKind(type);
            m_Attributes = type.GetCustomAttributes(false).Cast<Attribute>();

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
        public Tuple<AccessLevel, SealedEnum, AbstractENum> m_Modifiers;
        public TypeKind m_TypeKind;
        public IEnumerable<Attribute> m_Attributes;
        public IEnumerable<TypeMetadata> m_ImplementedInterfaces;
        public IEnumerable<TypeMetadata> m_NestedTypes;
        public IEnumerable<PropertyMetadata> m_Properties;
        public TypeMetadata m_DeclaringType;
        public IEnumerable<MethodMetadata> m_Methods;
        public IEnumerable<MethodMetadata> m_Constructors;

        public override string Name { get => m_typeName; set => m_typeName = value; }

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
        static Tuple<AccessLevel, SealedEnum, AbstractENum> EmitModifiers(Type type)
        {
            //set defaults 
            AccessLevel _access = AccessLevel.IsPrivate;
            AbstractENum _abstract = AbstractENum.NotAbstract;
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
                _abstract = AbstractENum.Abstract;
            return new Tuple<AccessLevel, SealedEnum, AbstractENum>(_access, _sealed, _abstract);
        }
        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) || baseType == typeof(Enum))
                return null;
            return EmitReference(baseType);
        }

        public override IEnumerable<NamespaceMetadata> GetAllNamespaces()
        {
            return null;
        }

        public override IEnumerable<TypeMetadata> GetAllTypes()
        {
            IEnumerable<TypeMetadata> result = Enumerable.Empty<TypeMetadata>();
            if (m_BaseType != null) result.Concat(new[] { m_BaseType });
            if (m_GenericArguments != null) result.Concat(m_GenericArguments);
            if (m_ImplementedInterfaces != null) result.Concat(m_ImplementedInterfaces);
            if (m_NestedTypes != null) result.Concat(m_NestedTypes);
            if (m_DeclaringType != null) result.Concat(new[] { m_DeclaringType });
            return result;
        }

        public override IEnumerable<PropertyMetadata> GetAllProperties()
        {
            return m_Properties;
        }

        public override IEnumerable<MethodMetadata> GetAllMethods()
        {
            if (m_Methods == null) m_Methods = Enumerable.Empty<MethodMetadata>();
            if (m_Constructors == null) m_Constructors = Enumerable.Empty<MethodMetadata>();
            return m_Methods.Concat(m_Constructors);
        }

        public override IEnumerable<ParameterMetadata> GetAllParameters()
        {
            return null;
        }
        #endregion


    }
}
