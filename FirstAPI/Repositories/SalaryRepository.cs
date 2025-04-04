using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FirstAPI.Repositories
{
    public class SalaryRepository : Repository<int, Salary>
    {
        public SalaryRepository(EmployeeManagementContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Salary>> GetAll()
        {
            var salaries = await _context.Salaries.ToListAsync();
            if (salaries == null || !salaries.Any())
            {
                throw new KeyNotFoundException("No salaries found.");
            }
            return salaries;
        }

        public override async Task<Salary> GetById(int id)
        {
            var salary = await _context.Salaries.SingleOrDefaultAsync(s => s.Id == id);
            if (salary == null)
            {
                throw new KeyNotFoundException($"Salary with ID {id} not found.");
            }
            return salary;
        }
    }
}
