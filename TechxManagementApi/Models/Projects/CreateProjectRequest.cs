using System;

namespace TechxManagementApi.Models.Projects
{
	public class CreateProjectRequest
	{
		public int CompanyId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
	}
}

