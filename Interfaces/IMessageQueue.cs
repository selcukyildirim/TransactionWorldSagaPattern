using System.Threading.Tasks;

public interface IMessageQueue
{
    Task PublishAsync(string queueName, object message);
}
