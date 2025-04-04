using FirstAPI.Models.DTOs;

namespace FirstAPI.Interfaces
{
    public interface ISalaryService
    {
        Task<IEnumerable<SalaryResponse>> GetAllSalaries();
        Task<AssignSalaryResponse> AssignSalaryToEmployee(EmployeeSalaryRequest salaryRequest);

    }
}
