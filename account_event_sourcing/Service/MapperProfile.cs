using account_event_sourcing.Controllers.Dto;
using account_event_sourcing.Domain;
using account_event_sourcing.Domain.Event;
using AutoMapper;

namespace account_event_sourcing.Service
{
    public class MapperProfile:Profile
    {
       public MapperProfile()
        {
            CreateMap<Account, TransactionDto>().ForMember(d=>d.TransactionDate,m=>m.MapFrom(src=> src.CreateDate))
                .ForMember(a=>a.TransactionType,m=>m.MapFrom(src=>src.ActionType.ToString()));
            CreateMap<AccountCreateEvent, EventDto>().ForMember(a => a.CurrnetBalance, m => m.MapFrom(src => 0));
            CreateMap<AccountDepositEvent, EventDto>().ForMember(a => a.CurrnetBalance, m => m.MapFrom(src => src.Account.Money));
            CreateMap<AccountTransferEvent, EventDto>().ForMember(a => a.CurrnetBalance, m => m.MapFrom(src => src.Account.Money));
            CreateMap<DomainEvent, EventDto>();
        }
    }
}
