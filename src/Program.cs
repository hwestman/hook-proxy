using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<HAService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/{hook}", async (string hook, Device device, [FromServices] HAService hAService) => {
    await hAService.PostToHA(device.ToEntities());
})
.WithOpenApi();

app.Run();