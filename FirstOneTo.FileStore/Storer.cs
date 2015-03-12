using System;
using System.IO;
using Nancy;

namespace FirstOneTo.FileStore
{
    public class Storer : IStorer
    {
        public string Store(HttpFile file)
        {
            //string filename = Path.Combine(storagePath, file.Name);

            //using (var fileStream = new FileStream(filename, FileMode.Create))
            //{
            //    file.Value.CopyTo(fileStream);
            //}
            throw new NotImplementedException();
        }

        public string Store(string fileNameIncludingExtension, Stream fileStream)
        {
            throw new NotImplementedException();
        }
    }
}