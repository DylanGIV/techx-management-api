using System;
using AutoMapper;
using Microsoft.Extensions.Options;
using TechxManagementApi.Authorization;
using TechxManagementApi.Entities;
using TechxManagementApi.Helpers;
using TechxManagementApi.Models.Teams;

namespace TechxManagementApi.Services
{
    public interface ITeamService
    {
        void CreateTeam(CreateTeamRequest model);
        void UpdateTeamName(UpdateTeamNameRequest model);
        public List<Team> GetTeamsByCompany(long companyId);

    }

	public class TeamService : ITeamService
	{
        private readonly DataContext _context;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        // private readonly IEmailService _emailService;

        public TeamService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings.Value;
           ;
        }

        public void CreateTeam(CreateTeamRequest model)
        {
            var company = _context.Companies.Find(model.CompanyId);
            // validate
            if (_context.Teams.Any(t => t.TeamName == model.TeamName && t.Company == company))
            {
                // if team already exists within company
                throw new AppException("Team with this name already exists in this Company.");
            }

            var team = new Team();

            team.TeamName = model.TeamName;
            team.Company = company;

            // save Team
            _context.Teams.Add(team);
            _context.SaveChanges();
        }
        public void UpdateTeamName(UpdateTeamNameRequest model)
        {
            bool hasChanged = false;
            var team = _context.Teams.Find(model.TeamId);

            if (model.TeamName != null || model.TeamName != "")
            {
                team.TeamName = model.TeamName;
                hasChanged = true;
            }

            if (hasChanged)
            {
                _context.SaveChanges();
            }
        }
        public List<Team> GetTeamsByCompany(long companyId)
        {
            var teams = _context.Teams.Where(t => t.Company.Id == companyId).ToList();
            return teams;
        }
    }
}

