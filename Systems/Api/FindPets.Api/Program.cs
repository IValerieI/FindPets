using FindPets.Api;
using FindPets.Api.Configuration;
using FindPets.Context;
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

services.AddAppDbContext(builder.Configuration);

services.AddAppVersioning();
services.AddAppHealthChecks();
services.AddAppSwagger(mainSettings, swaggerSettings);
services.AddAppAutoMappers();

services.AddAppControllerAndViews();

services.RegisterAppServices();



var app = builder.Build();

app.UseStaticFiles();

app.UseAppControllerAndViews();
app.UseAppSwagger();


app.UseAppCors();
app.UseAppHealthChecks();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true, true);

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Run();
