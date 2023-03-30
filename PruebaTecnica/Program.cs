using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Interfaces;
using PruebaTecnica.Models;
using PruebaTecnica.Repositories;
using PruebaTecnica.Unit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
