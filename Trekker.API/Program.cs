using DotNetEnv;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Common;
using Trekker.API.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

Env.Load("../.env");

builder.Services.AddKeycloakWebApiAuthentication(options =>
{
    options.Audience = TrekkerEnvironment.KcClientId;
    options.Realm = TrekkerEnvironment.Realm;
    options.SslRequired = "none";
    options.Resource = TrekkerEnvironment.KcClientId;
    options.VerifyTokenAudience = true;
    options.Credentials = new KeycloakClientInstallationCredentials
    {
        Secret = TrekkerEnvironment.KcClientSecret
    };
    options.AuthServerUrl = TrekkerEnvironment.KcAuthServerUrl;
});
builder.Services.AddKeycloakAuthorization(options =>
{
    options.VerifyTokenAudience = true;
    options.Realm = TrekkerEnvironment.Realm;
    options.SslRequired = "none";
    options.Resource = TrekkerEnvironment.KcClientId;
    options.VerifyTokenAudience = true;
    options.Credentials = new KeycloakClientInstallationCredentials
    {
        Secret = TrekkerEnvironment.KcClientSecret
    };
    options.AuthServerUrl = TrekkerEnvironment.KcAuthServerUrl;
});

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