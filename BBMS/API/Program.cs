global using BBMS.Repository;
global using BBMS.Repository.Data;
global using BBMS.Repository.Interfaces;
 
using BBMS.Services;
using BBMS.Services.Interfaces;
using Services;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBeerService, BeerService>();
builder.Services.AddScoped<IBeerRepository, BeerRepository>();

builder.Services.AddScoped<IBreweryService, BreweryService>();
builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();

builder.Services.AddScoped<IBarService, BarService>();
builder.Services.AddScoped<IBarRepository, BarRepository>(); 

builder.Services.AddScoped<IBarBeerService, BarBeerService>();
builder.Services.AddScoped<IBarBeersRepository, BarBeersRepository>();

builder.Services.AddScoped<IBreweryBeerService, BreweryBeerService>();
builder.Services.AddScoped<IBreweryBeerRepository, BreweryBeerRepository>();

builder.Services.AddDbContext<DataContext>();

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
