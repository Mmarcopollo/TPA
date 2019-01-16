using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    [DataContract]
    public abstract class BaseAssemblyMetadata
    {
        [DataMember]
        public virtual Guid Guid { get; set; }
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseNamespaceMetadata> Namespaces { get; set; }
    }
}
