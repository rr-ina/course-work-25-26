using Microsoft.EntityFrameworkCore;
using Cinema.API.Data;
using Cinema.API.Repositories.Interfaces;
using Cinema.API.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CinemaDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IMovieRepo, MovieRepo>();
builder.Services.AddScoped<IHallRepo, HallRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();
builder.Services.AddScoped<ITiketRepo, TiketRepo>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
