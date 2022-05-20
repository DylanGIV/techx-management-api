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
        public IActionResult CreateCompany([FromBody] CreateCompanyRequest createCompanyRequest, [FromHeader] string authorization)
        {
            _companyService.CreateCompany(createCompanyRequest, authorization);
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

    }
}
