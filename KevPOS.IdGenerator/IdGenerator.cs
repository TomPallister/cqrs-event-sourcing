namespace KevPOS.IdGenerator
{
    /// <summary>
    ///     Client side Id Generator.
    /// </summary>
    /// <remarks>
    ///     Ids are generated from a block provided by a service
    /// </remarks>
    public class IdGenerator : IIdGenerator
    {
        private readonly int _blockSize;
        private readonly IIdBlockGenerator _idBlockGenerator;
        private long _currentHi;
        private long _currentLo;

        public IdGenerator(IIdBlockGenerator idBlockGenerator)
        {
            _idBlockGenerator = idBlockGenerator;
            _blockSize = _idBlockGenerator.BlockCapacity();
            _currentHi = _idBlockGenerator.NextBlock();
            _currentLo = 0;
        }

        public long NextId()
        {
            if (_currentLo == _blockSize)
            {
                _currentHi = _idBlockGenerator.NextBlock();
                _currentLo = 0;
            }
            long nextId = (_currentHi - 1)*_blockSize + (++_currentLo);
            return nextId;
        }
    }
}