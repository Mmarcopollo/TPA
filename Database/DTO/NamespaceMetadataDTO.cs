using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.DTO
{
    [Table("NamespaceMetadata")]
    public class NamespaceMetadataDTO : BaseNamespaceMetadata
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public override string NamespaceName { get; set; }
        public override Guid Guid { get; set; }
        public new List<TypeMetadataDTO> Types { get; set; }
    }
}