using System;
namespace TechxManagementApi.Entities
{
	public class Message
	{
		public int Id { get; set; }
		public virtual Account Sender { get; set; }
		public virtual List<Account> Recipients { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}

