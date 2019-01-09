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
    }
}
