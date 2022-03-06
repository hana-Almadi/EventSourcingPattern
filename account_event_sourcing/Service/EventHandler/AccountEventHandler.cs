using account_event_sourcing.Domain.Event;
using account_event_sourcing.Repository;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace account_event_sourcing.Service
{
    public class AccountEventHandler : EventHandler<DomainEvent>
    {
        private readonly Repository<DomainEvent,long> _repository;

        public AccountEventHandler(Repository<DomainEvent,long> repository)
        {
            _repository = repository;
        }
        public Task<DomainEvent> EventProcessing(DomainEvent domainEvent)
        {
            DomainEvent currentEvent = (DomainEvent)JsonConvert.DeserializeObject
                (domainEvent.EventJson, domainEvent.GetType());
             currentEvent.Process();
            return Task.FromResult(currentEvent);
        }
        public  async Task SaveEvent(DomainEvent domainEvent)
        {
            domainEvent.EventJson = JsonConvert.SerializeObject(domainEvent,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            await _repository.Save(domainEvent);

        }
    }
}
