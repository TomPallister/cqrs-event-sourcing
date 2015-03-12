using FluentAssertions;
using KevPOS.InfrastructureByKevin.Extensions;
using NUnit.Framework;

namespace KevPOS.Infrastructure.TypeExtensions.Tests
{
    [TestFixture]
    public class Int32Tests
    {
        [Test]
        public void Times_iterates_expected_number_of_times()
        {
            int x = 0;
            3.Times(() => x++);
            x.Should().Be(3);
        }

        [Test]
        public void Times_iterates_expected_number_of_times_taking_a_single_parameter()
        {
            int x = 0;
            3.Times(y => { x = y; });
            x.Should().Be(2);
        }
    }
}