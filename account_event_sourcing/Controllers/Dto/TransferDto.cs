using System;

namespace account_event_sourcing.Controllers.Dto
{
    public class TransferDto
    {
        public Guid FromAccountNumber { get; set; }
        public int Money { get;  set; }
        public Guid ToAccountNumber { get; set; }

        public override string ToString()
        {
            return "<FromAccountNumber> " + FromAccountNumber + " </FromAccountNumber>\n"
                   + "<Money> " + Money + " </Money>\n"
                   +"<ToAccountNumber> " + ToAccountNumber + " </ToAccountNumber>\n";
        }

    }
}
