using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Models.Companies;
using TechxManagementApi.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechxManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateCompany([FromBody] CreateCompanyRequest createCompanyRequest)
        {
            _companyService.CreateCompany(createCompanyRequest);
            return Ok(new { message = "Company created successfully." });
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("invite")]
        public IActionResult InviteToCompany([FromBody] InviteToCompanyRequest inviteToCompanyRequest)
        {
            _companyService.InviteToCompany(inviteToCompanyRequest);
            return Ok(new { message = "Invites sent successfully to eligible users." });
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddEmployeesToCompany([FromBody] AddEmployeesToCompanyRequest addEmployeesToCompanyRequest)
        {
            _companyService.AddEmployeesToCompany(addEmployeesToCompanyRequest);
            return Ok(new { message = "Employees added to company." });
        }
        [HttpGet]
        [Route("all")]
        public IActionResult GetAllCompanies()
        {
            var companies = _companyService.GetAllCompanies();
            return Ok(companies);
        }
        [HttpGet]
        [Route("employee")]
        public IActionResult GetCompaniesByAccount()
        {
            var companies = _companyService.GetCompaniesByAccount();
            return Ok(companies);
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteCompany([FromBody] DeleteCompanyRequest deleteCompanyRequest)
        {
            _companyService.DeleteCompany(deleteCompanyRequest.CompanyId);
            return Ok(new { message = "Company deleted." });
        }

    }
}
