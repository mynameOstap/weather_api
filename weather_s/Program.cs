using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache(); 
builder.Services.AddScoped<Service.CachedResponseService>();
builder.Services.AddScoped<Service.ICache, Service.CachedResponseService>();
builder.Services.AddScoped<Service.GetWeather>();
var configuration = builder.Configuration;

builder.Services.AddHttpClient(); 
builder.Services.AddDbContext<WeatherContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
    )
);

var app = builder.Build();
using(var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WeatherContext>();
    context.Database.Migrate();  
}

// Налаштування конвеєра HTTP-запитів
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Підключити маршрути контролерів
app.MapControllers();

app.Run();