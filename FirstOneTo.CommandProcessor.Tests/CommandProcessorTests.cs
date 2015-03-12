using System;
using System.Collections.Generic;
using System.Reflection;
using FirstOneTo.Commands;
using FirstOneTo.Domain.Service.Concrete;
using FirstOneTo.Domain.Service.Interface;
using FirstOneTo.Events;
using FluentAssertions;
using KevPOS.EventRepository;
using KevPOS.EventStore;
using NUnit.Framework;

namespace FirstOneTo.CommandProcessor.Tests
{
    [TestFixture]
    public class CommandProcessorTests
    {
        [TearDown]
        public void TearDown()
        {
            _resolver = null;
            _db = null;
            _eventRepository = null;
            _challengeService = null;
            _commandProcessor = null;
        }

        [SetUp]
        public void SetUp()
        {
            _db = new InMemoryEventDatabase(new List<EventData>());
            _resolver = new MessageResolver(Assembly.GetAssembly(typeof (ExampleCreated)));
            _eventRepository = new EventRepository(new EventStore(_db), _resolver);
            _challengeService = new ExampleService(_eventRepository);
            _commandProcessor = new CommandProcessor(_challengeService);
        }

        private ICommandProcessor _commandProcessor;
        private IExampleService _challengeService;
        private InMemoryEventDatabase _db;
        private EventRepository _eventRepository;
        private IMessageResolver _resolver;

        [Test]
        public void can_accept_create_example_command()
        {
            var createExample = new CreateExample()
            {
                AggregateId = 1,
                Description = "description"
            };
            var events = _commandProcessor.Accept(createExample);
            events.Count.Should().Be(1);
        }
    }
}