using MediatR;
using Microsoft.EntityFrameworkCore;
using MyMediatR.API.Application.Behaviors;
using MyMediatR.Data.Context;
using MyMediatR.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(x => x.UseInMemoryDatabase("MyDatabase"));

// Register MediatR services
builder.Services.AddMediatR(typeof(Program));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MyLoggingBehavior<,>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICustomerRepository, CustomersRepository>();
builder.Services.AddTransient<ICachedCustomers, CachedCustomers>();

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
