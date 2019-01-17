using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [DataContract(IsReference = true)]
    public class TypeMetadataDTO : BaseTypeMetadata
    {
        public static Dictionary<string, TypeMetadataDTO> DTOTypeDictionary = new Dictionary<string, TypeMetadataDTO>();

        [DataMember]
        public override string TypeName { get => base.TypeName; set => base.TypeName = value; }
        [DataMember]
        public override string NamespaceName { get => base.NamespaceName; set => base.NamespaceName = value; }
        [DataMember]
        public override BaseTypeMetadata BaseType { get => base.BaseType; set => base.BaseType = value; }
        [DataMember]
        public override IEnumerable<BaseTypeMetadata> GenericArguments { get => base.GenericArguments; set => base.GenericArguments = value; }
        [DataMember]
        public override AccessLevel AccessLevel { get => base.AccessLevel; set => base.AccessLevel = value; }
        [DataMember]
        public override AbstractEnum AbstractEnum { get => base.AbstractEnum; set => base.AbstractEnum = value; }
        [DataMember]
        public override SealedEnum SealedEnum { get => base.SealedEnum; set => base.SealedEnum = value; }
        [DataMember]
        public override TypeKind TypeKind { get => base.TypeKind; set => base.TypeKind = value; }
         [DataMember]
        public override IEnumerable<BaseTypeMetadata> ImplementedInterfaces { get => base.ImplementedInterfaces; set => base.ImplementedInterfaces = value; }
        [DataMember]
        public override IEnumerable<BaseTypeMetadata> NestedTypes { get => base.NestedTypes; set => base.NestedTypes = value; }
        [DataMember]
        public override IEnumerable<BasePropertyMetadata> Properties { get => base.Properties; set => base.Properties = value; }
        [DataMember]
        public override BaseTypeMetadata DeclaringType { get => base.DeclaringType; set => base.DeclaringType = value; }
        [DataMember]
        public override IEnumerable<BaseMethodMetadata> Methods { get => base.Methods; set => base.Methods = value; }
        [DataMember]
        public override IEnumerable<BaseMethodMetadata> Constructors { get => base.Constructors; set => base.Constructors = value; }
    }
}
