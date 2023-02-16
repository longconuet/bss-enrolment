using Enrolment.Requests;

namespace Enrolment.ModelViews
{
    public class StandardInfoModel
    {
        public string EmailRegister { get; set; }
        public PayeeModel? Payee { get; set; }
        public PayerRequest? Payer { get; set; }
        public EmployeeRequest? Employee { get; set; }
        public EmployerRequest? Employer { get; set; }
    }
}
