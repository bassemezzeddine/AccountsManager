using AccountsManager.Services.Core.Data.Contexts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsManager.Services.Core.Data.Repositories
{
    public class StatusRepo : GenericRepo<Status>, IStatusRepo
    {
        #region Properties
        public StatusRepo(CoreDbContext context)
            : base(context)
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
