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

app.MapPost("/api/check", ([FromBody] Transaction transaction) =>
{
    bool isFraud = transaction.AccountId.StartsWith("FRD"); // Simulate checking account history
    Console.WriteLine($"accounthistoryservice checking account history => Transaction Id: {transaction.TransactionId}, Is Fraud: {isFraud}");
    return Results.Ok(isFraud);
});

await app.RunAsync();