using System.Threading.Tasks;
using MassTransit;

public class TransferConsumer : IConsumer<TransferRequest>
{
    private readonly ISagaWorkflow _sagaWorkflow;

    public TransferConsumer(ISagaWorkflow sagaWorkflow)
    {
        _sagaWorkflow = sagaWorkflow;
    }

    public async Task Consume(ConsumeContext<TransferRequest> context)
    {
        var transactionId = context.Message.TransactionId;
        await _sagaWorkflow.StartSagaAsync(transactionId);
    }
}
