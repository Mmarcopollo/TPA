using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public abstract class BaseMethodMetadata
    {
        public virtual string Name { get; set; }
        public virtual IEnumerable<BaseTypeMetadata> GenericArguments { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual AbstractEnum AbstractEnum { get; set; }
        public virtual StaticEnum StaticEnum { get; set; }
        public virtual VirtualEnum VirtualEnum { get; set; }
        public virtual BaseTypeMetadata ReturnType { get; set; }
        public virtual bool Extension { get; set; }
        public virtual IEnumerable<BaseParameterMetadata> Parameters { get; set; }
    }
}
