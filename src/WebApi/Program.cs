using WhiteBear.Application;
using WhiteBear.Application.Books.Commands;
using WhiteBear.Application.Bookshelf.Queries;
using WhiteBear.Infrastructure;
using WhiteBear.WebApi.Exceptions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

#if Linux
builder.Host.UseSystemd();
#endif
#if Windows
builder.Host.UseWindowsService(options =>
{
    options.ServiceName = ServiceConstants.ServiceName;
});
#endif

builder.Logging.AddSeq();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
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

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();
app.MapInfrastructure();

app.MapPost("/books", async ([FromBody] AddBook command, [FromServices] IMediator mediator, 
        CancellationToken cancellationToken = default) => await mediator.Send(command, cancellationToken))
.WithName("AddBook")
.WithOpenApi();

app.MapGet("/bookshelf", async ([FromServices] IMediator mediator,
        CancellationToken cancellationToken = default) => await mediator.Send(new GetBooksOnBookshelf(), cancellationToken))
    .WithName("GetBooks")
    .WithOpenApi();

app.MapGet("/version", async (CancellationToken cancelationToken = default)  =>
{
    var productName = ServiceConstants.ServiceName;
    var version = ServiceConstants.ServiceVersion;
    var result = $"{productName}, version: {version}";

    return await Task.FromResult(result);
});

await app.RunAsync();
