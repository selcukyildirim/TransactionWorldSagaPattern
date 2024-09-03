using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;
    private readonly IMessageQueue _messageQueue;
    private readonly IAccountService _accountService;

    public TransferController(ITransferService transferService, IMessageQueue messageQueue, IAccountService accountService)
    {
        _transferService = transferService;
        _messageQueue = messageQueue;
        _accountService = accountService;
    }

    [HttpPost("start-transfer")]
    public async Task<IActionResult> StartTransfer([FromBody] TransferRequest request)
    {
        var validationResult = await _accountService.Validate(request.SenderAccountId);
        if (!validationResult.IsValid)
            return BadRequest(new { Message = "Hesap doğrulaması başarısız." });

        var transactionId = await _transferService.InitiateTransfer(request);
        await _messageQueue.PublishAsync("transferQueue", new { TransactionId = transactionId });

        return Accepted(new { TransactionId = transactionId, Status = "Transfer Başlatıldı" });
    }
}
