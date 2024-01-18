using MediatR;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using WhiteBear.Application;
using WhiteBear.Application.Books.Commands;
using WhiteBear.Application.Bookshelf.Queries;
using WhiteBear.Infrastructure;
using WhiteBear.WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSeq();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 1024 * 1024;
    options.ResponseBodyLogLimit = 1024 * 1024;
    options.CombineLogs = false;
});


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.Add("request", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
        });

builder.Services.AddExceptionHandler<ExceptionsHandler>();

var app = builder.Build();

app.UseHttpLogging();
app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.MapPost("/books", async ([FromBody] AddBook command, [FromServices] IMediator mediator, 
        CancellationToken cancellationToken = default) => await mediator.Send(command, cancellationToken))
.WithName("AddBook")
.WithOpenApi();

app.MapGet("/bookshelf", async ([FromServices] IMediator mediator,
        CancellationToken cancellationToken = default) => await mediator.Send(new GetBooksOnBookshelf(), cancellationToken))
    .WithName("GetBooks")
    .WithOpenApi();
await app.RunAsync();
