using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using AccountsManager.Services.Core.Data.Models;

namespace AccountsManager.Services.Core.Service.Models.Mapping
{
    public class MapperProfile : Profile
    {
        private readonly IDataProtector _dataProtector;

        public MapperProfile(IDataProtector dataProtector)
        {
            _dataProtector = dataProtector;

            CreateMap<Customer, CustomerListDTO> ()
               .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => _dataProtector.Protect(src.Id.ToString())))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname));

            CreateMap<Customer, CustomerDTO>()
               .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => _dataProtector.Protect(src.Id.ToString())))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
               .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedOn))
               .ForMember(dest => dest.Accounts, opt => opt.MapFrom(src => src.Accounts));

            CreateMap<Account, AccountDTO>()
               .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.AccountBalance, opt => opt.MapFrom(src => src.Balance.ToString("F")))
               .ForMember(dest => dest.Transactions, opt => opt.MapFrom(src => src.Transactions))
               .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<Transaction, TransactionDTO>()
               .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount.ToString("F")))
               .ForMember(dest => dest.Reference, opt => opt.MapFrom(src => src.Reference))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.OpeningBalance, opt => opt.MapFrom(src => src.OpeningBalance.ToString("F")))
               .ForMember(dest => dest.ClosingBalance, opt => opt.MapFrom(src => src.ClosingBalance.ToString("F")))
               .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreatedOn));
        }
    }
}
