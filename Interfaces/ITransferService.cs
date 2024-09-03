using System.Threading.Tasks;

public interface ITransferService
{
    Task<string> InitiateTransfer(TransferRequest request);
    Task<bool> ExecuteTransfer(string transactionId);
}
