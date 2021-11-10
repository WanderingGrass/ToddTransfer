namespace Transfer.Types
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}