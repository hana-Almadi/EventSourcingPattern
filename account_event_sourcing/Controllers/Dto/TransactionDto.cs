

namespace account_event_sourcing.Controllers.Dto
{
    public class TransactionDto
    {
       public string TransactionType { get; set; }
       public string TransactionDate { get; set; }
       public int Money { get; set; }

        public override string ToString()
        {
            return "<TransactionType> " + TransactionType + " </TransactionType>\n"
                  + "<Money> " + Money + " </Money>\n"
                  + "<TransactionDate> " + TransactionDate + " </TransactionDate>\n";
        }

    }
}
