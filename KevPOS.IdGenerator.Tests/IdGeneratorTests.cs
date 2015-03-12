using FluentAssertions;
using KevPOS.TypeExtensions.Infrastructure;
using Moq;
using NUnit.Framework;

namespace KevPOS.IdGenerator.Tests
{
    [TestFixture]
    public class IdGeneratorTests : AbstractTests
    {
        [SetUp]
        public void TestInitialise()
        {
            _idBlockGenerator = new Mock<IIdBlockGenerator>();
            Given_AIdBlockGeneratorWithConfiguration(1, 3);
            _idGenerator = new IdGenerator(_idBlockGenerator.Object);
        }

        private const int Twice = 2;

        private IdGenerator _idGenerator;
        private Mock<IIdBlockGenerator> _idBlockGenerator;

        private void Given_AIdBlockGeneratorWithConfiguration(int nextBlock, int capacity)
        {
            _idBlockGenerator.Setup(gen => gen.BlockCapacity()).Returns(capacity);
            _idBlockGenerator.Setup(gen => gen.NextBlock()).Returns(() => nextBlock++);
        }

        [Test]
        public void after_call_for_next_block_the_next_id_is_the_next_after_call()
        {
            Consume(3).Ids.Using(_idGenerator);
            long nextId = _idGenerator.NextId();
            nextId.Should().Be(4);
        }

        [Test]
        public void call_IdBlockProvider_on_start_up_to_getBlock()
        {
            _idBlockGenerator.Verify(srv => srv.NextBlock(), Times.Once);
        }

        [Test]
        public void call_IdBlockProvider_on_start_up_to_get_capacity()
        {
            _idBlockGenerator.Verify(srv => srv.BlockCapacity(), Times.Once);
        }

        [Test]
        public void call_IdBlockProvider_to_get_next_block_on_Id_exhaustion()
        {
            4.Times(() => _idGenerator.NextId());
            _idBlockGenerator.Verify(srv => srv.NextBlock(), Times.Exactly(Twice));
        }

        [Test]
        public void returning_Zero_for_initial_Id_block_request()
        {
            long high = _idGenerator.NextId();
            high.Should().Be(1);
        }

        [Test]
        public void returning_the_next_id_in_the_block()
        {
            _idGenerator.NextId();
            long nextId = _idGenerator.NextId();
            nextId.Should().Be(2);
        }

        [Test]
        public void two_IdGenerators_do_not_overlap_when_requesting_next_block()
        {
            var idGenerator1 = new IdGenerator(_idBlockGenerator.Object);
            Consume(3).Ids.Using(_idGenerator);
            Consume(3).Ids.Using(idGenerator1);
            long id = _idGenerator.NextId();
            id.Should().Be(7);
        }

        [Test]
        public void two_IdGenerators_generate_ids_in_different_blocks()
        {
            var idGenerator1 = new IdGenerator(_idBlockGenerator.Object);
            var idGenerator2 = new IdGenerator(_idBlockGenerator.Object);
            long id1 = idGenerator1.NextId();
            long id2 = idGenerator2.NextId();
            id2.Should().BeGreaterThan(id1);
        }
    }
}