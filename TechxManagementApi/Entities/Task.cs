using System;
namespace TechxManagementApi.Entities
{
	public class Task
	{
		public int Id { get; set; }
		public string TaskTitle { get; set; }
		public string TaskDescription { get; set; }
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
		public virtual Account CreatedBy { get; set; }
	}
}

