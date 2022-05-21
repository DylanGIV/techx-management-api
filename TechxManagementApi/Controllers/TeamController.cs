using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Models.Teams;
using TechxManagementApi.Services;

namespace TechxManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : BaseController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateTeam([FromBody] CreateTeamRequest createTeamRequest)
        {
            _teamService.CreateTeam(createTeamRequest);
            return Ok(new { message = "Team created successfully." });
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        [Route("update")]
        public IActionResult UpdateTeamName([FromBody] UpdateTeamNameRequest updateTeamNameRequest)
        {
            _teamService.UpdateTeamName(updateTeamNameRequest);
            return Ok(new { message = "Team has been updated" });
        }
        
        [HttpGet("companyId")]
        public IActionResult GetTeamsByCompany(long companyId)
        {
            var teams = _teamService.GetTeamsByCompany(companyId);
            return Ok(teams);

        }
    }
}
