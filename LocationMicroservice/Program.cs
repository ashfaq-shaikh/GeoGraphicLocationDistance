using GeoLocationDemo.Core.Models;
using GeoLocationDemo.Services.IContractService;
using GeoLocationDemo.Services.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MongoDBSettingsSection = builder.Configuration.GetSection("MongoDBSettings");
builder.Services.Configure<MongoDBSettings>(MongoDBSettingsSection);

builder.Services.AddScoped<ILocationService, LocationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
