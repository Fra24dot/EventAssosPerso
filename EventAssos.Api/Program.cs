using EventAssos.Api.Extensions;
using EventAssos.Security.Extensions;
using EventAssos.Security.Services.Tools;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSecurityServices(builder.Configuration);

builder.Services.ConfigureJwTAuthentication(builder.Configuration);




builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();
app.MapControllers();

app.Run();
