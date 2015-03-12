using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using KevPOS.TypeExtensions.Infrastructure;
using NUnit.Framework;

namespace KevPOS.EventStore.Tests
{
    [TestFixture]
    public class EventStoreTests
    {
        [SetUp]
        public void TestInitialise()
        {
            _eventDatabase = new InMemoryEventDatabase(new List<EventData>());
            _eventStore = new EventStore(_eventDatabase);
        }

        private InMemoryEventDatabase _eventDatabase;
        private EventStore _eventStore;

        [Test]
        public void events_for_AggregateId_are_read_in_fifo_order()
        {
            const int aggregateId = 42;
            IEnumerable<int> range = Enumerable.Range(1, 5).Reverse();
            range.Each(day => _eventDatabase.EventStore.Add(new EventData(aggregateId, "",
                "".GetType().FullName, new DateTime(2000, 01, day), 1)));
            IEnumerable<EventData> events =  _eventStore.Read(aggregateId);
            int expectedDay = 1;
            events.Each(e =>
            {
                e.Created.Day.Should().Be(expectedDay);
                expectedDay++;
            });
        }

        [Test]
        public void reads_events_for_an_AggregateId()
        {
            const int aggregateId = 42;
            3.Times(
                () =>
                    _eventDatabase.EventStore.Add(new EventData(aggregateId, "", "".GetType().FullName, DateTime.Now, 1)));
            3.Times(() => _eventDatabase.EventStore.Add(new EventData(666, "", "".GetType().FullName, DateTime.Now, 1)));
            IEnumerable<EventData> events =  _eventStore.Read(aggregateId);
            events.Count().Should().Be(3);
        }

        [Test]
        public void writes_event_data()
        {
             _eventStore.Write(new EventData(48, "", "".GetType().FullName, DateTime.Now, 1));
            _eventDatabase.EventStore.Count.Should().Be(3);
        }
    }
}