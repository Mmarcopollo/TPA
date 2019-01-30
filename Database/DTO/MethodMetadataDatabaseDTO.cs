using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using BasicData;

namespace Database.DTO
{
    [Table("MethodMetadata")]
    public class MethodMetadataDatabaseDTO : BaseMethodMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string Name { get => base.Name; set => base.Name = value; }
        [NotMapped]
        public new IEnumerable<TypeMetadataDatabaseDTO> GenericArguments { get => (IEnumerable<TypeMetadataDatabaseDTO>)base.GenericArguments; set => base.GenericArguments = value; }
        public List<TypeMetadataDatabaseDTO> GenericArgumentsEF { get; set; } = new List<TypeMetadataDatabaseDTO>();
        public override AccessLevel AccessLevel { get => base.AccessLevel; set => base.AccessLevel = value; }
        public override AbstractEnum AbstractEnum { get => base.AbstractEnum; set => base.AbstractEnum = value; }
        public override StaticEnum StaticEnum { get => base.StaticEnum; set => base.StaticEnum = value; }
        public override VirtualEnum VirtualEnum { get => base.VirtualEnum; set => base.VirtualEnum = value; }
        public new TypeMetadataDatabaseDTO ReturnType { get => (TypeMetadataDatabaseDTO)base.ReturnType; set => base.ReturnType = value; }
        public override bool Extension { get => base.Extension; set => base.Extension = value; }
        [NotMapped]
        public new IEnumerable<ParameterMetadataDatabaseDTO> Parameters { get => (IEnumerable<ParameterMetadataDatabaseDTO>)base.Parameters; set => base.Parameters = value; }
        public List<ParameterMetadataDatabaseDTO> ParametersEF { get; set; } = new List<ParameterMetadataDatabaseDTO>();

        public MethodMetadataDatabaseDTO(BaseMethodMetadata methodMetadataDTO)
        {
            Name = "";
            Name = methodMetadataDTO.Name;
            GenericArguments = TypeMetadataDatabaseDTO.EmitGenericArgumentsDatabase(methodMetadataDTO.GenericArguments);
            ReturnType = EmitReturnTypeDatabase(methodMetadataDTO);
            Parameters = EmitParametersDatabase(methodMetadataDTO.Parameters);
            AccessLevel = methodMetadataDTO.AccessLevel;
            AbstractEnum = methodMetadataDTO.AbstractEnum;
            StaticEnum = methodMetadataDTO.StaticEnum;
            VirtualEnum = methodMetadataDTO.VirtualEnum;
        }

        public void ToEntityFramework()
        {
            ParametersEF.ToList();
        }

        internal static IEnumerable<MethodMetadataDatabaseDTO> EmitMethodsDatabase(IEnumerable<BaseMethodMetadata> methods)
        {
            if (methods == null) return null;
            return from BaseMethodMetadata _currentMethod in methods
                   select new MethodMetadataDatabaseDTO(_currentMethod);
        }

        private static IEnumerable<ParameterMetadataDatabaseDTO> EmitParametersDatabase(IEnumerable<BaseParameterMetadata> parms)
        {
            return from parm in parms
                   select new ParameterMetadataDatabaseDTO(parm);
        }

        private static TypeMetadataDatabaseDTO EmitReturnTypeDatabase(BaseMethodMetadata method)
        {
            if (!(method is BaseMethodMetadata methodInfo)) return null;
            return TypeMetadataDatabaseDTO.EmitReferenceDatabase(methodInfo.ReturnType);
        }

        public void RepopulateMethod()
        {
            GenericArguments = GenericArgumentsEF;
            if (GenericArguments != null)
            {
                foreach (TypeMetadataDatabaseDTO typeMetadata in GenericArguments)
                {
                    if (!Mapper.RepopulatedTypesDictionary.ContainsKey(typeMetadata.TypeName)) typeMetadata.RepopulateType();
                }
            }

            Parameters = ParametersEF;
        }

        public MethodMetadataDatabaseDTO() { }
    }
}