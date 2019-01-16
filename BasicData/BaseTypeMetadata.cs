using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    [DataContract(IsReference = true)]
    public abstract class BaseTypeMetadata
    {
        [DataMember]
        public virtual string TypeName { get; set; }
        [DataMember]
        public virtual string NamespaceName { get; set; }
        [DataMember]
        public virtual BaseTypeMetadata BaseType { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseTypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public virtual AccessLevel AccessLevel { get; set; }
        [DataMember]
        public virtual AbstractEnum AbstractEnum { get; set; }
        [DataMember]
        public virtual SealedEnum SealedEnum { get; set; }
        [DataMember]
        public virtual TypeKind TypeKind { get; set; }
        [DataMember]
        public virtual IEnumerable<Attribute> Attributes { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseTypeMetadata> ImplementedInterfaces { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseTypeMetadata> NestedTypes { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseFieldMetadata> Fields { get; set; }
        [DataMember]
        public virtual IEnumerable<BasePropertyMetadata> Properties { get; set; }
        [DataMember]
        public virtual BaseTypeMetadata DeclaringType { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseMethodMetadata> Methods { get; set; }
        [DataMember]
        public virtual IEnumerable<BaseMethodMetadata> Constructors { get; set; }
    }
}
