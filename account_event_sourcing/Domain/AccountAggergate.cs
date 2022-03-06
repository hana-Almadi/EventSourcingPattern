using account_event_sourcing.Domain.Event;
using System;
using System.Collections.Generic;
using System.Linq;

namespace account_event_sourcing.Domain
{
    public class AccountAggergate
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid AccountNumber { get; set; }
        public string OwnerName { get; set; }
        public string OwnerID { get; set;}
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Account> AccountList { get; set; }

        public virtual ICollection<DomainEvent> DomainEvent { get; set;}

        public AccountAggergate(string ownerName, string ownerID,
            string ownerEmail,string ownerPhone,Guid accountNumber)
        {
            Id = Guid.NewGuid();
            AccountList = new List<Account>();
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            OwnerID = ownerID;
            OwnerEmail = ownerEmail;
            OwnerPhone = ownerPhone;
        }
        public void AddAccount(Account account)
        {
            AccountList.Add(account);
            UpdateDate = DateTime.Now;
        }
        public Account GetCurrentStatus()
        {
            return AccountList.LastOrDefault();
        }

    }
}
