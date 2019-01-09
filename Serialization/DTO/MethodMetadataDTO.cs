using BasicData;
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
    public class MethodMetadataDTO : BaseMethodMetadata
    {
        [DataMember]
        public override string Name { get => base.Name; set => base.Name = value; }
        [DataMember]
        public override IEnumerable<BaseTypeMetadata> GenericArguments { get => base.GenericArguments; set => base.GenericArguments = value; }
        [DataMember]
        public override AccessLevel AccessLevel { get => base.AccessLevel; set => base.AccessLevel = value; }
        [DataMember]
        public override AbstractEnum AbstractEnum { get => base.AbstractEnum; set => base.AbstractEnum = value; }
        [DataMember]
        public override StaticEnum StaticEnum { get => base.StaticEnum; set => base.StaticEnum = value; }
        [DataMember]
        public override VirtualEnum VirtualEnum { get => base.VirtualEnum; set => base.VirtualEnum = value; }
        [DataMember]
        public override BaseTypeMetadata ReturnType { get => base.ReturnType; set => base.ReturnType = value; }
        [DataMember]
        public override bool Extension { get => base.Extension; set => base.Extension = value; }
        [DataMember]
        public override IEnumerable<BaseParameterMetadata> Parameters { get => base.Parameters; set => base.Parameters = value; }
    }
}
