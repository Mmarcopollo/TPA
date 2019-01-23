using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO
{
    [Table("AssemblyMetadata")]
    [Export(typeof(BaseAssemblyMetadata))]
    public class AssemblyMetadataDatabaseDTO : BaseAssemblyMetadata
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string Name { get => base.Name; set => base.Name = value; }
        public override Guid Guid { get => base.Guid; set => base.Guid = value; }
        [NotMapped]
        public new IEnumerable<NamespaceMetadataDatabaseDTO> Namespaces { get => (IEnumerable<NamespaceMetadataDatabaseDTO>)base.Namespaces; set => base.Namespaces = value; }
        public List<NamespaceMetadataDatabaseDTO> NamespacesEF { get; set; } = new List<NamespaceMetadataDatabaseDTO>();
        public AssemblyMetadataDatabaseDTO(BaseAssemblyMetadata assemblyMetadataDTO)
        {
            Mapper.DatabaseDTOTypeDictionary.Clear();
            Name = "";
            Name = assemblyMetadataDTO.Name;
            if (assemblyMetadataDTO.Namespaces != null)
            {
                Namespaces = from BaseNamespaceMetadata _namespace in assemblyMetadataDTO.Namespaces
                             select new NamespaceMetadataDatabaseDTO(_namespace);
                if(Namespaces != null) NamespacesEF = Namespaces.ToList();
            }
            NamespacesEF = Namespaces.ToList();
            foreach(NamespaceMetadataDatabaseDTO @namespace in NamespacesEF)
            {
                @namespace.ToEntityFramework();
            }
        }

        public void RepopulateAssembly()
        {
            Mapper.RepopulatedTypesDictionary.Clear();
            Namespaces = NamespacesEF;
            if(Namespaces != null)
            {
                foreach(NamespaceMetadataDatabaseDTO namespaceMetadata in Namespaces)
                {
                    namespaceMetadata.RepopulateNamespace();
                }
            }
        }

        public AssemblyMetadataDatabaseDTO() { }
    }
}
