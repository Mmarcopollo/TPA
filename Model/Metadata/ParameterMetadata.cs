using BasicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ParameterMetadata : BaseParameterMetadata
    {
        public new string Name { get => base.Name; set => base.Name = value; }
        public new TypeMetadata UsedTypeMetadata { get => (TypeMetadata)base.UsedTypeMetadata; set => base.UsedTypeMetadata = value; }

        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.Name = name;
            this.UsedTypeMetadata = typeMetadata;

            if (!TypeMetadata.TypeDictionary.ContainsKey(typeMetadata.TypeName))
            {
                TypeMetadata.TypeDictionary.Add(typeMetadata.TypeName, typeMetadata);
            }
        }

        public ParameterMetadata(BaseParameterMetadata parameterMetadataDTO)
        {
            Name = parameterMetadataDTO.Name;
            if (parameterMetadataDTO.UsedTypeMetadata != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(parameterMetadataDTO.UsedTypeMetadata.TypeName)) UsedTypeMetadata = TypeMetadata.TypeDictionary[parameterMetadataDTO.UsedTypeMetadata.TypeName];
                else UsedTypeMetadata = new TypeMetadata(parameterMetadataDTO.UsedTypeMetadata);
            }
        }
    }
}
