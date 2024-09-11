using Dapr.Client;
using Dapr.Workflow;
using FraudDetectionWorkflow.Models;
using Newtonsoft.Json;

namespace FraudDetectionWorkflow
{
    public class RunMLAnalysisActivity : WorkflowActivity<Transaction, bool>
    {
        private readonly DaprClient _daprClient;

        public RunMLAnalysisActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }
        public override async Task<bool> RunAsync(WorkflowActivityContext context, Transaction input)
        {
            Console.WriteLine($"RunMLAnalysisActivity received transaction input {JsonConvert.SerializeObject(input)}");
            // Call MLAnalysisService
            return await _daprClient.InvokeMethodAsync<Transaction, bool>(
                HttpMethod.Post, "mlanalysisservice", "api/analyze", input);
        }
    }
}
