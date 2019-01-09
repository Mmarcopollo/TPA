using System.IO;

namespace Serialization
{
    public interface ISerializer
    {
        void Write(AssemblyMetadataDTO obj, string filePath);
        AssemblyMetadataDTO Read(string filePath);
    }
}
