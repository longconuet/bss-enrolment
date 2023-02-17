using Enrolment.Requests;

namespace Enrolment.ModelViews
{
    public class StandardInfoModel
    {
        public string EmailRegister { get; set; }
        public PayeeModel? Payee { get; set; }
        public PayerModel? Payer { get; set; }
        public EmployeeModel? Employee { get; set; }
        public EmployerModel? Employer { get; set; }
    }
}
