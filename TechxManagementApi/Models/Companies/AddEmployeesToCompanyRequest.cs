using System;
using TechxManagementApi.Entities;

namespace TechxManagementApi.Models.Companies
{
	public class AddEmployeesToCompanyRequest
	{
		public int CompanyId { get; set; }
		public List<int> AccountIds { get; set; }
	}
}

