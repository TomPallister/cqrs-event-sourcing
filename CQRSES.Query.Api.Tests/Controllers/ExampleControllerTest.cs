using FirstOneTo.Readmodel.Models;
using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FirstOneTo.Readmodel.Services.Concrete;
using FirstOneTo.Readmodel.Services.Interface;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Tasks.Command.Services.Concrete;
using Tasks.Command.Services.Interface;
using Tasks.Query.Api.Controllers;

namespace Tasks.Query.Api.Tests.Controllers
{
    [TestFixture]
    public class ExampleControllerTest
    {
        [Test]
        public void GetById()
        {
            // Arrange
            IExampleDatabase exampleDatabase = new InMemoryExampleDatabase();
            ICommandExampleService commandExampleService = new CommandExampleService(exampleDatabase);
            IQueryExampleService queryExampleService = new QueryExampleService(exampleDatabase);
            var exampleModel = new ExampleModel
            {
                AggregateId = 1,
                Description = "description"
            };
            commandExampleService.Store(exampleModel);
            var controller = new ExampleController(queryExampleService);
            // Act
            string result = controller.Get(1);

            // Assert
            var returnedExampleModel = JsonConvert.DeserializeObject<ExampleModel>(result);
            returnedExampleModel.AggregateId.Should().Be(1);
            returnedExampleModel.Description.Should().Be("description");
        }
    }
}