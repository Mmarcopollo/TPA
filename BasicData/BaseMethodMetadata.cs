using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    [DataContract(IsReference = true)]
    public abstract class BaseMethodMetadata
    {
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseTypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public virtual AccessLevel AccessLevel { get; set; }
        [DataMember]
        public virtual AbstractEnum AbstractEnum { get; set; }
        [DataMember]
        public virtual StaticEnum StaticEnum { get; set; }
        [DataMember]
        public virtual VirtualEnum VirtualEnum { get; set; }
        [DataMember]
        public virtual BaseTypeMetadata ReturnType { get; set; }
        [DataMember]
        public virtual bool Extension { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseParameterMetadata> Parameters { get; set; }
    }
}
