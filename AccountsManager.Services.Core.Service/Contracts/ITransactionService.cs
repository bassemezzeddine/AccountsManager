using AccountsManager.Services.Core.Data.Models;

namespace AccountsManager.Services.Core.Service.Contracts
{
    public interface ITransactionService
    {
        bool AddInitialCredit(int accountId, decimal initialCredit);
        Transaction AddTransaction(int accountId, string description, decimal openingBalance, decimal amount);
    }
}
