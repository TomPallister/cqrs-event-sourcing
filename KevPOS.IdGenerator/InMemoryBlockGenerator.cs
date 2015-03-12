namespace KevPOS.IdGenerator
{
    public class InMemoryBlockGenerator : IIdBlockGenerator
    {
        private long _currentBlock;

        public long NextBlock()
        {
            return ++_currentBlock;
        }

        public int BlockCapacity()
        {
            return 10;
        }
    }
}