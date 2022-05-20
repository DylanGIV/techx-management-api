using System;
namespace TechxManagementApi.Entities
{
	public class Message
	{
		public long Id { get; set; }
		public virtual Account Sender { get; set; }
		public List<Account> Recipients { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}

