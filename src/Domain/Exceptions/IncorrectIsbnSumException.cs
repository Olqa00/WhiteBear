namespace WhiteBear.Domain.Exceptions;

public sealed class IncorrectIsbnSumException : Exception
{
    public IncorrectIsbnSumException(string isbn) : base($"'{isbn}' is not valid ISBN")
    {
    }
}

