using Inventory.Business.Implementation;
using Inventory.Business.Interfaces;
using Inventory.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<InventoryAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventroryConnectionString") ?? throw new InvalidOperationException("Connection string 'InventoryAPIContext' not found.")));
builder.Services.AddApplicationInsightsTelemetry();
// Add services to the container.
builder.Services.AddScoped<IInventoryBusiness, InventoryBusiness>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
