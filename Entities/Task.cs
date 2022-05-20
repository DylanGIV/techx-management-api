using System;
namespace TechxManagementApi.Entities
{
	public class Task
	{
		public long Id { get; set; }
		public string TaskTitle { get; set; }
		public string TaskDescription { get; set; }
		public virtual Project Project { get; set; }
		public virtual Account Employee { get; set; }
		public virtual Account CreatedBy { get; set; }
	}
}

