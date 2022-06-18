using System;
namespace TechxManagementApi.Entities
{
	public class AccountTask
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ProjectId { get; set; }
		public Boolean Completed { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime DueDate { get; set; }
		public virtual Project Project { get; set; }
		public virtual Account CreatedBy { get; set; }
		public virtual Account AssignedTo { get; set; }
	}
}

