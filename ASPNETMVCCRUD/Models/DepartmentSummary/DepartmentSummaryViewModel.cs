using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNETMVCCRUD.Models.DepartmentSummary
{
	public class DepartmentSummaryViewModel
	{
		public string DepartmentName { get; set; }
		public string DepartmentStatus { get; set; }
		public decimal Salary { get; set; }

		public int EmployeeCount { get; set; }
		public decimal AverageSalary { get; set; }
		public List<string> EmployeeName { get; internal set; }

	}
}
