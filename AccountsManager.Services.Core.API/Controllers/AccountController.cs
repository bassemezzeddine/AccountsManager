using AccountsManager.Services.Core.API.Contracts;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Service.Models.Custom;
using AccountsManager.Services.Core.Service.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AccountsManager.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        #region Properties
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly IResponseHelper _responseHelper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService,
                                 ICustomerService customerService,           
                                 IResponseHelper responseHelper,
                                 ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _customerService = customerService;
            _responseHelper = responseHelper;
            _logger = logger;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get list of customers
        /// </summary>
        /// <param name="request">BaseRequest request</param>
        /// <returns>The list of customers</returns>
        [HttpPost("GetCustomers")]
        public ActionResult<APIResponse> GetCustomers(BaseRequest request)
        {
            try
            {
                var result = _customerService.GetCustomers(request.PageSize, request.PageNumber);
                return new APIResponse
                {
                    Data = result,
                    Status = result != null ? (int)APIResponseEnum.Success : (int)APIResponseEnum.Failure
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return _responseHelper.GenerateExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="request">CreateCustomerRequest request</param>
        /// <returns>The created customer ID</returns>
        [HttpPost("CreateCustomer")]
        public ActionResult<APIResponse> CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                var result = _customerService.CreateCustomer(request.Name, request.Surname);
                return new APIResponse
                {
                    Data = result,
                    Status = result != null ? (int)APIResponseEnum.Success : (int)APIResponseEnum.Failure
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return _responseHelper.GenerateExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Get customer info
        /// </summary>
        /// <param name="request">GetCustomerInfoRequest request</param>
        /// <returns>The customer info</returns>
        [HttpPost("GetCustomerInfo")]
        public ActionResult<APIResponse> GetCustomerInfo(GetCustomerInfoRequest request)
        {
            try
            {
                var result = _customerService.GetCustomerInfo(request.CustomerId);
                return new APIResponse
                {
                    Data = result,
                    Status = result != null ? (int)APIResponseEnum.Success : (int)APIResponseEnum.Failure
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return _responseHelper.GenerateExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Create new customer account
        /// </summary>
        /// <param name="request">CreateAccountRequest request</param>
        /// <returns>The created account number</returns>
        [HttpPost("CreateAccount")]
        public ActionResult<APIResponse> CreateAccount(CreateAccountRequest request)
        {
            try
            {
                var result = _accountService.CreateAccount(request.CustomerId, request.AccountDescription, request.InitialCredit);
                return new APIResponse
                {
                    Data = result,
                    Status = result != null ? (int)APIResponseEnum.Success : (int)APIResponseEnum.Failure
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return _responseHelper.GenerateExceptionResponse(ex);
            }
        }

        /// <summary>
        /// Add new account transaction
        /// </summary>
        /// <param name="request">AddAccountTransactionRequest request</param>
        /// <returns>The created transaction reference</returns>
        [HttpPost("AddAccountTransaction")]
        public ActionResult<APIResponse> AddAccountTransaction(AddAccountTransactionRequest request)
        {
            try
            {
                var result = _accountService.AddAccountTransaction(request.CustomerId, 
                                                                   request.AccountNumber, 
                                                                   request.TransactionDescription, 
                                                                   request.Amount);
                return new APIResponse
                {
                    Data = result,
                    Status = result != null ? (int)APIResponseEnum.Success : (int)APIResponseEnum.Failure
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, string.Empty);
                return _responseHelper.GenerateExceptionResponse(ex);
            }
        }
        #endregion
    }
}