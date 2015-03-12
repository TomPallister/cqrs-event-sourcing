using System;
using System.Reflection;
using FluentAssertions;
using KevPOS.Messages;
using KevPOS.Messages.Events.Product;
using NUnit.Framework;

namespace KevPOS.EventRepository.Tests
{
    [TestFixture]
    public class MessageResolvingTests
    {
        [Test]
        public void can_resolve_message_type()
        {
            Assembly messages = Assembly.GetAssembly(typeof (IMessage));
            IMessageResolver resolver = new MessageResolver(messages);
            Type type = resolver.TypeForName("ProductCreated");
            type.Should().Be(typeof (ProductCreated));
        }
    }
}