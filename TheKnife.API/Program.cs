using TheKnife.Services;
using TheKnife.API.Configurations.Documentation;
using TheKnife.API.Configurations.Persistance;
using TheKnife.API.Configurations.Security;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCustomApiDocumentation();
builder.Services.AddCustomApiSecurity(configuration);
builder.Services.AddCustomServiceDependencyRegister(configuration);
builder.Services.AddCustomDatabaseConfiguration(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCustomApiDocumentation();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
