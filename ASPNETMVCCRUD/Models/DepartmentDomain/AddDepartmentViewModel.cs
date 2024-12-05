using System.ComponentModel.DataAnnotations;

namespace ASPNETMVCCRUD.Models.DepartmentDomain
{
  public class AddDepartmentViewModel
  {

    [Key]
    public Guid Id { get; set; }  
    public required string Name { get; set; }
    public string? Status { get; set; }
  }
}
