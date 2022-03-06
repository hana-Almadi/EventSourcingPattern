using System;

namespace account_event_sourcing.Domain.Event
{
    public abstract class DomainEvent
    {
        public long SequenceId { get; set; }

        public DateTime CreateDate { get; set; }

        public string EventJson { get; set; }

        public Guid AccountAggergateId { get; set; }
        public virtual AccountAggergate AccountAggergate { get; set; }
        public DomainEvent(){}
        public DomainEvent(Guid accountAggergateId)
        {
            AccountAggergateId = accountAggergateId;
            CreateDate = DateTime.Now;
        }

        public abstract void Process();


    }
}
