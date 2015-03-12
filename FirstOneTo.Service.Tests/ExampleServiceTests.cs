using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FirstOneTo.Commands;
using FirstOneTo.Domain.Service.Concrete;
using FirstOneTo.Domain.Service.Interface;
using FirstOneTo.Events;
using FluentAssertions;
using KevPOS.EventRepository;
using KevPOS.EventStore;
using KevPOS.Serialisation.Infrasturcture;
using NUnit.Framework;

namespace FirstOneTo.Domain.Service.Tests
{
    [TestFixture]
    public class ExampleServiceTests
    {
        [SetUp]
        public void SetUp()
        {
            _db = new InMemoryEventDatabase(new List<EventData>());
            _resolver = new MessageResolver(Assembly.GetAssembly(typeof (ExampleCreated)));
            _eventRepository = new EventRepository(new EventStore(_db), _resolver);
            _exampleService = new ExampleService(_eventRepository);
        }

        private InMemoryEventDatabase _db;
        private EventRepository _eventRepository;
        private IMessageResolver _resolver;
        private IExampleService _exampleService;


        [Test]
        public void can_create_example()
        {
            var createExampleCommand = new CreateExample
            {
                AggregateId = 1,
                Description = "something"
            };
            _exampleService.CreateExample(createExampleCommand);
            IEnumerable<EventData> events = _eventRepository.EventStore.Read(createExampleCommand.AggregateId);
            EventData lastEvent = events.Last();
            Type type = _resolver.TypeForName(lastEvent.Type);
            var storedEvent = (ExampleCreated) JsonSerialiser.Deserialise(lastEvent.Data, type);
            storedEvent.Should().BeOfType<ExampleCreated>();
            storedEvent.AggregateId.Should().Be(createExampleCommand.AggregateId);
            storedEvent.Description.Should().Be(createExampleCommand.Description);
        }
    }
}