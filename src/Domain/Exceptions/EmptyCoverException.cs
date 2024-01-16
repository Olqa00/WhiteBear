namespace WhiteBear.Domain.Exceptions;

public sealed class EmptyCoverException : Exception
{
    public EmptyCoverException(string message) : base(message)
    {
    }
}

