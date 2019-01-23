using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO
{
    class Mapper
    {
        public static Dictionary<string, TypeMetadataDatabaseDTO> DatabaseDTOTypeDictionary = new Dictionary<string, TypeMetadataDatabaseDTO>();
        public static Dictionary<string, NamespaceMetadataDatabaseDTO> DatabaseDTONamespaceDictionary = new Dictionary<string, NamespaceMetadataDatabaseDTO>();
        public static Dictionary<string, ParameterMetadataDatabaseDTO> DatabaseDTOParameterDictionary = new Dictionary<string, ParameterMetadataDatabaseDTO>();
        public static Dictionary<string, PropertyMetadataDatabaseDTO> DatabaseDTOPropertyDictionary = new Dictionary<string, PropertyMetadataDatabaseDTO>();
        public static Dictionary<string, MethodMetadataDatabaseDTO> DatabaseDTOMethodDictionary = new Dictionary<string, MethodMetadataDatabaseDTO>();
        public static Dictionary<string, FieldMetadataDatabaseDTO> DatabaseDTOFieldDictionary = new Dictionary<string, FieldMetadataDatabaseDTO>();
    }
}
