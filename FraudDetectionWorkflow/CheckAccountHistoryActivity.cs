using Dapr.Client;
using Dapr.Workflow;
using FraudDetectionWorkflow.Models;
using Newtonsoft.Json;

namespace FraudDetectionWorkflow
{
    public class CheckAccountHistoryActivity : WorkflowActivity<Transaction, bool>
    {
        private readonly DaprClient _daprClient;

        public CheckAccountHistoryActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        public override async Task<bool> RunAsync(WorkflowActivityContext context, Transaction input)
        {
            Console.WriteLine($"CheckAccountHistoryActivity received transaction input {JsonConvert.SerializeObject(input)}");
            // Call AccountHistoryService
            return await _daprClient.InvokeMethodAsync<Transaction, bool>(
                HttpMethod.Post, "accounthistoryservice", "api/check", input);
        }
    }
}
