using static Enrolment.Enums.CommonEnum;

namespace Enrolment.ModelViews
{
    public class PayerModel
    {
        public string BusinessNumber { get; set; }
        public string BranchNumber { get; set; }
        public int AppliedForNumber { get; set; }
        public string LegalName { get; set; }
        public string BusinessAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string BusinessPhone { get; set; }
        public int MakePayment { get; set; }
        public string? Signature { get; set; }
        public string? DeclarationDate { get; set; }
    }
}
