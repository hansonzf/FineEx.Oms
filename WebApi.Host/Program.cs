using WebApi.Host;

var builder = WebApplication.CreateBuilder(args);
builder.Host.AddAppSettingsSecretsJson()
    .UseAutofac();
await builder.AddApplicationAsync<WebApiHostModule>();
var app = builder.Build();
await app.InitializeApplicationAsync();
await app.RunAsync();
