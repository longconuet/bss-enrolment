﻿using System.ComponentModel.DataAnnotations.Schema;
using static Enrolment.Enums.CommonEnum;

namespace Enrolment.Models
{
    [Table("Payers")]
    public class Payer : BaseModel
    {
        public string BusinessNumber { get; set; }
        public string BranchNumber { get; set; }
        public YesNoEnum AppliedForNumber { get; set; }
        public string LegalName { get; set; }
        public string BusinessAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string BusinessPhone { get; set; }
        public YesNoEnum MakePayment { get; set; }
        public string Signature { get; set; }
        public DateTime DeclarationDate { get; set; }

        public virtual EmailRegister EmailRegister { get; set; }
    }
}
