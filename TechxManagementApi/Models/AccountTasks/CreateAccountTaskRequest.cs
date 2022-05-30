using System;

namespace TechxManagementApi.Models.AccountTasks
{
	public class CreateAccountTaskRequest
	{
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
		public int AccountId { get; set; }
		public int ProjectId { get; set; }
	}
}

