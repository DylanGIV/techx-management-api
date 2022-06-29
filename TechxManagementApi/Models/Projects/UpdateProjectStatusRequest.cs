using System;
using TechxManagementApi.Entities;

namespace TechxManagementApi.Models.Projects
{
	public class UpdateProjectStatusRequest
	{
        public int ProjectId { get; set; }
        public bool Status { get; set; }
	}
}

