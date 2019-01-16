using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public abstract class BaseTypeMetadata
    {
        public virtual string TypeName { get; set; }
        public virtual string NamespaceName { get; set; }
        public virtual BaseTypeMetadata BaseType { get; set; }
        public virtual IEnumerable<BaseTypeMetadata> GenericArguments { get; set; }
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual AbstractEnum AbstractEnum { get; set; }
        public virtual SealedEnum SealedEnum { get; set; }
        public virtual TypeKind TypeKind { get; set; }
        public virtual IEnumerable<Attribute> Attributes { get; set; }
        public virtual IEnumerable<BaseTypeMetadata> ImplementedInterfaces { get; set; }
        public virtual IEnumerable<BaseTypeMetadata> NestedTypes { get; set; }
        public virtual IEnumerable<BaseFieldMetadata> Fields { get; set; }
        public virtual IEnumerable<BasePropertyMetadata> Properties { get; set; }
        public virtual BaseTypeMetadata DeclaringType { get; set; }
        public virtual IEnumerable<BaseMethodMetadata> Methods { get; set; }
        public virtual IEnumerable<BaseMethodMetadata> Constructors { get; set; }
    }
}
