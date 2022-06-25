using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Models;

namespace AccountsManager.Services.Core.Data.Repositories
{
    public class TransactionRepo : GenericRepo<Transaction>, ITransactionRepo
    {
        #region Properties
        public TransactionRepo(CoreDbContext context)
            : base(context)
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
