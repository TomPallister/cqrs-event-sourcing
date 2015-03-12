using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using FirstOneTo.Authentication;
using FirstOneTo.Authentication.Service;
using FirstOneTo.CommandProcessor;
using FirstOneTo.Commands;
using FirstOneTo.CommandSender;
using FirstOneTo.CommandValidation;
using FirstOneTo.Domain.Service.Concrete;
using FirstOneTo.Domain.Service.Interface;
using FirstOneTo.EmailSender;
using FirstOneTo.Events;
using FirstOneTo.EventsPublisher;
using FirstOneTo.FileStore;
using FirstOneTo.Handlers;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.Azure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FirstOneTo.Readmodel.Services.Concrete;
using FirstOneTo.Readmodel.Services.Interface;
using KevPOS.EventRepository;
using KevPOS.EventsProcessor;
using KevPOS.EventStore;
using KevPOS.IdGenerator;
using KevPOS.ValueObjects;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using Nancy.Authentication.Token;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Tasks.Command.Services.Concrete;
using Tasks.Command.Services.Interface;

namespace FistOneTo.Bootstrapper
{
    public static class ApiBootstrapper
    {
        public static BootstrappedObjects InMemoryBootstrapper(TinyIoCContainer container,
            CloudStorageAccount storageAccount)
        {
            var userDb = new InMemoryUserDatabase();
            var authenticationService = new AuthenticationService(userDb);
            var exampleDatabase = new InMemoryExampleDatabase();
            IQueryExampleService queryExampleService = new QueryExampleService(exampleDatabase);
            ICommandExampleService commandExampleService = new CommandExampleService(exampleDatabase);
            var mailer = new SendGridMailer(ConfigurationManager.AppSettings["SendGridUserName"],
                ConfigurationManager.AppSettings["SendGridPassword"]);
            var eventsProcessor = new EventsProcessor(authenticationService, commandExampleService, queryExampleService);
            var eventDb = new InMemoryEventDatabase();
            var eventStore = new EventStore(eventDb);
            var resolver = new MessageResolver(Assembly.GetAssembly(typeof (ExampleCreated)));
            var eventRepository = new EventRepository(eventStore, resolver);
            var challengeService = new ExampleService(eventRepository);
            var commandProcessor = new CommandProcessor(challengeService);
            var validator = new Validator();
            var eventsPublisher = new EventsPublisher(eventsProcessor);
            container.Register<IEventsPublisher>(eventsPublisher);

            container.Register<IMailer>(mailer);
            container.Register<IValidator>(validator);
            container.Register<IEventsProcessor>(eventsProcessor);
            container.Register<IEventDatabase>(eventDb);
            container.Register<IMessageResolver>(resolver);
            container.Register<IEventRepository>(eventRepository);
            container.Register<IExampleService>(challengeService);
            container.Register<IAuthenticationService>(authenticationService);
            container.Register<IReadModelUserDatabase>(userDb);
            container.Register<IExampleDatabase>(exampleDatabase);
            container.Register<IQueryExampleService>(queryExampleService);
            container.Register<ICommandExampleService>(commandExampleService);
            var idBlockGenerator = new InMemoryBlockGenerator();
            var idGenerator = new IdGenerator(idBlockGenerator);
            var commandAndEventHandler = new CommandAndEventHandler(idGenerator, commandProcessor, eventsPublisher,
                validator);
            container.Register<IIdBlockGenerator>(idBlockGenerator);
            container.Register<IIdGenerator>(idGenerator);
            container.Register(commandAndEventHandler);
            container.Register(new CommandSender(commandAndEventHandler));
            return new BootstrappedObjects(commandAndEventHandler, authenticationService);
        }

