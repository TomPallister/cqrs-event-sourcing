using FluentAssertions;
using KevPOS.Infrastructure;
using KevPOS.Messages.Commands.Product;
using KevPOS.Messages.Events.Product;
using NUnit.Framework;

namespace KevPOS.EventRepository.Tests
{
    /// <summary>
    ///     Summary description for Serialising
    /// </summary>
    [TestFixture]
    public class SerialisingTests
    {
        [Test]
        public void deserialising_an_event_message()
        {
            const string serialisedEvent = "{\"Name\":\"Fred\", \"AggregateId\":1}";
            var command = (ProductCreated) Serialiser.Deserialise(serialisedEvent, typeof (ProductCreated));
            command.AggregateId.Should().Be(1);
        }

        [Test]
        public void deserialising_an_object_containing_a_Name()
        {
            const string serialisedCommand = "{\"Name\":\"Fred\"}";
            var command = (ProductCreated) Serialiser.Deserialise(serialisedCommand, typeof (ProductCreated));
            command.Name.Should().Be("Fred");
        }

        [Test]
        public void deserialising_an_object_containing_an_Id()
        {
            const string serialisedCommand = "{\"AggregateId\":1}";
            var command = (ProductCreated) Serialiser.Deserialise(serialisedCommand, typeof (ProductCreated));
            command.AggregateId.Should().Be(1);
        }

        [Test]
        public void serialising_an_object_containing_a_Name()
        {
            var command = new ProductCreated {Name = "Fred", ProductCode = "test1234"};
            string serialised = Serialiser.Serialise(command);
            serialised.Should().Contain("Name");
            serialised.Should().Contain("Fred");
            serialised.Should().Contain("ProductCode");
            serialised.Should().Contain("test1234");
        }

        [Test]
        public void serialising_an_object_containing_an_Id()
        {
            var command = new CreateProduct {AggregateId = 1};
            string serialised = Serialiser.Serialise(command);
            serialised.Should().Contain("Id");
            serialised.Should().Contain("1");
        }
    }
}