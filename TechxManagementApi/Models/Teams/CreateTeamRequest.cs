using System;

namespace TechxManagementApi.Models.Teams
{
	public class CreateTeamRequest
	{
        public string TeamName { get; set; }
        public int CompanyId { get; set; }
	}
}

