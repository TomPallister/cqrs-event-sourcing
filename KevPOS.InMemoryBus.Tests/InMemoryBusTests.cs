using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KevPOS.Messages;
using NUnit.Framework;

namespace KevPOS.InMemoryBus.Tests
{
    [TestFixture]
    public class InMemoryBusTests
    {
        [Test]
        public void message_sending_event_bus_will_publish_event()
        {
            var productAddedToBasket = new VariantAddedToBasket
            {
                AggregateId = 1,
                VariantId = 1,
                Quantity = 1,
                Version = 1
            };
            IEventPublisher bus = new FakeBus();
            bus.Publish(productAddedToBasket);
        }

        [Test]
        public void will_publish_an_event()
        {
            bool wasCalled = false;
            var bus = new EventBus();
            bus.RegisterHandler<TestEvent>(@event => wasCalled = true);
            bus.Publish(new TestEvent());
            wasCalled.Should().BeTrue();
        }

        [Test]
        public void will_publish_an_event_in_list()
        {
            bool wasCalled = false;
            var bus = new EventBus();
            bus.RegisterHandler<TestEvent>(@event => wasCalled = true);
            var testEvent = new TestEvent();
            var testEventTwo = new TestEvent();
            IList<AbstractEvent> abstractEvents = new List<AbstractEvent>
            {
                testEvent,
                testEventTwo
            };

            foreach (dynamic desEvent in abstractEvents.Select(@event => Converter.ChangeTo(@event, @event.GetType())))
            {
                bus.Publish(desEvent);
            }
            wasCalled.Should().BeTrue();
        }
    }

    public class MessageSendingTestEvent : AbstractEvent
    {
        public string Something { get; set; }
        public decimal SomethingElse { get; set; }
    }

    public class TestEvent : AbstractEvent
    {
    }
}