using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Helpers;
using TechxManagementApi.Models.Companies;

namespace TechxManagementApi.Services
{
    public interface ICompanyService
    {
        void CreateCompany(CreateCompanyRequest model);
        void InviteToCompany(InviteToCompanyRequest model);
        public void AddEmployeesToCompany(AddEmployeesToCompanyRequest addEmployeesToCompanyRequest);
        public List<Company> GetAllCompanies();
        public List<Company> GetCompaniesByAccount();
        public void DeleteCompany(int id);

    }

	public class CompanyService : ICompanyService
	{
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        // private readonly IEmailService _emailService;

        public CompanyService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
           ;
        }

        public void CreateCompany(CreateCompanyRequest model)
        {
            // validate
            if (_context.Companies.Any(x => x.CompanyName == model.CompanyName))
            {
                // if company already exists
                throw new AppException("Company with this name already exists. Please contact support to resolve this issue.");
            }
            var httpContext = _accessor.HttpContext;

            var company = new Company();
            var owner = (Account)httpContext.Items["Account"];

            company.CompanyName = model.CompanyName;
            company.Owner = owner;

            // save company
            _context.Companies.Add(company);
            _context.SaveChanges();
        }
        public void InviteToCompany(InviteToCompanyRequest model)
        {
            // send email to each account invited.
        }
        public void AddEmployeesToCompany(AddEmployeesToCompanyRequest addEmployeesToCompanyRequest)
        {
            bool add = true;
            var accounts = new List<Account>();
            // addEmployeesToCompanyRequest.AccountIds.ForEach(a => accounts.Add(_context.Accounts.Find(a)));

            var company = _context.Companies.Find(addEmployeesToCompanyRequest.CompanyId);
            
            if (company.Employees != null && addEmployeesToCompanyRequest.AccountIds.Count > 0)
            {
                foreach (var a in addEmployeesToCompanyRequest.AccountIds)
                {
                    add = true;
                    foreach (var e in company.Employees)
                    {
                        if (e.Id == a)
                        {
                            add = false;
                        }
                    }
                    if (add)
                    {
                        accounts.Add(_context.Accounts.Find(a));
                    }
                }

                // _context.Accounts.Where(a => )
                // .Join(_context.Companies, a => a.Id, c => c.Id, (a, c) => new {a, c})
            }

            if (accounts.Count > 0)
            {
                foreach (var a in accounts)
                {
                    company.Employees.Add(a);
                }
                _context.SaveChanges();
            }
        }
        public List<Company> GetAllCompanies()
        {
            var companies = _context.Companies.ToList();
            return companies;
        }
        public List<Company> GetCompaniesByAccount()
        {
            var httpContext = _accessor.HttpContext;

            var account = (Account)httpContext.Items["Account"];
            if (account == null) 
            {
                throw new NullReferenceException("Error retrieving account information.");
            }
            var companies = _context.Companies.Where(c => c.Owner == account).ToList();
            return companies;
        }
        public void DeleteCompany(int id)
        {
            var company = getCompany(id);
            if (company == null) 
            {
                throw new KeyNotFoundException("Company not found.");
            }
            _context.Companies.Remove(company);
            _context.SaveChanges();
        }

        // helper functions
        private Company getCompany(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null) 
            {
                throw new KeyNotFoundException("Company not found.");
            }
            return company;
        }
    }
}

