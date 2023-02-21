﻿using static Enrolment.Enums.CommonEnum;

namespace Enrolment.Requests
{
    public class SubmitPayeeRequest
    {
        public string EmailRegister { get; set; }
        public string TaxFileNumber { get; set; }
        public NameTitleEnum NameTitle { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string? OtherName { get; set; }
        public string HomeAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string? PreviousFamilyName { get; set; }
        public string Email { get; set; }
        public int DayOfBirth { get; set; }
        public int MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public PaidBasisEnum PaidBasis { get; set; }
        public ResidencyStatusEnum ResidencyStatus { get; set; }
        public YesNoEnum ClaimTaxFree { get; set; }
        public YesNoEnum HaveLoanProgram { get; set; }
        public string? Signature { get; set; }
        public string? DeclarationDate { get; set; }
        public string? FilePath { get; set; }
    }
}
