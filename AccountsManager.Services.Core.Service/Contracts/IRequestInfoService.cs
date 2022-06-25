using AccountsManager.Services.Core.Data.Models;
using System;
using System.Collections.Generic;

namespace AccountsManager.Services.Core.Service.Contracts
{
    public interface IRequestInfoService
    {
        Guid UserId { get; }
    }
}
