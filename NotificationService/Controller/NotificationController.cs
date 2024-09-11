using FraudDetectionWorkflow;
using Microsoft.AspNetCore.Mvc;

namespace NotificationService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        [HttpPost("notify")]
        public ActionResult<bool> Notify([FromBody] FraudDetectionResult result)
        {
            // Simulate sending notification
            Console.WriteLine($"Notification sent: Transaction {result.TransactionId} is fraud: {result.IsFraud}");
            return Ok(true);
        }
    }
}
