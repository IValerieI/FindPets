using FindPets.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger();

var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppVersioning();
services.AddAppHealthChecks();
services.AddAppSwagger();

services.AddControllers();


var app = builder.Build();

app.UseAppSwagger();

app.UseAppCors();

app.UseAppHealthChecks();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Run();
