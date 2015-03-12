using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FirstOneTo.Commands;
using FirstOneTo.Domain.Aggregates;
using FirstOneTo.Events;
using FluentAssertions;
using KevPOS.EventRepository;
using KevPOS.EventStore;
using KevPOS.Serialisation.Infrasturcture;
using NUnit.Framework;

namespace FirstOneTo.Domain.Tests
{
    [TestFixture]
    public class ExampleTests
    {
        [SetUp]
        public void SetUp()
        {
            _db = new InMemoryEventDatabase(new List<EventData>());
            _resolver = new MessageResolver(Assembly.GetAssembly(typeof (ExampleCreated)));
            _eventRepository = new EventRepository(new EventStore(_db), _resolver);
        }

        [TearDown]
        public void TearDown()
        {
        }

        private InMemoryEventDatabase _db;
        private EventRepository _eventRepository;
        private IMessageResolver _resolver;

        [Test]
        public void can_create_challenge()
        {
            var createExampleCommand = new CreateExample
            {
                Description = "something",
                AggregateId = 1
            };
            var challengeOne = new ExampleAggregate(createExampleCommand);
            _eventRepository.Store(challengeOne);
            IEnumerable<EventData> events = _eventRepository.EventStore.Read(createExampleCommand.AggregateId);
            EventData lastEvent = events.Last();
            Type type = _resolver.TypeForName(lastEvent.Type);
            var storedEvent = (ExampleCreated) JsonSerialiser.Deserialise(lastEvent.Data, type);
            storedEvent.Should().BeOfType<ExampleCreated>();
        }
    }
}