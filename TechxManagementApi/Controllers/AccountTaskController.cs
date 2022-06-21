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

        [HttpPut]
        [Route("status")]
        public async Task<IActionResult> UpdateAccountTaskStatus([FromBody] UpdateAccountTaskStatus updateAccountTaskStatus)
        {
            await _accountTaskService.UpdateAccountTaskStatusAsync(updateAccountTaskStatus);
            return Ok(new { message = "Task status updated." });
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAllAccountTasks()
        {
            var accountTasks = _accountTaskService.GetAllAssignedAccountTasks();
            return Ok(accountTasks);
        }
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteAccountTask(int id)
        {
            _accountTaskService.DeleteAccountTaskAsync(id);
            return Ok(new { message = "Task deleted." });
        }
        [HttpDelete]
        [Route("all")]
        public IActionResult DeleteAllAccountTasks()
        {
            _accountTaskService.DeleteAllAccountTasksAsync();
            return Ok(new { message = "All tasks deleted." });
        }
    }
}

