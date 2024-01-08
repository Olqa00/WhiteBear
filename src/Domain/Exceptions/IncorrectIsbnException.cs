namespace WhiteBear.Domain.Exceptions;

public sealed class IncorrectIsbnException : Exception
{
    public IncorrectIsbnException(string isbn) : base($"'{isbn}' is not valid ISBN")
    {
    }
}
