using ASPNETMVCCRUD.Data;
using ASPNETMVCCRUD.Models;
using ASPNETMVCCRUD.Models.DepartmentDomain;
using ASPNETMVCCRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Controllers
{
  public class DepartmentController : Controller
  {
    private readonly EmployeeDbContext context;

    public DepartmentController(EmployeeDbContext context)
    {
      this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
      
      return View();
    }

    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var activeDepartments = await context.Departments.ToListAsync();

      return View(activeDepartments);
    }


    [HttpPost]
    public async Task<IActionResult> Add(AddDepartmentViewModel addDepartment)
    {
      if (ModelState.IsValid)
      {
        var existingDepartment = await context.Departments
            .FirstOrDefaultAsync(d => d.Name.ToLower() == addDepartment.Name.ToLower());

        if (existingDepartment != null)
        {
          ModelState.AddModelError("Name", "A department with this name already exists.");
          return View(addDepartment);
        }

        var department = new Department()
        {
          Id = Guid.NewGuid(),
          Name = addDepartment.Name,
          Status = "Active"
        };

        await context.Departments.AddAsync(department);
        await context.SaveChangesAsync();

        return RedirectToAction("Index");
      }

      return View(addDepartment);
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
      var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == id);
      if (department == null)
      {
        return NotFound();
      }

      var updateDepartmentViewModel = new AddDepartmentViewModel
      {
        Id = department.Id,
        Name = department.Name
      };

      return View("Add", updateDepartmentViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AddDepartmentViewModel model)
    {
      if (ModelState.IsValid)
      {
        var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == model.Id);
        if (department == null)
        {
          return NotFound();
        }

        department.Name = model.Name;

        await context.SaveChangesAsync();

        return RedirectToAction("Index");
      }

      return View("Add", model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
      var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == id);

      var employee = await context.Employees
        .Include(x => x.Department)
        .FirstOrDefaultAsync(e => e.Department.Id == id);

      if(employee != null)
      {
        throw new Exception("Department as active employee. Cannot delete at the moment");
      }

      if (department == null)
      {
        return NotFound(); 
      }

      context.Departments.Remove(department);
      await context.SaveChangesAsync();

      return RedirectToAction("Index");
    }



    [HttpPost]
    public async Task<IActionResult> Activate(Guid id)
    {
      var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == id);

      if (department == null)
      {
        return NotFound(); 
      }

      department.Status = "Active";

      await context.SaveChangesAsync();

			return RedirectToAction("Index");
		}

    [HttpPost]
    public async Task<IActionResult> Deactivate(Guid id)
    {
      var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == id);

      if (department == null)
      {
        return NotFound();
      }

      department.Status = "Inactive";

      await context.SaveChangesAsync();

      return RedirectToAction("Index");
    }


  }
}
