using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public abstract class BaseNamespaceMetadata
    {
        public virtual string NamespaceName { get; set; }
        public virtual Guid Guid { get; set; }
        public virtual IEnumerable<BaseTypeMetadata> Types { get; set; }
    }
}
