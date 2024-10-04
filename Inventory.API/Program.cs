using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Inventory.API.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<InventoryAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventroryConnectionString") ?? throw new InvalidOperationException("Connection string 'InventoryAPIContext' not found.")));
builder.Services.AddApplicationInsightsTelemetry();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
