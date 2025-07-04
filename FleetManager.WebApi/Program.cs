using FleetManager.WebApi.Extensions.Application;
using FleetManager.WebApi.Extensions.Migrations;
using FleetManager.WebApi.Extensions.WebApplication;
using FleetManager.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSwaggerDocumentation();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation(app.Environment);

app.ApplyMigrations();

app.UseMiddleware<ErrorMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
