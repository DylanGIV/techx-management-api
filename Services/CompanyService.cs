using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Helpers;
using TechxManagementApi.Models.Companies;

namespace TechxManagementApi.Services
{
    public interface ICompanyService
    {
        void CreateCompany(CreateCompanyRequest model, string token);
        void InviteToCompany(InviteToCompanyRequest model);
    }

	public class CompanyService : ICompanyService
	{
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public CompanyService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
           ;
        }

        public void CreateCompany(CreateCompanyRequest model, string token)
        {
            // validate
            if (_context.Companies.Any(x => x.CompanyName == model.CompanyName))
            {
                // if company already exists
                throw new AppException("Company with this name already exists. Please contact support to resolve this issue.");
            }

            var company = new Company();
            var accountId = _jwtUtils.ValidateJwtToken(token);
            var owner = _context.Accounts.Find(accountId);

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
    }
}

