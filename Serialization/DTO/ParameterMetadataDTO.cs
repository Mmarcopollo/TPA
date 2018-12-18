using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract(IsReference = true)]
    public class ParameterMetadataDTO
    {
        [DataMember]
        public string m_Name;
        [DataMember]
        public TypeMetadataDTO m_TypeMetadata;
    }
}
