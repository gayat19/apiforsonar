namespace FirstAPI.Models.DTOs
{
    public class PhoneUpdate
    {
        public string NewPhoneNumber { get; set; }
    }
    public class AgeUpdate
    {
        public int NewAge { get; set; }
    }
    public class DepartmentUpdate
    {
        public int NewDepartmentId { get; set; }
    }
    public class UpdateEmployeeRequest
    {
        public int EmployeeId { get; set; }
        public PhoneUpdate? PhoneUpdate { get; set; }
        public AgeUpdate? AgeUpdate { get; set; }
        public DepartmentUpdate? DepartmentUpdate { get; set; }
    }
}
