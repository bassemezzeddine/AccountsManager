using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Contracts;
using System;
namespace AccountsManager.Services.Core.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties
        private readonly CoreDbContext context;
        private bool disposed = false;

        public UnitOfWork(CoreDbContext coreDbContext)
        {
            context = coreDbContext;
        }
        #endregion

        #region Repositories
        public AccountRepo AccountRepo
        {
            get
            {
                return new AccountRepo(context);
            }
        }
        public CustomerRepo CustomerRepo
        {
            get
            {
                return new CustomerRepo(context);
            }
        }
        public StatusRepo StatusRepo
        {
            get
            {
                return new StatusRepo(context);
            }
        }
        public TransactionRepo TransactionRepo
        {
            get
            {
                return new TransactionRepo(context);
            }
        }
        #endregion

        #region Methods
        public void Save()
        {
            context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}