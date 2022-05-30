using System;

namespace TechxManagementApi.Models.AccountTasks
{
	public class AccountTaskResponse
	{
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
		public int AccountId { get; set; }
		public int ProjectId { get; set; }
	}
}

