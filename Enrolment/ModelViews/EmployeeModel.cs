using static Enrolment.Enums.CommonEnum;

namespace Enrolment.ModelViews
{
    public class EmployeeModel
    {
        public int SuperannuationFund { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string TaxFileNumber { get; set; }
        public string FundName { get; set; }
        public string FundAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string? MemberNo { get; set; }
        public string AccountName { get; set; }
        public string? BusinessNumber { get; set; }
        public string? SuperannuationProductIdentificationNumber { get; set; }
        public string DaytimePhoneNumber { get; set; }
        public int HaveAttached { get; set; }
        public string? Signature { get; set; }
        public string? DeclarationDate { get; set; }
    }
}
