using System.Reflection;
using FirstOneTo.Authentication.Service;
using FirstOneTo.CommandProcessor;
using FirstOneTo.Commands;
using FirstOneTo.CommandSender;
using FirstOneTo.CommandValidation;
using FirstOneTo.Domain.Service.Concrete;
using FirstOneTo.Domain.Service.Interface;
using FirstOneTo.EventsPublisher;
using FirstOneTo.Handlers;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FirstOneTo.Readmodel.Services.Concrete;
using FirstOneTo.Readmodel.Services.Interface;
using KevPOS.EventRepository;
using KevPOS.EventsProcessor;
using KevPOS.EventStore;
using KevPOS.IdGenerator;
using NUnit.Framework;
using Tasks.Command.Api.Controllers;
using Tasks.Command.Services.Concrete;
using Tasks.Command.Services.Interface;

namespace Tasks.Command.Api.Tests.Controllers
{
    [TestFixture]
    public class ExampleControllerTest
    {
        [Test]
        public void Post()
        {
            // Arrange
            IIdGenerator idGenerator = new IdGenerator(new InMemoryBlockGenerator());
            var eventStore = new EventStore(new InMemoryEventDatabase());
            IEventRepository eventRepository = new EventRepository(eventStore, new MessageResolver(Assembly.GetAssembly(typeof(CreateExample))));
            IExampleService exampleService = new ExampleService(eventRepository);
            ICommandProcessor commandProcessor = new CommandProcessor(exampleService);
            IExampleDatabase databse = new InMemoryExampleDatabase();
            IAuthenticationService authenticationService = new AuthenticationService(new InMemoryUserDatabase());
            ICommandExampleService commandExampleService = new CommandExampleService(databse);
            IQueryExampleService queryExampleService = new QueryExampleService(databse);
            IEventsProcessor eventsProcessor = new EventsProcessor(authenticationService, commandExampleService,
                queryExampleService);
            IEventsPublisher eventsPublisher = new EventsPublisher(eventsProcessor);
            IValidator validator = new Validator();
            var commandAndEventHandler = new CommandAndEventHandler(idGenerator, commandProcessor, eventsPublisher,
                validator);
            var commandSender = new CommandSender(commandAndEventHandler);
            var controller = new ExampleController(commandSender);
            // Act
            controller.Post(new CreateExample
            {
                Description = "description"
            });

            // Assert
        }
    }
}