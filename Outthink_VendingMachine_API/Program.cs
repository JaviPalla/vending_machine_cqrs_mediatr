using Microsoft.EntityFrameworkCore;
using Outthink_VendingMachine_API.Database;
using MediatR;
using Outthink_VendingMachine_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// In memory Database
builder.Services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<VendingMachineDb>(opt => opt.UseInMemoryDatabase(databaseName: "OutThink_VendingMachine"), ServiceLifetime.Singleton)
                    .AddSingleton<IVendingMachineDb>(p => p.GetService<VendingMachineDb>());

builder.Services.AddSingleton<ICoinChangeService, CoinChangeService>();

// CQRS pattern with Mediatr
builder.Services.AddMediatR(new Type[]
    {
        // Queries
        typeof(Outthink_VendingMachine_API.Queries.GetProductsQuery),
        typeof(Outthink_VendingMachine_API.Queries.GetCoinsQuery),
        // Commands
        typeof(Outthink_VendingMachine_API.Commands.AddCoinCommand),
        typeof(Outthink_VendingMachine_API.Commands.CancelPurchaseProductCommand),
        typeof(Outthink_VendingMachine_API.Commands.PurchaseProductCommand),
    });

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
