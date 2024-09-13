using FraudDetectionWorkflow;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/notify", ([FromBody] FraudDetectionResult fraudDetectionResult) =>
{
    Console.WriteLine($"notificationservice sending notification: Transaction ID: {fraudDetectionResult.TransactionId}, Is Fraud: {fraudDetectionResult.IsFraud}");
    return Results.Ok(fraudDetectionResult);
});

await app.RunAsync();
