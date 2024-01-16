namespace WhiteBear.Domain.Types;

using System.Text.RegularExpressions;
using WhiteBear.Domain.Exceptions;

public record struct Isbn
{
    private const string PATTERN = "^[0-9]{9}[0-9X]{1}$";
    public string Value { get; private init; }

    //public Isbn(string value)
    //{
    //    var regex = new Regex(PATTERN);

    //    if (regex.IsMatch(value) is false)
    //    {
    //        throw new IncorrectIsbnException(nameof(value));
    //    }

    //    this.Value = value;
    //}
    public Isbn(string value)
    {
        var regex = new Regex(PATTERN);

        if (regex.IsMatch(value) is false)
        {
            throw new IncorrectIsbnException(nameof(value));
        }

        if (IsValidISBN(value) is false)
        {
            throw new IncorrectIsbnSumException(nameof(value));
        }

        this.Value = value;
    }

    public static bool IsValidISBN(string isbn)
    {
        if (isbn.Length is not 10)
        {
            return false;
        }

        var sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += ((int)isbn[i] - 48) * (10 - i);
        }

        sum = 11 - sum % 11;

        return (sum is 10 && isbn[9] is 'X') || ((int)isbn[9] - 48 == sum);
    }
}
