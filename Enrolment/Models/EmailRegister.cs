using System.ComponentModel.DataAnnotations.Schema;

namespace Enrolment.Models
{
    [Table("EmailRegisters")]
    public class EmailRegister : BaseModel
    {
        public string Email { get; set; } = "";
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public string? HashCode { get; set; }

        public int? PayeeId { get; set; }
        public virtual Payee Payee { get; set; }
        //public int? PayerId { get; set; }
        //public virtual Payer Payer { get; set; }
        //public int? EmployeeId { get; set; }
        //public virtual Employee Employee { get; set; }
        //public int? EmployerId { get; set; }
        //public virtual Employer Employer { get; set; }
    }
}
