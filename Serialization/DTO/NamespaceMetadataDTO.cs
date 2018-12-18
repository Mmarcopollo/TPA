using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract(IsReference = true)]
    public class NamespaceMetadataDTO
    {
        [DataMember]
        public string m_NamespaceName;
        [DataMember]
        public IEnumerable<TypeMetadataDTO> m_Types;
    }
}
