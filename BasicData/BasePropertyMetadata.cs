using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasicData
{
    public abstract class BasePropertyMetadata
    {
        public virtual string Name { get; set; }
        public virtual BaseTypeMetadata UsedTypeMetadata { get; set; }
    }
}
