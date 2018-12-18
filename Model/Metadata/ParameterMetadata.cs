using Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ParameterMetadata
    {
        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;
        }

        public ParameterMetadata(ParameterMetadataDTO parameterMetadataDTO)
        {
            m_Name = parameterMetadataDTO.m_Name;
            if (parameterMetadataDTO.m_TypeMetadata != null)
            {
                if (TypeMetadata.TypeDictionary.ContainsKey(parameterMetadataDTO.m_TypeMetadata.m_typeName)) m_TypeMetadata = TypeMetadata.TypeDictionary[parameterMetadataDTO.m_TypeMetadata.m_typeName];
                else m_TypeMetadata = new TypeMetadata(parameterMetadataDTO.m_TypeMetadata);
            }
        }

        //private vars
        public string m_Name;
        public TypeMetadata m_TypeMetadata;
        public string Name { get => m_Name; set => m_Name = value; }
    }
}
