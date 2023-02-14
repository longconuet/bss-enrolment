namespace Enrolment.Models
{
    public class EmailRegister : BaseModel
    {
        public string Email { get; set; } = "";
        public bool IsConfirmed { get; set; }
    }
}
