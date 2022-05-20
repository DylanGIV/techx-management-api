using System;

namespace TechxManagementApi.Entities
{
	public class Project
	{
		public long Id { get; set; }
		public string ProjectName { get; set; }
		public string ProjectDescription { get; set; }
		public virtual List<Task> Tasks { get; set; }
		public virtual Team Team { get; set; }
		public virtual Company Company { get; set; }
	}
}

