using BasicData;
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
    public class MethodMetadata : BaseMethodMetadata
    {
        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                   where _currentMethod.GetVisible()
                   select new MethodMetadata(_currentMethod);
        }

        #region private

        public override string Name { get => base.Name; set => base.Name = value; }
        public new IEnumerable<TypeMetadata> GenericArguments { get => (IEnumerable<TypeMetadata>)base.GenericArguments; set => base.GenericArguments = value; }
        public override AccessLevel AccessLevel { get => base.AccessLevel; set => base.AccessLevel = value; }
        public override AbstractEnum AbstractEnum { get => base.AbstractEnum; set => base.AbstractEnum = value; }
        public override StaticEnum StaticEnum { get => base.StaticEnum; set => base.StaticEnum = value; }
        public override VirtualEnum VirtualEnum { get => base.VirtualEnum; set => base.VirtualEnum = value; }
        public new TypeMetadata ReturnType { get => (TypeMetadata)base.ReturnType; set => base.ReturnType = value; }
        public override bool Extension { get => base.Extension; set => base.Extension = value; }
        public new IEnumerable<ParameterMetadata> Parameters { get => (IEnumerable<ParameterMetadata>)base.Parameters; set => base.Parameters = value; }


        //constructor
        private MethodMetadata(MethodBase method)
        {
            base.Name = method.Name;
            GenericArguments = !method.IsGenericMethodDefinition ? null : TypeMetadata.EmitGenericArguments(method.GetGenericArguments());
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters());
            EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodMetadata(MethodMetadataDTO methodMetadataDTO)
        {
            base.Name = methodMetadataDTO.Name;
            if (methodMetadataDTO.GenericArguments != null)
            {
                List<TypeMetadata> generic = new List<TypeMetadata>();
                foreach (TypeMetadataDTO DTO in methodMetadataDTO.GenericArguments)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadata.TypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadata(DTO);
                    generic.Add(metadata);
                }
                GenericArguments = generic;
            }
            AccessLevel = methodMetadataDTO.AccessLevel;
            AbstractEnum = methodMetadataDTO.AbstractEnum;
            StaticEnum = methodMetadataDTO.StaticEnum;
            VirtualEnum = methodMetadataDTO.VirtualEnum;

            if (methodMetadataDTO.ReturnType != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(methodMetadataDTO.ReturnType.TypeName)) ReturnType = TypeMetadata.TypeDictionary[methodMetadataDTO.ReturnType.TypeName];
                else ReturnType = new TypeMetadata((TypeMetadataDTO)methodMetadataDTO.ReturnType);
            }

            Extension = methodMetadataDTO.Extension;

            if (methodMetadataDTO.Parameters != null)
            {
                List<ParameterMetadata> parameters = new List<ParameterMetadata>();
                foreach (ParameterMetadataDTO DTO in methodMetadataDTO.Parameters)
                {
                    ParameterMetadata methodMetadata = new ParameterMetadata(DTO);
                    parameters.Add(methodMetadata);
                }
                Parameters = parameters;
            }
        }

        public MethodMetadataDTO ConvertToDTO()
        {
            MethodMetadataDTO result = new MethodMetadataDTO
            {
                Name = base.Name
            };
            if (GenericArguments != null)
            {
                List<TypeMetadataDTO> generic = new List<TypeMetadataDTO>();
                foreach (TypeMetadata metadata in GenericArguments)
                {
                    TypeMetadataDTO DTO;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(metadata.TypeName)) DTO = TypeMetadataDTO.DTOTypeDictionary[metadata.TypeName];
                    else DTO = metadata.ConvertToDTO();
                    generic.Add(DTO);
                }
                result.GenericArguments = generic;
            }
            result.AccessLevel = AccessLevel;
            result.AbstractEnum = AbstractEnum;
            result.StaticEnum = StaticEnum;
            result.VirtualEnum = VirtualEnum;

            if (ReturnType != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(ReturnType.TypeName)) result.ReturnType = TypeMetadataDTO.DTOTypeDictionary[ReturnType.TypeName];
                else result.ReturnType = ((TypeMetadata)ReturnType).ConvertToDTO();
            }

            result.Extension = Extension;

            if (Parameters != null)
            {
                List<ParameterMetadataDTO> parameters = new List<ParameterMetadataDTO>();
                foreach (ParameterMetadata metadata in Parameters)
                {
                    ParameterMetadataDTO methodMetadataDTO = metadata.ConvertToDTO();
                    parameters.Add(methodMetadataDTO);
                }
                result.Parameters = parameters;
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
