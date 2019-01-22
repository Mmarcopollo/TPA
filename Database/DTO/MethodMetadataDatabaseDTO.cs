using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BasicData;

namespace Database.DTO
{
    [Table("MethodMetadata")]
    public class MethodMetadataDatabaseDTO : BaseMethodMetadata
    {
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
    }
}