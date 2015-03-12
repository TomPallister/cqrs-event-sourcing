using System.IO;
using Nancy;

namespace FirstOneTo.FileStore
{
    public interface IStorer
    {
        string Store(HttpFile file);
        string Store(string fileNameIncludingExtension, Stream fileStream);
    }
}