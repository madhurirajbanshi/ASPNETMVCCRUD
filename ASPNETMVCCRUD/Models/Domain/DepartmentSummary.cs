using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models.Domain
{
	public class DepartmentSummary
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Employee names are required.")]
		public List<string> EmployeeName { get; set; }

		[Required(ErrorMessage = "Department name is required.")]
		public string DepartmentName { get; set; }

		public string DepartmentStatus { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
		public decimal Salary { get; set; }

		public int EmployeeCount { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Average Salary must be a positive value.")]
		public decimal AverageSalary { get; set; }
	}
}
