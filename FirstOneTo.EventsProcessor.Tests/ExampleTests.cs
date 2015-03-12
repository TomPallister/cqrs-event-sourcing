using System.Configuration;
using FirstOneTo.Authentication.Service;
using FirstOneTo.EmailSender;
using FirstOneTo.Events;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FirstOneTo.Readmodel.Models;
using FirstOneTo.Readmodel.Services.Concrete;
using FirstOneTo.Readmodel.Services.Interface;
using FluentAssertions;
using KevPOS.EventsProcessor;
using NUnit.Framework;
using Tasks.Command.Services.Concrete;
using Tasks.Command.Services.Interface;

namespace FirstOneTo.EventsProcessor.Tests
{
    [TestFixture]
    public class ExampleTests
    {
        [SetUp]
        public void SetUp()
        {
            _userDatabase = new InMemoryUserDatabase();
            _authenticationService = new AuthenticationService(_userDatabase);
            _exampleDatabase = new InMemoryExampleDatabase();
            _commandExampleService = new CommandExampleService(_exampleDatabase);
            _queryExampleService = new QueryExampleService(_exampleDatabase);
            _mailer = new SendGridMailer(ConfigurationManager.AppSettings["SendGridUserName"],
                ConfigurationManager.AppSettings["SendGridPassword"]);

            _eventsProcessor = new KevPOS.EventsProcessor.EventsProcessor(_authenticationService, _commandExampleService,
                _queryExampleService);
        }

        private IExampleDatabase _exampleDatabase;
        private ICommandExampleService _commandExampleService;
        private IQueryExampleService _queryExampleService;
        private IEventsProcessor _eventsProcessor;
        private IReadModelUserDatabase _userDatabase;
        private IAuthenticationService _authenticationService;
        private IMailer _mailer;

        [Test]
        public void can_accept_example_created_event()
        {
            const string description = "some challenge";
            var evt = new ExampleCreated
            {
                AggregateId = 1,
                Description = description,
            };
            _eventsProcessor.Accept(evt);
            ExampleModel challenge = _queryExampleService.GetExample(1);
            challenge.Description.Should().Be(description);
        }
    }
}