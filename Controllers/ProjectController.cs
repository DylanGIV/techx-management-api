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
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public IActionResult CreateProject([FromBody] CreateProjectRequest createProjectRequest)
        {
            _projectService.CreateProject(createProjectRequest);
            return Ok(new { message = "Project created successfully." });
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public IActionResult UpdateProjectTeam([FromBody] UpdateProjectTeamRequest updateProjectTeamRequest)
        {
            _projectService.UpdateProjectTeam(updateProjectTeamRequest);
            return Ok(new { message = "Project Team updated." });
        }

        public IActionResult DeleteProject([FromBody] DeleteProjectRequest deleteProjectRequest)
        {
            _projectService.DeleteProject(deleteProjectRequest.ProjectId);
            return Ok(new { message = "Project deleted." });
        }

        public IActionResult GetAllCompanyProjects([FromBody] GetAllCompanyProjectsRequest getAllCompanyProjectsRequest)
        {
            var projects = _projectService.GetAllCompanyProjects(getAllCompanyProjectsRequest.CompanyId);
            return Ok(projects);
        }

    }
}

