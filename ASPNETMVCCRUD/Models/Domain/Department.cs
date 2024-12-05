using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models.Domain
{
	public class Department
	{
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Department name is required.")]
		[StringLength(100, ErrorMessage = "Department name can't be longer than 100 characters.")]
		public string Name { get; set; }

		public string? Status { get; set; }

		public ICollection<Employee> Employees { get; set; }
	}
}
