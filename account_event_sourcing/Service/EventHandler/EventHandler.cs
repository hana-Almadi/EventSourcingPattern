

using account_event_sourcing.Domain.Event;
using System.Threading.Tasks;

namespace account_event_sourcing.Service
{
    public interface EventHandler<T>
    {
        public Task<DomainEvent> EventProcessing(T t);
        public Task SaveEvent(T t);

    }
}
