using Enrolment.Data;
using Enrolment.Models;
using Enrolment.ModelViews;
using Enrolment.Requests;
using Enrolment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;

namespace Enrolment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRegisterEmailService _registerEmailService;
        private IStandardInfoService _standardInfoService;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSenderService _emailSender;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IEmailSenderService emailSender, IConfiguration configuration)
        {
            _logger = logger;
            _registerEmailService = new RegisterEmailService(context, emailSender, configuration);
            _standardInfoService = new StandardInfoService(context);
            _context = context;
            _emailSender = emailSender;
            _configuration = configuration;
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
            ViewBag.Email = checkEmail?.Email;
            return View();

        }

        [HttpGet("Home/StandardInfo/{email}")]
        public IActionResult StandardInfo(string email)
        {
            ViewBag.Email = email;
            return View();
        }

        [HttpGet("Home/StandardInfoDetail/{email}")]
        public async Task<ActionResult<ResponseModel<StandardInfoModel>>> StandardInfoDetail(string email)
        {
            try
            {
                var checkEmail = await _context.EmailRegisters.Include(x => x.Payee)
                    .Include(x => x.Payer)
                    .Include(x => x.Employee)
                    .Include(x => x.Employer)
                    .FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.HashCode)
                    && x.Email == email
                    && x.IsConfirmed
                    && x.IsDeleted == 0);
                if (checkEmail == null)
                {
                    return new ResponseModel<StandardInfoModel>
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                var payee = checkEmail.Payee;
                var payer = checkEmail.Payer;
                var employee = checkEmail.Employee;
                var employer = checkEmail.Employer;

                var data = new StandardInfoModel
                {
                    EmailRegister = email,
                    Payee = payee != null ? new PayeeModel
                    {
                        Surname = payee!.Surname,
                        FirstName = payee!.FirstName,
                        TaxFileNumber = payee!.TaxFileNumber,
                        NameTitle = (int)payee!.NameTitle,
                        OtherName = payee!.OtherName,
                        HomeAddress = payee!.HomeAddress,
                        Suburb = payee!.Suburb,
                        State = payee!.State,
                        PostCode = payee!.PostCode,
                        PreviousFamilyName = payee!.PreviousFamilyName,
                        Email = payee!.Email,
                        DayOfBirth = payee!.DayOfBirth,
                        MonthOfBirth = payee!.MonthOfBirth,
                        YearOfBirth = payee!.YearOfBirth,
                        PaidBasis = (int)payee!.PaidBasis,
                        ResidencyStatus = (int)payee!.ResidencyStatus,
                        ClaimTaxFree = (int)payee!.ClaimTaxFree,
                        HaveLoanProgram = (int)payee!.HaveLoanProgram
                    } : null,
                    Payer = payer != null ? new PayerModel
                    {
                        BusinessNumber = payer.BusinessNumber,
                        BranchNumber = payer.BranchNumber,
                        AppliedForNumber = (int)payer.AppliedForNumber,
                        LegalName = payer.LegalName,
                        BusinessAddress = payer.BusinessAddress,
                        Suburb = payer.Suburb,
                        State = payer.State,
                        PostCode = payer.PostCode,
                        Email = payer.Email,
                        ContactPerson = payer.ContactPerson,
                        BusinessPhone = payer.BusinessPhone,
                        MakePayment = (int)payer.MakePayment
                    } : null,
                    Employee = employee != null ? new EmployeeModel
                    {
                        SuperannuationFund = employee.SuperannuationFund,
                        Name = employee.Name,
                        IdentificationNumber = employee.IdentificationNumber,
                        TaxFileNumber = employee.TaxFileNumber,
                        FundName = employee.FundName,
                        FundAddress = employee.FundAddress,
                        Suburb = employee.Suburb,
                        State = employee.State,
                        PostCode = employee.PostCode,
                        MemberNo = employee.MemberNo,
                        AccountName = employee.AccountName,
                        BusinessNumber = employee.BusinessNumber,
                        SuperannuationProductIdentificationNumber = employee.SuperannuationProductIdentificationNumber,
                        DaytimePhoneNumber = employee.DaytimePhoneNumber,
                        HaveAttached = (int)employee.HaveAttached,
                    } : null,
                    Employer = employer != null ? new EmployerModel
                    {
                        BusinessName = employer.BusinessName,
                        BusinessNumber = employer.BusinessNumber,
                        FundName = employer.FundName,
                        SuperannuationProductIdentificationNumber = employer.SuperannuationProductIdentificationNumber,
                        FundPhone = employer.FundPhone,
                        FundWebsite = employer.FundWebsite,
                    } : null
                };

                return new ResponseModel<StandardInfoModel>
                {
                    Status = 1,
                    Data = data
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<StandardInfoModel>
                {
                    Status = 0,
                    Message = "Server error"
                };
            }
        }

        [HttpPost()]
        public async Task<ActionResult<ResponseModel>> SubmitStandardInfo([FromBody] SubmitStandardInfoRequest request)
        {
            // validate
            if (!ModelState.IsValid)
            {
                var message = string.Join(" </br> ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(ModelState);
            }

            var result = await _standardInfoService.Update(request);
            if (result.Status == 0)
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = result.Message
                };
            }

            return new ResponseModel
            {
                Status = 1,
                Message = result.Message
            };
        }

    }
}