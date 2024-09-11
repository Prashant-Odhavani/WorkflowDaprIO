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
    // Log the incoming fraud detection result to the console

    bool isFraud = transaction.Amount > 1000; // Example logic
    Console.WriteLine($"fraudpatternservice check: Transaction Id: {transaction.TransactionId}, Is Fraud: {isFraud}");
    return Results.Ok(isFraud);
});

await app.RunAsync();
