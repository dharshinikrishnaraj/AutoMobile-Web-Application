using AutoMob_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using AutoMob_WebAPI.Repository;
using AutoMob_WebAPI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register the DbContext with the services container
builder.Services.AddDbContext<VehicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleDbContext")));

//Register the repository with the services container
builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

//Add Exception Middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
