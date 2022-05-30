using System;
namespace TechxManagementApi.Entities
{
	public class TeamTask
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ProjectId { get; set; }
		public virtual Project Project { get; set; }
		public virtual Account CreatedBy { get; set; }
		public virtual Team AssignedTo { get; set; }
	}
}

