using Confluent.Kafka;
using KafkaPlayground.Configuration;
using KafkaPlayground.Producer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder();

configBuilder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = configBuilder.AddEnvironmentVariables()
    .Build();

builder.Services.Configure<UserEventsConfiguration>(configuration.GetSection(nameof(UserEventsConfiguration)));
builder.Services.Configure<VehicleEventsConfiguration>(configuration.GetSection(nameof(VehicleEventsConfiguration)));
builder.Services.AddScoped<IProducer, UserEventsProducer>();
builder.Services.AddScoped<IProducer, VehicleEventsProducer>();
builder.Services.AddScoped<UserEventsProducer>();
builder.Services.AddScoped<VehicleEventsProducer>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messages Api", Version = "v1" });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messages Api v1"));

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
