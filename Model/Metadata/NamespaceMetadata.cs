using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class NamespaceMetadata : BaseNamespaceMetadata
    {

        public override string NamespaceName { get => base.NamespaceName; set => base.NamespaceName = value; }
        public override Guid Guid { get => base.Guid; set => base.Guid = value; }
        public new IEnumerable<TypeMetadata> Types { get => (IEnumerable<TypeMetadata>)base.Types; set => base.Types = value; }

        internal NamespaceMetadata(string name, IEnumerable<Type> types)
        {
            Guid = Guid.NewGuid();
            NamespaceName = name;
            Types = from type in types orderby type.Name select new TypeMetadata(type);

        }

        public NamespaceMetadata(BaseNamespaceMetadata namespaceMetadataDTO)
        {
            NamespaceName = namespaceMetadataDTO.NamespaceName;
            Guid = namespaceMetadataDTO.Guid;
            if (namespaceMetadataDTO.Types != null)
            {
                List<TypeMetadata> types = new List<TypeMetadata>();
                foreach (BaseTypeMetadata DTO in namespaceMetadataDTO.Types)
                {
                    TypeMetadata metadata;
                    if (TypeMetadata.TypeDictionary.ContainsKey(DTO.TypeName)) metadata = TypeMetadata.TypeDictionary[DTO.TypeName];
                    else metadata = new TypeMetadata(DTO);
                    types.Add(metadata);
                }
                Types = types;
            }
        }

        public override bool Equals(object obj)
        {
            var metadata = obj as NamespaceMetadata;
            return metadata != null &&
                   NamespaceName == metadata.NamespaceName &&
                   Types.SequenceEqual(metadata.Types);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
