using JewelryShopAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using JewelryShopAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<JewelryShopAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JewelryShopAPIContext") ?? throw new InvalidOperationException("Connection string 'JewelryShopAPIContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapEmployeeEndpoints();

app.MapCustomerEndpoints();

app.MapCategoryEndpoints();

app.MapJewelryItemEndpoints();

app.MapPurchaseEndpoints();

app.Run();
