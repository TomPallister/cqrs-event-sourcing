using System.Configuration;
using System.IO;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using NUnit.Framework;

namespace FirstOneTo.FileStore.Tests
{
    [TestFixture]
    public class StorageTests
    {
        [Test]
        public void can_store_image()
        {
            var storageAccount = CloudStorageAccount.Parse(
                                   ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var azureCdnStorer = new AzureCdnStorer(storageAccount, "images");
            var fileStream = new FileStream(@"Chrysanthemum.jpg", FileMode.Open);
            var result = azureCdnStorer.Store("Chrysanthemum.jpg", fileStream);
            result.Should().Be("http://127.0.0.1:10000/devstoreaccount1/images/Chrysanthemum.jpg");
        }
    }
}
