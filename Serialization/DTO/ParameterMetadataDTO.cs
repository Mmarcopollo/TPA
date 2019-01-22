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
    public class ParameterMetadataDTO : BaseParameterMetadata
    {
        [DataMember]
        public override string Name { get => base.Name; set => base.Name = value; }
        [DataMember]
        public override BaseTypeMetadata UsedTypeMetadata { get => base.UsedTypeMetadata; set => base.UsedTypeMetadata = value; }

        public ParameterMetadataDTO(BaseParameterMetadata parameterMetadataDTO)
        {
            Name = parameterMetadataDTO.Name;
            if (parameterMetadataDTO.UsedTypeMetadata != null)
            {
                if (TypeMetadataDTO.DTOTypeDictionary.ContainsKey(parameterMetadataDTO.UsedTypeMetadata.TypeName)) UsedTypeMetadata = TypeMetadataDTO.DTOTypeDictionary[parameterMetadataDTO.UsedTypeMetadata.TypeName];
                else UsedTypeMetadata = new TypeMetadataDTO(parameterMetadataDTO.UsedTypeMetadata);
            }
        }
    }
}
