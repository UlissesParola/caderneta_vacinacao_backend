using FastEndpoints;
using FastEndpoints.Swagger;
using Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerDocumentation();
builder.Services.AddFastEndpoints();
builder.Services.AddIoC();
builder.Services.AddIdentityConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration);

DapperExtension.AddDapperTypeHandlers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwaggerDocumentation();
app.UseFastEndpoints();

app.Run();


