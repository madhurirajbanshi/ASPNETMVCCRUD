using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models.Domain
{
	public class Employee
	{
		[Key]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Employee name is required.")]
		[StringLength(100, ErrorMessage = "Employee name can't be longer than 100 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string Email { get; set; }

		[Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
		public decimal Salary { get; set; }

		[DataType(DataType.Date)]
		public DateTime DateofBirth { get; set; }

		public string? ImageFileName { get; set; }

		public virtual Department Department { get; set; }

		public byte[]? ImageData { get; set; }
	}
}
