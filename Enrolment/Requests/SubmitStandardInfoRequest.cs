namespace Enrolment.Requests
{
    public class SubmitStandardInfoRequest
    {
        public string EmailRegister { get; set; }
        public PayeeRequest Payee { get; set; }
        public PayerRequest Payer { get; set; }
        public EmployeeRequest Employee { get; set; }
        public EmployerRequest Employer { get; set; }

        //public IList<IFormFile> Files {get; set; }
    }
}
