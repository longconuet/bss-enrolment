using Enrolment.Data;
using Enrolment.Models;
using Enrolment.ModelViews;
using Enrolment.Requests;
using Enrolment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Enrolment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRegisterEmailService _registerEmailService;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSenderService _emailSender;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IEmailSenderService emailSender)
        {
            _logger = logger;
            _registerEmailService = new RegisterEmailService(context, emailSender);
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult RegisterEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<RegisterEmailModel>>> RegisterEmail([FromBody] RegisterEmailRequest request)
        {
            // validate
            if (!ModelState.IsValid)
            {
                var message = string.Join(" </br> ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(ModelState);
            }

            var result = await _registerEmailService.RegisterEmail(request);
            if (result.Status == 0)
            {
                return new ResponseModel<RegisterEmailModel>
                {
                    Status = 0,
                    Data = result.Data,
                    Message = result.Message
                };
            }

            return new ResponseModel<RegisterEmailModel>
            {
                Status = 1,
                Data = result.Data,
                Message = result.Message
            };
        }

        [HttpGet("Home/VerifyEmail/{code}")]
        public async Task<IActionResult> VerifyEmail(string code)
        {
            var checkEmail = await _context.EmailRegisters.FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.HashCode) && x.HashCode == code && x.IsDeleted == 0);

            bool hasConfirmedYet = checkEmail != null && checkEmail.IsConfirmed;
            bool hasError = false;
            if (!hasConfirmedYet)
            {
                var result = await _registerEmailService.VerifyEmail(code);
                if (result.Status == 0)
                {
                    hasError = true;
                }
            }

            ViewBag.IsValidEmail = checkEmail != null;
            ViewBag.HasConfirmedYet = hasConfirmedYet;
            ViewBag.HasError = hasError;
            ViewBag.Code = checkEmail?.HashCode;
            return View();

        }

        [HttpGet("Home/StandardInfo/{email}")]
        public async Task<IActionResult> StandardInfo(string email)
        {
            var checkEmail = await _context.EmailRegisters.FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.HashCode) 
                && x.Email == email 
                && x.IsConfirmed 
                && x.IsDeleted == 0);
            if (checkEmail == null)
            {
                return RedirectToAction("RegisterEmail");
            }

            var payee = checkEmail.Payee;
            ViewData.Add("Info", new StandardInfoModel
            {
                Payee = new PayeeRequest
                {
                    FirstName = payee!.FirstName,
                    Surname = payee!.Surname
                }
            });

            return View();
        }
    }
}