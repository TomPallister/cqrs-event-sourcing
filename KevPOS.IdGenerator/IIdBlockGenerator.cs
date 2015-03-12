namespace KevPOS.IdGenerator
{
    public interface IIdBlockGenerator
    {
        long NextBlock();
        int BlockCapacity();
    }
}