using System;
using System.Configuration;
using FirstOneTo.ReadModel.Infrastructure.Azure;
using FirstOneTo.Readmodel.Models;
using FluentAssertions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;

namespace FirstOneTo.ReadModel.Infrastructure.Tests
{
    [TestFixture]
    public class AzureExampleDatabaseTests
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            var rnd = new Random();
            int number = rnd.Next(999999999);
            _tableReference = string.Format("DatabaseForUnitTesting{0}", number);
            _storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
            _database = new AzureExampleDatabase(_storageAccount, _tableReference);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(_tableReference);
            table.DeleteIfExists();
        }

        private AzureExampleDatabase _database;
        private CloudStorageAccount _storageAccount;
        private string _tableReference;

        [Test]
        public void can_get_example()
        {
            _database.StoreExample(new ExampleModel(1, "description"));
            ExampleModel acceptedChallenge = _database.GetExample(1);
            acceptedChallenge.AggregateId.Should().Be(1);
            acceptedChallenge.Description.Should().Be("description");
        }

        [Test]
        public void can_store_example()
        {
            _database.StoreExample(new ExampleModel(1, "description"));
            ExampleModel acceptedChallenge = _database.GetExample(1);
            acceptedChallenge.AggregateId.Should().Be(1);
            acceptedChallenge.Description.Should().Be("description");
        }
    }
}