using Dapr.Client;
using Dapr.Workflow;
using FraudDetectionWorkflow;
using FraudDetectionWorkflow.Models;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDaprClient();
builder.Services.AddDaprWorkflow(options =>
{
    // Register the main workflow and activities here
    options.RegisterWorkflow<FraudDetectionWorkflow.FraudDetectionWorkflow>();
    options.RegisterActivity<CheckFraudPatternsActivity>();
    options.RegisterActivity<RunMLAnalysisActivity>();
    options.RegisterActivity<CheckAccountHistoryActivity>();
    options.RegisterActivity<NotifyUserActivity>();
});

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/transaction/start", async (DaprClient daprClient, Transaction transaction) =>
{
    Console.WriteLine($"Transaction: {JsonConvert.SerializeObject(transaction)}");
    // TODO: Update to a stable method when available
    var workflowInstance = await daprClient.StartWorkflowAsync(
        workflowComponent: "dapr",
        workflowName: "FraudDetectionWorkflow",
        instanceId: Guid.NewGuid().ToString(),
        input: transaction);

    return Results.Ok(new { WorkflowInstanceId = workflowInstance });
});

await app.RunAsync();
