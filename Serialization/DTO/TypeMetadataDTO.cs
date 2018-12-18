using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract(IsReference = true)]
    public class TypeMetadataDTO
    { 
        [DataMember]
        public string m_typeName;
        [DataMember]
        public string m_NamespaceName;
        [DataMember]
        public TypeMetadataDTO m_BaseType;
        [DataMember]
        public IEnumerable<TypeMetadataDTO> m_GenericArguments;
        [DataMember]
        public AccessLevel AccessLevel { get; set; }
        [DataMember]
        public AbstractEnum AbstractEnum { get; set; }
        [DataMember]
        public SealedEnum SealedEnum { get; set; }
        [DataMember]
        public TypeKind m_TypeKind;
        [DataMember]
        public IEnumerable<Attribute> m_Attributes;
        [DataMember]
        public IEnumerable<TypeMetadataDTO> m_ImplementedInterfaces;
        [DataMember]
        public IEnumerable<TypeMetadataDTO> m_NestedTypes;
        [DataMember]
        public IEnumerable<PropertyMetadataDTO> m_Properties;
        [DataMember]
        public TypeMetadataDTO m_DeclaringType;
        [DataMember]
        public IEnumerable<MethodMetadataDTO> m_Methods;
        [DataMember]
        public IEnumerable<MethodMetadataDTO> m_Constructors;
    }
}
