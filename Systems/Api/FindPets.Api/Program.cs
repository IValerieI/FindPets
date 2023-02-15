using FindPets.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddAppLogger();

var services = builder.Services;

services.AddAppHealthChecks();

builder.Services.AddControllers();


var app = builder.Build();

app.UseAppHealthChecks();

// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();

app.Run();
