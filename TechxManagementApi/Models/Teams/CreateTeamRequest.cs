using System;

namespace TechxManagementApi.Models.Teams
{
	public class CreateTeamRequest
	{
        public string TeamName { get; set; }
        public long CompanyId { get; set; }
	}
}

