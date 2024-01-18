namespace WhiteBear.Application.Common.Exceptions;

using FluentValidation.Results;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<string> failures) : base(failures.FirstOrDefault() ?? string.Empty)
    {
        //string.Join(Environment.NewLine, failures);
    }
}
