using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    [DataContract(IsReference = true)]
    public abstract class BaseFieldMetadata
    {
        [DataMember]
        public virtual Guid Guid { get; set; }
        [DataMember]
        public virtual string FieldName { get; set; }
        [DataMember]
        public virtual bool IsReadOnly { get; set; }
        [DataMember]
        public virtual BaseTypeMetadata FieldType { get; set; }
        [DataMember]
        public virtual Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseTypeMetadata> Attributes { get; set; }

    }
}
