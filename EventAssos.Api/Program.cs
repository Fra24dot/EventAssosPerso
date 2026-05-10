using EventAssos.Api.Extensions;
using EventAssos.Security.Extensions;
using EventAssos.Security.Services.Tools;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSecurityServices(builder.Configuration);

builder.Services.ConfigureJwTAuthentication(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // L'URL d'Angular
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Important pour les tokens 
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();
app.MapControllers();

app.Run();
