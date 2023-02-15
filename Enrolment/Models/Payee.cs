using System.ComponentModel.DataAnnotations.Schema;

namespace Enrolment.Models
{
    [Table("Payees")]
    public class Payee : BaseModel
    {
        public string TaxFileNumber { get; set; }
        public int NameTitle { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string HomeAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string PreviousFamilyName { get; set; }
        public string Email { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public int PaidBasis { get; set; }
        public int ResidencyStatus { get; set; }
        public int ClaimTaxFree { get; set; }
        public int HaveLoanProgram { get; set; }
        public string Signature { get; set; }
        public DateTime DeclarationDate { get; set; }

        public virtual EmailRegister EmailRegister { get; set; }
    }
}
