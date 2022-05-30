using System;
namespace TechxManagementApi.Entities
{
	public class Team
	{
		public int Id { get; set; }
		public string TeamName { get; set; }
		public virtual List<Project> Projects { get; set; }
		public int CompanyId { get; set; }
		public virtual Company Company { get; set; }
		public virtual ICollection<Account> Employees { get; set; }
		public virtual List<TeamTask> Tasks { get; set; }
	}
}

