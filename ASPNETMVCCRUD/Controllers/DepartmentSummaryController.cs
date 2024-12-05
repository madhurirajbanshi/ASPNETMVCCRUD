using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models.DepartmentSummary;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
	public class DepartmentSummaryController : Controller
	{

		private readonly EmployeeDbContext context;

		public DepartmentSummaryController(EmployeeDbContext context)
		{
			this.context = context;
		}


		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var departmentSummaries = await context.Departments
					.Where(d => d.Status == "active")
					.Include(d => d.Employees)
					.Select(d => new DepartmentSummaryViewModel
					{
						EmployeeName = d.Employees.Select(e => e.Name).ToList(),
						DepartmentName = d.Name,
						DepartmentStatus = d.Status,
						EmployeeCount = d.Employees.Count,
						AverageSalary = d.Employees.Average(e => e.Salary)
					})
					.ToListAsync();
			return View(departmentSummaries);

		}
	}
}

