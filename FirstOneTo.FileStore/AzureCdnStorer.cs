using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Nancy;

namespace FirstOneTo.FileStore
{
    public class AzureCdnStorer : IStorer
    {
        private readonly CloudBlobContainer _cloudBlobContainer;
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly string _containerName;

        public AzureCdnStorer(CloudStorageAccount storageAccount, string containerName)
        {
            _cloudStorageAccount = storageAccount;
            _containerName = containerName;
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            _cloudBlobContainer = cloudBlobClient.GetContainerReference(_containerName);
            _cloudBlobContainer.CreateIfNotExists();
        }

        /// <summary>
        ///     will store nancy file in azure
        /// </summary>
        /// <param name="file">File from nancy this could easily be changed to a straight stream</param>
        public string Store(HttpFile file)
        {
            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(file.Name);
            blob.UploadFromStream(file.Value);
            return string.Format("{0}{1}/{2}", _cloudStorageAccount.BlobStorageUri.PrimaryUri.AbsoluteUri,
                _containerName, file.Name);
        }

        /// <summary>
        ///     Stores filestream in azure
        /// </summary>
        /// <param name="fileNameIncludingExtension"></param>
        /// <param name="fileStream"></param>
        public string Store(string fileNameIncludingExtension, Stream fileStream)
        {
            CloudBlockBlob blob = _cloudBlobContainer.GetBlockBlobReference(fileNameIncludingExtension);
            blob.UploadFromStream(fileStream);

            string azureUrl = _cloudStorageAccount.BlobStorageUri.PrimaryUri.AbsoluteUri;

            if (CheckIfStringEndsWithForwardSlash(azureUrl))
            {
                return string.Format("{0}{1}/{2}", azureUrl, _containerName, fileNameIncludingExtension);
            }
            return string.Format("{0}/{1}/{2}", azureUrl, _containerName, fileNameIncludingExtension);
        }

        private bool CheckIfStringEndsWithForwardSlash(string myString)
        {
            if (myString.EndsWith("/"))
            {
                return true;
            }
            return false;
        }
    }
}