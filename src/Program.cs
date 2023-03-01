using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HAService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/{hook}", async (string hook, JsonDocument content, [FromServices] HAService hAService) => {
    var parsedDevice = JsonSerializer.Deserialize<Device>(content);
    var entities = DeviceMapper.MapDeviceToEntity(parsedDevice);
    await hAService.PostToHA(entities);
})
.WithOpenApi();

app.Run();