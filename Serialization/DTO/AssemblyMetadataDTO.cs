using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract]
    public class AssemblyMetadataDTO : BaseAssemblyMetadata
    {
        [DataMember]
        public override string Name { get => base.Name; set => base.Name = value; }
        [DataMember]
        public override IEnumerable<BaseNamespaceMetadata> Namespaces { get => base.Namespaces; set => base.Namespaces = value; }
    }
}
