using System;

namespace TechxManagementApi.Entities
{
	public class Project
	{
		public int Id { get; set; }
		public string ProjectName { get; set; }
		public string ProjectDescription { get; set; }
		public virtual List<AccountTask> AccountTasks { get; set; }
		public virtual List<TeamTask> TeamTasks { get; set; }
		public int? TeamId { get; set; }
		public virtual Team Team { get; set; }
		public int CompanyId { get; set; }
		public virtual Company Company { get; set; }
		public bool Completed { get; set; }
	}
}

