
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace account_event_sourcing.Domain.Event
{
    public class AccountDepositEvent:DomainEvent
    {
        [NotMapped]
        public int Money { get; set; }
        [NotMapped]
        public Account Account { get; set; }

        public AccountDepositEvent():base()
        {
        }
        public AccountDepositEvent(int money, Account account,
            Guid accountAggergateId) :base(accountAggergateId)
        {
            Money = money;
            Account = account;
        }

        public override void Process()
        {
            Account.DepositMoney(Money);
        }
    }
}
