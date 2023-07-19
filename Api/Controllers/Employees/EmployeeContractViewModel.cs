namespace Api.Controllers.Employees;

public class EmployeeContractViewModel
{
    public string JobTitle { get; set; }
    public decimal Salary { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}