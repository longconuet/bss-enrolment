using Enrolment.Data;
using Enrolment.Models;
using Enrolment.ModelViews;
using Enrolment.Requests;
using Enrolment.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Enrolment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IRegisterEmailService _registerEmailService;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _registerEmailService = new RegisterEmailService(context);
            _context = context;
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
        public async Task<ActionResult<ResponseModel<RegisterEmailModel>>> ConfirmEmail([FromBody] RegisterEmailRequest request)
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

        [HttpGet]
        public IActionResult StudentDetail()
        {
            return View();
        }
    }
}