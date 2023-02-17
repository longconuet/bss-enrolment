using System.ComponentModel.DataAnnotations.Schema;

namespace Enrolment.Models
{
    [Table("Employers")]
    public class Employer : BaseModel
    {
        public string BusinessName { get; set; }
        public string BusinessNumber { get; set; }
        public string Signature { get; set; }
        public DateTime DeclarationDate { get; set; }
        public string FundName { get; set; }
        public string? SuperannuationProductIdentificationNumber { get; set; }
        public string? FundPhone { get; set; }
        public string FundWebsite { get; set; }
        public DateTime DateValidChoice { get; set; }
        public DateTime ActDateValidChoice { get; set; }

        public virtual EmailRegister EmailRegister { get; set; }
    }
}