        public static BootstrappedObjects AzureBootstrapper(TinyIoCContainer container,
            CloudStorageAccount storageAccount, string tablePrefix, bool qaMode)
        {
            var userDb = new AzureUserDatabase(storageAccount, string.Format("{0}UserDatabase", tablePrefix));
            var authenticationService = new AuthenticationService( userDb);
            var exampleDatabase = new AzureExampleDatabase(storageAccount,
                string.Format("{0}AcceptedExampleDatabase", tablePrefix));
            ICommandExampleService commandExampleService = new CommandExampleService(exampleDatabase);
            IQueryExampleService queryExampleService = new QueryExampleService(exampleDatabase);
            var eventsProcessor = new EventsProcessor(authenticationService,commandExampleService, queryExampleService);
            var eventDb = new AzureEventDatabase(storageAccount, string.Format("{0}EventDatabase", tablePrefix));
            var eventStore = new EventStore(eventDb);
            var resolver = new MessageResolver(Assembly.GetAssembly(typeof (ExampleCreated)));
            var eventRepository = new EventRepository(eventStore, resolver);
            var challengeService = new ExampleService(eventRepository);
            var commandProcessor = new CommandProcessor(challengeService);
            var validator = new Validator();
            var eventsPublisher = new EventsPublisher(eventsProcessor);
            container.Register<IEventsPublisher>(eventsPublisher);
            container.Register<IValidator>(validator);
            container.Register<IEventsProcessor>(eventsProcessor);
            container.Register<IEventDatabase>(eventDb);
            container.Register<IMessageResolver>(resolver);
            container.Register<IEventRepository>(eventRepository);
            container.Register<IExampleService>(challengeService);
            container.Register<IAuthenticationService>(authenticationService);
            container.Register<IReadModelUserDatabase>(userDb);
            container.Register<IExampleDatabase>(exampleDatabase);
            container.Register<IQueryExampleService>(queryExampleService);
            container.Register<ICommandExampleService>(commandExampleService);
            //dirty hack to make sure we dont reset the wrong table in our free sql database, todo when we have money sort
            //this out!
            IIdBlockGenerator idBlockGenerator;
            if (qaMode)
            {
                idBlockGenerator = new InMemoryBlockGenerator();
            }
            else
            {
                idBlockGenerator = new BlockGenerator();
            }
            var idGenerator = new IdGenerator(idBlockGenerator);
            var commandAndEventHandler = new CommandAndEventHandler(idGenerator, commandProcessor, eventsPublisher,
                validator);
            container.Register(idBlockGenerator);
            container.Register<IIdGenerator>(idGenerator);
            container.Register(commandAndEventHandler);
            container.Register(new CommandSender(commandAndEventHandler));
            return new BootstrappedObjects(commandAndEventHandler, authenticationService);
        }

        public static void TearDownAzure(CloudStorageAccount storageAccount)
        {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            IEnumerable<CloudTable> tables = tableClient.ListTables();
            foreach (CloudTable table in tables)
            {
                table.DeleteIfExists();
            }
            IEnumerable<CloudBlobContainer> containers = blobClient.ListContainers();
            foreach (CloudBlobContainer container in containers)
            {
                container.DeleteIfExists();
            }
        }

        public static void CreateAdminUserIfDoesntExisitAlready(CommandAndEventHandler commandAndEventHandler,
            IAuthenticationService authenticationService)
        {
            IUser user = authenticationService.GetUser("boss");
            if (user == null)
            {
                var createPlayer = new CreateUser
                {
                    Password = "Ody553u5",
                    UserName = "boss",
                    Email = "tom+boss@threemammals.email"
                };
                ResponseContent boss =
                    commandAndEventHandler.Process("CreateUser", createPlayer);
            }
        }

        public static void SetUpExamples(CommandAndEventHandler commandAndEventHandler)
        {
            //first of all look to see if we have any challenges, If we do dont bother running this method

            //if we are running this method the first thing we need to do is make sure our CurrentBlock is 0
            using (
                var connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("update control.IdBlock set CurrentBlock = 0", connection))
                {
                    command.ExecuteNonQueryAsync();
                }
            }

            //create challenges
            var createChallenge = new CreateExample
            {
                Description = "Fight a tiger!",
            };
            commandAndEventHandler.Process("CreateChallenge", createChallenge);
            createChallenge = new CreateExample
            {
                Description = "Show us your skyline by day.",
            };
            commandAndEventHandler.Process("CreateChallenge", createChallenge);
            createChallenge = new CreateExample
            {
                Description = "Show us your skyline by night.",
            };
            commandAndEventHandler.Process("CreateChallenge", createChallenge);
            createChallenge = new CreateExample
            {
                Description = "Eat two large XL Bacon Double cheeseburger meals at Burger King.",
            };
            commandAndEventHandler.Process("CreateChallenge", createChallenge);
        }

        public static void TearDownOldTestTables(string tablePrefix, CloudStorageAccount storageAccount)
        {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            IEnumerable<CloudTable> rmsTable = tableClient.ListTables(tablePrefix);
            foreach (CloudTable cloudTable in rmsTable)
            {
                cloudTable.DeleteIfExists();
            }
        }
    }
}