using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

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
        [NotMapped]
        public new IEnumerable<TypeMetadataDatabaseDTO> Types { get; set; }
        public List<TypeMetadataDatabaseDTO> TypesEF { get; set; } = new List<TypeMetadataDatabaseDTO>();

        public NamespaceMetadataDatabaseDTO(BaseNamespaceMetadata namespaceMetadataDTO)
        {
            NamespaceName = "";
            NamespaceName = namespaceMetadataDTO.NamespaceName;
            List<TypeMetadataDatabaseDTO> m_Types = new List<TypeMetadataDatabaseDTO>();
            foreach (var type in namespaceMetadataDTO.Types)
            {
                Mapper.DatabaseDTOTypeDictionary[type.TypeName] = new TypeMetadataDatabaseDTO(type);
                m_Types.Add(TypeMetadataDatabaseDTO.FillType(Mapper.DatabaseDTOTypeDictionary[type.TypeName], type));
            }
            Types = m_Types;
        }

        public void ToEntityFramework()
        {
            TypesEF = Types.ToList();
            foreach (TypeMetadataDatabaseDTO type in TypesEF)
            {
                type.ToEntityFramework();
            }
        }

        public void RepopulateNamespace()
        {
            Types = TypesEF;
            if (Types != null)
            {
                foreach (TypeMetadataDatabaseDTO typeMetadata in Types)
                {
                    if(!Mapper.RepopulatedTypesDictionary.ContainsKey(typeMetadata.TypeName))typeMetadata.RepopulateType();
                }
            }
        }

        public NamespaceMetadataDatabaseDTO() { }
    }
}