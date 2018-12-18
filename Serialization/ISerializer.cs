using System.IO;

namespace Serialization
{
    public interface ISerializer
    {
        void Write<T>(T obj, string filePath);
        T Read<T>(string filePath);
    }
}
