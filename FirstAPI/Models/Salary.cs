namespace FirstAPI.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public float Basic { get; set; }
        public float HRA { get; set; }
        public float DA { get; set; }
        public float Allowance { get; set; }
        public float PF { get; set; }
        public string Status { get; set; } = "Active";

        public List<EmployeeSalary>? EmployeeSalaries { get; set; } 
    }
}
