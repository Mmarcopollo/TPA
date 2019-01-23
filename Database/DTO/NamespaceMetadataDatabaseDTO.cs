using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("NamespaceMetadata")]
    public class NamespaceMetadataDatabaseDTO : BaseNamespaceMetadata
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string NamespaceName { get; set; }
        public override Guid Guid { get; set; }
        public new List<TypeMetadataDatabaseDTO> Types { get; set; }

        public NamespaceMetadataDatabaseDTO(BaseNamespaceMetadata namespaceMetadataDTO)
        {
            NamespaceName = namespaceMetadataDTO.NamespaceName;
            Guid = namespaceMetadataDTO.Guid;
            if (namespaceMetadataDTO.Types != null)
            {
                List<TypeMetadataDatabaseDTO> types = new List<TypeMetadataDatabaseDTO>();
                foreach (BaseTypeMetadata DTO in namespaceMetadataDTO.Types)
                {
                    TypeMetadataDatabaseDTO metadata;
                    if (TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDatabaseDTO.DatabaseDTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDatabaseDTO(DTO);
                    types.Add(metadata);
                }
                Types = types;
            }
        }
    }
}