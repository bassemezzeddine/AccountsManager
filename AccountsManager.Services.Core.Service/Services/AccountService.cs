using AutoMapper;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Data.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;

namespace AccountsManager.Services.Core.Service.Services
{
    public class AccountService : IAccountService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestInfoService _requestInfoService;
        private readonly IDataProtector _dataProtector;

        public AccountService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IRequestInfoService requestInfoService,
                              IDataProtector dataProtector)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _requestInfoService = requestInfoService;
            _dataProtector = dataProtector;
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
