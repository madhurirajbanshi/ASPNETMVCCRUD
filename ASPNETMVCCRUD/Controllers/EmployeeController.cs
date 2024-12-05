
using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Buffers.Text;

namespace ASPNETMVCCRUD.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly EmployeeDbContext context;

		public EmployeeController(EmployeeDbContext context)
		{
			this.context = context;
		}


		[HttpGet]

		public async Task<IActionResult> Add()
		{
			var departments = await context.Departments
					.Where(d => d.Status == "active") 
					.Select(d => new SelectListItem
					{
						Value = d.Id.ToString(),
						Text = d.Name
					})
					.ToListAsync();

			var viewModel = new AddEmployeeViewModel
			{
				Departments = departments
			};

			return View(viewModel);
		}


		[HttpPost]
		public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
		{
			if (!ModelState.IsValid)
			{
				addEmployeeRequest.Departments = await context.Departments
						.Where(d => d.Status == "active")
						.Select(d => new SelectListItem
						{
							Value = d.Id.ToString(),
							Text = d.Name
						})
						.ToListAsync();
				return View(addEmployeeRequest);
			}


			var department = await context.Departments.FindAsync(addEmployeeRequest.SelectedDepartment);
			if (department == null)
			{
				throw new Exception("Deparment not found");
			}

			byte[]? imageData = null;
			string? imageFileName = null;
			if (addEmployeeRequest.ImageFileName != null)
			{
				using (var memoryStream = new MemoryStream())
				{
					await addEmployeeRequest.ImageFileName.CopyToAsync(memoryStream);
					imageData = memoryStream.ToArray(); 
					imageFileName = addEmployeeRequest.ImageFileName.FileName; 
				}
			}

			var employee = new Employee
			{
				Id = Guid.NewGuid(),
				Name = addEmployeeRequest.Name,
				Email = addEmployeeRequest.Email,
				Salary = addEmployeeRequest.Salary,
				DateofBirth = addEmployeeRequest.DateofBirth,
				Department = department,
				ImageData =imageData,
				ImageFileName = addEmployeeRequest.ImageFileName != null
							? await SaveFileAndReturnPath(addEmployeeRequest.ImageFileName)
							: string.Empty
			};

			await context.Employees.AddAsync(employee);
			await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var employees = await context.Employees
					.Include(e => e.Department)
					.ToListAsync();

      List<EmployeeViewModel> emp = employees.Select(x => new EmployeeViewModel()
			{
				Name = x.Name,
				DateofBirth = x.DateofBirth,
				DepartmentName = x.Department.Name,
				Email = x.Email,
				Id = x.Id,
				ImageFileName = x.ImageFileName,
				Salary = x.Salary,
				Base64Img = String.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(x.ImageData))
			}).ToList();



			return View(emp);
		}


		[HttpGet]
		public async Task<IActionResult> Update(Guid id)
		{
			var employee = await context.Employees
					.Include(e => e.Department) 
					.FirstOrDefaultAsync(e => e.Id == id);

			if (employee == null)
			{
				return NotFound();
			}

			var departments = await context.Departments
					.Where(d => d.Status == "active")
					.Select(d => new SelectListItem
					{
						Value = d.Id.ToString(),
						Text = d.Name
					})
					.ToListAsync();

			var viewModel = new UpdateViewModel
			{
				Name = employee.Name,
				Email = employee.Email,
				Salary = employee.Salary,
				DateofBirth = employee.DateofBirth,
				SelectedDepartment = employee.Department.Id,
				Departments = departments,
				ExistingImagePath = employee.ImageFileName 
			};

			return View(viewModel);
		}


		[HttpPost]
		public async Task<IActionResult> Update(UpdateViewModel editEmployeeRequest)
		{
			if (!ModelState.IsValid)
			{
				editEmployeeRequest.Departments = await context.Departments
						.Where(d => d.Status == "active")
						.Select(d => new SelectListItem
						{
							Value = d.Id.ToString(),
							Text = d.Name
						})
						.ToListAsync();

				return View(editEmployeeRequest);
			}

			var employee = await context.Employees
					.Include(e => e.Department) 
					.FirstOrDefaultAsync(e => e.Id == editEmployeeRequest.Id);

			if (employee == null)
			{
				return NotFound();
			}

			employee.Name = editEmployeeRequest.Name;
			employee.Email = editEmployeeRequest.Email;
			employee.Salary = editEmployeeRequest.Salary;
			employee.DateofBirth = editEmployeeRequest.DateofBirth;

			var department = await context.Departments.FindAsync(editEmployeeRequest.SelectedDepartment);
			if (department != null)
			{
				employee.Department = department;
			}

			if (editEmployeeRequest.ImageFileName != null)

			{
				using (var memoryStream = new MemoryStream())
				{
					await editEmployeeRequest.ImageFileName.CopyToAsync(memoryStream);
					employee.ImageData = memoryStream.ToArray();
					employee.ImageFileName = editEmployeeRequest.ImageFileName.FileName;
				}

			}
			await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}


			[HttpGet]
			public async Task<IActionResult> Delete(Guid id)
			{
				var employee = await context.Employees
						.Include(e => e.Department) 
						.FirstOrDefaultAsync(e => e.Id == id);

				if (employee == null)
				{
					return NotFound(); 
				}

				if (!string.IsNullOrEmpty(employee.ImageFileName))
				{
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", employee.ImageFileName.TrimStart('/'));
					if (System.IO.File.Exists(filePath))
					{
						System.IO.File.Delete(filePath); 
					}
				}

				context.Employees.Remove(employee);
				await context.SaveChangesAsync();

				return RedirectToAction("Index");
			}
			

			private async Task<string> SaveFileAndReturnPath(IFormFile file)
			{
				if (file == null || file.Length == 0)
					return string.Empty;

				var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
				Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

				var fileName = $"{Guid.NewGuid()}_{file.FileName}";
				var filePath = Path.Combine(uploadsFolder, fileName);

				using (var stream = new FileStream(filePath, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}

				return $"/uploads/{fileName}";
			}
			public async Task<IActionResult> ConvertImageBytoToImage(byte[] image)
				{
					return File(image, "image/jpeg");
				}
		}


	}


