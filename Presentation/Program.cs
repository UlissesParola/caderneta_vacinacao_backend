using Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();


