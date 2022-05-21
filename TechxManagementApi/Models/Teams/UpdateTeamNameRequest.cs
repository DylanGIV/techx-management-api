using System;

namespace TechxManagementApi.Models.Teams
{
	public class UpdateTeamNameRequest
	{
		public long TeamId { get; set; }
        public string TeamName { get; set; }
	}
}

