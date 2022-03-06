
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace account_event_sourcing.Domain.Event
{
    public class AccountTransferEvent:DomainEvent
    {
        [NotMapped]
        public int Money { get; set; }
        [NotMapped]
        public Account Account { get; set; }

        private AccountTransferEvent() : base()
        {
        }

        public AccountTransferEvent(int money, Account account,
            Guid accountAggergateId) :base(accountAggergateId)
        {
            Money = money;
            Account = account;
        }

        public override void Process()
        {
            Account.WithdrawtMoney(Money);
        }
    }
}
