using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{ 
    public abstract class BaseAssemblyMetadata
    {
        public virtual Guid Guid { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<BaseNamespaceMetadata> Namespaces { get; set; }
    }
}
