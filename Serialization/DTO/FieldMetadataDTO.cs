using BasicData;
using DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.DTO
{
    [DataContract(IsReference = true)]
    public class FieldMetadataDTO : BaseFieldMetadata
    {
        [DataMember]
        public override Guid Guid { get => base.Guid; set => base.Guid = value; }

        [DataMember(Name = "FieldName")]
        public override string FieldName { get => base.FieldName; set => base.FieldName = value; }

        [DataMember(Name = "FieldIsReadOnly")]
        public override bool IsReadOnly { get => base.IsReadOnly; set => base.IsReadOnly = value; }

        [DataMember(Name = "FieldType")]
        public override BaseTypeMetadata FieldType { get => base.FieldType; set => base.FieldType = value; }

        [DataMember(Name = "FieldModifiers")]
        public override Tuple<AccessLevel, StaticEnum> Modifiers { get => base.Modifiers; set => base.Modifiers = value; }

        [DataMember(Name = "FieldAttributes")]
        public override IEnumerable<BaseTypeMetadata> Attributes { get => base.Attributes; set => base.Attributes = value; }
    }
}
