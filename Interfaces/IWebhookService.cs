using System.Threading.Tasks;

public interface IWebhookService
{
    Task<string> GetCustomerWebhookUrl(string transactionId);
}
