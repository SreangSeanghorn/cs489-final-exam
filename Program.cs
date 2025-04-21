using AstronautSatelliteAPI.DataPersistence;
using AstronautSatelliteAPI.Repositories;
using AstronautSatelliteAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AstronautSatelliteAPI",
        Version = "v1",
        Description = "API for managing astronauts and satellites."
    });
});
builder.Services.AddScoped<IAstronautRepository, AstronautRepository>();
builder.Services.AddScoped<ISatelliteRepository, SatelliteRepository>();
builder.Services.AddScoped<AstronautService>();
builder.Services.AddScoped<SatelliteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AstronautSatelliteAPI v1");
        c.RoutePrefix = string.Empty; // Makes Swagger UI available at the root URL
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();

