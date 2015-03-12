using System;
using System.Configuration;
using System.Threading.Tasks;
using FistOneTo.Bootstrapper;
using Microsoft.WindowsAzure.Storage;
using Nancy.TinyIoc;
using NUnit.Framework;

namespace FirstOneTo.Helpers.Tests
{
    [TestFixture]
    public class BootstrapperTests
    {
        private string _azureTablePrefix;
        private CloudStorageAccount _cloudStorageAccount;
        [SetUp]
        public void SetUp()
        {
            var tinyIoCContainer = new TinyIoCContainer();
            _azureTablePrefix = "RouteTests";
            _cloudStorageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var rnd = new Random();
            int number = rnd.Next(999999999);
            BootstrappedObjects bootstrappedObjects = ApiBootstrapper.AzureBootstrapper(tinyIoCContainer, _cloudStorageAccount,
                string.Format("{0}{1}", _azureTablePrefix, number), true);
        }

        [TearDown]
        public void TearDown()
        {
            ApiBootstrapper.TearDownOldTestTables(_azureTablePrefix, _cloudStorageAccount);       
        }


        [Test]
        public void CanTearDownOldAzure()
        {
            var tinyIoCContainer = new TinyIoCContainer();
            const string azureTablePrefix = "RouteTests";
            _cloudStorageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var rnd = new Random();
            int number = rnd.Next(999999999);
            BootstrappedObjects bootstrappedObjects = ApiBootstrapper.AzureBootstrapper(tinyIoCContainer, _cloudStorageAccount,
                string.Format("{0}{1}", azureTablePrefix, number), true);
            ApiBootstrapper.TearDownOldTestTables(_azureTablePrefix, _cloudStorageAccount);       
        }
    }
}