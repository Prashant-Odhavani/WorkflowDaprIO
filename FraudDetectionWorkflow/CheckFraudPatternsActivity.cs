using Dapr.Client;
using Dapr.Workflow;
using FraudDetectionWorkflow.Models;
using Newtonsoft.Json;

namespace FraudDetectionWorkflow
{
    public class CheckFraudPatternsActivity : WorkflowActivity<Transaction, bool>
    {
        private readonly DaprClient _daprClient;
        public CheckFraudPatternsActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        public override async Task<bool> RunAsync(WorkflowActivityContext context, Transaction input)
        {
            Console.WriteLine($"CheckFraudPatternsActivity received transaction input {JsonConvert.SerializeObject(input)}");
            // Call FraudPatternService
            return await _daprClient.InvokeMethodAsync<Transaction, bool>(
                HttpMethod.Post, "fraudpatternservice", "api/check", input);
        }
    }
}
