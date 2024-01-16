using MediatR;
using Microsoft.AspNetCore.Mvc;
using WhiteBear.Application;
using WhiteBear.Application.Books.Commands;
using WhiteBear.Application.Bookshelf.Queries;
using WhiteBear.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/books", async ([FromBody] AddBook command, [FromServices] IMediator mediator, 
        CancellationToken cancellationToken = default) => await mediator.Send(command, cancellationToken))
.WithName("AddBook")
.WithOpenApi();

app.MapGet("/bookshelf", async ([FromServices] IMediator mediator,
        CancellationToken cancellationToken = default) => await mediator.Send(new GetBooksOnBookshelf(), cancellationToken))
    .WithName("GetBooks")
    .WithOpenApi();
await app.RunAsync();
