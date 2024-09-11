namespace FraudDetectionWorkflow.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; } = Guid.NewGuid().ToString();
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
