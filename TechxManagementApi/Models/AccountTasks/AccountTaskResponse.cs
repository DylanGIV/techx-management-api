using System;
using TechxManagementApi.Models.Accounts;
using TechxManagementApi.Models.Projects;

namespace TechxManagementApi.Models.AccountTasks
{
	public class AccountTaskResponse
	{
		public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
		public Boolean Completed { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime DueDate { get; set; }
		public ProjectResponse Project { get; set; }
		public AccountResponse CreatedBy { get; set; }
		public AccountResponse AssignedTo { get; set; }
	}
}

