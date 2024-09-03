using System.Threading.Tasks;

public class WebhookService : IWebhookService
{
    public async Task<string> GetCustomerWebhookUrl(string transactionId)
    {
        // Simulate fetching a webhook URL from a data store or configuration
        await Task.Delay(100); // Simulated async operation
        return "https://example.com/webhook"; // Example webhook URL
    }
}
