using KevPOS.TypeExtensions.Infrastructure;

namespace KevPOS.IdGenerator.Tests
{
    public abstract class AbstractTests
    {
        protected IdConsumer Consume(int n)
        {
            return new IdConsumer(n);
        }

        protected class IdConsumer
        {
            private readonly int _number;

            public IdConsumer(int number)
            {
                _number = number;
            }

            public IdConsumer Ids
            {
                get { return this; }
            }

            public void Using(IdGenerator generator)
            {
                _number.Times(() => generator.NextId());
            }
        }
    }
}