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
    public class AssemblyMetadataDTO
    {
        [DataMember]
        public string m_Name;
        [DataMember]
        public IEnumerable<NamespaceMetadataDTO> m_Namespaces;
    }
}
