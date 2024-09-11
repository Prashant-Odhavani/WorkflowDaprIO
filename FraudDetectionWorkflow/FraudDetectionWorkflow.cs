using Dapr.Workflow;
using FraudDetectionWorkflow.Models;
using Newtonsoft.Json;

namespace FraudDetectionWorkflow;

public class FraudDetectionWorkflow : Workflow<Transaction, FraudDetectionResult>
{
    public override async Task<FraudDetectionResult> RunAsync(WorkflowContext context, Transaction input)
    {
        Console.WriteLine($"FraudDetectionWorkflow received transaction input {JsonConvert.SerializeObject(input)}");

        var fraudPatternTask = context.CallActivityAsync<bool>("CheckFraudPatternsActivity", input);
        var mlAnalysisTask = context.CallActivityAsync<bool>("RunMLAnalysisActivity", input);
        var accountHistoryTask = context.CallActivityAsync<bool>("CheckAccountHistoryActivity", input);

        await Task.WhenAll(fraudPatternTask, mlAnalysisTask, accountHistoryTask);

        bool isFraud = await fraudPatternTask || await mlAnalysisTask || await accountHistoryTask;

        var result = new FraudDetectionResult
        {
            TransactionId = input.TransactionId,
            IsFraud = isFraud
        };

        await context.CallActivityAsync("NotifyUserActivity", result);

        return result;
    }
}
