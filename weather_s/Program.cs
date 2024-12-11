var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache(); 
builder.Services.AddSingleton<Service.CachedResponseService>();
builder.Services.AddSingleton<Service.GetWeather>(); 
builder.Services.AddSingleton<Service.ICache, Service.CachedResponseService>(); 


builder.Services.AddHttpClient(); 

var app = builder.Build();

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