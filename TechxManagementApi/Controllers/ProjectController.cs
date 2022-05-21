using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Models.Projects;
using TechxManagementApi.Services;

namespace TechxManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("create")]
        public IActionResult CreateProject([FromBody] CreateProjectRequest createProjectRequest)
        {
            _projectService.CreateProject(createProjectRequest);
            return Ok(new { message = "Project created successfully." });
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        [Route("team")]
        public IActionResult UpdateProjectTeam([FromBody] UpdateProjectTeamRequest updateProjectTeamRequest)
        {
            _projectService.UpdateProjectTeam(updateProjectTeamRequest);
            return Ok(new { message = "Project Team updated." });
        }
        [Authorize(Role.Admin)]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteProject([FromBody] DeleteProjectRequest deleteProjectRequest)
        {
            _projectService.DeleteProject(deleteProjectRequest.ProjectId);
            return Ok(new { message = "Project deleted." });
        }
        [HttpGet("companyId")]
        public IActionResult GetAllCompanyProjects(long companyId)
        {
            var projects = _projectService.GetAllCompanyProjects(companyId);
            return Ok(projects);
        }

    }
}

