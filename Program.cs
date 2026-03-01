using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); 

var connectionString = "Host=postgres;Username=postgres;Password=post123;Database=GameStore.Db";
builder.Services.AddDbContext<GameStorecontext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
