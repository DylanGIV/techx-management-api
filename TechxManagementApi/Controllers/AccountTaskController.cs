using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Models.AccountTasks;
using TechxManagementApi.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechxManagementApi.Controllers
{
    [Authorize]
    [Controller]
    [Route("[controller]")]
    public class AccountTaskController : Controller
    {
        private readonly IAccountTaskService _accountTaskService;

        public AccountTaskController(IAccountTaskService accountTaskService)
        {
            _accountTaskService = accountTaskService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccountTaskAsync([FromBody] CreateAccountTaskRequest createAccountTaskRequest)
        {
            await _accountTaskService.CreateAccountTaskAsync(createAccountTaskRequest);
            return Ok(new { message = "Task created successfully." });
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllAccountTasks()
        {
            var accountTasks = _accountTaskService.GetAllAccountTasks();
            return Ok(accountTasks);
        }

    }
}

