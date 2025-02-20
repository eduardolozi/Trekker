using DotNetEnv;
using Trekker.API.IoC;
using Trekker.Application.IoC;
using Trekker.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);
Env.Load("../.env");

builder.Services.AddApi();
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();