using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using KevPOS.InfrastructureByKevin.Extensions;
using NUnit.Framework;

namespace KevPOS.Infrastructure.TypeExtensions.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        [Test]
        public void each_method_loops_through_collection()
        {
            int count = 0;
            IEnumerable<int> range = Enumerable.Range(1, 5);
            range.Each(x => count++);
            count.Should().Be(5);
        }
    }
}