using System.Collections.Generic;
using FirstOneTo.Commands;
using FirstOneTo.Domain.Aggregates;
using FirstOneTo.Domain.Service.Interface;
using KevPOS.EventRepository;
using KevPOS.Messages;
using KevPOS.ValueObjects;

namespace FirstOneTo.Domain.Service.Concrete
{
    public class ExampleService : IExampleService
    {
        private readonly IEventRepository _eventRepository;

        public ExampleService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IList<AbstractEvent> CreateExample(CreateExample createExampleCommand)
        {
            var example = new ExampleAggregate(createExampleCommand);
            IList<AbstractEvent> events = _eventRepository.Store(example);
            return events;
        }

        public IList<AbstractEvent> ChangeExample(ChangeExample changeExampleCommand)
        {
            var example = _eventRepository.Get<ExampleAggregate>(new Id(changeExampleCommand.AggregateId));
            example.ChangeExampleDescription(changeExampleCommand);
            IList<AbstractEvent> events = _eventRepository.Store(example);
            return events;
        }
    }
}