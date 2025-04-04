
using FirstAPI.Models.DTOs;

namespace FirstAPI.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeeResponse>> GetEmployeesByDepartment(int departmentId);

        Task<CreateEmployeeResponse> AddEmployee(CreateEmployeeRequest request);

        Task<IEnumerable<GetEmployeeResponse>> GetEmployeesByFilter(EmployeeRequest request);

        Task<GetEmployeeResponse> UpdateEmployeeDetails(UpdateEmployeeRequest updateEmployeeRequest);
    }
}
