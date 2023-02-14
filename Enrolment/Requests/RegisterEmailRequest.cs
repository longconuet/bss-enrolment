using System.ComponentModel.DataAnnotations;

namespace Enrolment.Requests
{
    public class RegisterEmailRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}
