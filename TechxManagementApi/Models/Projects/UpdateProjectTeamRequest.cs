using System;
using TechxManagementApi.Entities;

namespace TechxManagementApi.Models.Projects
{
	public class UpdateProjectTeamRequest
	{
        public int ProjectId { get; set; }
        public int TeamId { get; set; }
	}
}

