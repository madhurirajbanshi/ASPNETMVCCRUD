using ASPNETMVCCRUD.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models
{
  public class EmployeeViewModel
  {

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
    public DateTime DateofBirth { get; set; }

    public string? ImageFileName { get; set; }
    public virtual Department Department { get; set; }
    public string DepartmentName { get;  set; }
    public string? Base64Img { get; set; }
  }
}
