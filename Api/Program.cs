using FastEndpoints;
using FastEndpoints.Swagger;
using Infra.Data.Context;
using Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerDocumentation();
builder.Services.AddFastEndpoints();
builder.Services.AddIoC();
builder.Services.AddIdentityConfiguration();
builder.Services.AddDatabaseConfiguration(builder.Configuration, builder.Environment);

DapperExtension.AddDapperTypeHandlers();

var app = builder.Build();

app.MigrateDatabase<AppDbContext>();

app.UseHttpsRedirection();
app.UseSwaggerDocumentation();
app.UseFastEndpoints();

app.Run();

public partial class Program { }


