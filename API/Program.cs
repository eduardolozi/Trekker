using API.Extensions;
using Application.Extensions;
using DotNetEnv;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

Env.Load("../.env");

builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddApi();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();