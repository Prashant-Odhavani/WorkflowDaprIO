using FraudDetectionWorkflow.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/analyze", ([FromBody] Transaction transaction) =>
{
    bool isFraud = new Random().Next(0, 2) == 1; // Simulate running ML analysis. Randomized result for example.
    Console.WriteLine($"mlanalysisservice running ML analysis => Transaction Id: {transaction.TransactionId}, Is Fraud: {isFraud}");
    return Results.Ok(isFraud);
});

await app.RunAsync();