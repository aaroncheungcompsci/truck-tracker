using System.Globalization;
using Microsoft.EntityFrameworkCore;
using trucktracker.core.Interfaces;
using trucktracker.core.Services;
using trucktracker.data;
using trucktracker.data.Interfaces;
using trucktracker.data.Repositories;

/// <summary>
/// Main file that holds everything in the 3 layers together.
/// </summary>

var allowedOrigins = "_allowedOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddPolicy(name: allowedOrigins, 
        policy => {
            policy.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
}); //add more to this once webhosting is available
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Change connection string according to what the SQL server is. Specify connection strings in appsettings.json found within same directory
builder.Services.AddDbContext<TruckTrackerContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("local-db-laptop")));

//Services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<ITruckHistoryService, TruckHistoryService>();

//Repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ITruckHistoryRepository, TruckHistoryRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowedOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
