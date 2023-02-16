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
        Task<ServiceResponse> Update(SubmitStandardInfoRequest request);
    }

    public class StandardInfoService : IStandardInfoService
    {
        private readonly ApplicationDbContext _context;

        public StandardInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> Update(SubmitStandardInfoRequest request)
        {
            try
            {
                var emailRegister = await _context.EmailRegisters.Include(x => x.Payee).FirstOrDefaultAsync(x => x.Email == request.EmailRegister && x.IsConfirmed && x.IsDeleted == 0);
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
                    var payee = new Payee
                    {
                        Surname = request.Payee.Surname,
                        FirstName = request.Payee.FirstName,
                        TaxFileNumber = request.Payee.TaxFileNumber,
                        NameTitle = request.Payee.NameTitle,
                        OtherName = request.Payee.OtherName,
                        HomeAddress = request.Payee.HomeAddress,
                        Suburb = request.Payee.Suburb,
                        State = request.Payee.State,
                        PostCode = request.Payee.PostCode,
                        PreviousFamilyName = request.Payee.PreviousFamilyName,
                        Email = request.Payee.Email,
                        DayOfBirth = request.Payee.DayOfBirth,
                        MonthOfBirth = request.Payee.MonthOfBirth,
                        YearOfBirth = request.Payee.YearOfBirth,
                        PaidBasis = request.Payee.PaidBasis,
                        ResidencyStatus = request.Payee.ResidencyStatus,
                        ClaimTaxFree = request.Payee.ClaimTaxFree,
                        HaveLoanProgram = request.Payee.HaveLoanProgram,
                        Signature = "",
                        DeclarationDate = DateTime.Now,
                        //...
                        EmailRegister = emailRegister
                    };

                    _context.Payees.Add(payee);
                }
                else
                {
                    var payee = emailRegister.Payee;
                    payee.Surname = request.Payee.Surname;
                    payee.FirstName = request.Payee.FirstName;
                    payee.TaxFileNumber = request.Payee.TaxFileNumber;
                    payee.NameTitle = request.Payee.NameTitle;
                    payee.OtherName = request.Payee.OtherName;
                    payee.HomeAddress = request.Payee.HomeAddress;
                    payee.Suburb = request.Payee.Suburb;
                    payee.State = request.Payee.State;
                    payee.PostCode = request.Payee.PostCode;
                    payee.PreviousFamilyName = request.Payee.PreviousFamilyName;
                    payee.Email = request.Payee.Email;
                    payee.DayOfBirth = request.Payee.DayOfBirth;
                    payee.MonthOfBirth = request.Payee.MonthOfBirth;
                    payee.YearOfBirth = request.Payee.YearOfBirth;
                    payee.PaidBasis = request.Payee.PaidBasis;
                    payee.ResidencyStatus = request.Payee.ResidencyStatus;
                    payee.ClaimTaxFree = request.Payee.ClaimTaxFree;
                    payee.HaveLoanProgram = request.Payee.HaveLoanProgram;
                    payee.Signature = "";
                    payee.DeclarationDate = DateTime.Now;
                    payee.UpdatedAt = DateTime.Now;

                    _context.Payees.Update(payee);
                }

                //if (emailRegister.Payer == null)
                //{
                //    _context.Payers.Add(new Payer
                //    {
                //        //BusinessNumber = request.Payer.BusinessNumber,
                //        //BranchNumber = request.Payer.BranchNumber,
                //        //...
                //        EmailRegister = emailRegister
                //    });
                //}

                //if (emailRegister.Employee == null)
                //{
                //    _context.Employees.Add(new Employee
                //    {
                //        //SuperannuationFund = request.Employee.SuperannuationFund,
                //        //Name = request.Employee.Name,
                //        //...
                //        EmailRegister = emailRegister
                //    });
                //}

                //if (emailRegister.Employer == null)
                //{
                //    _context.Employers.Add(new Employer
                //    {
                //        //BusinessName = request.Employer.BusinessName,
                //        //BusinessNumber = request.Employer.BusinessNumber,
                //        //...
                //        EmailRegister = emailRegister
                //    });
                //}          


                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Status = 1,
                    Message = "Update info successfully"
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
