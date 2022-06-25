using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Models;

namespace AccountsManager.Services.Core.Data.Repositories
{
    public class CustomerRepo : GenericRepo<Customer>, ICustomerRepo
    {
        #region Properties
        public CustomerRepo(CoreDbContext context)
            : base(context)
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
