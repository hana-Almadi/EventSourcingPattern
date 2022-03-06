using account_event_sourcing.Controllers.Dto;
using account_event_sourcing.Domain.Event;
using account_event_sourcing.Repository;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace account_event_sourcing.Service.EventHandler
{
    public class AccountEventService
    {
        private readonly DomainEventRepository _repository;

        private readonly EventHandler<DomainEvent> _eventHandler;

        private readonly IMapper _mapper;


        public AccountEventService(DomainEventRepository repository,
            EventHandler<DomainEvent> eventHandler, IMapper mapper)
        {
            _repository = repository;
            _eventHandler = eventHandler;
            _mapper = mapper;
        }
        public async Task<EventDto> EventReset(Guid accountAggergateId)
        {
            DomainEvent currnetEvent = null;
            var events = await _repository.
                FindAllByaccountAggergateId(accountAggergateId);
            foreach (var @event in events) 
                currnetEvent = _eventHandler.EventProcessing(@event).Result;
            return _mapper.Map<DomainEvent, EventDto> (currnetEvent);

        }

    }
}
