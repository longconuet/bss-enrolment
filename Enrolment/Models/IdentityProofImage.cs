using System.ComponentModel.DataAnnotations.Schema;

namespace Enrolment.Models
{
	[Table("IdentityProofImages")]
	public class IdentityProofImage : BaseModel
	{
		public string Path { get; set; } = "";
		public virtual EmailRegister EmailRegister { get; set; }
	}
}
