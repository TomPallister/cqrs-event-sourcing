using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using FirstOneTo.Authentication.Service;
using FirstOneTo.CommandProcessor;
using FirstOneTo.Commands;
using FirstOneTo.CommandValidation;
using FirstOneTo.Domain.Service.Concrete;
using FirstOneTo.EmailSender;
using FirstOneTo.EventsPublisher;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FirstOneTo.Readmodel.Services.Concrete;
using FirstOneTo.Readmodel.Services.Interface;
using KevPOS.EventRepository;
using KevPOS.EventsProcessor;
using KevPOS.EventStore;
using KevPOS.IdGenerator;
using Nancy.Authentication.Token;
using NUnit.Framework;
using Tasks.Command.Services.Concrete;
using Tasks.Command.Services.Interface;

namespace FirstOneTo.Handlers.Tests
{
    [TestFixture]
    public class PerformanceTests
    {
        [SetUp]
        public void SetUp()
        {
            _userDatabase = new InMemoryUserDatabase();
            _authenticationService = new AuthenticationService(_userDatabase);
            _acceptedChallengeDatabase = new InMemoryExampleDatabase();
            _mailer = new SendGridMailer(ConfigurationManager.AppSettings["SendGridUserName"],
                ConfigurationManager.AppSettings["SendGridPassword"]);
            IExampleDatabase exampleDatabase = new InMemoryExampleDatabase();
            ICommandExampleService commandExampleService = new CommandExampleService(exampleDatabase);
            IQueryExampleService queryExampleService = new QueryExampleService(exampleDatabase);
            _eventsProcessor = new EventsProcessor(_authenticationService, commandExampleService, queryExampleService);
            _eventDatabase = new InMemoryEventDatabase();
            _eventStore = new EventStore(_eventDatabase);
            _messageResolver = new MessageResolver(Assembly.GetAssembly(typeof (CreateExample)));
            _eventRepository = new EventRepository(_eventStore, _messageResolver);
            _challengeService = new ExampleService(_eventRepository);
            _commandProcessor = new CommandProcessor.CommandProcessor(_challengeService);
            _blockGenerator = new InMemoryBlockGenerator();
            _idGenerator = new IdGenerator(_blockGenerator);
            _validator = new Validator();
            _eventsPublisher = new EventsPublisher.EventsPublisher(_eventsProcessor);
            _commandAndEventHandler = new CommandAndEventHandler(_idGenerator, _commandProcessor, _eventsPublisher,
                _validator);
        }

        private Tokenizer _tokenizer;
        private InMemoryUserDatabase _userDatabase;
        private IAuthenticationService _authenticationService;
        private InMemoryExampleDatabase _acceptedChallengeDatabase;
        private EventsProcessor _eventsProcessor;
        private InMemoryEventDatabase _eventDatabase;
        private EventStore _eventStore;
        private MessageResolver _messageResolver;
        private EventRepository _eventRepository;
        private ExampleService _challengeService;
        private ICommandProcessor _commandProcessor;
        private InMemoryBlockGenerator _blockGenerator;
        private IdGenerator _idGenerator;
        private CommandAndEventHandler _commandAndEventHandler;
        private IValidator _validator;
        private IMailer _mailer;
        private IEventsPublisher _eventsPublisher;

        [Test]
        public void how_many_ops_per_second()
        {
            var createPlayer = new CreateExample
            {
                Description = "des",
            };
            const int iterations = 1000000;
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                _commandAndEventHandler.Process("CreateExample", createPlayer);
            }
            watch.Stop();
            long opsPerSecond = (iterations*1000L)/watch.ElapsedMilliseconds;
            Console.WriteLine("operations per second = {0}", opsPerSecond);
            Console.WriteLine("operations per minute = {0}", opsPerSecond*60);
        }
    }
}