using FirstAPI.Contexts;
using FirstAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstAPI.Repositories
{
    public class DepartmentRepository : Repository<int, Department>
    {
        public DepartmentRepository(EmployeeManagementContext context) : base(context)
        {
        }
        public async override Task<IEnumerable<Department>> GetAll()
        {
            var departments = _context.Departments.Include(d => d.Employees);//eager loading - Join in SQL terms
            if (departments.Count() == 0)
                throw new Exception("No departments found");
            return departments;
        }

        public async override Task<Department> GetById(int id)
        {
            var department = _context.Departments.Include(d => d.Employees).FirstOrDefault(d => d.Id == id);
            if (department == null)
                throw new Exception("Department not found");
            return department;
        }
    }
}
