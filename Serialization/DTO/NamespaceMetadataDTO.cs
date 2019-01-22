using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadataDTO : BaseNamespaceMetadata
    {
        [DataMember]
        public override string NamespaceName { get => base.NamespaceName; set => base.NamespaceName = value; }
        [DataMember]
        public override Guid Guid { get => base.Guid; set => base.Guid = value; }
        [DataMember]
        public override IEnumerable<BaseTypeMetadata> Types { get => base.Types; set => base.Types = value; }

        public NamespaceMetadataDTO(BaseNamespaceMetadata namespaceMetadataDTO)
        {
            NamespaceName = namespaceMetadataDTO.NamespaceName;
            Guid = namespaceMetadataDTO.Guid;
            if (namespaceMetadataDTO.Types != null)
            {
                List<TypeMetadataDTO> types = new List<TypeMetadataDTO>();
                foreach (BaseTypeMetadata DTO in namespaceMetadataDTO.Types)
                {
                    TypeMetadataDTO metadata;
                    if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadataDTO.DTOTypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadataDTO(DTO);
                    types.Add(metadata);
                }
                Types = types;
            }
        }
    }
}
