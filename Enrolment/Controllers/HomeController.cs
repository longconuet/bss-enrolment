using Enrolment.Data;
using Enrolment.Helpers;
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

        [HttpPost]
        public async Task<ActionResult<ResponseModel<string>>> AjaxUpload(IList<IFormFile> files)
        {
            try
            {
                string path = "";
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", formFile.FileName);
                        var filePath = Path.GetTempFileName();

                        using (var stream = System.IO.File.Create(path))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                return new ResponseModel<string>
                {
                    Status = 1,
                    Data = path
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<string>
                {
                    Status = 0,
                    Message = "Upload file failed"
                };
            }
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
				return new ResponseModel<RegisterEmailModel>
				{
					Status = 0,
					Message = message
				};
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

        [HttpPost]
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

            var payeeBirthDay = $"{request.Payee.DayOfBirth}/{request.Payee.MonthOfBirth}/{request.Payee.YearOfBirth}";
            if (FuncHelper.CheckValidDate(payeeBirthDay))
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Invalid birthday"
                };
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

        [HttpGet("Home/PayeeDetail/{email}")]
        public async Task<ActionResult<ResponseModel<PayeeModel>>> PayeeDetail(string email)
        {
            try
            {
                var checkEmail = await _context.EmailRegisters.Include(x => x.Payee)
                    .FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.HashCode)
                    && x.Email == email
                    && x.IsConfirmed
                    && x.IsDeleted == 0);
                if (checkEmail == null)
                {
                    return new ResponseModel<PayeeModel>
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                var payee = checkEmail.Payee;

                var data = payee != null ? new PayeeModel
                {
                    EmailRegister = email,
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
                } : null;

                return new ResponseModel<PayeeModel>
                {
                    Status = 1,
                    Data = data
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<PayeeModel>
                {
                    Status = 0,
                    Message = "Server error"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SubmitPayeeInfo([FromBody] SubmitPayeeRequest request)
        {
            // validate
            if (!ModelState.IsValid)
            {
                var message = string.Join(" </br> ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
				return new ResponseModel
				{
					Status = 0,
					Message = message
				};
			}

            var payeeBirthDay = $"{request.DayOfBirth}/{request.MonthOfBirth}/{request.YearOfBirth}";
            if (FuncHelper.CheckValidDate(payeeBirthDay))
            {
                return new ResponseModel
                {
                    Status = 0,
                    Message = "Invalid birthday"
                };
            }

            var result = await _standardInfoService.UpdatePayee(request);
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

        [HttpGet("Home/PayerDetail/{email}")]
        public async Task<ActionResult<ResponseModel<PayerModel>>> PayerDetail(string email)
        {
            try
            {
                var checkEmail = await _context.EmailRegisters.Include(x => x.Payer)
                    .FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.HashCode)
                    && x.Email == email
                    && x.IsConfirmed
                    && x.IsDeleted == 0);
                if (checkEmail == null)
                {
                    return new ResponseModel<PayerModel>
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                var payer = checkEmail.Payer;

                var data = payer != null ? new PayerModel
                {
                    EmailRegister = email,
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
                } : null;

                return new ResponseModel<PayerModel>
                {
                    Status = 1,
                    Data = data
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<PayerModel>
                {
                    Status = 0,
                    Message = "Server error"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SubmitPayerInfo([FromBody] SubmitPayerRequest request)
        {
            // validate
            if (!ModelState.IsValid)
            {
                var message = string.Join(" </br> ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(ModelState);
            }

            var result = await _standardInfoService.UpdatePayer(request);
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

        [HttpGet("Home/EmployeeDetail/{email}")]
        public async Task<ActionResult<ResponseModel<EmployeeModel>>> EmployeeDetail(string email)
        {
            try
            {
                var checkEmail = await _context.EmailRegisters.Include(x => x.Employee)
                    .FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.HashCode)
                    && x.Email == email
                    && x.IsConfirmed
                    && x.IsDeleted == 0);
                if (checkEmail == null)
                {
                    return new ResponseModel<EmployeeModel>
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                var employee = checkEmail.Employee;

                var data = employee != null ? new EmployeeModel
                {
                    EmailRegister = email,
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
                } : null;

                return new ResponseModel<EmployeeModel>
                {
                    Status = 1,
                    Data = data
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<EmployeeModel>
                {
                    Status = 0,
                    Message = "Server error"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SubmitEmployeeInfo([FromBody] SubmitEmployeeRequest request)
        {
            // validate
            if (!ModelState.IsValid)
            {
                var message = string.Join(" </br> ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
				return new ResponseModel
				{
					Status = 0,
					Message = message
				};
			}

            var result = await _standardInfoService.UpdateEmployee(request);
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

        [HttpGet("Home/EmployerDetail/{email}")]
        public async Task<ActionResult<ResponseModel<EmployerModel>>> EmployerDetail(string email)
        {
            try
            {
                var checkEmail = await _context.EmailRegisters.Include(x => x.Employer)
                    .FirstOrDefaultAsync(x => !string.IsNullOrEmpty(x.HashCode)
                    && x.Email == email
                    && x.IsConfirmed
                    && x.IsDeleted == 0);
                if (checkEmail == null)
                {
                    return new ResponseModel<EmployerModel>
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                var employer = checkEmail.Employer;

                var data = employer != null ? new EmployerModel
                {
                    EmailRegister = email,
                    BusinessName = employer.BusinessName,
                    BusinessNumber = employer.BusinessNumber,
                    FundName = employer.FundName,
                    SuperannuationProductIdentificationNumber = employer.SuperannuationProductIdentificationNumber,
                    FundPhone = employer.FundPhone,
                    FundWebsite = employer.FundWebsite,
                } : null;

                return new ResponseModel<EmployerModel>
                {
                    Status = 1,
                    Data = data
                };
            }
            catch (Exception e)
            {
                return new ResponseModel<EmployerModel>
                {
                    Status = 0,
                    Message = "Server error"
                };
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel>> SubmitEmployerInfo([FromBody] SubmitEmployerRequest request)
        {
            // validate
            if (!ModelState.IsValid)
            {
                var message = string.Join(" </br> ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return BadRequest(ModelState);
            }

            var result = await _standardInfoService.UpdateEmployer(request);
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