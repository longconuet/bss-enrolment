namespace Enrolment.Requests
{
    public class AddStandardInfoRequest
    {
        public string EmailRegister { get; set; }
        public PayeeRequest Payee { get; set; }
        public PayerRequest Payer { get; set; }
        public EmployeeRequest Employee { get; set; }
        public EmployerRequest Employer { get; set; }
    }
}
