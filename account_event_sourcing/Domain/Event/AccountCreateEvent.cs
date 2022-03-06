using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_event_sourcing.Domain.Event
{
    
    public class AccountCreateEvent : DomainEvent
    {
        [NotMapped]
        public string Owner { get; set; }
        [NotMapped]
        public AccountAggergate AccountAggergate { get; set; }
        [NotMapped]
        public Guid AccountNumber { get; set; }
        [NotMapped]
        public string OwnerID { get; set; }
        [NotMapped]
        public string OwnerEmail { get; set; }
        [NotMapped]
        public string OwnerPhone { get;set; }

        private AccountCreateEvent() : base()
        {
        }

        public AccountCreateEvent(string owner, Guid accountNumber,
            string ownerID, string ownerEmail, string ownerPhone,
            Guid accountAggergateId) : base(accountAggergateId)
        {
            Owner = owner;
            AccountNumber = accountNumber;
            OwnerID = ownerID;
            OwnerEmail = ownerEmail;
            OwnerPhone = ownerPhone;
        }


        public override void Process()
        {
            AccountAggergate = new AccountAggergate(Owner, OwnerID, 
                OwnerEmail, OwnerPhone, AccountNumber);
            AccountAggergate.AddAccount(new Account());
        }
    }
}
