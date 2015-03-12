using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using KevPOS.Domain.Ecommerce.Services.Concrete;
using KevPOS.Domain.Ecommerce.Services.Interface;
using KevPOS.EventStore;
using KevPOS.Infrastructure;
using KevPOS.InMemoryBus;
using KevPOS.Messages.Events.Product;
using KevPOS.ValueObjects;
using NUnit.Framework;

namespace KevPOS.EventRepository.Tests
{
    [TestFixture]
    public class EventRepositoryTests
    {
        [SetUp]
        public void TestInitialise()
        {
            _db = new InMemoryEventDatabase(new List<EventData>());
            _resolver = new MessageResolver(Assembly.GetAssembly(typeof (ProductCreated)));
            _eventRepository = new EventRepository(new EventStore.EventStore(_db), _resolver, new FakeBus());
            _productService = new ProductService(_eventRepository);
        }

        private InMemoryEventDatabase _db;
        private EventRepository _eventRepository;
        private IMessageResolver _resolver;
        private IProductService _productService;

        [Test]
        public void attempting_to_load_a_non_existent_aggregate_generates_an_exeception()
        {
            Assert.Inconclusive("Not written yet.");
        }

        [Test]
        public void can_rehydate_aggregate_from_repository()
        {
            const int id = 1;
            var @event = new ProductCreated {AggregateId = id, Name = "Foo"};
            string serialised = Serialiser.Serialise(@event);
            var eventData = new EventData(id, serialised, @event.GetType().Name, DateTime.Now, 1);
            _db.EventStore.Add(eventData);
            EventData lastEvent = _eventRepository.EventStore.Read(id).Last();
            Type type = _resolver.TypeForName(lastEvent.Type);
            var @storedEvent = (ProductCreated) Serialiser.Deserialise(lastEvent.Data, type);
            @storedEvent.Should().BeOfType<ProductCreated>();
        }

        [Test]
        public void can_store_a_simple_aggregate()
        {
            const int id = 1;
            var @event = new ProductCreated { AggregateId = id, Name = "Foo" };
            string serialised = Serialiser.Serialise(@event);
            var eventData = new EventData(id, serialised, @event.GetType().Name, DateTime.Now, 1);
            _db.EventStore.Add(eventData);
            EventData serialisedEvent = _db.EventStore[0];
            string evt = serialisedEvent.Data;
            evt.Should().Contain("AggregateId");
            evt.Should().Contain("1");
        }
    }
}