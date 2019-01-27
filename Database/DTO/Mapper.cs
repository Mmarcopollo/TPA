using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DTO
{
    internal class Mapper
    {
        public static Dictionary<string, TypeMetadataDatabaseDTO> DatabaseDTOTypeDictionary = new Dictionary<string, TypeMetadataDatabaseDTO>();
        public static Dictionary<string, TypeMetadataDatabaseDTO> RepopulatedTypesDictionary = new Dictionary<string, TypeMetadataDatabaseDTO>();
    }
}
