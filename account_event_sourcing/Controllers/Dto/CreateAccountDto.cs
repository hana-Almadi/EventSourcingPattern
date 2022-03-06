

namespace account_event_sourcing.Controllers.Dto
{
    public class CreateAccountDto
    {
        public string AccountOwnerName { get; set; }
        public string AccountOwnerEmail { get; set;}
        public string AccountOwnerPhone { get; set;}
        public string AccountOwnerId{ get; set; }

        public override string ToString()
        {
            return "<AccountOwnerName> "+ AccountOwnerName+ " </AccountOwnerName>\n"
                + "<AccountOwnerEmail> " + AccountOwnerEmail + " </AccountOwnerEmail>\n"
                + "<AccountOwnerPhone> " + AccountOwnerPhone + " </AccountOwnerPhone>\n"
                + "<AccountOwnerId> " + AccountOwnerId + " </AccountOwnerId>\n";
        }

    }
}
