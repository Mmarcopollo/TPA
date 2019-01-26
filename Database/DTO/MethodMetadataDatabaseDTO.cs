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
        public override string Name { get; set; }
        [NotMapped]
        public new IEnumerable<TypeMetadataDatabaseDTO> GenericArguments { get; set; }
        public List<TypeMetadataDatabaseDTO> GenericArgumentsEF { get; set; } = new List<TypeMetadataDatabaseDTO>();
        public override AccessLevel AccessLevel { get; set; }
        public override AbstractEnum AbstractEnum { get; set; }
        public override StaticEnum StaticEnum { get; set; }
        public override VirtualEnum VirtualEnum { get; set; }
        public new TypeMetadataDatabaseDTO ReturnType { get; set; }
        public override bool Extension { get; set; }
        [NotMapped]
        public new IEnumerable<ParameterMetadataDatabaseDTO> Parameters { get; set; }
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

            if (!Mapper.DatabaseDTOMethodDictionary.ContainsKey(Name))
            {
                Mapper.DatabaseDTOMethodDictionary.Add(Name, this);
            }
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

        public MethodMetadataDatabaseDTO() { }
    }
}