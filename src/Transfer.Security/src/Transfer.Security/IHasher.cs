namespace Transfer.Security
{
    // SHA-256
    public interface IHasher
    {
        string Hash(string data);
        byte[] Hash(byte[] data);
    }
}