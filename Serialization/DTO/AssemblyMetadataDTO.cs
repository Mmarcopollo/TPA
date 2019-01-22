using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [Export(typeof(BaseAssemblyMetadata))]
    [DataContract]
    public class AssemblyMetadataDTO : BaseAssemblyMetadata
    {
        [DataMember]
        public override string Name { get => base.Name; set => base.Name = value; }
        [DataMember]
        public override IEnumerable<BaseNamespaceMetadata> Namespaces { get => base.Namespaces; set => base.Namespaces = value; }

        public AssemblyMetadataDTO(BaseAssemblyMetadata assemblyMetadataDTO)
        {
            Name = assemblyMetadataDTO.Name;
            if (assemblyMetadataDTO.Namespaces != null)
            {
                List<NamespaceMetadataDTO> namespaces = new List<NamespaceMetadataDTO>();
                foreach (BaseNamespaceMetadata DTO in assemblyMetadataDTO.Namespaces)
                {
                    NamespaceMetadataDTO methodMetadata = new NamespaceMetadataDTO(DTO);
                    namespaces.Add(methodMetadata);
                }
                Namespaces = namespaces;
            }
        }
    }
}
