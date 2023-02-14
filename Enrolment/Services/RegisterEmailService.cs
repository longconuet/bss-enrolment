using Enrolment.Data;
using Enrolment.Models;
using Enrolment.ModelViews;
using Enrolment.Requests;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Enrolment.Services
{
    public interface IRegisterEmailService
    {
        Task<ServiceResponse<RegisterEmailModel>> RegisterEmail(RegisterEmailRequest request);
    }

    public class RegisterEmailService : IRegisterEmailService
    {
        private readonly ApplicationDbContext _context;

        public RegisterEmailService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<RegisterEmailModel>> RegisterEmail(RegisterEmailRequest request)
        {
            try
            {
                string htmlContent = $"<p>We have sent an email to {request.Email} to verify your email address. Please open the email and click the 'Verify My Email' link, you will then be able to continue</p>";

                var checkEmail = await _context.EmailRegisters.FirstOrDefaultAsync(x => x.Email == request.Email && x.IsDeleted == 0);
                if (checkEmail != null)
                {
                    return new ServiceResponse<RegisterEmailModel>
                    {
                        Status = 1,
                        Data = new RegisterEmailModel
                        {
                            IsNewEmail = false,
                            IsConfirmed = checkEmail.IsConfirmed,
                            HtmlContent = htmlContent
                        },
                    };
                }                

                // create new email
                _context.EmailRegisters.Add(new EmailRegister
                {
                    Email = request.Email,
                    IsConfirmed = true
                });
                await _context.SaveChangesAsync();

                // send verification email

                return new ServiceResponse<RegisterEmailModel>
                {
                    Status = 1,
                    Data = new RegisterEmailModel
                    {
                        IsNewEmail = true,
                        IsConfirmed = false,
                        HtmlContent = htmlContent
                    },
                    Message = "Register new email successfully"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<RegisterEmailModel>
                {
                    Status = 0,
                    Message = e.Message
                };
            }
        }
    }
}
