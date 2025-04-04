using System.ComponentModel.DataAnnotations.Schema;

namespace FirstAPI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int Department_Id { get; set; } 

        public string Phone { get; set; } = string.Empty;
        public int Age { get; set; }

        [ForeignKey("Phone")]
        public User? User { get; set; }

        public Department? Department { get; set; }
        public List<EmployeeSalary>? EmployeeSalaries { get; set; }

        public string Status { get; set; } = "Active";

    }
}
