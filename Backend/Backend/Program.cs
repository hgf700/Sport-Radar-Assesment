using Backend.DB;
using Backend.Patterns;
using DotNetEnv;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

builder.Services.AddControllers();
//swagger
builder.Services.AddOpenApi();

//data from .env
//var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
//var db = Environment.GetEnvironmentVariable("POSTGRES_DB");
//var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
//var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
//var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");

//var connectionString =
//  $"Host={host};Port={port};Database={db};Username={user};Password={pass}";

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(connectionString));

//for unit tests
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

//cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Prod",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
    );
});

//rate limit 
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("RateLimitGet", opt =>
    {
        opt.PermitLimit = 30;
        opt.Window = TimeSpan.FromSeconds(2);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 10;
    });

    options.AddFixedWindowLimiter("RateLimitPost", opt =>
    {
        opt.PermitLimit = 3;
        opt.Window = TimeSpan.FromSeconds(10);
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opt.QueueLimit = 2;
    });
});

builder.Services.AddScoped<RetryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting();

//rate limit
app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

//usage cors
app.UseCors("Prod");

app.MapControllers();

app.Run();
