using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly IDatabase _database;

    public AccountService(IDatabase database)
    {
        _database = database;
    }

    public async Task<ValidationResult> Validate(string accountId)
    {
        var account = await _database.GetAccountByIdAsync(accountId);

        if (account == null)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Hesap bulunamadı."
            };
        }

        if (account.Balance <= 0)
        {
            return new ValidationResult
            {
                IsValid = false,
                ErrorMessage = "Yetersiz bakiye."
            };
        }

        return new ValidationResult { IsValid = true };
    }
}
