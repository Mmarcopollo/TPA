using System.IO;

namespace BasicData
{
    public interface ISerializer
    {
        void Write(BaseAssemblyMetadata obj, string filePath);
        BaseAssemblyMetadata Read(string filePath);
    }
}
