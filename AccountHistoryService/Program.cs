using FraudDetectionWorkflow.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapPost("/api/check", ([FromBody] Transaction transaction) =>
{
    // Simulate checking account history
    bool isFraud = transaction.AccountId.StartsWith("FRD"); // Example logic
    Console.WriteLine($"accounthistoryservice check: Transaction Id: {transaction.TransactionId}, Is Fraud: {isFraud}");
    return Results.Ok(isFraud);
});

await app.RunAsync();