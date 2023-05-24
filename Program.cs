using Microsoft.EntityFrameworkCore;
using Prodaja_kruha_backend.Data;
using Prodaja_kruha_backend.Data.Repositories;
using Prodaja_kruha_backend.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllHeadersAndMethods",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

app.UseCors("AllowAllHeadersAndMethods");



// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();
