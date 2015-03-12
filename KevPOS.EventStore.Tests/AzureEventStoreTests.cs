using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;

namespace KevPOS.EventStore.Tests
{
    [TestFixture]
    public class AzureEventStoreTests
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            var rnd = new Random();
            int number = rnd.Next(999999999);
            _tableReference = string.Format("EventStoreForUnitTesting{0}", number);
            _storageAccount = CloudStorageAccount.Parse(
                                ConfigurationManager.ConnectionStrings["QAStorageConnectionString"].ConnectionString);
            _eventDatabase = new AzureEventDatabase(_storageAccount, _tableReference);
            _eventStore = new EventStore(_eventDatabase);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(_tableReference);
            table.DeleteIfExists();
        }

        private IEventDatabase _eventDatabase;
        private EventStore _eventStore;
        private CloudStorageAccount _storageAccount;
        private string _tableReference;

        [Test]
        public void can_fetch()
        {
            var created = DateTime.Now;
            var eventData = new EventData(1, "some json", "SomeType2", created, 0);
             _eventStore.Write(eventData);
            var events =  _eventStore.Read(1);
            var list = events.ToList();
            list.Should().Contain(x => x.AggregateId == eventData.AggregateId);
            list.Should().Contain(x => x.Data == "some json");
            list.Should().Contain(x => x.Type == "SomeType2");
            list.Should().Contain(x => x.Version == 0);
        }

        [Test]
        public void can_store_event()
        {
            var eventData = new EventData(1, "some json", "SomeType2", DateTime.Now, 0);
             _eventStore.Write(eventData);
        }

        [Test]
        public void can_store_more_than_one_event_for_aggregate_id()
        {
            var eventData = new EventData(1, "some json", "SomeType", DateTime.Now, 0);
             _eventStore.Write(eventData);
            eventData = new EventData(1, "some json", "SomeType", DateTime.Now, 0);
             _eventStore.Write(eventData);
            eventData = new EventData(1, "some json", "SomeType", DateTime.Now, 0);
             _eventStore.Write(eventData); 
            eventData = new EventData(1, "some json", "SomeType", DateTime.Now, 0);
             _eventStore.Write(eventData); 
            eventData = new EventData(1, "some json", "SomeType", DateTime.Now, 0);
             _eventStore.Write(eventData);
        }
    }
}