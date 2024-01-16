namespace WhiteBear.Domain.Exceptions;

public sealed class EmptyAuthorNameException : Exception
{
    public EmptyAuthorNameException(string message) : base(message)
    {
    }
}

