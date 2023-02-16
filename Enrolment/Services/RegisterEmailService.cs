using Enrolment.Data;
using Enrolment.Helpers;
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
        Task<ServiceResponse> VerifyEmail(string code);
    }

    public class RegisterEmailService : IRegisterEmailService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSenderService _emailSender;
        private readonly IConfiguration _configuration;


        public RegisterEmailService(ApplicationDbContext context, IEmailSenderService emailSender, IConfiguration configuration)
        {
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
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
                string hashCode = FuncHelper.RandomHashString(10);
                _context.EmailRegisters.Add(new EmailRegister
                {
                    Email = request.Email,
                    IsConfirmed = false,
                    HashCode = hashCode
                });
                await _context.SaveChangesAsync();

                // send verification email
                await SendVerificationEmail(request.Email, hashCode);

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

        public async Task<ServiceResponse> VerifyEmail(string code)
        {
            try
            {
                var email = await _context.EmailRegisters.FirstOrDefaultAsync(x => x.HashCode == code && !x.IsConfirmed && x.IsDeleted == 0);
                if (email != null)
                {
                    email.IsConfirmed = true;
                    email.ConfirmedAt = DateTime.Now;

                    _context.EmailRegisters.Update(email);
                    await _context.SaveChangesAsync();
                }

                return new ServiceResponse
                {
                    Status = 1
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse
                {
                    Status = 0,
                    Message = e.Message
                };
            }
        }

        private async Task SendVerificationEmail(string email, string code)
        {
            await Task.Yield();

            string subject = "Email Address Verification";
            string content = "";

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(path, $"EmailTemplates/VerifyEmail.html");
            using (StreamReader reader = new(filePath))
            {
                content = reader.ReadToEnd();
            }

            var baseUrl = _configuration["BaseUrl"];
            content = content.Replace("{VerifyEmailLink}", $"{baseUrl}/Home/VerifyEmail/{code}");

            var message = new Message(new string[] { email }, subject, content);
            _emailSender.SendEmail(message);
        }
    }
}
