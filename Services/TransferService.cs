using System.Threading.Tasks;

public class TransferService : ITransferService
{
    public async Task<string> InitiateTransfer(TransferRequest request)
    {
        // Transfer başlatma işlemleri burada yapılır
        return "transaction-id-123"; // örnek işlem kimliği
    }

    public async Task<bool> ExecuteTransfer(string transactionId)
    {
        // Transfer işlemi tamamlanır
        return true; // işlem başarılı
    }
}
