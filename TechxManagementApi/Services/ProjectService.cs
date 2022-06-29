using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Helpers;
using TechxManagementApi.Models.Projects;

namespace TechxManagementApi.Services
{
    public interface IProjectService
    {
        public System.Threading.Tasks.Task CreateProject(CreateProjectRequest model);
        public void UpdateProjectTeam(UpdateProjectTeamRequest model);
        public void DeleteProject(int id);
        public List<Project> GetAllAccountProjects();
        public void UpdateProjectStatus(UpdateProjectStatusRequest model);

    }

	public class ProjectService : IProjectService
	{
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly AppSettings _appSettings;
        // private readonly IEmailService _emailService;

        public ProjectService(
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
        }

        public async System.Threading.Tasks.Task CreateProject(CreateProjectRequest model)
        {
            var project = new Project();
            // find company with matching id.
            project.Company = await _context.Companies.FindAsync(model.CompanyId);
            project.ProjectName = model.ProjectName;
            project.ProjectDescription = model.ProjectDescription;
            project.Completed = false;

            // in the case that no matching company could be found in the database
            if (project.Company == null)
            {
                throw new AppException("No company with this ID was found.");
            }
            // validate
            if (_context.Projects.Any(x => x.ProjectName == model.ProjectName && x.Company == project.Company))
            {
                // if project with same name already exists
                throw new AppException("Company already contains project with this name.");
            }

            // save project
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
        }
        public void UpdateProjectTeam(UpdateProjectTeamRequest model)
        {
            //switch toi use find
            var project = _context.Projects.Find(model.ProjectId);
            var team = _context.Teams.Find(model.TeamId);
            project.Team = team;

            _context.SaveChanges();
        }
        public void UpdateProjectStatus(UpdateProjectStatusRequest model)
        {
            var project = _context.Projects.Find(model.ProjectId);
            project.Completed = model.Status;

            _context.SaveChanges();
        }
        public List<Project> GetAllAccountProjects()
        {
            var httpContext = _accessor.HttpContext;
            var account = (Account)httpContext.Items["Account"];
            
            if (account == null) 
            {
                throw new KeyNotFoundException("Account not found.");
            }

            var projects = _context.Projects.Where(p => p.Company.Owner == account || p.Company.Employees.Any(e => e.Id == account.Id)).ToList();
            return projects;
        }
        public void DeleteProject(int id)
        {
            var project = getProject(id);
            if (project == null) 
            {
                throw new KeyNotFoundException("Project not found.");
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        // helper functions
        private Project getProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null) 
            {
                throw new KeyNotFoundException("Project not found");
            }
            return project;
        }
    }
}

