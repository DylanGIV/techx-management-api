using System;
using TechxManagementApi.Entities;

namespace TechxManagementApi.Models.Projects
{
	public class UpdateProjectTeamRequest
	{
        public long ProjectId { get; set; }
        public long TeamId { get; set; }
	}
}

