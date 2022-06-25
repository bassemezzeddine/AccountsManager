using AccountsManager.Services.Core.API.Contracts;
using AccountsManager.Services.Core.Service.Models.Custom;
using System;

namespace AccountsManager.Services.Core.API.Helpers
{
    public class ResponseHelper : IResponseHelper
    {
        #region Properties
        public ResponseHelper()
        {

        }
        #endregion

        #region Methods
        public string GetExceptionFormattedMessage(Exception ex)
        {
            return String.Join(" | ", ex.Source, ex.Message, ex.InnerException, ex.StackTrace);
        }

        public APIResponse GenerateExceptionResponse(Exception ex)
        {
            return new APIResponse
            {
                Message = "An error has occurred.",
                ErrorMessage = GetExceptionFormattedMessage(ex),
                DisplayMessage = APIResponseMessageEnum.ErrorMessage,
                Status = (int)APIResponseEnum.Exception
            };
        } 
        #endregion
    }
}
