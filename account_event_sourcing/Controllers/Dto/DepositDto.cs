using System;

namespace account_event_sourcing.Controllers.Dto
{
    public class DepositDto
    {
        public Guid AccountNumber { get; set; }
        public int Money { get; set; }

        public override string ToString()
        {
            return "<AccountNumber> " + AccountNumber + " </AccountNumber>\n"
                 + "<Money> " + Money + " </Money>\n";
        }

    }
}
