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
        public void CreateProject(CreateProjectRequest model);
        public void UpdateProjectTeam(UpdateProjectTeamRequest model);
        public void DeleteProject(long id);
        public List<Project> GetAllCompanyProjects(long CompanyId);
    }

	public class ProjectService
	{
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public ProjectService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public void CreateProject(CreateProjectRequest model)
        {
            var project = new Project();
            // find company with matching id.
            project.Company = _context.Companies.Find(model.CompanyId);
            project.ProjectName = model.ProjectName;
            project.ProjectDescription = model.ProjectDescription;

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
            _context.Projects.Add(project);
            _context.SaveChanges();
        }
        public void UpdateProjectTeam(UpdateProjectTeamRequest model)
        {
            //switch toi use find
            var project = _context.Projects.First(p => p.Id == model.ProjectId);
            var team = _context.Teams.First(t => t.Id == model.TeamId);
            project.Team = team;

            _context.SaveChanges();
        }
        public List<Project> GetAllCompanyProjects(long CompanyId)
        {
            var projects = _context.Projects.Where(p => p.Company.Id == CompanyId).ToList();
            return projects;
        }
        public void DeleteProject(long id)
        {
            var project = getProject(id);
            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        // helper functions
        private Project getProject(long id)
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

