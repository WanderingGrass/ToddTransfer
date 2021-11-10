namespace Transfer.HTTP
{
    internal class EmptyCorrelationContextFactory : ICorrelationContextFactory
    {
        public string Create() => default;
    }
}