namespace FirstAPI.Models
{
    public class EmployeeSalary
    {
        public int SalaryEmployeeId { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryId { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;

        public Employee? Employee { get; set; }
        public Salary? Salary { get; set; }

    }
}
