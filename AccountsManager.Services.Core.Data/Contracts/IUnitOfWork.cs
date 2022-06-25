using AccountsManager.Services.Core.Data.Repositories;
using System;

namespace AccountsManager.Services.Core.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        new void Dispose();
        AccountRepo AccountRepo { get; }
        CustomerRepo CustomerRepo { get; }
        StatusRepo StatusRepo { get; }
        TransactionRepo TransactionRepo { get; }
    }
}
