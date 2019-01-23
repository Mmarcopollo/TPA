using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public new List<TypeMetadataDatabaseDTO> GenericArguments { get; set; }
        public override AccessLevel AccessLevel { get; set; }
        public override AbstractEnum AbstractEnum { get; set; }
        public override StaticEnum StaticEnum { get; set; }
        public override VirtualEnum VirtualEnum { get; set; }
        public new TypeMetadataDatabaseDTO ReturnType { get; set; }
        public override bool Extension { get; set; }
        public new List<ParameterMetadataDatabaseDTO> Parameters { get; set; }

        public MethodMetadataDatabaseDTO(BaseMethodMetadata methodMetadataDTO)
        {
            base.Name = methodMetadataDTO.Name;
            if (methodMetadataDTO.GenericArguments != null)
            {
                List<TypeMetadataDatabaseDTO> generic = new List<TypeMetadataDatabaseDTO>();
                foreach (BaseTypeMetadata DTO in methodMetadataDTO.GenericArguments)
                {
                    TypeMetadataDatabaseDTO metadata;
                    if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = Mapper.DatabaseDTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDatabaseDTO(DTO);
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
                if (Mapper.DatabaseDTOTypeDictionary.ContainsKey(methodMetadataDTO.ReturnType.TypeName)) ReturnType = Mapper.DatabaseDTOTypeDictionary[methodMetadataDTO.ReturnType.TypeName];
                else ReturnType = new TypeMetadataDatabaseDTO(methodMetadataDTO.ReturnType);
            }

            Extension = methodMetadataDTO.Extension;

            if (methodMetadataDTO.Parameters != null)
            {
                List<ParameterMetadataDatabaseDTO> parameters = new List<ParameterMetadataDatabaseDTO>();
                foreach (BaseParameterMetadata DTO in methodMetadataDTO.Parameters)
                {
                    ParameterMetadataDatabaseDTO methodMetadata;
                    if (Mapper.DatabaseDTOParameterDictionary.ContainsKey(DTO.Name)) methodMetadata = Mapper.DatabaseDTOParameterDictionary[DTO.Name];
                    else methodMetadata = new ParameterMetadataDatabaseDTO(DTO);
                    parameters.Add(methodMetadata);
                }
                Parameters = parameters;
            }

            if (!Mapper.DatabaseDTOMethodDictionary.ContainsKey(methodMetadataDTO.Name))
            {
                Mapper.DatabaseDTOMethodDictionary.Add(Name, this);
            }
        }
    }
}