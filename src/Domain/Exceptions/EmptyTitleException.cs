namespace WhiteBear.Domain.Exceptions;

public sealed class EmptyTitleException : Exception
{
    public EmptyTitleException(string message) : base(message)
    {
    }
}
