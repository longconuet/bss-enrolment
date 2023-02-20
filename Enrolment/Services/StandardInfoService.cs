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
        Task<ServiceResponse> UpdatePayee(SubmitPayeeRequest request);
        Task<ServiceResponse> UpdatePayer(SubmitPayerRequest request);
        Task<ServiceResponse> UpdateEmployee(SubmitEmployeeRequest request);
        Task<ServiceResponse> UpdateEmployer(SubmitEmployerRequest request);
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
                var emailRegister = await _context.EmailRegisters.Include(x => x.Payee)
                    .Include(x => x.Payer)
                    .Include(x => x.Employee)
                    .Include(x => x.Employer)
                    .FirstOrDefaultAsync(x => x.Email == request.EmailRegister && x.IsConfirmed && x.IsDeleted == 0);
                if (emailRegister == null)
                {
                    return new ServiceResponse
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                // payee
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

                // payer
                if (emailRegister.Payer == null)
                {
                    _context.Payers.Add(new Payer
                    {
                        BusinessNumber = request.Payer.BusinessNumber,
                        BranchNumber = request.Payer.BranchNumber,
                        AppliedForNumber = request.Payer.AppliedForNumber,
                        LegalName = request.Payer.LegalName,
                        BusinessAddress = request.Payer.BusinessAddress,
                        Suburb = request.Payer.Suburb,
                        State = request.Payer.State,
                        PostCode = request.Payer.PostCode,
                        Email = request.Payer.Email,
                        ContactPerson = request.Payer.ContactPerson,
                        BusinessPhone = request.Payer.BusinessPhone,
                        MakePayment = request.Payer.MakePayment,
                        Signature = "",
                        DeclarationDate = DateTime.Now,
                        EmailRegister = emailRegister
                    });
                }
                else
                {
                    var payer = emailRegister.Payer;
                    payer.BusinessNumber = request.Payer.BusinessNumber;
                    payer.BranchNumber = request.Payer.BranchNumber;
                    payer.AppliedForNumber = request.Payer.AppliedForNumber;
                    payer.LegalName = request.Payer.LegalName;
                    payer.BusinessAddress = request.Payer.BusinessAddress;
                    payer.Suburb = request.Payer.Suburb;
                    payer.State = request.Payer.State;
                    payer.PostCode = request.Payer.PostCode;
                    payer.Email = request.Payer.Email;
                    payer.ContactPerson = request.Payer.ContactPerson;
                    payer.BusinessPhone = request.Payer.BusinessPhone;
                    payer.MakePayment = request.Payer.MakePayment;
                    payer.Signature = "";
                    payer.DeclarationDate = DateTime.Now;
                    payer.UpdatedAt = DateTime.Now;

                    _context.Payers.Update(payer);
                }

                // employee
                if (emailRegister.Employee == null)
                {
                    _context.Employees.Add(new Employee
                    {
                        SuperannuationFund = request.Employee.SuperannuationFund,
                        Name = request.Employee.Name,
                        IdentificationNumber = request.Employee.IdentificationNumber,
                        TaxFileNumber = request.Employee.TaxFileNumber,
                        FundName = request.Employee.FundName,
                        FundAddress = request.Employee.FundAddress,
                        Suburb = request.Employee.Suburb,
                        State = request.Employee.State,
                        PostCode = request.Employee.PostCode,
                        MemberNo = request.Employee.MemberNo,
                        AccountName = request.Employee.AccountName,
                        BusinessNumber = request.Employee.BusinessNumber,
                        SuperannuationProductIdentificationNumber = request.Employee.SuperannuationProductIdentificationNumber,
                        DaytimePhoneNumber = request.Employee.DaytimePhoneNumber,
                        HaveAttached = request.Employee.HaveAttached,
                        Signature = "",
                        DeclarationDate = DateTime.Now,
                        EmailRegister = emailRegister
                    });
                }
                else
                {
                    var employee = emailRegister.Employee;
                    employee.SuperannuationFund = request.Employee.SuperannuationFund;
                    employee.Name = request.Employee.Name;
                    employee.IdentificationNumber = request.Employee.IdentificationNumber;
                    employee.TaxFileNumber = request.Employee.TaxFileNumber;
                    employee.FundName = request.Employee.FundName;
                    employee.FundAddress = request.Employee.FundAddress;
                    employee.Suburb = request.Employee.Suburb;
                    employee.State = request.Employee.State;
                    employee.PostCode = request.Employee.PostCode;
                    employee.MemberNo = request.Employee.MemberNo;
                    employee.AccountName = request.Employee.AccountName;
                    employee.BusinessNumber = request.Employee.BusinessNumber;
                    employee.SuperannuationProductIdentificationNumber = request.Employee.SuperannuationProductIdentificationNumber;
                    employee.DaytimePhoneNumber = request.Employee.DaytimePhoneNumber;
                    employee.HaveAttached = request.Employee.HaveAttached;
                    employee.Signature = "";
                    employee.DeclarationDate = DateTime.Now;
                    employee.UpdatedAt = DateTime.Now;

                    _context.Employees.Update(employee);
                }

                // employer
                if (emailRegister.Employer == null)
                {
                    _context.Employers.Add(new Employer
                    {
                        BusinessName = request.Employer.BusinessName,
                        BusinessNumber = request.Employer.BusinessNumber,
                        FundName = request.Employer.FundName,
                        SuperannuationProductIdentificationNumber = request.Employer.SuperannuationProductIdentificationNumber,
                        FundPhone = request.Employer.FundPhone,
                        FundWebsite = request.Employer.FundWebsite,
                        Signature = "",
                        DeclarationDate = DateTime.Now,
                        DateValidChoice = DateTime.Now,
                        ActDateValidChoice = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        EmailRegister = emailRegister
                    });
                }
                else
                {
                    var employer = emailRegister.Employer;
                    employer.BusinessName = request.Employer.BusinessName;
                    employer.BusinessNumber = request.Employer.BusinessNumber;
                    employer.FundName = request.Employer.FundName;
                    employer.SuperannuationProductIdentificationNumber = request.Employer.SuperannuationProductIdentificationNumber;
                    employer.FundPhone = request.Employer.FundPhone;
                    employer.FundWebsite = request.Employer.FundWebsite;
                    employer.Signature = "";
                    employer.DeclarationDate = DateTime.Now;
                    employer.DateValidChoice = DateTime.Now;
                    employer.ActDateValidChoice = DateTime.Now;
                    employer.UpdatedAt = DateTime.Now;

                    _context.Employers.Update(employer);
                }

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

        public async Task<ServiceResponse> UpdatePayee(SubmitPayeeRequest request)
        {
            try
            {
                var emailRegister = await _context.EmailRegisters.Include(x => x.Payee)
                    .FirstOrDefaultAsync(x => x.Email == request.EmailRegister && x.IsConfirmed && x.IsDeleted == 0);
                if (emailRegister == null)
                {
                    return new ServiceResponse
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                // payee
                if (emailRegister.Payee == null)
                {
                    var payee = new Payee
                    {
                        Surname = request.Surname,
                        FirstName = request.FirstName,
                        TaxFileNumber = request.TaxFileNumber,
                        NameTitle = request.NameTitle,
                        OtherName = request.OtherName,
                        HomeAddress = request.HomeAddress,
                        Suburb = request.Suburb,
                        State = request.State,
                        PostCode = request.PostCode,
                        PreviousFamilyName = request.PreviousFamilyName,
                        Email = request.Email,
                        DayOfBirth = request.DayOfBirth,
                        MonthOfBirth = request.MonthOfBirth,
                        YearOfBirth = request.YearOfBirth,
                        PaidBasis = request.PaidBasis,
                        ResidencyStatus = request.ResidencyStatus,
                        ClaimTaxFree = request.ClaimTaxFree,
                        HaveLoanProgram = request.HaveLoanProgram,
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
                    payee.Surname = request.Surname;
                    payee.FirstName = request.FirstName;
                    payee.TaxFileNumber = request.TaxFileNumber;
                    payee.NameTitle = request.NameTitle;
                    payee.OtherName = request.OtherName;
                    payee.HomeAddress = request.HomeAddress;
                    payee.Suburb = request.Suburb;
                    payee.State = request.State;
                    payee.PostCode = request.PostCode;
                    payee.PreviousFamilyName = request.PreviousFamilyName;
                    payee.Email = request.Email;
                    payee.DayOfBirth = request.DayOfBirth;
                    payee.MonthOfBirth = request.MonthOfBirth;
                    payee.YearOfBirth = request.YearOfBirth;
                    payee.PaidBasis = request.PaidBasis;
                    payee.ResidencyStatus = request.ResidencyStatus;
                    payee.ClaimTaxFree = request.ClaimTaxFree;
                    payee.HaveLoanProgram = request.HaveLoanProgram;
                    payee.Signature = "";
                    payee.DeclarationDate = DateTime.Now;
                    payee.UpdatedAt = DateTime.Now;

                    _context.Payees.Update(payee);
                }

                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Status = 1,
                    Message = "Update payee info successfully"
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

        public async Task<ServiceResponse> UpdatePayer(SubmitPayerRequest request)
        {
            try
            {
                var emailRegister = await _context.EmailRegisters.Include(x => x.Payer)
                    .FirstOrDefaultAsync(x => x.Email == request.EmailRegister && x.IsConfirmed && x.IsDeleted == 0);
                if (emailRegister == null)
                {
                    return new ServiceResponse
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                // payer
                if (emailRegister.Payer == null)
                {
                    _context.Payers.Add(new Payer
                    {
                        BusinessNumber = request.BusinessNumber,
                        BranchNumber = request.BranchNumber,
                        AppliedForNumber = request.AppliedForNumber,
                        LegalName = request.LegalName,
                        BusinessAddress = request.BusinessAddress,
                        Suburb = request.Suburb,
                        State = request.State,
                        PostCode = request.PostCode,
                        Email = request.Email,
                        ContactPerson = request.ContactPerson,
                        BusinessPhone = request.BusinessPhone,
                        MakePayment = request.MakePayment,
                        Signature = "",
                        DeclarationDate = DateTime.Now,
                        EmailRegister = emailRegister
                    });
                }
                else
                {
                    var payer = emailRegister.Payer;
                    payer.BusinessNumber = request.BusinessNumber;
                    payer.BranchNumber = request.BranchNumber;
                    payer.AppliedForNumber = request.AppliedForNumber;
                    payer.LegalName = request.LegalName;
                    payer.BusinessAddress = request.BusinessAddress;
                    payer.Suburb = request.Suburb;
                    payer.State = request.State;
                    payer.PostCode = request.PostCode;
                    payer.Email = request.Email;
                    payer.ContactPerson = request.ContactPerson;
                    payer.BusinessPhone = request.BusinessPhone;
                    payer.MakePayment = request.MakePayment;
                    payer.Signature = "";
                    payer.DeclarationDate = DateTime.Now;
                    payer.UpdatedAt = DateTime.Now;

                    _context.Payers.Update(payer);
                }

                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Status = 1,
                    Message = "Update payer info successfully"
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

        public async Task<ServiceResponse> UpdateEmployee(SubmitEmployeeRequest request)
        {
            try
            {
                var emailRegister = await _context.EmailRegisters.Include(x => x.Employee)
                    .FirstOrDefaultAsync(x => x.Email == request.EmailRegister && x.IsConfirmed && x.IsDeleted == 0);
                if (emailRegister == null)
                {
                    return new ServiceResponse
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                // employee
                if (emailRegister.Employee == null)
                {
                    _context.Employees.Add(new Employee
                    {
                        SuperannuationFund = request.SuperannuationFund,
                        Name = request.Name,
                        IdentificationNumber = request.IdentificationNumber,
                        TaxFileNumber = request.TaxFileNumber,
                        FundName = request.FundName,
                        FundAddress = request.FundAddress,
                        Suburb = request.Suburb,
                        State = request.State,
                        PostCode = request.PostCode,
                        MemberNo = request.MemberNo,
                        AccountName = request.AccountName,
                        BusinessNumber = request.BusinessNumber,
                        SuperannuationProductIdentificationNumber = request.SuperannuationProductIdentificationNumber,
                        DaytimePhoneNumber = request.DaytimePhoneNumber,
                        HaveAttached = request.HaveAttached,
                        Signature = "",
                        DeclarationDate = DateTime.Now,
                        EmailRegister = emailRegister
                    });
                }
                else
                {
                    var employee = emailRegister.Employee;
                    employee.SuperannuationFund = request.SuperannuationFund;
                    employee.Name = request.Name;
                    employee.IdentificationNumber = request.IdentificationNumber;
                    employee.TaxFileNumber = request.TaxFileNumber;
                    employee.FundName = request.FundName;
                    employee.FundAddress = request.FundAddress;
                    employee.Suburb = request.Suburb;
                    employee.State = request.State;
                    employee.PostCode = request.PostCode;
                    employee.MemberNo = request.MemberNo;
                    employee.AccountName = request.AccountName;
                    employee.BusinessNumber = request.BusinessNumber;
                    employee.SuperannuationProductIdentificationNumber = request.SuperannuationProductIdentificationNumber;
                    employee.DaytimePhoneNumber = request.DaytimePhoneNumber;
                    employee.HaveAttached = request.HaveAttached;
                    employee.Signature = "";
                    employee.DeclarationDate = DateTime.Now;
                    employee.UpdatedAt = DateTime.Now;

                    _context.Employees.Update(employee);
                }

                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Status = 1,
                    Message = "Update employee info successfully"
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

        public async Task<ServiceResponse> UpdateEmployer(SubmitEmployerRequest request)
        {
            try
            {
                var emailRegister = await _context.EmailRegisters.Include(x => x.Payee)
                    .FirstOrDefaultAsync(x => x.Email == request.EmailRegister && x.IsConfirmed && x.IsDeleted == 0);
                if (emailRegister == null)
                {
                    return new ServiceResponse
                    {
                        Status = 0,
                        Message = "Invalid email"
                    };
                }

                // employer
                if (emailRegister.Employer == null)
                {
                    _context.Employers.Add(new Employer
                    {
                        BusinessName = request.BusinessName,
                        BusinessNumber = request.BusinessNumber,
                        FundName = request.FundName,
                        SuperannuationProductIdentificationNumber = request.SuperannuationProductIdentificationNumber,
                        FundPhone = request.FundPhone,
                        FundWebsite = request.FundWebsite,
                        Signature = "",
                        DeclarationDate = DateTime.Now,
                        DateValidChoice = DateTime.Now,
                        ActDateValidChoice = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        EmailRegister = emailRegister
                    });
                }
                else
                {
                    var employer = emailRegister.Employer;
                    employer.BusinessName = request.BusinessName;
                    employer.BusinessNumber = request.BusinessNumber;
                    employer.FundName = request.FundName;
                    employer.SuperannuationProductIdentificationNumber = request.SuperannuationProductIdentificationNumber;
                    employer.FundPhone = request.FundPhone;
                    employer.FundWebsite = request.FundWebsite;
                    employer.Signature = "";
                    employer.DeclarationDate = DateTime.Now;
                    employer.DateValidChoice = DateTime.Now;
                    employer.ActDateValidChoice = DateTime.Now;
                    employer.UpdatedAt = DateTime.Now;

                    _context.Employers.Update(employer);
                }

                await _context.SaveChangesAsync();

                return new ServiceResponse
                {
                    Status = 1,
                    Message = "Update employer info successfully"
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
