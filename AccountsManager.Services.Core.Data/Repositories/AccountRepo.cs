using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Models;

namespace AccountsManager.Services.Core.Data.Repositories
{
    public class AccountRepo : GenericRepo<Account>, IAccountRepo
    {
        #region Properties
        public AccountRepo(CoreDbContext context)
            : base(context)
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
