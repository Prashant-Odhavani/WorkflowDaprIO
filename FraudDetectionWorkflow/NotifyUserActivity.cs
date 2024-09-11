using Dapr.Client;
using Dapr.Workflow;
using Newtonsoft.Json;

namespace FraudDetectionWorkflow
{
    public class NotifyUserActivity : WorkflowActivity<FraudDetectionResult, bool>
    {
        private readonly DaprClient _daprClient;

        public NotifyUserActivity(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        public override async Task<bool> RunAsync(WorkflowActivityContext context, FraudDetectionResult input)
        {
            Console.WriteLine($"NotifyUserActivity received transaction input {JsonConvert.SerializeObject(input)}");
            // Call NotificationService
            return await _daprClient.InvokeMethodAsync<FraudDetectionResult, bool>(
                HttpMethod.Post, "notificationservice", "/api/notify", input);
        }
    }
}
