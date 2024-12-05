
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models
{
	public class AddEmployeeViewModel
	{
		public string Name { get; set; }

		public string Email { get; set; }

		public decimal Salary { get; set; }

		public DateTime DateofBirth { get; set; }

		public IFormFile? ImageFileName { get; set; }

		public List<SelectListItem>? Departments { get; set; }

		public Guid SelectedDepartment { get; set; }


    public Guid Id { get;  set; }
		public string? Base64Img { get; set; }


  }
}

