namespace WhiteBear.Application.Validators;

using FluentValidation;
using WhiteBear.Application.Books.Commands;

internal sealed class AddBookValidator : AbstractValidator<AddBook>
{
    public AddBookValidator()
    {
        RuleFor(book => book.Isbn)
            .NotNull()
            .Must(isbn=> isbn.Length is 10)
            .WithMessage(isbn=> $"Isbn should have 10 chars")
            ;
        RuleFor(book => book.Title)
            .NotEmpty()
            ;
        RuleFor(book => book.Cover)
            .NotNull()
            ;
    }
}

