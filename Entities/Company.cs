using System;
namespace TechxManagementApi.Entities
{
	public class Company
	{
		public long Id { get; set; }
		public string CompanyName { get; set; }
		public List<Project> Projects { get; set; }
		public List<Account> Employees { get; set; }
		public Account Owner { get; set; }
	}
}

