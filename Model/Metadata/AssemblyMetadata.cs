using BasicData;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AssemblyMetadata : BaseAssemblyMetadata
    {

        public override Guid Guid { get => base.Guid; set => base.Guid = value; }
        public override string Name { get => base.Name; set => base.Name = value; }
        public new IEnumerable<NamespaceMetadata> Namespaces { get => (IEnumerable<NamespaceMetadata>)base.Namespaces; set => base.Namespaces = value; }

        public AssemblyMetadata(Assembly assembly)
        {
            Guid = Guid.NewGuid();
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                           group _type by _type.GetNamespace() into _group
                           orderby _group.Key
                           select new NamespaceMetadata(_group.Key, _group);
        }

        public AssemblyMetadata(BaseAssemblyMetadata assemblyMetadataDTO)
        {
            Name = assemblyMetadataDTO.Name;
            if(assemblyMetadataDTO.Namespaces != null)
            {
                List<NamespaceMetadata> namespaces = new List<NamespaceMetadata>();
                foreach (BaseNamespaceMetadata DTO in assemblyMetadataDTO.Namespaces)
                {
                    NamespaceMetadata methodMetadata = new NamespaceMetadata(DTO);
                    namespaces.Add(methodMetadata);
                }
                Namespaces = namespaces;
            }
        }

        public override bool Equals(object obj)
        {
            AssemblyMetadata metadata = obj as AssemblyMetadata;
            return metadata != null &&
                   Name == metadata.Name &&
                   Namespaces.SequenceEqual(metadata.Namespaces);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
