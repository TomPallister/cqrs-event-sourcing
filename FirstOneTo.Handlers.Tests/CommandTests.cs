using System.Configuration;
using System.Reflection;
using FirstOneTo.Authentication.Service;
using FirstOneTo.CommandProcessor;
using FirstOneTo.Commands;
using FirstOneTo.CommandValidation;
using FirstOneTo.Domain.Service.Concrete;
using FirstOneTo.EmailSender;
using FirstOneTo.Events;
using FirstOneTo.EventsPublisher;
using FirstOneTo.Readmodel.Services.Concrete;
using FirstOneTo.Readmodel.Services.Interface;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FluentAssertions;
using KevPOS.EventRepository;
using KevPOS.EventsProcessor;
using KevPOS.EventStore;
using KevPOS.IdGenerator;
using KevPOS.ValueObjects;
using Nancy.Authentication.Token;
using NUnit.Framework;
using Tasks.Command.Services.Concrete;
using Tasks.Command.Services.Interface;

namespace FirstOneTo.Handlers.Tests
{
    [TestFixture]
    public class CommandTests
    {
        [SetUp]
        public void SetUp()
        {
            _userDatabase = new InMemoryUserDatabase();
            _authenticationService = new AuthenticationService(_userDatabase);
            _exampleDatabase = new InMemoryExampleDatabase();
            _mailer = new SendGridMailer(ConfigurationManager.AppSettings["SendGridUserName"],
                ConfigurationManager.AppSettings["SendGridPassword"]);
            _eventDatabase = new InMemoryEventDatabase();
            _eventStore = new EventStore(_eventDatabase);
            _messageResolver = new MessageResolver(Assembly.GetAssembly(typeof (ExampleCreated)));
            _eventRepository = new EventRepository(_eventStore, _messageResolver);
            _exampleService = new ExampleService(_eventRepository);
            _commandProcessor = new CommandProcessor.CommandProcessor(_exampleService);
            _blockGenerator = new InMemoryBlockGenerator();
            _idGenerator = new IdGenerator(_blockGenerator);
            _validator = new Validator();
            _commandExampleService = new CommandExampleService(_exampleDatabase);
            _queryExampleService = new QueryExampleService(_exampleDatabase);
            _eventsProcessor = new EventsProcessor(_authenticationService, _commandExampleService, _queryExampleService);
            _eventsPublisher = new EventsPublisher.EventsPublisher(_eventsProcessor);
            _commandAndEventHandler = new CommandAndEventHandler(_idGenerator, _commandProcessor, _eventsPublisher,
                _validator);
        }

        private ICommandExampleService _commandExampleService;
        private IQueryExampleService _queryExampleService;
        private InMemoryUserDatabase _userDatabase;
        private IAuthenticationService _authenticationService;
        private IExampleDatabase _exampleDatabase;
        private EventsProcessor _eventsProcessor;
        private InMemoryEventDatabase _eventDatabase;
        private EventStore _eventStore;
        private MessageResolver _messageResolver;
        private EventRepository _eventRepository;
        private ExampleService _exampleService;
        private ICommandProcessor _commandProcessor;
        private InMemoryBlockGenerator _blockGenerator;
        private IdGenerator _idGenerator;
        private CommandAndEventHandler _commandAndEventHandler;
        private IValidator _validator;
        private IMailer _mailer;
        private IEventsPublisher _eventsPublisher;


        [Test]
        public void can_create_example()
        {
            var createExample = new CreateExample
            {
                Description = "some old challenge",
            };

            ResponseContent response = _commandAndEventHandler.Process("CreateExample", createExample);
            response.AggregateId.Should().NotBe(null);
        }
    }
}