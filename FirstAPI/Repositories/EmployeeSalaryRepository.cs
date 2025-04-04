using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Repositories
{
    public class EmployeeSalaryRepository : Repository<int, EmployeeSalary>
    {
        public EmployeeSalaryRepository(EmployeeManagementContext context):base(context)
        {
            
        }
        public override async Task<IEnumerable<EmployeeSalary>> GetAll()
        {
            var employeeSalaries = await _context.EmployeeSalaries.ToListAsync();
            if (employeeSalaries == null || !employeeSalaries.Any())
            {
                throw new Exception("No employee salaries found.");
            }
            return employeeSalaries;
        }

        public override async Task<EmployeeSalary> GetById(int id)
        {
            var employeeSalary = await _context.EmployeeSalaries.SingleOrDefaultAsync(es => es.SalaryEmployeeId == id);
            if (employeeSalary == null)
            {
                throw new KeyNotFoundException($"Employee salary with ID {id} not found.");
            }
            return employeeSalary;
        }
    }
}
