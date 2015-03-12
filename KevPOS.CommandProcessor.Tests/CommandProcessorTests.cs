using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using KevPOS.Domain.Ecommerce.Services.Concrete;
using KevPOS.Domain.Ecommerce.Services.Interface;
using KevPOS.EventRepository;
using KevPOS.EventStore;
using KevPOS.InMemoryBus;
using KevPOS.Messages;
using KevPOS.Messages.Commands.Product;
using KevPOS.Messages.Events.Product;
using NUnit.Framework;

namespace KevPOS.CommandProcessor.Tests
{
    [TestFixture]
    public class CommandProcessorTests
    {
        [SetUp]
        public void TestInitialise()
        {
            _events = new List<EventData>();
            _db = new InMemoryEventDatabase(_events);
            _eventStore = new EventStore.EventStore(_db);
            _resolver = new MessageResolver(Assembly.GetAssembly(typeof (AbstractEvent)));
            _repository = new EventRepository.EventRepository(_eventStore, _resolver, new FakeBus());
            IProductService productService = new ProductService(_repository);
            IVariantService variantService = new VariantService(_repository);
            ICategoryService categoryService = new CategoryService(_repository);
            IBasketService basketService = new BasketService(_repository);
            IOrderService orderService = new OrderService(_repository);
            ICustomerService customerService = new CustomerService(_repository);
            _commandProcessor = new CommandProcessor(productService, categoryService, orderService, variantService,
                basketService, customerService);
        }

        private List<EventData> _events;
        private InMemoryEventDatabase _db;
        private EventStore.EventStore _eventStore;
        private IEventRepository _repository;
        private IMessageResolver _resolver;
        private CommandProcessor _commandProcessor;

        [Test]
        public void can_store_ProductCreated_Event_in_EventStore()
        {
            var registerEmployee = new CreateProduct
            {
                AggregateId = 1,
                Name = "Awesome Test Product",
                State = "Inactive",
                ProductCode = "test1234"
            };
            var result = _commandProcessor.Accept(registerEmployee);
            result.GetType().Name.Should().Be(typeof(ProductCreated).Name);
        }

        [Test]
        public void can_store_ProductNameChanged_Event_in_EventStore()
        {
            const int id = 1;
            var registerEmployee = new CreateProduct
            {
                AggregateId = id,
                Name = "Awesome Test Product",
                State = "Inactive",
                ProductCode = "test1234"
            };
            _commandProcessor.Accept(registerEmployee);
            var newAddress = new ChangeProductName {AggregateId = id, Name = "Name Changed"};
            var result = _commandProcessor.Accept(newAddress);
            result.GetType().Name.Should().Be(typeof(ProductNameChanged).Name);
        }
    }
}