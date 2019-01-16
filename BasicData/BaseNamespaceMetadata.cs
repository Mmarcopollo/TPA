using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    [DataContract(IsReference = true)]
    public abstract class BaseNamespaceMetadata
    {
        [DataMember]
        public virtual string NamespaceName { get; set; }
        [DataMember]
        public virtual Guid Guid { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseTypeMetadata> Types { get; set; }
    }
}
