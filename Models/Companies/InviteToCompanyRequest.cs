using System;
using TechxManagementApi.Entities;

namespace TechxManagementApi.Models.Companies
{
	public class InviteToCompanyRequest
	{
		public string CompanyName { get; set; }
		public List<string> Emails { get; set; }
	}
}

