using AutoMapper;
using AccountsManager.Services.Core.Service.Contracts;
using AccountsManager.Services.Core.Data.Contracts;
using AccountsManager.Services.Core.Data.Repositories;
using AccountsManager.Services.Core.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace AccountsManager.Services.Core.Service.Services
{
    public class TransactionService : ITransactionService
    {
        #region Properties
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TransactionRepo _transactionRepo;
        private readonly IRequestInfoService _requestInfoService;

        public TransactionService(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  IRequestInfoService requestInfoService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _transactionRepo = _unitOfWork.TransactionRepo;
            _requestInfoService = requestInfoService;
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
