using System;

namespace TechxManagementApi.Models.Teams
{
	public class UpdateTeamNameRequest
	{
		public int TeamId { get; set; }
        public string TeamName { get; set; }
	}
}

