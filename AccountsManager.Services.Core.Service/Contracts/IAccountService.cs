using AccountsManager.Services.Core.Service.Models.Responses;

namespace AccountsManager.Services.Core.Service.Contracts
{
    public interface IAccountService
    {
        CreateAccountResponse CreateAccount(string customerId, string accountDescription, decimal initialCredit);
        AddTransactionResponse AddAccountTransaction(string customerId, string accountNumber, string description, decimal amount);
    }
}
