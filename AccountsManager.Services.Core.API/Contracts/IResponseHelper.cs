using AccountsManager.Services.Core.Service.Models.Custom;
using System;

namespace AccountsManager.Services.Core.API.Contracts
{
    public interface IResponseHelper
    {
        string GetExceptionFormattedMessage(Exception ex);

        APIResponse GenerateExceptionResponse(Exception ex);
    }
}
