using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class SagaService
{
    private readonly ITransferService _transferService;
    private readonly IWebhookService _webhookService;
    private readonly HttpClient _httpClient;

    public SagaService(ITransferService transferService, IWebhookService webhookService, HttpClient httpClient)
    {
        _transferService = transferService;
        _webhookService = webhookService;
        _httpClient = httpClient;
    }

    public async Task StartSagaAsync(string transactionId)
    {
        var saga = new SagaWorkflow();

        saga.AddStep(async () => await ApprovePayment(transactionId));
        saga.AddStep(async () => await ExecuteTransfer(transactionId));

        saga.OnCompensation(async (step) =>
        {
            if (step == "ApprovePayment")
                await ReversePayment(transactionId);
            else if (step == "ExecuteTransfer")
                await RefundTransfer(transactionId);
        });

        await saga.ExecuteAsync();
    }

    public async Task ExecuteTransfer(string transactionId)
    {
        var result = await _transferService.Execute(transactionId);
        if (!result)
            throw new Exception("Transfer işlemi başarısız.");

        await SendWebhookNotification(transactionId, "Transfer Başarılı");
    }

    private async Task SendWebhookNotification(string transactionId, string status)
    {
        var webhookUrl = await _webhookService.GetCustomerWebhookUrl(transactionId);
        var content = new StringContent(JsonConvert.SerializeObject(new { TransactionId = transactionId, Status = status }), Encoding.UTF8, "application/json");
        await _httpClient.PostAsync(webhookUrl, content);
    }

    private async Task ApprovePayment(string transactionId)
    {
        // Ödeme onaylama işlemi burada gerçekleştirilir
        await Task.Delay(100); // Simüle edilen işlem
        Console.WriteLine($"Payment approved for transaction: {transactionId}");
    }

    private async Task ReversePayment(string transactionId)
    {
        // Ödemeyi geri alma işlemi burada gerçekleştirilir
        await Task.Delay(100); // Simüle edilen işlem
        Console.WriteLine($"Payment reversed for transaction: {transactionId}");
    }

    private async Task RefundTransfer(string transactionId)
    {
        // Transfer iadesi işlemi burada gerçekleştirilir
        await Task.Delay(100); // Simüle edilen işlem
        Console.WriteLine($"Transfer refunded for transaction: {transactionId}");
    }
}
