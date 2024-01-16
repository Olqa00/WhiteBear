namespace WhiteBear.Application.Validators;

using FluentValidation;
using WhiteBear.Domain.Entities;

internal class AddBookValidator : AbstractValidator<BookEntity>
{
    public AddBookValidator()
    {
        RuleFor(book => book.Title).NotNull();
        RuleFor(book => book.Cover).NotNull();
    }
}

