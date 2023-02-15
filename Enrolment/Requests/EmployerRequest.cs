using Enrolment.Models;

namespace Enrolment.Requests
{
    public class EmployerRequest
    {
        public string BusinessName { get; set; }
        public string BusinessNumber { get; set; }
        public string Signature { get; set; }
        public DateTime DeclarationDate { get; set; }
        public string FundName { get; set; }
        public string SuperannuationProductIdentificationNumber { get; set; }
        public string FundPhone { get; set; }
        public string FundWebsite { get; set; }
        public string DateValidChoice { get; set; }
        public string ActDateValidChoice { get; set; }
    }
}
