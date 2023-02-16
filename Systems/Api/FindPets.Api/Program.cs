using FindPets.Api;
using FindPets.Api.Configuration;
using FindPets.Services.Settings;
using FindPets.Settings;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

// Add services to the container.

builder.AddAppLogger();

var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppVersioning();
services.AddAppHealthChecks();
services.AddAppSwagger(mainSettings, swaggerSettings);

services.AddAppControllerAndViews();

services.RegisterAppServices();


var app = builder.Build();

app.UseAppControllerAndViews();
app.UseAppSwagger();

app.UseAppCors();
app.UseAppHealthChecks();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Run();
