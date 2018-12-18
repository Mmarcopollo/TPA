using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract(IsReference = true)]
    public class MethodMetadataDTO
    {
        [DataMember]
        public string m_Name;
        [DataMember]
        public IEnumerable<TypeMetadataDTO> m_GenericArguments;
        [DataMember]
        public AccessLevel AccessLevel;
        [DataMember]
        public AbstractEnum AbstractEnum;
        [DataMember]
        public StaticEnum StaticEnum;
        [DataMember]
        public VirtualEnum VirtualEnum;
        [DataMember]
        public TypeMetadataDTO m_ReturnType;
        [DataMember]
        public bool m_Extension;
        [DataMember]
        public IEnumerable<ParameterMetadataDTO> m_Parameters;
    }
}
