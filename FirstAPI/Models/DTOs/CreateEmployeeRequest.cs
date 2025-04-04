using FirstAPI.Misc;

namespace FirstAPI.Models.DTOs
{
    public class CreateEmployeeRequest
    {
        [NameValidation(ErrorMessage ="Invalid entry for name")]
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
