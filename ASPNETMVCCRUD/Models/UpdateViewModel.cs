using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNETMVCCRUD.Models
{
	public class UpdateViewModel
	{
		public string Name { get; set; }

		public string Email { get; set; }

		public decimal Salary { get; set; }

		public DateTime DateofBirth { get; set; }

		public IFormFile? ImageFileName { get; set; }

		public List<SelectListItem>? Departments { get; set; }

		public Guid SelectedDepartment { get; set; }


		public string? ExistingImagePath { get; set; }
		public Guid Id { get; set; }
		public string? Base64Img { get; set; }



	}
}
