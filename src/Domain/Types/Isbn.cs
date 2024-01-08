namespace WhiteBear.Domain.Types;

using System.Text.RegularExpressions;
using WhiteBear.Domain.Exceptions;

public record struct Isbn
{
    private const string PATTERN = "^[0-9]{9}[0-9X]{1}$";

    public string Value { get; private init; }

    public Isbn(string value)
    {
        var regex = new Regex(PATTERN);

        if (regex.IsMatch(value) is false)
        {
            throw new IncorrectIsbnException(value);
        }

        this.Value = value;
    }
}
