using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        public ParameterMetadata(string name, TypeMetadata typeMetadata)
        {
            this.m_Name = name;
            this.m_TypeMetadata = typeMetadata;

            if (!TypeMetadata.TypeDictionary.ContainsKey(typeMetadata.m_typeName))
            {
                TypeMetadata.TypeDictionary.Add(typeMetadata.m_typeName, typeMetadata);
            }
        }

        //private vars
        [DataMember]
        public string m_Name;
        [DataMember]
        public TypeMetadata m_TypeMetadata;
        public string Name { get => m_Name; set => m_Name = value; }
    }
}
