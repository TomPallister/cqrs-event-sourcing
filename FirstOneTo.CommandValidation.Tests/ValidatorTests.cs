using FirstOneTo.Commands;
using FluentAssertions;
using NUnit.Framework;

namespace FirstOneTo.CommandValidation.Tests
{
    [TestFixture]
    public class ValidatorTests
    {
        private IValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new Validator();
        }

        [Test]
        public void can_validate_create_example()
        {
            var createExample = new CreateExample
            {
                Description = "description",
                AggregateId = 1
            };
            ValidationResult result = _validator.ValidateCreateExample(createExample);
            result.ErrorMessages.Count.Should().Be(0);
            result.Valid.Should().Be(true);
        }
    }
}