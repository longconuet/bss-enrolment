using Enrolment.Extensions;

namespace Enrolment.Requests
{
	public class SubmitIdentityProofImageRequest
	{
		public string EmailRegister { get; set; }

		[MaxFileSize(5 * 1024 * 1024)]
		[AllowedExtensions(new string[] { ".jpg", ".png" })]
		public IFormFile ImageFile { get; set; }
	}
}
