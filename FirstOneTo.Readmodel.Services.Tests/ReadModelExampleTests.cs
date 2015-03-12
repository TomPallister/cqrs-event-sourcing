using FirstOneTo.ReadModel.Infrastructure;
using FirstOneTo.ReadModel.Infrastructure.InMemory;
using FirstOneTo.Readmodel.Models;
using FirstOneTo.Readmodel.Services.Concrete;
using FirstOneTo.Readmodel.Services.Interface;
using FluentAssertions;
using NUnit.Framework;
using Tasks.Command.Services.Concrete;
using Tasks.Command.Services.Interface;

namespace FirstOneTo.Readmodel.Services.Tests
{
    [TestFixture]
    public class ReadModelExampleTests
    {
        private IExampleDatabase _readModelExampleDatabase;
        private IQueryExampleService _readModelExampleService;
        private ICommandExampleService _commandExampleService;

        [SetUp]
        public void SetUp()
        {
            _readModelExampleDatabase = new InMemoryExampleDatabase();
            _commandExampleService = new CommandExampleService(_readModelExampleDatabase);
            _readModelExampleService = new QueryExampleService(_readModelExampleDatabase);
        }

        [Test]
        public void can_get_example()
        {
            _commandExampleService.Store(new ExampleModel(1, "description"));
            ExampleModel example = _readModelExampleService.GetExample(1);
            example.AggregateId.Should().Be(1);
            example.Description.Should().Be("description");
        }
    }
}