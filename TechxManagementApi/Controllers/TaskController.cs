//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using TechxManagementApi.Authorization;
//using TechxManagementApi.Entities;
//using TechxManagementApi.Services;

//// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace TechxManagementApi.Controllers
//{
//    public class TaskController : Controller
//    {
//        private readonly ITaskService _taskService;

//        public TaskController(ITaskService taskService)
//        {
//            _taskService = taskService;
//        }

//        [Authorize(Role.Admin)]
//        [HttpPost]
//        public IActionResult CreateTask([FromBody] CreateTaskRequest createCompanyRequest, [FromHeader] string authorization)
//        {
//            _companyService.CreateCompany(createCompanyRequest, authorization);
//            return Ok(new { message = "Company created successfully." });
//        }

//        [Authorize(Role.Admin)]
//        [HttpPost]
//        public IActionResult InviteToCompany([FromBody] InviteToCompanyRequest inviteToCompanyRequest)
//        {
//            _companyService.InviteToCompany(inviteToCompanyRequest);
//            return Ok(new { message = "Invites sent successfully to eligible users." });
//        }

//    }
//}

