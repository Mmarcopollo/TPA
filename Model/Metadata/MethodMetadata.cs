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
    public class MethodMetadata
    {
        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodMetadata(_currentMethod);
        }

        #region private
        //vars
        public string m_Name;
        public IEnumerable<TypeMetadata> m_GenericArguments;
        public AccessLevel AccessLevel;
        public AbstractEnum AbstractEnum;
        public StaticEnum StaticEnum;
        public VirtualEnum VirtualEnum;
        public TypeMetadata m_ReturnType;
        public bool m_Extension;
        public IEnumerable<ParameterMetadata> m_Parameters;

        public string Name { get => m_Name; set => m_Name = value; }

        //constructor
        private MethodMetadata(MethodBase method)
        {
            m_Name = method.Name;
            m_GenericArguments = !method.IsGenericMethodDefinition ? null : TypeMetadata.EmitGenericArguments(method.GetGenericArguments());
            m_ReturnType = EmitReturnType(method);
            m_Parameters = EmitParameters(method.GetParameters());
            EmitModifiers(method);
            m_Extension = EmitExtension(method);
        }

        public MethodMetadata(MethodMetadataDTO methodMetadataDTO)
        {
            m_Name = methodMetadataDTO.m_Name;
            if (methodMetadataDTO.m_GenericArguments != null)
            {
                List<TypeMetadata> generic = new List<TypeMetadata>();
                foreach (TypeMetadataDTO DTO in methodMetadataDTO.m_GenericArguments)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.m_typeName)) metadata = TypeMetadata.TypeDictionary[DTO.m_typeName];
                    else metadata = new TypeMetadata(DTO);
                    generic.Add(metadata);
                }
                m_GenericArguments = generic;
            }
            AccessLevel = (AccessLevel)methodMetadataDTO.AccessLevel;
            AbstractEnum = (AbstractEnum)methodMetadataDTO.AbstractEnum;
            StaticEnum = (StaticEnum)methodMetadataDTO.StaticEnum;
            VirtualEnum = (VirtualEnum)methodMetadataDTO.VirtualEnum;

            if (methodMetadataDTO.m_ReturnType != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(methodMetadataDTO.m_ReturnType.m_typeName)) m_ReturnType = TypeMetadata.TypeDictionary[methodMetadataDTO.m_ReturnType.m_typeName];
                else m_ReturnType = new TypeMetadata(methodMetadataDTO.m_ReturnType);
            }

            m_Extension = methodMetadataDTO.m_Extension;

            if (methodMetadataDTO.m_Parameters != null)
            {
                List<ParameterMetadata> parameters = new List<ParameterMetadata>();
                foreach (ParameterMetadataDTO DTO in methodMetadataDTO.m_Parameters)
                {
                    ParameterMetadata methodMetadata = new ParameterMetadata(DTO);
                    parameters.Add(methodMetadata);
                }
                m_Parameters = parameters;
            }
        }

        public MethodMetadataDTO ConvertToDTO()
        {
            MethodMetadataDTO result = new MethodMetadataDTO();
            result.m_Name = m_Name;
            if (m_GenericArguments != null)
            {
                List<TypeMetadataDTO> generic = new List<TypeMetadataDTO>();
                foreach (TypeMetadata metadata in m_GenericArguments)
                {
                    TypeMetadataDTO DTO;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(metadata.m_typeName)) DTO = TypeMetadataDTO.DTOTypeDictionary[metadata.m_typeName];
                    else DTO = metadata.ConvertToDTO();
                    generic.Add(DTO);
                }
                result.m_GenericArguments = generic;
            }
            result.AccessLevel = (Serialization.AccessLevel)AccessLevel;
            result.AbstractEnum = (Serialization.AbstractEnum)AbstractEnum;
            result.StaticEnum = (Serialization.StaticEnum)StaticEnum;
            result.VirtualEnum = (Serialization.VirtualEnum)VirtualEnum;

            if (m_ReturnType != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(m_ReturnType.m_typeName)) result.m_ReturnType = TypeMetadataDTO.DTOTypeDictionary[m_ReturnType.m_typeName];
                else result.m_ReturnType = m_ReturnType.ConvertToDTO();
            }

            result.m_Extension = m_Extension;

            if (m_Parameters != null)
            {
                List<ParameterMetadataDTO> parameters = new List<ParameterMetadataDTO>();
                foreach (ParameterMetadata metadata in m_Parameters)
                {
                    ParameterMetadataDTO methodMetadataDTO = metadata.ConvertToDTO();
                    parameters.Add(methodMetadataDTO);
                }
                result.m_Parameters = parameters;
            }
            return result;
        }
        //methods
        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm.Name, TypeMetadata.EmitReference(parm.ParameterType));
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
