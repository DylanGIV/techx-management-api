using System;
namespace TechxManagementApi.Entities
{
	public class Company
	{
		public int Id { get; set; }
		public string CompanyName { get; set; }
		public virtual List<Project> Projects { get; set; }
		public virtual ICollection<Account> Employees { get; set; }
		public virtual List<Team> Teams { get; set; }
		public virtual Account Owner { get; set; }
	}
}

