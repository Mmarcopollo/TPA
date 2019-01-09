using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    [DataContract(IsReference = true)]
    public abstract class BasePropertyMetadata
    {
        [DataMember]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual BaseTypeMetadata UsedTypeMetadata { get; set; }
    }
}
