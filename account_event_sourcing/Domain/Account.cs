using account_event_sourcing.Domain.Enum;
using System;


namespace account_event_sourcing.Domain
{
    public class Account
    {
        public long AccountId { get; set; }
        public int Money { get; set; }
        public DateTime CreateDate;
        public ActionType ActionType { get; set; }
        public Guid AccountAggergateId { get; set; }
        public virtual AccountAggergate AccountAggergate { get; set; }

        public Account(int money, Guid accountAggergateId)
        {
            CreateDate = DateTime.Now;
            Money = money;
            AccountAggergateId= accountAggergateId;
        }

        public Account()
        {
            CreateDate = DateTime.Now;
            ActionType = ActionType.Create;
            Money = 0;
        }
        public void DepositMoney(int amount)
        {
            Money+= amount;
            ActionType = ActionType.Deposit;

        }
        public void WithdrawtMoney(int amount)
        {
            if (amount != 0)
            {
                Money -= amount;
                ActionType = ActionType.Transfer;
            }

        }
    }
}
