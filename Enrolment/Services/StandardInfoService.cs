using Enrolment.Data;
using Enrolment.Helpers;
using Enrolment.Models;
using Enrolment.ModelViews;
using Enrolment.Requests;
using Microsoft.EntityFrameworkCore;

namespace Enrolment.Services
{
    public interface IStandardInfoService
    {
        Task<ServiceResponse> Create(AddStandardInfoRequest request);
    }

    public class StandardInfoService : IStandardInfoService
    {
        private readonly ApplicationDbContext _context;

        public StandardInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> Create(AddStandardInfoRequest request)
        {
            try
            {
                var emailRegister = await _context.EmailRegisters.FirstOrDefaultAsync(x => x.Email == request.EmailRegister && x.IsConfirmed && x.IsDeleted == 0);
                if (emailRegister == null)
                {
                    return new ServiceResponse
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                if (emailRegister.Payee == null)
                {
                    _context.Payees.Add(new Payee
                    {
                        TaxFileNumber = request.Payee.TaxFileNumber,
                        NameTitle = request.Payee.NameTitle,
                        //...
                        EmailRegister = emailRegister
                    });
                }

                if (emailRegister.Payee == null)
                {
                    _context.Payers.Add(new Payer
                    {
                        BusinessNumber = request.Payer.BusinessNumber,
                        BranchNumber = request.Payer.BranchNumber,
                        //...
                        EmailRegister = emailRegister
                    });
                }

                if (emailRegister.Payee == null)
                {
                    _context.Employees.Add(new Employee
                    {
                        SuperannuationFund = request.Employee.SuperannuationFund,
                        Name = request.Employee.Name,
                        //...
                        EmailRegister = emailRegister
                    });
                }

                if (emailRegister.Payee == null)
                {
                    _context.Employers.Add(new Employer
                    {
                        BusinessName = request.Employer.BusinessName,
                        BusinessNumber = request.Employer.BusinessNumber,
                        //...
                        EmailRegister = emailRegister
                    });
                }          

                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Status = 1,
                    Message = "Add new info successfully"
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
    }
}
