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

app.MapPost("/api/analyze", ([FromBody] Transaction transaction) =>
{
    // Simulate running ML analysis
    bool isFraud = new Random().Next(0, 2) == 1; // Randomized result for example

    Console.WriteLine($"mlanalysisservice analyze: Transaction Id: {transaction.TransactionId}, Is Fraud: {isFraud}");
    return Results.Ok(isFraud);
});

await app.RunAsync();