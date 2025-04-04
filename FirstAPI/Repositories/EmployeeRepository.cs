using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Repositories
{
    public class EmployeeRepository : Repository<int, Employee>
    {
        public EmployeeRepository(EmployeeManagementContext context) : base(context)
        {
        }
        public async override Task<IEnumerable<Employee>> GetAll()
        {
            var employees = _context.Employees.Include(e => e.Department);
            if (employees.Count() == 0)
                throw new Exception("No employees found");
            return employees;
        }

        public async override Task<Employee> GetById(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
                throw new Exception("Employee not found");
            return employee;
        }
    }
}
