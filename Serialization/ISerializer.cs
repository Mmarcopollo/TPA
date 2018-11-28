using Model;
using System.IO;

namespace Serialization
{
    public interface ISerializer
    {
        void Write(AssemblyMetadata obj, string filePath);
        AssemblyMetadata Read(string filePath);
    }
}
