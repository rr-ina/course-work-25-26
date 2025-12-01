using Microsoft.EntityFrameworkCore;
using Cinema.API.Data;
using Cinema.API.Repositories.Interfaces;
using Cinema.API.Repositories.Implementations;
using Cinema.API.Services;
using Cinema.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

//підключення до бд
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CinemaDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddScoped<IMovieRepo, MovieRepo>();
builder.Services.AddScoped<IHallRepo, HallRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();
builder.Services.AddScoped<ITiketRepo, TiketRepo>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
