using FluentAssertions;
using NUnit.Framework;

namespace KevPOS.ValueObjects.Tests
{
    [TestFixture]
    public class IdTests
    {
        [Test]
        public void NullId_is_not_equal_to_an_Id()
        {
            new Id(1).Should().NotBe(Id.NullId);
        }

        [Test]
        public void can_compare_two_ids_using_equals()
        {
            var id = new Id(1);
            id.Should().Be(new Id(1));
        }

        [Test]
        public void two_different_ids_are_not_equivalent_using_equality_operator()
        {
            var id1 = new Id(1);
            var id2 = new Id(2);
            (id1 == id2).Should().BeFalse();
        }

        [Test]
        public void two_different_ids_are_not_equivalent_using_inequality_operator()
        {
            var id1 = new Id(1);
            var id2 = new Id(2);
            (id1 != id2).Should().BeTrue();
        }

        [Test]
        public void two_similar_ids_are_equivalent_using_equality_operator()
        {
            var id1 = new Id(1);
            var id2 = new Id(1);
            (id1 == id2).Should().BeTrue();
        }

        [Test]
        public void two_similar_ids_are_not_equivalent_using_equality_operator()
        {
            var id1 = new Id(1);
            var id2 = new Id(1);
            (id1 != id2).Should().BeFalse();
        }
    }
}