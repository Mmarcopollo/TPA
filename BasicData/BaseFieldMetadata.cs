using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public abstract class BaseFieldMetadata
    {
        public virtual Guid Guid { get; set; }
        public virtual string FieldName { get; set; }
        public virtual bool IsReadOnly { get; set; }
        public virtual BaseTypeMetadata FieldType { get; set; }
        public virtual Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public virtual IEnumerable<BaseTypeMetadata> Attributes { get; set; }
    }
}
