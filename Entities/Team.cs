using System;
namespace TechxManagementApi.Entities
{
	public class Team
	{
		public long Id { get; set; }
		public string TeamName { get; set; }
		public List<Project> Projects { get; set; }
		public List<Account> Employees { get; set; }
	}
}

