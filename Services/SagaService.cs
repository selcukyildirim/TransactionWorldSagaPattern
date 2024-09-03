using System.Threading.Tasks;

public class SagaService
{
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

    private async Task ApprovePayment(string transactionId)
    {
        // Ödeme onaylama işlemi burada gerçekleştirilir
        await Task.Delay(100); // Simüle edilen işlem
        Console.WriteLine($"Payment approved for transaction: {transactionId}");
    }

    private async Task ExecuteTransfer(string transactionId)
    {
        // Transfer işlemi burada gerçekleştirilir
        await Task.Delay(100); // Simüle edilen işlem
        Console.WriteLine($"Transfer executed for transaction: {transactionId}");
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
