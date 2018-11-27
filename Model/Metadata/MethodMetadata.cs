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
    [DataContract(IsReference = true)]
    public class MethodMetadata
    {
        internal static List<MethodMetadata> EmitMethods(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Public |
                                              BindingFlags.Static | BindingFlags.Instance).Select(t => new MethodMetadata(t)).ToList();

        }

        public MethodMetadata() { }

        #region private
        //vars
        [DataMember]
        public string m_Name;
        [DataMember]
        public List<TypeMetadata> m_GenericArguments;
        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> m_Modifiers;
        [DataMember]
        public TypeMetadata m_ReturnType;
        [DataMember]
        public bool m_Extension;
        [DataMember]
        public List<ParameterMetadata> m_Parameters;

        public string Name { get => m_Name; set => m_Name = value; }

        //constructor
        private MethodMetadata(MethodBase method)
        {
            m_Name = method.Name;
            m_GenericArguments = !method.IsGenericMethodDefinition ? null : EmitGenericArguments(method);
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method);
            m_Modifiers = EmitModifiers(method);
            m_Extension = EmitExtension(method);

        }
        //methods
        private List<TypeMetadata> EmitGenericArguments(MethodBase method)
        {
            return method.GetGenericArguments().Select(t => new TypeMetadata(t)).ToList();
        }
        public static List<MethodMetadata> EmitConstructors(Type type)
        {
            return type.GetConstructors().Select(t => new MethodMetadata(t)).ToList();
        }
        private static List<ParameterMetadata> EmitParameters(MethodBase method)
        {
            return method.GetParameters().Select(t => new ParameterMetadata(t.Name, TypeMetadata.EmitReference(t.ParameterType))).ToList();
        }
        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeMetadata.EmitReference(methodInfo.ReturnType);
        }
        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }
        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel _access = AccessLevel.IsPrivate;
            if (method.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (method.IsFamily)
                _access = AccessLevel.IsProtected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevel.IsProtectedInternal;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;
            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;
            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(_access, _abstract, _static, _virtual);
        }

        #endregion
    }
}
