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
        public int Id { get; set; }
        [Required, StringLength(100)]
        public override string Name { get; set; }
        public new List<NamespaceMetadataDatabaseDTO> Namespaces { get; set; }

    }
}
