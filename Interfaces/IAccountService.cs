using System.Threading.Tasks;

public interface IAccountService
{
    Task<ValidationResult> Validate(string accountId);
}
